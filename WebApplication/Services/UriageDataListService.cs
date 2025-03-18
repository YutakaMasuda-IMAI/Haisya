using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    /// <summary>
    /// 売上データリストサービス
    /// </summary>
    public class UriageDataListService: IUriageDataListService
    {
        private readonly IUriageDataListRepository _uriageDataListRepository;

        public UriageDataListService(IUriageDataListRepository uriageDataListRepository)
        {
            _uriageDataListRepository = uriageDataListRepository;
        }

        /// <summary>
        /// 売上データリストを取得します。
        /// </summary>
        /// <param name="companyId">会社ID。</param>
        /// <param name="uriageKubun">売上区分。</param>
        /// <param name="tourokuKubun">登録区分。</param>
        /// <param name="fromDate">開始日。</param>
        /// <param name="toDate">終了日。</param>
        /// <param name="fromTokuisaki">開始得意先コード。</param>
        /// <param name="toTokuisaki">終了得意先コード。</param>
        /// <param name="fromYosya">開始yosyaコード。</param>
        /// <param name="toYosya">終了yosyaコード。</param>
        /// <param name="shimeDay">締め日。</param>
        /// <param name="seikyudateTo">請求日。</param>
        /// <param name="seikyuTantou">請求担当者ID。</param>
        /// <param name="haisyaTantou">配車担当者ID。</param>
        /// <param name="driverId">ドライバーID。</param>
        /// <returns>売上データリスト (IEnumerable&lt;V_UriageDataList&gt;) を表すTask。</returns>
        /// <remarks>
        /// このメソッドは、指定された条件に基づいて売上データリストを取得します。
        /// `fromYosya` や `toYosya` が空でない場合、それぞれの値が使用されます。
        /// `_uriageDataListRepository.GetUriageDataList` メソッドを呼び出し、
        /// 売上データを取得して返します。
        /// </remarks>
        public async Task<IEnumerable<V_UriageDataList>> GetUriageDataList(int companyId, int uriageKubun, int tourokuKubun, string fromDate, string toDate, string fromTokuisaki, string toTokuisaki, string fromYosya, string toYosya, int shimeDay, string seikyudateTo, int seikyuTantou, int haisyaTantou, int driverId)
        {
            string fromYosya2 = "";
            if(!string.IsNullOrEmpty(fromYosya))
            {
                fromYosya2 = fromYosya;
            }
            string toYosya2 = "";
            if(!string.IsNullOrEmpty(toYosya))
            {
                toYosya2 = toYosya;
            }
            IEnumerable<V_UriageDataList> uriageDataList = await _uriageDataListRepository.GetUriageDataList(companyId, uriageKubun, tourokuKubun, fromDate, toDate, fromTokuisaki, toTokuisaki, fromYosya2, toYosya2, shimeDay, seikyudateTo, seikyuTantou, haisyaTantou, driverId);
            return uriageDataList;
        }
       
        /// <summary>
        /// 売上リストを取得する
        /// </summary>
        /// <param name="ids">売上のidリスト</param>
        /// <returns>売上リスト（T_Uriage）のIEnumerableを表すTask。</returns>
        public async Task<IEnumerable<T_Uriage>> GetUriageByIds(List<int> ids)
        {
            IEnumerable<T_Uriage> uriageDataList = await _uriageDataListRepository.GetUriageByIds(ids);
            return uriageDataList;
        }
    }

    /// <summary>
    /// 売上データリストサービスのインターフェース
    /// </summary>
    public interface IUriageDataListService
    {
        /// <summary>
        /// 売上データリストを取得します。
        /// </summary>
        /// <param name="companyId">会社ID。</param>
        /// <param name="uriageKubun">売上区分。</param>
        /// <param name="tourokuKubun">登録区分。</param>
        /// <param name="fromDate">開始日。</param>
        /// <param name="toDate">終了日。</param>
        /// <param name="fromTokuisaki">開始得意先コード。</param>
        /// <param name="toTokuisaki">終了得意先コード。</param>
        /// <param name="fromYosya">開始yosyaコード。</param>
        /// <param name="toYosya">終了yosyaコード。</param>
        /// <param name="shimeDay">締め日。</param>
        /// <param name="seikyudateTo">請求日。</param>
        /// <param name="seikyuTantou">請求担当者ID。</param>
        /// <param name="haisyaTantou">配車担当者ID。</param>
        /// <param name="driverId">ドライバーID。</param>
        /// <returns>売上データリスト (IEnumerable&lt;V_UriageDataList&gt;) を表すTask。</returns>
        Task<IEnumerable<V_UriageDataList>> GetUriageDataList(int companyId, int uriageKubun, int tourokuKubun, string fromDate, string toDate, string fromTokuisaki, string toTokuisaki, string fromYosya, string toYosya, int shimeDay, string seikyudateTo, int seikyuTantou, int haisyaTantou, int driverId);
        Task<IEnumerable<T_Uriage>> GetUriageByIds(List<int> ids);
    }
}