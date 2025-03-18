using RenkeiDB.Data;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Dto.MapPointDto;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 地図ポイントリポジトリのインターフェースを定義します。
    /// </summary>
    public interface IMapPointRepository : IRepositoryBaseAsync<T_Point, ApplicationDbContext>
    {
        /// <summary>
        /// 指定されたユーザーID、グループID、および住所2に基づいてポイントリストを取得します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="groupId">グループID</param>
        /// <param name="address2">住所2</param>
        /// <returns>ポイントリストを含むタスク</returns>
        public Task<IEnumerable<MapPointDto>> GetListPointByUserIdAndAddress2(int userId, int? groupId, string address2);
    }
}
