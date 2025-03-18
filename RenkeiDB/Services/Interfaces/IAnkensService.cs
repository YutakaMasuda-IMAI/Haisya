using RenkeiDB.Dto;
using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.PortalDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 案件サービスインターフェース
    /// </summary>
    public interface IAnkensService
    {
        /// <summary>
        /// 案件リストを非同期で取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="oroshiAddress">卸先住所</param>
        /// <returns>案件DTOの列挙</returns>
        Task<IEnumerable<AnkensDto>> GetAnkenListAsync(int companyId, string oroshiAddress);

        /// <summary>
        /// 案件詳細を非同期で取得します。
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <returns>連携案件DTO</returns>
        Task<RenkeiAnkenDto> GetAnkenDetailAsync(int id, int userId);

        /// <summary>
        /// 案件を非同期で更新します。
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="dto">更新案件DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateAnkenAsync(int id, int userId, int companyId, UpdateAnkenDto dto);

        /// <summary>
        /// 案件変更履歴情報を取得します。
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <returns>案件変更履歴DTO</returns>
        Task<GetAnkenChangeDto> GetHistoryChangeAnkenInfo(int id);

        /// <summary>
        /// 案件を作成します。
        /// </summary>
        /// <param name="user">ユーザーログインDTO</param>
        /// <param name="dto">案件作成DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> CreateAnken(UserLoginDto user, AnkenCreateDto dto);

        /// <summary>
        /// 空車情報リストを非同期で取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>会社ポータルDTO</returns>
        Task<CompanyPortalsDto> GetInfoListKeepEmptyCarAsync(int companyId, int branchId);

        /// <summary>
        /// 注文を非同期で取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>受注案件DTOのリスト</returns>
        Task<List<JuchuAnkenDto>> GetOrdersAsync(int companyId, int branchId);
    }
}
