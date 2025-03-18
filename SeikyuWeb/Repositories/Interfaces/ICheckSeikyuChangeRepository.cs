using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 請求変更チェックリポジトリインターフェース
    /// </summary>
    public interface ICheckSeikyuChangeRepository : IRepositoryBaseAsync<TCheckSeikyuChange, HaisyaContext>
    {
        /// <summary>
        /// 指定された請求IDと運賃IDの請求変更を非同期で取得します。
        /// </summary>
        /// <param name="checkSeikyuIds">請求IDのリスト</param>
        /// <param name="uriageUnchinIds">運賃IDのリスト</param>
        /// <returns>請求変更のリスト</returns>
        Task<IEnumerable<TCheckSeikyuChange>> GetByIdsAsync(List<int> checkSeikyuIds, List<int> uriageUnchinIds);

        /// <summary>
        /// 指定された請求IDと運賃IDの請求変更を非同期で取得します。
        /// </summary>
        /// <param name="checkSeikyuIds">請求IDのリスト</param>
        /// <param name="uriageUnchinIds">運賃IDのリスト</param>
        /// <returns>請求変更のリスト</returns>
        Task<IEnumerable<TCheckSeikyuChange>> GetBySeikyuIdAndUnchinId(List<int> checkSeikyuIds, List<int> uriageUnchinIds);
    }
}
