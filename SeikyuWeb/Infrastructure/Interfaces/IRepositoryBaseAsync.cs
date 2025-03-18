using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SeikyuWeb.Infrastructure.Interfaces
{
    /// <summary>
    /// 非同期リポジトリクエリの基本インターフェース
    /// </summary>
    public interface IRepositoryQueryBaseAsync<T> where T : class
    {
        IQueryable<T> FindAll(bool trackChanges = false);

        IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);

        Task<int> SaveChangeAsync();
    }

    /// <summary>
    /// 非同期リポジトリの基本インターフェース
    /// </summary>
    public interface IRepositoryBaseAsync<T> : IRepositoryQueryBaseAsync<T> where T : class
    {
        Task CreateAsync(T entity);

        Task CreatListAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

        Task UpdateListAsync(IEnumerable<T> entities);

        Task DeleteAsync(T entity);

        Task DeleteListAsync(IEnumerable<T> entities);

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task EndTransactionAsync();

        Task RollbackTransactionAsync();
    }

    /// <summary>
    /// 非同期リポジトリクエリの基本インターフェース（コンテキスト付き）
    /// </summary>
    public interface IRepositoryQueryBaseAsync<T, TContext> : IRepositoryQueryBaseAsync<T>
        where T : class where TContext : DbContext
    {
    }

    /// <summary>
    /// 非同期リポジトリの基本インターフェース（コンテキスト付き）
    /// </summary>
    public interface IRepositoryBaseAsync<T, TContext> : IRepositoryBaseAsync<T>
        where T : class where TContext : DbContext
    {
    }
}