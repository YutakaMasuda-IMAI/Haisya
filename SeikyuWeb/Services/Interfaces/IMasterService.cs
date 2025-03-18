using SeikyuWeb.Dto.MasterDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Services.Interfaces
{
    /// <summary>
    /// マスターサービスインターフェース
    /// </summary>
    public interface IMasterService
    {
        /// <summary>
        /// マスターコードデータを取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>マスターコードデータリスト</returns>
        Task<IEnumerable<CodeDataDto>> GetMasterCodeDataAsync(int id);
    }
}
