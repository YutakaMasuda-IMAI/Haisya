using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace RenkeiDB.Infrastructure.Interfaces
{
    /// <summary>
    /// ユニットオブワークのインターフェース
    /// </summary>
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        /// <summary>
        /// 非同期でコミットを行う
        /// </summary>
        /// <returns>コミット結果</returns>
        Task<int> CommitAsync();
    }
}