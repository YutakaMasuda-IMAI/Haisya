using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace SeikyuWeb.Infrastructure.Interfaces
{
    /// <summary>
    /// ユニットオブワークインターフェース
    /// </summary>
    /// <typeparam name="TContext">DbContextのタイプ</typeparam>
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        /// <summary>
        /// 非同期でコミットを行います
        /// </summary>
        /// <returns>変更された行数</returns>
        Task<int> CommitAsync();
    }
}