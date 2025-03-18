using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 共有荷物セキュアリポジトリのインターフェースを定義します。
    /// </summary>
    public interface IShareLuggageSecureRepository : IRepositoryBaseAsync<T_Share_Luggage_Secure, ApplicationDbContext>
    {
    }
}
