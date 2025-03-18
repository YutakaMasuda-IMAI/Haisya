using SeikyuWeb.Dto;
using SeikyuWeb.Dto.InfoDto;
using SeikyuWeb.Dto.PortalDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Services.Interfaces
{
    /// <summary>
    /// ポータル情報サービスインターフェース
    /// </summary>
    public interface IPortalInfoService
    {
        /// <summary>
        /// 通知情報を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isCompany">会社フラグ</param>
        /// <returns>通知情報リスト</returns>
        Task<IEnumerable<PortalInfoDto>> GetNotification(int id, bool isCompany);

        /// <summary>
        /// ポータル情報を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isCompany">会社フラグ</param>
        /// <returns>ポータル情報リスト</returns>
        Task<IEnumerable<PortalDto>> GetPortals(int id, bool isCompany);

        /// <summary>
        /// 表示フラグを更新します。
        /// </summary>
        /// <param name="ids">IDリスト</param>
        /// <returns>タスク</returns>
        Task UpdateDisplayFlag(List<int> ids);

        /// <summary>
        /// IDによって表示フラグを更新します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="displayFlg">表示フラグ</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateDisplayFlagById(int id, bool? displayFlg = false);
    }
}
