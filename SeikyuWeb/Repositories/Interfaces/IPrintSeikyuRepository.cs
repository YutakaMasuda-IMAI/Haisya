using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 請求印刷リポジトリインターフェース
    /// </summary>
    public interface IPrintSeikyuRepository : IRepositoryBaseAsync<TPrintSeikyu, HaisyaContext>
    {
        /// <summary>
        /// 会社の請求書を取得します。
        /// </summary>
        /// <param name="fromYm">開始年月</param>
        /// <param name="toYm">終了年月</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="shimeDay">締め日</param>
        /// <returns>請求書リスト</returns>
        Task<IEnumerable<TPrintSeikyu>> GetInvoiceForCompany(DateTime? fromYm, DateTime? toYm, int[] zeiKubun, int? shimeDay);

        /// <summary>
        /// 顧客の請求書を取得します。
        /// </summary>
        /// <param name="tantouId">担当者ID</param>
        /// <param name="fromYm">開始年月</param>
        /// <param name="toYm">終了年月</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="shimeDay">締め日</param>
        /// <returns>請求書リスト</returns>
        Task<IEnumerable<TPrintSeikyu>> GetInvoiceForCustomer(int tantouId, DateTime? fromYm, DateTime? toYm, int[] zeiKubun, int? shimeDay);

        /// <summary>
        /// 請求書リストを取得します。
        /// </summary>
        /// <param name="idInt">ID（会社ID）/（担当ID）</param>
        /// <param name="isCompany">True:（会社ID）/False:（担当ID）</param>
        /// <param name="fromYm">開始年月</param>
        /// <param name="toYm">終了年月</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="shimeDay">締め日</param>
        /// <returns>請求書リスト</returns>
        Task<IEnumerable<InvoiceQueryDto>> GetInvoiceList(int idInt, bool isCompany, DateTime? fromYm, DateTime? toYm, int[] zeiKubun, int? shimeDay);

        /// <summary>
        /// すべての請求書を取得します。
        /// </summary>
        /// <returns>請求書リスト</returns>
        Task<IEnumerable<TPrintSeikyu>> GetAllAsync();

        /// <summary>
        /// 請求IDで請求書を取得します。
        /// </summary>
        /// <param name="checkSeikyuID">請求ID</param>
        /// <returns>請求書リスト</returns>
        Task<IEnumerable<TPrintSeikyu>> GetBycheckSeikyuIdAsync(int checkSeikyuID);

        /// <summary>
        /// 印刷請求IDで請求書を取得します。
        /// </summary>
        /// <param name="printSeikyuID">印刷請求ID</param>
        /// <returns>請求書リスト</returns>
        Task<IEnumerable<TPrintSeikyu>> GetByprintSeikyuIdAsync(int printSeikyuID);
    }
}
