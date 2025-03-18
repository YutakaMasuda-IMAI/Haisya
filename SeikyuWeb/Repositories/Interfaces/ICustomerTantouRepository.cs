using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 顧客担当リポジトリインターフェース
    /// </summary>
    public interface ICustomerTantouRepository : IRepositoryBaseAsync<MCustomerTantou, HaisyaContext>
    {
    }
}
