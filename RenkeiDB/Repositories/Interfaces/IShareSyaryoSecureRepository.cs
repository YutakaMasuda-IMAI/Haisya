using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 共有車両セキュアリポジトリのインターフェースを定義します。
    /// </summary>
    public interface IShareSyaryoSecureRepository : IRepositoryBaseAsync<T_Share_Syaryo_Secure, ApplicationDbContext>
    {
    }
}
