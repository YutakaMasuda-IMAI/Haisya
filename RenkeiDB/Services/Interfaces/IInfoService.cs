using RenkeiDB.Dto.InfoDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 情報サービスインターフェース
    /// </summary>
    public interface IInfoService
    {
        /// <summary>
        /// 情報リストを取得します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="companyId">会社ID</param>
        /// <returns>情報DTOのリスト</returns>
        Task<List<InfoDto>> GetListInfo(int userId, int companyId);

        /// <summary>
        /// 表示フラグを設定します。
        /// </summary>
        /// <param name="user_id">ユーザーID</param>
        /// <param name="company_id">会社ID</param>
        /// <param name="info_id">情報ID</param>
        /// <param name="display_flg">表示フラグ</param>
        Task Set_display_flg(int user_id, int company_id, int info_id, bool display_flg);
    }
}
