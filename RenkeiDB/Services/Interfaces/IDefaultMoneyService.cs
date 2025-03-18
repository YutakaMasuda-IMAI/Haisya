using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.MasterDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// デフォルト金額サービスインターフェース
    /// </summary>
    public interface IDefaultMoneyService
    {
        /// <summary>
        /// デフォルト金額を非同期で取得します。
        /// </summary>
        /// <returns>デフォルト金額DTOの列挙</returns>
        Task<IEnumerable<DefaultMoneyDto>> GetDefaultMoneysAsync();
    }
}
