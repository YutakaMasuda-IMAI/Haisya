using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// マスターコードデータリポジトリのインターフェースを定義します。
    /// </summary>
    public interface IMasterCodeDataRepository : IRepositoryBaseAsync<M_Code_Datum, ApplicationDbContext>
    {
        /// <summary>
        /// 指定されたコードIDに基づいてマスターコードデータを取得します。
        /// </summary>
        /// <param name="codeId">コードID</param>
        /// <returns>マスターコードデータのコレクションを含むタスク</returns>
        Task<IEnumerable<M_Code_Datum>> GetMasterCodeData(int codeId);
    }
}
