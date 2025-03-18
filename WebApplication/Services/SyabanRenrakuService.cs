using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Model;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    /// <summary>
    /// 車番連絡サービス
    /// </summary>
    public class SyabanRenrakuService : ISyabanRenrakuService
    {
        private readonly ISyabanRenrakuRepository _syabanRenrakuRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="syabanRenrakuRepository"></param>
        public SyabanRenrakuService(ISyabanRenrakuRepository syabanRenrakuRepository)
            => _syabanRenrakuRepository = syabanRenrakuRepository;

        /// <summary>
        /// 車番連絡データを取得
        /// </summary>
        /// <param name="companyID">会社ID。</param>
        /// <param name="selectTantou">選択担当者。</param>
        /// <param name="selectedDate">選択開始日。</param>
        /// <param name="selectedEndDate">選択終了日。</param>
        /// <param name="filter">フィルター。</param>
        /// <returns>車番連絡データのリスト。</returns>
        public Task<List<SyabanRenrakuModel>> GetSyabanRenrakuAsync(int companyID, int selectTantou, DateTime selectedDate, DateTime selectedEndDate, int filter)
        {
            Task<List<SyabanRenrakuModel>> syabanRenraku = _syabanRenrakuRepository.GetSyabanRenrakuAsync(companyID, selectTantou, selectedDate, selectedEndDate, filter);
            return syabanRenraku;
        }

        /// <summary>
        /// 車番連絡データの備考を取得
        /// </summary>
        /// <param name="AnkenDisplay_ID">案件表示ID。</param>
        /// <returns>備考。</returns>
        public async Task<string> GetSyabanRenrakuRemarks(int AnkenDisplay_ID)
            => await _syabanRenrakuRepository.GetSyabanRenrakuRemarks(AnkenDisplay_ID);

        /// <summary>
        /// 車番連絡データを登録
        /// </summary>
        /// <param name="syabanRenrakuPostModel">車番連絡ポストモデル。</param>
        /// <returns>登録結果。</returns>
        public async Task<bool> PostSyabanRenrakuAsync(List<SyabanRenrakuPostModel> syabanRenrakuPostModel)
        {
            // syabanRenrakuPostModelリスト内の各項目について処理を行う
            bool result = await _syabanRenrakuRepository.PostSyabanRenrakuAsync(syabanRenrakuPostModel);
            return result;
        }

    }

    /// <summary>
    /// 車番連絡サービスのインターフェース
    /// </summary>
    public interface ISyabanRenrakuService
    {
        /// <summary>
        /// 車番連絡データを取得
        /// </summary>
        /// <param name="companyID">会社ID。</param>
        /// <param name="selectTantou">選択担当者。</param>
        /// <param name="selectedDate">選択開始日。</param>
        /// <param name="selectedEndDate">選択終了日。</param>
        /// <param name="filter">フィルター。</param>
        /// <returns>車番連絡データのリスト。</returns>
        public Task<List<SyabanRenrakuModel>> GetSyabanRenrakuAsync(int companyID, int selectTantou, DateTime selectedDate, DateTime selectedEndDate, int filter);

        /// <summary>
        /// 車番連絡データの備考を取得
        /// </summary>
        /// <param name="AnkenDisplay_ID">案件表示ID。</param>
        /// <returns>備考。</returns>
        public Task<string> GetSyabanRenrakuRemarks(int AnkenDisplay_ID);

        /// <summary>
        /// 車番連絡データを登録
        /// </summary>
        /// <param name="syabanRenrakuPostModel">車番連絡ポストモデル。</param>
        /// <returns>登録結果。</returns>
        public Task<bool> PostSyabanRenrakuAsync(List<SyabanRenrakuPostModel> syabanRenrakuPostModel);
    }
}