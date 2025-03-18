using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.MapPointDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// マップポイントサービスインターフェース
    /// </summary>
    public interface IMapPointService
    {
        /// <summary>
        /// マップポイントを取得します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="groupId">グループID</param>
        /// <param name="address2">住所2</param>
        /// <returns>マップポイントDTOの列挙</returns>
        Task<IEnumerable<MapPointDto>> GetMapPoints(int userId, int? groupId, string address2);

        /// <summary>
        /// マップポイントを作成します。
        /// </summary>
        /// <param name="dto">マップポイント更新DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> CreateMapPoint(UpdateMapPointDto dto);

        /// <summary>
        /// マップポイントを更新します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dto">マップポイント更新DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateMapPoint(int id, UpdateMapPointDto dto);

        /// <summary>
        /// マップポイントをチェックします。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="groupId">グループID</param>
        /// <param name="addressCode">住所コード</param>
        /// <returns>マップポイント</returns>
        Task<T_Point> CheckMapPoint(int userId, int groupId, string addressCode);
    }
}
