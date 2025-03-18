using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RenkeiDB.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RenkeiDB.Infrastructure
{
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

        public IQueryable<T> FindAll(bool trackChanges = false) => !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            _dbContext.Set<T>().Where(expression).AsNoTracking() :
            _dbContext.Set<T>().Where(expression);

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public Task<IDbContextTransaction> BeginTransactionAsync() => _dbContext.Database.BeginTransactionAsync();

        public async Task EndTransactionAsync()
        {
            await SaveChangeAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }

        public Task RollbackTransactionAsync() => _dbContext.Database.RollbackTransactionAsync();

        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await SaveChangeAsync();
        }
        public async Task<T> CreateEntityAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await SaveChangeAsync();
            return entity;
        }

        public async Task CreatListAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await SaveChangeAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await SaveChangeAsync();
        }

        public async Task UpdateListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            await SaveChangeAsync();
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public Task<int> SaveChangeAsync() => _unitOfWork.CommitAsync();

        protected TContext DbContext
        {
            get { return _dbContext; }
        }

        public async Task<T> ExecuteStoredProcedureAsync(string storedProcedure, params object[] parameters)
        {
            var result = await _dbContext.Set<T>().FromSqlRaw(storedProcedure, parameters).AsNoTracking().ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task<List<T>> ExecuteStoredProcedureListAsync(string storedProcedure, params object[] parameters)
        {
            return await _dbContext.Set<T>().FromSqlRaw(storedProcedure, parameters).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// ストアドプロシージャの実行（結果はExecuteScalarAsyncで取得）
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns>return the first column of the first record</returns>
        public async Task<TResult> ExecuteScalarStoredProcedureAsync<TResult>(string storedProcedure,
            params SqlParameter[] parameters)
        {
            try
            {
                var connection = _dbContext.Database.GetDbConnection();

                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                await using (var command = connection.CreateCommand())
                {
                    command.CommandText = storedProcedure;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

                    if (_dbContext.Database.CurrentTransaction != null)
                    {
                        command.Transaction = _dbContext.Database.CurrentTransaction.GetDbTransaction();
                    }

                    var result = await command.ExecuteScalarAsync();
                    return (TResult)result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Store Proc Error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Store Proc Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// ストアドプロシージャの実行（結果はリスト型で返す）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<List<T2>> ExecuteStoredProcedureAsync<T2>(string storedProcedure,
            params SqlParameter[] parameters) where T2 : class, new()
        {
            var resultList = new List<T2>();

            try
            {
                var connection = _dbContext.Database.GetDbConnection();
                await using (var command = connection.CreateCommand())
                {
                    command.CommandText = storedProcedure;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

                    if (_dbContext.Database.CurrentTransaction != null)
                    {
                        command.Transaction = _dbContext.Database.CurrentTransaction.GetDbTransaction();
                    }

                    await connection.OpenAsync();
                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        var properties = typeof(T2).GetProperties();

                        while (await reader.ReadAsync())
                        {
                            var instance = new T2();
                            foreach (var property in properties)
                            {
                                if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                                {
                                    property.SetValue(instance, reader[property.Name]);
                                }
                            }
                            resultList.Add(instance);
                        }
                    }
                    await connection.CloseAsync();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Store Proc Error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Store Proc Error: {ex.Message}");
                throw;
            }

            return resultList;
        }
    }
}