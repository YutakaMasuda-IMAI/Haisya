using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace SeikyuWeb.Infrastructure
{
    /// <summary>
    /// ユニットオブワーククラス
    /// </summary>
    /// <typeparam name="TContext">DbContextの型</typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// リソースを解放する
        /// </summary>
        public void Dispose() => _context.Dispose();

        /// <summary>
        /// 非同期でコミットする
        /// </summary>
        /// <returns>コミット結果</returns>
        public Task<int> CommitAsync() => _context.SaveChangesAsync();
    }
}