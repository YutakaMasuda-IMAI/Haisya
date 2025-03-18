using RenkeiDB.Dto.CarInfoDto;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 車両情報サービスインターフェース
    /// </summary>
    public interface ICarInfoService
    {
        /// <summary>
        /// 車両情報を非同期で取得します。
        /// </summary>
        /// <param name="carNo">車両番号</param>
        /// <param name="groupIds">グループIDの配列</param>
        /// <returns>車両情報DTO</returns>
        Task<CarInfoDto> GetCarInfoAsync(string carNo, int[] groupIds);
    }
}
