using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 支払照会修正リストリポジトリ
    /// </summary>
    public class ShitabaraiInquiryModifyListRepository : IShitabaraiInquiryModifyListRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">アプリケーションDBコンテキスト</param>
        /// <param name="contextKintai">勤怠DBコンテキスト</param>
        public ShitabaraiInquiryModifyListRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// データリストをDBから取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="shiharaiNengetsu">支払年月</param>
        /// <param name="shimeDay">締日</param>
        /// <param name="shiharaiTantou">支払担当</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="inquiryStatus">照会ステータス</param>
        /// <param name="shiharaiChanged">支払変更</param>
        /// <param name="yosyasakiFrom">予算先開始</param>
        /// <param name="yosyasakiTo">予算先終了</param>
        /// <param name="checkShitabaraiId">支払チェックID</param>
        /// <returns>V_ShitabaraiCheckDataListのリスト</returns>
        public async Task<List<V_ShitabaraiCheckDataList>> GetShitabaraiCheckDataList(int CompanyID, DateTime? shiharaiNengetsu, int? shimeDay, string shiharaiTantou, int? zeiKubun, int? inquiryStatus, int? shiharaiChanged, string yosyasakiFrom, string yosyasakiTo, int? checkShitabaraiId)
        {
            // 条件に一致するデータを取得
            string sql = string.Format("EXECUTE [dbo].[Proc_V_ShitabaraiCheckDataList] ");
            // sql += string.Format("@COMPANY_ID = {0}", CompanyID);
            if (shiharaiNengetsu != null) { sql += string.Format("@SHIHARAI_NENGETSU = '{0}'", ((DateTime)shiharaiNengetsu).ToString("yyyy/MM/dd")); }
            if (shimeDay != null) { sql += string.Format(", @SHIME_DAY = {0}", shimeDay); }
            if (shiharaiTantou != "") { sql += string.Format(", @SHIHARAI_TANTOU = '{0}'", shiharaiTantou); }
            if (zeiKubun != null) { sql += string.Format(", @ZEI_KUBUN = {0}", zeiKubun); }
            if (inquiryStatus != null) { sql += string.Format(", @INQUIRY_STATUS = {0}", inquiryStatus); }
            if (shiharaiChanged != null) { sql += string.Format(", @SHIHARAI_CHANGED = {0}", shiharaiChanged); }
            if (yosyasakiFrom != "") { sql += string.Format(", @FROM_YOSYASAKI = '{0}'", yosyasakiFrom); }
            if (yosyasakiTo != "") { sql += string.Format(", @TO_YOSYASAKI = '{0}'", yosyasakiTo); }
            if (checkShitabaraiId != null) { sql += string.Format(", @CHECK_SHITABARAI_ID = '{0}'", checkShitabaraiId); }
            sql += string.Format(", @PRINT_DATE = ''");

            return await _context.V_ShitabaraiCheckDataLists.FromSqlRaw(sql).AsNoTracking().ToListAsync();
        }
    }

    /// <summary>
    /// 支払照会修正リストリポジトリインターフェース
    /// </summary>
    public interface IShitabaraiInquiryModifyListRepository
    {
        /// <summary>
        /// データリストをDBから取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="shiharaiNengetsu">支払年月</param>
        /// <param name="shimeDay">締日</param>
        /// <param name="shiharaiTantou">支払担当</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="inquiryStatus">照会ステータス</param>
        /// <param name="shiharaiChanged">支払変更</param>
        /// <param name="yosyasakiFrom">予算先開始</param>
        /// <param name="yosyasakiTo">予算先終了</param>
        /// <param name="checkShitabaraiId">支払チェックID</param>
        /// <returns>V_ShitabaraiCheckDataListのリスト</returns>
        Task<List<V_ShitabaraiCheckDataList>> GetShitabaraiCheckDataList(int CompanyID, DateTime? shiharaiNengetsu, int? shimeDay, string shiharaiTantou, int? zeiKubun, int? inquiryStatus, int? shiharaiChanged, string yosyasakiFrom, string yosyasakiTo, int? checkShitabaraiId);
    }
}