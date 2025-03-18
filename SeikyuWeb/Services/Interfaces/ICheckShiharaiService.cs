using SeikyuWeb.Dto;
using SeikyuWeb.Dto.CheckShiharaisDto;
using SeikyuWeb.Dto.Shitabarai;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Services.Interfaces
{
    /// <summary>
    /// 支払い確認サービスインターフェース
    /// </summary>
    public interface ICheckShiharaiService
    {
        /// <summary>
        /// 支払い確認を更新
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="tantouId">担当ID</param>
        /// <param name="dto">更新データ</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateCheckShiharai(int id, int tantouId, RequestUpdateCheckShiharaisDto dto);

        /// <summary>
        /// 支払い確認リストを取得
        /// </summary>
        /// <param name="tantouId">担当ID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="kubun">区分</param>
        /// <returns>支払い確認リスト</returns>
        Task<IEnumerable<CheckShiharaisDto>> GetListCheckShiharaisAsync(int tantouId, int companyId, int kubun);

        /// <summary>
        /// 支払い問合せをIDで取得
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="idInt">ID（会社ID）/（担当ID）</param>
        /// <param name="isCompany">True:（会社ID）/False:（担当ID）</param>
        /// <returns>支払い問合せDTO</returns>
        Task<PaymentInquiryDto> GetPaymentInquiriesById(int id, int idInt, bool isCompany);
    }
}
