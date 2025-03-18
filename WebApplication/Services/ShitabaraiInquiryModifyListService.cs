using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    /// <summary>
    /// 下払い確認データリストサービスクラス
    /// </summary>
    public class ShitabaraiInquiryModifyListService : IShitabaraiInquiryModifyListService
    {
        private readonly IShitabaraiInquiryModifyListRepository _shitabaraiRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="shitabaraiRepository">下払い確認データリストリポジトリ</param>
        public ShitabaraiInquiryModifyListService(IShitabaraiInquiryModifyListRepository shitabaraiRepository)
        {
            _shitabaraiRepository = shitabaraiRepository;
        }

        /// <summary>
        /// 下払い確認データリストを取得します。
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="shiharaiNengetsu">支払い年月</param>
        /// <param name="shimeDay">締め日</param>
        /// <param name="shiharaiTantou">支払い担当者</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="inquiryStatus">問い合わせステータス</param>
        /// <param name="shiharaiChanged">支払い変更</param>
        /// <param name="yosyasakiFrom">寄送先（開始）</param>
        /// <param name="yosyasakiTo">寄送先（終了）</param>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>下払い確認データリストのタスク</returns>
        public async Task<List<V_ShitabaraiCheckDataList>> GetShitabaraiCheckDataList(int CompanyID, DateTime? shiharaiNengetsu, int? shimeDay, string shiharaiTantou, int? zeiKubun, int? inquiryStatus, int? shiharaiChanged, string yosyasakiFrom, string yosyasakiTo, int? checkShitabaraiId)
        {
            List<V_ShitabaraiCheckDataList> result = await _shitabaraiRepository.GetShitabaraiCheckDataList(CompanyID, shiharaiNengetsu, shimeDay, !(shiharaiTantou.Equals("ALL")) ? shiharaiTantou : "", zeiKubun, inquiryStatus, shiharaiChanged, yosyasakiFrom, yosyasakiTo, checkShitabaraiId);
            return result;
        }
    }

    /// <summary>
    /// 下払い確認データリストサービスインターフェース
    /// </summary>
    public interface IShitabaraiInquiryModifyListService
    {
        /// <summary>
        /// 下払い確認データリストを取得します。
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="shiharaiNengetsu">支払い年月</param>
        /// <param name="shimeDay">締め日</param>
        /// <param name="shiharaiTantou">支払い担当者</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="inquiryStatus">問い合わせステータス</param>
        /// <param name="shiharaiChanged">支払い変更</param>
        /// <param name="yosyasakiFrom">寄送先（開始）</param>
        /// <param name="yosyasakiTo">寄送先（終了）</param>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>下払い確認データリストのタスク</returns>
        Task<List<V_ShitabaraiCheckDataList>> GetShitabaraiCheckDataList(int CompanyID, DateTime? shiharaiNengetsu, int? shimeDay, string shiharaiTantou, int? zeiKubun, int? inquiryStatus, int? shiharaiChanged, string yosyasakiFrom, string yosyasakiTo, int? checkShitabaraiId);
    }
}