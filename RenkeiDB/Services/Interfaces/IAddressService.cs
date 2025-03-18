using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.PortalDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 住所サービスインターフェース
    /// </summary>
    public interface IAddressService
    {
        /// <summary>
        /// 連携案件ポイントリストを非同期で取得します。
        /// </summary>
        /// <returns>住所履歴DTO</returns>
        Task<AddressHistoryDto> GetRenkeiAnkenPointListAsync();
    }
}
