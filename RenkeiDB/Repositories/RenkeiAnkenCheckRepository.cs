using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 連携案件チェックリポジトリ
    /// </summary>
    public class RenkeiAnkenCheckRepository : RepositoryBaseAsync<T_Renkei_Anken_Check, ApplicationDbContext>, IRenkeiAnkenCheckRepository
    {
        private ApplicationDbContext _dbContext;
        public RenkeiAnkenCheckRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
        }
    }
}
