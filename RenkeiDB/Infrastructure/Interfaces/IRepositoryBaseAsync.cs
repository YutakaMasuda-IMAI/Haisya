using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RenkeiDB.Infrastructure.Interfaces
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

        Task<T> ExecuteStoredProcedureAsync(string storedProcedure, params object[] parameters);

        Task<List<T>> ExecuteStoredProcedureListAsync(string storedProcedure, params object[] parameters);

        Task<TResult> ExecuteScalarStoredProcedureAsync<TResult>(string storedProcedure, params SqlParameter[] parameters);

        Task<List<T2>> ExecuteStoredProcedureAsync<T2>(string storedProcedure, params SqlParameter[] parameters) where T2 : class, new();
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

        Task<T> CreateEntityAsync(T entity);
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