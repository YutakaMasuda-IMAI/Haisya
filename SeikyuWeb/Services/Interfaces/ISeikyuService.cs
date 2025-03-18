using SeikyuWeb.Dto.Seikyu;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Services.Interfaces
{
    /// <summary>
    /// 請求サービスインターフェース
    /// </summary>
    public interface ISeikyuService
    {
        /// <summary>
        /// 請求照会リストを取得します。
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="idInt">ID</param>
        /// <param name="isCompany">会社フラグ</param>
        /// <returns>請求照会リスト</returns>
        Task<IEnumerable<CheckSeikyuDto>> GetBillingInqueryList(int kubun, int idInt, bool isCompany);

        /// <summary>
        /// 請求書リストを取得します。
        /// </summary>
        /// <param name="idInt">ID</param>
        /// <param name="isCompany">会社フラグ</param>
        /// <param name="fromYm">開始年月</param>
        /// <param name="toYm">終了年月</param>
        /// <param name="shimeDay">締め日</param>
        /// <param name="zeiKubunIntValue">税区分</param>
        /// <param name="statusIntValue">ステータス</param>
        /// <returns>請求書リスト</returns>
        Task<IEnumerable<InvoiceDto>> GetInvoiceList(int idInt, bool isCompany, DateTime? fromYm, DateTime? toYm, int? shimeDay, int zeiKubunIntValue, int statusIntValue);

        /// <summary>
        /// 請求書の詳細を取得します。
        /// </summary>
        /// <param name="printSeikyuId">印刷請求ID</param>
        /// <param name="idInt">ID</param>
        /// <param name="isCompany">会社フラグ</param>
        /// <returns>請求書とレポートのレイアウト</returns>
        Task<InvoiceAndReportLayoutDto> GetInvoiceDetailAsync(int printSeikyuId, int idInt, bool isCompany);
    }
}
