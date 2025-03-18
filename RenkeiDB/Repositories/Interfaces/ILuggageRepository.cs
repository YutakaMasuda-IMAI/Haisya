using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 荷物リポジトリのインターフェースを定義します。
    /// </summary>
    public interface ILuggageRepository : IRepositoryBaseAsync<M_Luggage, ApplicationDbContext>
    {
    }
}
