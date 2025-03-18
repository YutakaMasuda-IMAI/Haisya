using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Common;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 印刷パラメータリポジトリクラス
    /// </summary>
    public class PrintParameterRepository : RepositoryBaseAsync<TPrintParameter, HaisyaContext>, IPrintParameterRepository
    {
        public PrintParameterRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDの印刷パラメータを非同期で取得します。
        /// </summary>
        /// <param name="id">印刷ID</param>
        /// <returns>印刷パラメータ</returns>
        public async Task<TPrintParameter> GetByIdAsync(int id)
            => await FindByCondition(x => x.PrintId.Equals(id)).FirstOrDefaultAsync();

        /// <summary>
        /// コード区分に基づいて印刷パラメータのリストを非同期で取得します。
        /// </summary>
        /// <returns>印刷パラメータのリスト</returns>
        public async Task<IEnumerable<TPrintParameter>> GetByCodeKubunsAsync()
        {
            int[] listPrintKubun = new int[] {
                SystemConstants.CodeData.請求問合せ,
                SystemConstants.CodeData.支払問合せ,
                SystemConstants.CodeData.請求書,
            };
            List<TPrintParameter> data = await (from PP in DbContext.Set<TPrintParameter>()
                              join CD in DbContext.Set<MCodeDatum>()
                                  on PP.PrintKubun.ToString() equals CD.CodeData
                              join C in DbContext.Set<MCode>()
                                  on CD.CodeId equals C.CodeId
                              where
                                  PP.DelDatetime == null
                                  //CodeData
                                  && (CD.CodeData.Equals(SystemConstants.CodeData.請求問合せ.ToString()) ||
                                      CD.CodeData.Equals(SystemConstants.CodeData.支払問合せ.ToString()) ||
                                      CD.CodeData.Equals(SystemConstants.CodeData.請求書.ToString()))
                                  //Code
                                  && C.CodeId.Equals(SystemConstants.CodeId.TEN)
                                  && listPrintKubun.Contains(PP.PrintKubun)
                              select PP).ToListAsync();
            return data;
        }

        /// <summary>
        /// 指定した請求IDに基づいて印刷パラメータのリストを非同期で取得します。
        /// </summary>
        /// <param name="id">請求ID</param>
        /// <returns>印刷パラメータのリスト</returns>
        public async Task<IEnumerable<TPrintParameter>> GetListBySeikyuIdAsync(int id)
            => await FindByCondition(x => x.DataId.Equals(id) && x.DelDatetime == null && x.PrintKubun == CodeData.請求書 && x.LimitDate >= DateTime.Now).ToListAsync();

        /// <summary>
        /// 指定したチェック請求IDに基づいて印刷パラメータのリストを非同期で取得します。
        /// </summary>
        /// <param name="id">チェック請求ID</param>
        /// <returns>印刷パラメータのリスト</returns>
        public async Task<IEnumerable<TPrintParameter>> GetListByCheckSeikyuIdAsync(int id)
            => await FindByCondition(x => x.DataId.Equals(id) && x.DelDatetime == null && x.LimitDate >= DateTime.Now).ToListAsync();

        /// <summary>
        /// 指定した支払チェックIDに基づいて、期限日を確認して印刷パラメータを非同期で取得します。
        /// </summary>
        /// <param name="id">支払チェックID</param>
        /// <returns>印刷パラメータのリスト</returns>
        public async Task<IEnumerable<TPrintParameter>> GetByCheckShitabaraiIdAsync(int id)
            => await FindByCondition(x => x.DataId.Equals(id) && x.DelDatetime == null && x.LimitDate >= DateTime.Now.Date).ToListAsync();
    }
}
