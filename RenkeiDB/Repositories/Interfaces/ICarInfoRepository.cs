using RenkeiDB.Data;
using RenkeiDB.Dto.CarInfoDto;
using RenkeiDB.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 車情報リポジトリのインターフェースを定義します。
    /// </summary>
    public interface ICarInfoRepository : IRepositoryBaseAsync<T_Share_Syaryo, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された車両番号とグループIDに基づいて車情報を非同期的に取得します。
        /// </summary>
        /// <param name="carNo">車両番号</param>
        /// <param name="groupIds">グループIDの配列</param>
        /// <returns>車情報を含む<see cref="JoinCarInfoDto"/>のタスク</returns>
        Task<JoinCarInfoDto> GetCarInfoAsync(string carNo, int[] groupIds);
    }
}
