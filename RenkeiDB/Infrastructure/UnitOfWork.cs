using Microsoft.EntityFrameworkCore;
using RenkeiDB.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Infrastructure
{
    /// <summary>
    /// ユニットオブワークの実装
    /// </summary>
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public void Dispose() => _context.Dispose();

        /// <summary>
        /// 非同期でコミットを行う
        /// </summary>
        /// <returns>コミット結果</returns>
        public Task<int> CommitAsync() => _context.SaveChangesAsync();
    }
}