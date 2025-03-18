using RenkeiDB.Dto;
using RenkeiDB.Dto.LuggageDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 荷物サービスインターフェース
    /// </summary>
    public interface ILuggageService
    {
        /// <summary>
        /// 荷物を取得します。
        /// </summary>
        /// <param name="dto">荷物パラメータDTO</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>荷物印刷データDTOの列挙</returns>
        Task<IEnumerable<LuggagePrintDataDto>> Get_luggages(LuggageParamsDto dto, int companyId, int branchId);

        /// <summary>
        /// 荷物を非同期で取得します。
        /// </summary>
        /// <param name="dto">荷物パラメータDTO</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>共有荷物DTOの列挙</returns>
        Task<IEnumerable<ShareLuggageDto>> GetLuggageAsync(LuggageParamsDto dto, int companyId, int branchId);

        /// <summary>
        /// 荷物の詳細を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>共有荷物DTO</returns>
        Task<ShareLuggageDto> GetLuggageDetailAsync(int id);

        /// <summary>
        /// 共有荷物を作成します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="minGroupId">最小グループID</param>
        /// <param name="dto">共有荷物作成DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> CreateShareLuggageAsync(int userId, int companyId, int branchId, int minGroupId, CreateShareLuggageDto dto);

        /// <summary>
        /// 共有荷物を更新します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="dto">共有荷物更新DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateShareLuggageAsync(int id, int userId, UpdateShareLuggageDto dto);

        /// <summary>
        /// 荷物を作成します。
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="user_id">ユーザーID</param>
        /// <param name="luggage">荷物データDTO</param>
        Task Create(int company_id, int user_id, LuggageDataDto luggage);

        /// <summary>
        /// 共有荷物のステータスを更新します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="dto">共有荷物ステータス更新DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateShareLuggageStatusAsync(int id, int userId, int companyId, int branchId, UpdateShareLuggageStatusDto dto);
    }
}
