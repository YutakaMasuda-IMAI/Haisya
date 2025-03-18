using Microsoft.AspNetCore.Mvc;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.EmptyCarDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 共有車両サービスインターフェース
    /// </summary>
    public interface IShareSyaryoService
    {
        /// <summary>
        /// 空車の詳細を非同期で取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>アクション結果</returns>
        Task<IActionResult> GetDetailEmptyCarAsync(int id);

        /// <summary>
        /// 空車リストを非同期で取得します。
        /// </summary>
        /// <param name="paramRequests">リクエストパラメータDTO</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>アクション結果</returns>
        Task<IActionResult> GetListEmptyCarAsync(ShareSyaryoListRequestDto paramRequests, int companyId, int branchId);

        /// <summary>
        /// 空車を非同期で更新します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="dto">空車更新DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateEmptyCarAsync(int id, int userId, UpdateEmptyCarDto dto);

        /// <summary>
        /// 空車を非同期で作成します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="dto">空車作成DTO</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="minGroupId">最小グループID</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> CreateEmptyCarAsync(int userId, CreateEmptyCarDto dto, int companyId, int branchId, int? minGroupId);

        /// <summary>
        /// 空車のステータスを非同期で更新します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="dto">空車ステータス更新DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateStatusEmptyCarAsync(int id, int userId, int companyId, int branchId, UpdateStatusEmptyCarDto dto);

        /// <summary>
        /// PDF用の共有空車リストを非同期で取得します。
        /// </summary>
        /// <param name="paramRequests">リクエストパラメータDTO</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>共有空車リスト</returns>
        Task<IEnumerable<T_Share_Syaryo>> GetShareEmptyCarForPDFAsync(ShareSyaryoListRequestDto paramRequests, int companyId, int branchId);
    }
}