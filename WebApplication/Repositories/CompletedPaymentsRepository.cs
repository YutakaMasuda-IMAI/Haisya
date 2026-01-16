using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Model;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 完了した支払いデータを管理するリポジトリクラスです。
    /// </summary>
    public class CompletedPaymentsRepository : ICompletedPaymentsRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        public CompletedPaymentsRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// T_Print_Seikyuを取得する
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <returns>PrintSeikyu</returns>
        public async Task<T_Print_Seikyu> GetPrintSeikyu(int Seikyu_ID)
        {

            T_Print_Seikyu PrintSeikyu = await _context.T_Print_Seikyus
                .Where(e => e.Seikyu_ID == Seikyu_ID)
                .FirstOrDefaultAsync();

            return PrintSeikyu;
        }

        /// <summary>
        /// T_Nyukin一覧を取得する
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <param name="Process_Kubun">処理区分</param>
        /// <returns>NyukinList</returns>
        public async Task<List<T_Nyukin>> GetNyukinList(int Seikyu_ID, int Process_Kubun)
        {

            List<T_Nyukin> NyukinList = await _context.T_Nyukins
                .Where(e => e.Seikyu_ID == Seikyu_ID && e.Process_Kubun == Process_Kubun && e.Del_Flg == false)
                .OrderBy(e => e.Process_Date)
                .ToListAsync();

            return NyukinList;
        }

        /// <summary>
        /// T_Print_Seikyuのリストを取得する
        /// </summary>
        /// <param name="PrintSeikyu">PrintSeikyuオブジェクト</param>
        /// <returns>PrintSeikyuList</returns>
        public async Task<List<BillingPaymentHistory>> GetPrintSeikyuList(T_Print_Seikyu PrintSeikyu)
        {
            // 1年前の日付を計算
            DateOnly oneYearAgo = PrintSeikyu.Seikyu_Month.AddYears(-1);

            // PrintSeikyuのCustomer_Branch_IDの一致かつ過去1年分のレコードを取得
            List<T_Print_Seikyu> printSeikyuList = await _context.T_Print_Seikyus
                .Where(ps => ps.Customer_Branch_ID == PrintSeikyu.Customer_Branch_ID // 顧客支店 ID でフィルタリング
                        && ps.Seikyu_Month >= oneYearAgo // 1 年前以降の請求月
                        && ps.Seikyu_Month <= PrintSeikyu.Seikyu_Month  // 指定された請求月以前
                        && ps.Seikyu_ID > 0
                        && ps.Del_Datetime == null)
                .ToListAsync();

            List<BillingPaymentHistory> result = new List<BillingPaymentHistory>();
            // 各 PrintSeikyu レコードに対して処理を実行
            foreach (var printSeikyu in printSeikyuList)
            {
                // 関連するT_Nyukinレコードを取得
                List<T_Nyukin> nyukinList = await _context.T_Nyukins
                    .Where(n => n.Seikyu_ID == printSeikyu.Seikyu_ID) // 該当する請求 ID でフィルタリング
                    .ToListAsync();

                // 入金額計を計算
                decimal depositTotal = nyukinList
                    .Where(n => n.Process_Kubun == 0) // 入金（Process_Kubun が 0）
                    .Sum(n => n.Total_Amount) // 入金額の合計
                    - nyukinList
                    .Where(n => n.Process_Kubun == 1) // 支払い（Process_Kubun が 1）
                    .Sum(n => n.Total_Amount); // 支払い額の合計

                // 最終入金日を取得
                DateTime lastProcessDate = nyukinList
                    .OrderByDescending(n => n.Process_Date) // 入金日で降順に並べ替え
                    .FirstOrDefault()?.Process_Date // 最初のレコードの入金日を取得。存在しない場合は null
                    ?? DateTime.MinValue; // null の場合は最小の DateTime 値を設定
                // 結果リストに BillingPaymentHistory オブジェクトを追加
                result.Add(new BillingPaymentHistory
                {
                    PrintSeikyu = printSeikyu,
                    Deposit_Total = depositTotal,
                    Process_Date = lastProcessDate
                });
            }

            return result;
        }

        /// <summary>
        /// T_Nyukin_Localリストのデータの追加
        /// </summary>
        /// <param name="list">T_Nyukinのリスト</param>
        /// <returns>非同期操作を表すTask</returns>
        public async Task InsertTNyukin(List<T_Nyukin> list)
        {
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (T_Nyukin item in list)
                {
                    T_Nyukin existItem = _context.T_Nyukins.FirstOrDefault(e => e.Nyukin_ID == item.Nyukin_ID && e.Seikyu_ID == item.Seikyu_ID);
                    if (existItem != null)
                    {
                        // 既存のレコードを更新
                        existItem.Process_Kubun = item.Process_Kubun;
                        existItem.Process_Date = item.Process_Date;
                        existItem.Cash_Amount = item.Cash_Amount;
                        existItem.Transfer_Amount = item.Transfer_Amount;
                        existItem.Draft_Amount = item.Draft_Amount;
                        existItem.Service_Charge_Amount = item.Service_Charge_Amount;
                        existItem.Unchin_Offset = item.Unchin_Offset;
                        existItem.General_Offset = item.General_Offset;
                        existItem.Adjustment_Amount = item.Adjustment_Amount;
                        existItem.Total_Amount = item.Total_Amount;
                        existItem.Remarks = item.Remarks;
                        existItem.Del_Flg = item.Del_Flg;
                        existItem.Update_User = item.Update_User;
                        existItem.Update_Datetime = DateTime.Now;
                        // 変更されたエンティティの状態を Modified に設定
                        _context.Entry(existItem).State = EntityState.Modified;
                        continue;
                    }
                    else
                    {
                        item.Nyukin_ID = 0;
                        item.Insert_Datetime = DateTime.Now;
                        item.Update_Datetime = DateTime.Now;
                        _context.T_Nyukins.Add(item);
                        continue;
                    }
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("InsertTNyukin:" + ex.Message);
                await transaction.RollbackAsync();
            }

        }

        /// <summary>
        /// T_Nyukinリストのデータの追加・更新
        /// </summary>
        /// <param name="list">T_Nyukinのリスト</param>
        /// <returns>非同期操作を表すTask</returns>
        public async Task InsertOrUpdateTNyukin(List<T_Nyukin> list)
        {
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var item in list)
                {
                    if (item.Nyukin_ID == 0)
                    {
                        item.Insert_Datetime = DateTime.Now;
                        item.Update_Datetime = DateTime.Now;
                        item.Del_Flg = false;
                        _context.T_Nyukins.Add(item);
                        continue;
                    }
                    T_Nyukin existingItem = _context.T_Nyukins.FirstOrDefault(e => e.Nyukin_ID == item.Nyukin_ID);
                    if(existingItem != null)
                    {
                        // 既存のレコードを更新
                        existingItem.Process_Date = item.Process_Date != DateTime.MinValue ? item.Process_Date : existingItem.Process_Date;
                        existingItem.Cash_Amount = item.Cash_Amount;
                        existingItem.Transfer_Amount = item.Transfer_Amount;
                        existingItem.Draft_Amount = item.Draft_Amount;
                        existingItem.Service_Charge_Amount = item.Service_Charge_Amount;
                        existingItem.Unchin_Offset = item.Unchin_Offset;
                        existingItem.General_Offset = item.General_Offset;
                        existingItem.Adjustment_Amount = item.Adjustment_Amount;
                        existingItem.Total_Amount = item.Total_Amount;
                        existingItem.Remarks = item.Remarks;
                        existingItem.Update_User = item.Update_User;
                        existingItem.Update_Datetime = DateTime.Now;
                        // 変更されたエンティティの状態を Modified に設定
                        _context.Entry(existingItem).State = EntityState.Modified;
                        continue;
                    }
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("InsertOrUpdateTNyukin:" + ex.Message);
                await transaction.RollbackAsync();
            }
        }

        /// <summary>
        /// 入金情報と返金情報の登録・更新・削除
        /// </summary>
        /// <param name="data">T_Nyukinのリスト</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <param name="Process_Kubun">処理区分</param>
        /// <returns>非同期操作を表すTask</returns>
        public async Task PaymentInputData(List<T_Nyukin> data, int User_ID, int Seikyu_ID, int Process_Kubun)
        {
            try
            {
                // 既存のレコードを取得
                List<T_Nyukin> existingRecords = await _context.T_Nyukins
                    .Where(n => n.Seikyu_ID == Seikyu_ID && n.Process_Kubun == Process_Kubun)
                    .ToListAsync();

                // 新しいデータアイテムを処理
                foreach (var item in data)
                {
                    // Nyukin_ID が 0 の場合、新規レコードとして処理
                    if (item.Nyukin_ID == 0)
                    {
                        // 新規登録
                        item.Insert_User = User_ID;
                        item.Insert_Datetime = DateTime.Now;
                        item.Update_User = User_ID;
                        item.Update_Datetime = DateTime.Now;
                        item.Del_Flg = false;
                        _context.T_Nyukins.Add(item);
                    }
                    else
                    {
                        // 更新
                        T_Nyukin existingItem = existingRecords.FirstOrDefault(e => e.Nyukin_ID == item.Nyukin_ID);
                        // 既存のレコードが見つかった場合
                        if (existingItem != null)
                        {
                            // 既存のレコードを更新
                            existingItem.Process_Date = item.Process_Date != DateTime.MinValue ? item.Process_Date : existingItem.Process_Date;
                            existingItem.Cash_Amount = item.Cash_Amount;
                            existingItem.Transfer_Amount = item.Transfer_Amount;
                            existingItem.Draft_Amount = item.Draft_Amount;
                            existingItem.Service_Charge_Amount = item.Service_Charge_Amount;
                            existingItem.Unchin_Offset = item.Unchin_Offset;
                            existingItem.General_Offset = item.General_Offset;
                            existingItem.Adjustment_Amount = item.Adjustment_Amount;
                            existingItem.Total_Amount = item.Total_Amount;
                            existingItem.Remarks = item.Remarks;
                            existingItem.Update_User = User_ID;
                            existingItem.Update_Datetime = DateTime.Now;
                            // 変更されたエンティティの状態を Modified に設定
                            _context.Entry(existingItem).State = EntityState.Modified;

                        }
                    }
                }
                // 既存のレコードで、新しいデータに存在しないレコードを削除対象としてマーク
                foreach (var existingItem in existingRecords)
                {
                    // 新しいデータに存在しないレコードを削除フラグを設定
                    if (!data.Any(d => d.Nyukin_ID == existingItem.Nyukin_ID))
                    {
                        existingItem.Del_Flg = true; // 削除フラグを true に設定
                        existingItem.Update_User = User_ID;
                        existingItem.Update_Datetime = DateTime.Now;
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PaymentInputData:" + ex.Message);
            }

        }
        /// <summary>
        /// 入金情報と返金情報の登録・更新・削除
        /// </summary>
        /// <param name="data">PostPaymentInputDataModelオブジェクト</param>
        /// <returns>非同期操作を表すTask</returns>
        public async Task<bool> PostPaymentInputData(PostPaymentInputDataModel data)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await PaymentInputData(data.PaymentNyukinList, data.User_ID, data.Seikyu_ID, 0);
                await PaymentInputData(data.RepaymentNyukinList, data.User_ID, data.Seikyu_ID, 1);
                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PaymentInputData:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

    }

    /// <summary>
    /// 完了した支払いデータを管理するリポジトリインターフェースです。
    /// </summary>
    public interface ICompletedPaymentsRepository
    {
        /// <summary>
        /// T_Nyukin_Localリストのデータの追加
        /// </summary>
        /// <param name="list">T_Nyukinのリスト</param>
        /// <returns>非同期操作を表すTask</returns>
        Task InsertTNyukin(List<T_Nyukin> list);

        /// <summary>
        /// T_Nyukinリストのデータの追加・更新
        /// </summary>
        /// <param name="list">T_Nyukinのリスト</param>
        /// <returns>非同期操作を表すTask</returns>
        Task InsertOrUpdateTNyukin(List<T_Nyukin> list);

        /// <summary>
        /// T_Print_Seikyuを取得する
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <returns>PrintSeikyu</returns>
        Task<T_Print_Seikyu> GetPrintSeikyu(int Seikyu_ID);

        /// <summary>
        /// T_Nyukin一覧を取得する
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <param name="Process_Kubun">処理区分</param>
        /// <returns>NyukinList</returns>
        Task<List<T_Nyukin>> GetNyukinList(int Seikyu_ID, int Process_Kubun);

        /// <summary>
        /// T_Print_Seikyuのリストを取得する
        /// </summary>
        /// <param name="PrintSeikyu">PrintSeikyuオブジェクト</param>
        /// <returns>PrintSeikyuList</returns>
        Task<List<BillingPaymentHistory>> GetPrintSeikyuList(T_Print_Seikyu PrintSeikyu);

        /// <summary>
        /// 入金情報と返金情報の登録・更新・削除
        /// </summary>
        /// <param name="data">T_Nyukinのリスト</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <param name="Process_Kubun">処理区分</param>
        /// <returns>非同期操作を表すTask</returns>
        Task PaymentInputData(List<T_Nyukin> data, int User_ID, int Seikyu_ID, int Process_Kubun);

        /// <summary>
        /// 入金情報と返金情報の登録・更新・削除
        /// </summary>
        /// <param name="data">PostPaymentInputDataModelオブジェクト</param>
        /// <returns>非同期操作を表すTask</returns>
        Task<bool> PostPaymentInputData(PostPaymentInputDataModel data);
    }
}