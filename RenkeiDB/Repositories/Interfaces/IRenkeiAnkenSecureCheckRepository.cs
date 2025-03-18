using RenkeiDB.Data;
using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 連携案件セキュアチェックリポジトリのインターフェースを定義します。
    /// </summary>
    public interface IRenkeiAnkenSecureCheckRepository : IRepositoryBaseAsync<T_Renkei_Anken_Secure_Check, ApplicationDbContext>
    {
    }
}
