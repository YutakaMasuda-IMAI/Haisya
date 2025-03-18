using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 共有荷物詳細リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IShareLuggageDetailRepository : IRepositoryBaseAsync<T_Share_Luggage_Detail, ApplicationDbContext>
    {
    }
}
