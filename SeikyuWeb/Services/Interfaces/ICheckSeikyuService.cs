using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.CheckShiharaisDto;
using System.Threading.Tasks;
using SeikyuWeb.Dto.ReportDto;

namespace SeikyuWeb.Services.Interfaces
{
    /// <summary>
    /// 請求確認サービスインターフェース
    /// </summary>
    public interface ICheckSeikyuService
    {
        /// <summary>
        /// 請求問合せをIDで取得
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="idLogin">ログインID</param>
        /// <param name="isCompany">True:（会社ID）/False:（担当ID）</param>
        /// <returns>請求問合せDTO</returns>
        Task<BillingInquiryDto> GetBillingInquiriesById(int id, int idLogin, bool isCompany);

        /// <summary>
        /// データエクスポートを取得
        /// </summary>
        /// <param name="checkSeikyuId">請求確認ID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="customerId">顧客ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>共通帳票DTO</returns>
        Task<ReportCommonDto> GetDataExport(int kubun, int checkSeikyuId);

        /// <summary>
        /// 請求確認を更新
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="tantouId">担当ID</param>
        /// <param name="dto">更新データ</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateCheckSeikyu(int id, int tantouId, RequestUpdateCheckSeikyusDto dto);
    }
}
