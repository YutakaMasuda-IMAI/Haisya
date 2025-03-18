using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 売上データリストリポジトリクラス
    /// </summary>
    public class UriageDataListRepository: IUriageDataListRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">ApplicationDbContextのインスタンス</param>
        /// <param name="contextKintai">ApplicationDbContextKintaiのインスタンス</param>
        public UriageDataListRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// 売上データリストを取得するためのストアドプロシージャを実行し、結果を非同期で取得します。
        /// </summary>
        /// <param name="companyId">会社IDを指定します。</param>
        /// <param name="uriageKubun">売上区分を指定します。</param>
        /// <param name="tourokuKubun">登録区分を指定します。</param>
        /// <param name="fromDate">取得するデータの開始日（YYYYMMDD形式）を指定します。</param>
        /// <param name="toDate">取得するデータの終了日（YYYYMMDD形式）を指定します。</param>
        /// <param name="fromTokuisaki">得意先の開始範囲を指定します。</param>
        /// <param name="toTokuisaki">得意先の終了範囲を指定します。</param>
        /// <param name="fromYosya">Yosyaの開始範囲を指定します。</param>
        /// <param name="toYosya">Yosyaの終了範囲を指定します。</param>
        /// <param name="shimeDay">締日を指定します。</param>
        /// <param name="seikyudateTo">請求日（YYYYMMDD形式）を指定します。</param>
        /// <param name="seikyuTantou">請求担当者のIDを指定します。</param>
        /// <param name="haisyaTantou">配車担当者のIDを指定します。</param>
        /// <param name="driverId">ドライバーのIDを指定します。</param>
        /// <returns>売上データリスト（V_UriageDataList）のIEnumerableを表すTask。</returns>
        /// <remarks>
        /// このメソッドは、指定されたパラメータに基づいてストアドプロシージャ `[dbo].[Proc_V_UriageDataList]` を実行し、売上データリストを取得します。
        /// </remarks>
        public async Task<IEnumerable<V_UriageDataList>> GetUriageDataList(int companyId, int uriageKubun, int tourokuKubun, string fromDate, string toDate, string fromTokuisaki, string toTokuisaki, string fromYosya, string toYosya, int shimeDay, string seikyudateTo, int seikyuTantou, int haisyaTantou, int driverId)
        {
            try 
            {
                List<V_UriageDataList> result = await _context.V_UriageDataLists
                    .FromSqlRaw("[dbo].[Proc_V_UriageDataList] @COMPANY_ID={0}, @URIAGE_KUBUN={1}, @TOUROKU_KUBUN={2}, @FROM_DATE={3}, @TO_DATE={4}, @FROM_TOKUISAKI={5}, @TO_TOKUISAKI={6}, @FROM_YOSYA={7}, @TO_YOSYA={8}, @SHIME_DAY={9}, @SEIKYUDATE_TO={10}, @SEIKYU_TANTOU={11}, @HAISYA_TANTOU={12}, @DRIVER_ID={13}",
                        companyId, uriageKubun, tourokuKubun, fromDate, toDate, fromTokuisaki, toTokuisaki, fromYosya, toYosya, shimeDay, seikyudateTo, seikyuTantou, haisyaTantou, driverId)
                    .ToListAsync();
                return result;
            } 
            catch(Exception)
            {
                return new List<V_UriageDataList>(); 
            }
        }

        /// <summary>
        /// 売上リストを取得する
        /// </summary>
        /// <param name="ids">売上のidリスト</param>
        /// <returns>売上リスト（T_Uriage）のIEnumerableを表すTask。</returns>
        public async Task<IEnumerable<T_Uriage>> GetUriageByIds(List<int> ids)
        {
            try 
            {
                List<T_Uriage> result = await _context.T_Uriages.Where(u => ids.Contains(u.Uriage_ID)).ToListAsync();
                return result;
            } 
            catch(Exception)
            {
                return new List<T_Uriage>(); 
            }
        }

    }

    /// <summary>
    /// 売上データリストリポジトリのインターフェース
    /// </summary>
    public interface IUriageDataListRepository
    {
        /// <summary>
        /// 売上データリストを取得するためのストアドプロシージャを実行し、結果を非同期で取得します。
        /// </summary>
        /// <param name="companyId">会社IDを指定します。</param>
        /// <param name="uriageKubun">売上区分を指定します。</param>
        /// <param name="tourokuKubun">登録区分を指定します。</param>
        /// <param name="fromDate">取得するデータの開始日（YYYYMMDD形式）を指定します。</param>
        /// <param name="toDate">取得するデータの終了日（YYYYMMDD形式）を指定します。</param>
        /// <param name="fromTokuisaki">得意先の開始範囲を指定します。</param>
        /// <param name="toTokuisaki">得意先の終了範囲を指定します。</param>
        /// <param name="fromYosya">Yosyaの開始範囲を指定します。</param>
        /// <param name="toYosya">Yosyaの終了範囲を指定します。</param>
        /// <param name="shimeDay">締日を指定します。</param>
        /// <param name="seikyudateTo">請求日（YYYYMMDD形式）を指定します。</param>
        /// <param name="seikyuTantou">請求担当者のIDを指定します。</param>
        /// <param name="haisyaTantou">配車担当者のIDを指定します。</param>
        /// <param name="driverId">ドライバーのIDを指定します。</param>
        /// <returns>売上データリスト（V_UriageDataList）のIEnumerableを表すTask。</returns>
        Task<IEnumerable<V_UriageDataList>> GetUriageDataList(int companyId, int uriageKubun, int tourokuKubun, string fromDate, string toDate, string fromTokuisaki, string toTokuisaki, string fromYosya, string toYosya, int shimeDay, string seikyudateTo, int seikyuTantou, int haisyaTantou, int driverId);
        Task<IEnumerable<T_Uriage>> GetUriageByIds(List<int> ids);
    }
}