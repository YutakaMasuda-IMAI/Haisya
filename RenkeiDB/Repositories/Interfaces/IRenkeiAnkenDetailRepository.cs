using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 連携案件詳細リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IRenkeiAnkenDetailRepository : IRepositoryBaseAsync<T_Renkei_Anken_Detail, ApplicationDbContext>
    {
    }
}
