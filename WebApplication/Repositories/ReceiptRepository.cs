using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebApplication.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 受領リポジトリクラス
    /// </summary>
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">アプリケーションデータベースコンテキスト</param>
        /// <param name="contextKintai">勤怠データベースコンテキスト</param>
        public ReceiptRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// 指定されたAnken_IDに基づいてT_Anken_Displayを非同期に取得します。
        /// </summary>
        /// <param name="Anken_ID">Anken_IDの値</param>
        /// <returns>T_Anken_Displayのインスタンス</returns>
        private async Task<T_Anken_Display> GetAnkenDisplay(int Anken_ID)
        {
            T_Anken_Display ankenDisplay = await _context.T_Anken_Displays
                .Where(anken => anken.Anken_ID == Anken_ID)
                .FirstOrDefaultAsync();

            return ankenDisplay;
        }

        /// <summary>
        /// コメントを登録する
        /// </summary>
        /// <param name="t_Nippou">日報情報</param>
        /// <returns>登録結果</returns>
        public async Task<bool> InsertCommentAsync(T_Nippou t_Nippou)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                T_Anken_Display ankendisplay = await GetAnkenDisplay(t_Nippou.Anken_ID);
                if (ankendisplay != null)
                {
                    t_Nippou.AnkenDisplay_ID = ankendisplay.AnkenDisplay_ID;
                }
                // 指定された `Anken_ID` と `AnkenDisplay_ID` に基づいて、既存のレコードを取得します。
                T_Nippou existingRecord = await _context.T_Nippous.FirstOrDefaultAsync(
                 r => r.Anken_ID == t_Nippou.Anken_ID && r.AnkenDisplay_ID == t_Nippou.AnkenDisplay_ID
                );
                if (existingRecord != null)
                {
                    // 既存のレコードが見つかった場合は、コメントとユーザー情報を更新します。
                    existingRecord.Commnet = t_Nippou.Commnet;
                    existingRecord.Update_User = t_Nippou.Insert_User;
                    existingRecord.Update_Datetime = DateTime.UtcNow;
                }
                else
                {
                    // 既存のレコードが見つからない場合は、新しいレコードを追加します。
                    t_Nippou.Insert_Datetime = DateTime.UtcNow;
                    _context.T_Nippous.Add(t_Nippou);
                }

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("InsertCommentAsync:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;

        }

        /// <summary>
        /// 受領を登録する
        /// </summary>
        /// <param name="t_Nippou">日報情報</param>
        /// <returns>登録結果</returns>
        public async Task<bool> InsertReceiptAsync(T_Nippou t_Nippou)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                T_Anken_Display ankendisplay = await GetAnkenDisplay(t_Nippou.Anken_ID);
                if (ankendisplay != null)
                {
                    t_Nippou.AnkenDisplay_ID = ankendisplay.AnkenDisplay_ID;
                }
                // 指定された `Anken_ID` と `AnkenDisplay_ID` に基づいて、既存のレコードを取得します。
                T_Nippou existingRecord = await _context.T_Nippous.FirstOrDefaultAsync(
                 r => r.Anken_ID == t_Nippou.Anken_ID && r.AnkenDisplay_ID == t_Nippou.AnkenDisplay_ID
                );
                if (existingRecord != null)
                {
                    // 既存のレコードが見つかった場合は、レシートとユーザー情報を更新します。
                    existingRecord.Receipt = t_Nippou.Receipt;
                    existingRecord.Receipt_Date = t_Nippou.Receipt_Date;
                    existingRecord.Update_User = t_Nippou.Insert_User;
                    existingRecord.Update_Datetime = DateTime.UtcNow;
                }
                else
                {
                    // 既存のレコードが見つからない場合は、新しいレコードを追加します。
                    t_Nippou.Receipt = t_Nippou.Receipt;

                    t_Nippou.Insert_Datetime = DateTime.UtcNow;
                    await _context.T_Nippous.AddAsync(t_Nippou);
                }

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("InsertReceiptAsync:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 指定された案件IDに紐づく日報情報を取得する
        /// </summary>
        /// <param name="t_Nippous">日報情報のリスト</param>
        /// <returns>日報情報のリスト</returns>
        public async Task<List<T_Nippou>> GetTNippouAsync(List<T_Nippou> t_Nippous)
        {
            foreach(var t_Nippou in t_Nippous)
            {
                T_Anken_Display ankendisplay = await GetAnkenDisplay(t_Nippou.Anken_ID);
                int ankendisplayid = 0;
                if(ankendisplay != null)
                {
                    ankendisplayid = ankendisplay.AnkenDisplay_ID;
                }
                t_Nippou.AnkenDisplay_ID = ankendisplayid;
            }
            List<T_Nippou> existingRecords = await _context.T_Nippous.ToListAsync();

            List<int> ankenIds = t_Nippous.Select(n => n.Anken_ID).ToList();

            List<int> ankenDisplayIds = t_Nippous.Select(n => n.AnkenDisplay_ID).ToList();

            List<T_Nippou> matchingRecords = existingRecords.Where(n => ankenIds.Contains(n.Anken_ID)).ToList();

            matchingRecords = matchingRecords.Where(n => ankenDisplayIds.Contains(n.AnkenDisplay_ID)).ToList();

            return matchingRecords;

        }

        /// <summary>
        /// 指定されたAnken_IDのリストに基づいてT_Anken_Displayのリストを非同期に取得します。
        /// </summary>
        /// <param name="Anken_ID">Anken_IDの値のリスト</param>
        /// <returns>T_Anken_Displayのインスタンスのリスト</returns>
        public async Task<List<T_Anken_Display>> GetAnkenDisplayList(List<int> Anken_ID)
        {
            List<T_Anken_Display> ankenDisplay = await _context.T_Anken_Displays
                .Where(anken => Anken_ID.Contains(anken.Anken_ID))
                .ToListAsync();

            return ankenDisplay;
        }
    }

    /// <summary>
    /// 受領リポジトリインターフェース
    /// </summary>
    public interface IReceiptRepository
    {
        /// <summary>
        /// コメントを登録する
        /// </summary>
        /// <param name="t_Nippou">日報情報</param>
        /// <returns>登録結果</returns>
        public Task<bool> InsertCommentAsync(T_Nippou t_Nippou);

        /// <summary>
        /// 受領を登録する
        /// </summary>
        /// <param name="t_Nippou">日報情報</param>
        /// <returns>登録結果</returns>
        public Task<bool> InsertReceiptAsync(T_Nippou t_Nippou);

        /// <summary>
        /// 指定されたAnken_IDのリストに基づいてT_Anken_Displayのリストを非同期に取得します。
        /// </summary>
        /// <param name="Anken_ID">Anken_IDの値のリスト</param>
        /// <returns>T_Anken_Displayのインスタンスのリスト</returns>
        public Task<List<T_Anken_Display>> GetAnkenDisplayList(List<int> Anken_ID);

        /// <summary>
        /// 指定された案件IDに紐づく日報情報を取得する
        /// </summary>
        /// <param name="t_Nippous">日報情報のリスト</param>
        /// <returns>日報情報のリスト</returns>
        public Task<List<T_Nippou>> GetTNippouAsync(List<T_Nippou> t_Nippous);
    }
}