using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SeikyuWeb.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SeikyuWeb.Infrastructure
{
    /// <summary>
    /// 非同期リポジトリの基本クラス
    /// </summary>
    /// <typeparam name="T">エンティティの型</typeparam>
    /// <typeparam name="TContext">DbContextの型</typeparam>
    public class RepositoryBaseAsync<T, TContext> : IRepositoryBaseAsync<T, TContext>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private readonly IUnitOfWork<TContext> _unitOfWork;

        public RepositoryBaseAsync(TContext dbContext, IUnitOfWork<TContext> unitOfWork)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        /// <summary>
        /// 全てのエンティティを取得する
        /// </summary>
        /// <param name="trackChanges">変更を追跡するかどうか</param>
        /// <returns>エンティティのクエリ</returns>
        public IQueryable<T> FindAll(bool trackChanges = false) => !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

        /// <summary>
        /// 全てのエンティティを取得する（関連プロパティを含む）
        /// </summary>
        /// <param name="trackChanges">変更を追跡するかどうか</param>
        /// <param name="includeProperties">関連プロパティ</param>
        /// <returns>エンティティのクエリ</returns>
        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = FindAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        /// <summary>
        /// 条件に一致するエンティティを取得する
        /// </summary>
        /// <param name="expression">条件式</param>
        /// <param name="trackChanges">変更を追跡するかどうか</param>
        /// <returns>エンティティのクエリ</returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            _dbContext.Set<T>().Where(expression).AsNoTracking() :
            _dbContext.Set<T>().Where(expression);

        /// <summary>
        /// 条件に一致するエンティティを取得する（関連プロパティを含む）
        /// </summary>
        /// <param name="expression">条件式</param>
        /// <param name="trackChanges">変更を追跡するかどうか</param>
        /// <param name="includeProperties">関連プロパティ</param>
        /// <returns>エンティティのクエリ</returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        /// <summary>
        /// トランザクションを開始する
        /// </summary>
        /// <returns>トランザクション</returns>
        public Task<IDbContextTransaction> BeginTransactionAsync() => _dbContext.Database.BeginTransactionAsync();

        /// <summary>
        /// トランザクションを終了する
        /// </summary>
        public async Task EndTransactionAsync()
        {
            await SaveChangeAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }

        /// <summary>
        /// トランザクションをロールバックする
        /// </summary>
        /// <returns>タスク</returns>
        public Task RollbackTransactionAsync() => _dbContext.Database.RollbackTransactionAsync();

        /// <summary>
        /// エンティティを作成する
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <returns>タスク</returns>
        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await SaveChangeAsync();
        }

        /// <summary>
        /// エンティティのリストを作成する
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        /// <returns>タスク</returns>
        public async Task CreatListAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await SaveChangeAsync();
        }

        /// <summary>
        /// エンティティを更新する
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <returns>タスク</returns>
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await SaveChangeAsync();
        }

        /// <summary>
        /// エンティティのリストを更新する
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        /// <returns>タスク</returns>
        public async Task UpdateListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            await SaveChangeAsync();
        }

        /// <summary>
        /// エンティティを削除する
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <returns>タスク</returns>
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// エンティティのリストを削除する
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        /// <returns>タスク</returns>
        public Task DeleteListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 変更を保存する
        /// </summary>
        /// <returns>保存結果</returns>
        public Task<int> SaveChangeAsync() => _unitOfWork.CommitAsync();

        /// <summary>
        /// DbContextを取得する
        /// </summary>
        protected TContext DbContext 
        {
            get { return _dbContext; }
        }
    }
}