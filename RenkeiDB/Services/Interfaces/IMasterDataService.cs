using RenkeiDB.Dto;
using RenkeiDB.Dto.EquipmentDto;
using RenkeiDB.Dto.MasterDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// マスターデータサービスインターフェース
    /// </summary>
    public interface IMasterDataService
    {
        /// <summary>
        /// マスターコードデータを取得します。
        /// </summary>
        /// <param name="codeId">コードID</param>
        /// <returns>マスターコードデータDTOの列挙</returns>
        Task<IEnumerable<MasterCodeDataDto>> GetMasterCodeData(int codeId);

        /// <summary>
        /// 郵便番号データを取得します。
        /// </summary>
        /// <returns>郵便番号の列挙</returns>
        Task<IEnumerable<string>> GetPostCodeData();

        /// <summary>
        /// 設備を非同期で作成します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="dto">設備作成DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> CreateEquipmentAsync(int companyId, int userId, CreateEquipmentDto dto);

        /// <summary>
        /// 設備グループを取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>設備グループDTOの列挙</returns>
        Task<IEnumerable<EquipmentGroupDto>> GetEquipmentGroupAsync(int companyId);

        /// <summary>
        /// 車両を取得します。
        /// </summary>
        /// <param name="syasyu">車種</param>
        /// <param name="kata">型</param>
        /// <returns>車両DTOのリスト</returns>
        Task<IList<SyaryoDto>> GetSyaryoAsync(string syasyu, string kata);

        /// <summary>
        /// 郵便番号を取得します。
        /// </summary>
        /// <param name="ken">県</param>
        /// <param name="shikucho">市区町</param>
        /// <param name="choiki">町域</param>
        /// <returns>郵便番号</returns>
        Task<string> GetPostCodeAsync(string ken, string shikucho, string choiki);

        /// <summary>
        /// 住所リストを取得します。
        /// </summary>
        /// <param name="word">検索ワード</param>
        /// <returns>住所DTOのリスト</returns>
        Task<IList<AddressDto>> GetAddressList(string word);

        /// <summary>
        /// 県から住所を取得します。
        /// </summary>
        /// <param name="ken">県</param>
        /// <param name="shikucho">市区町</param>
        /// <returns>住所の列挙</returns>
        Task<IEnumerable<string>> GetAddressByKenAsync(string ken, string shikucho);
    }
}
