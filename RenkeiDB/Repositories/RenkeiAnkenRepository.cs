using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.AnkenDto.AnkenChangeHistoryDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 連携案件リポジトリ
    /// </summary>
    public class RenkeiAnkenRepository : RepositoryBaseAsync<T_Renkei_Anken, ApplicationDbContext>, IRenkeiAnkenRepository
    {
        public RenkeiAnkenRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 案件の取得
        /// </summary>
        /// <param name="companyId">ログインユーザのcompany id</param>
        /// <param name="oroshiAddress">検索キーワード</param>
        /// <returns>案件のリスト</returns>
        public async Task<IEnumerable<JoinAnken>> GetAnkensAsync(int companyId, string oroshiAddress)
        {
            List<JoinAnken> data = await (from TRA in DbContext.Set<T_Renkei_Anken>()
                              join C in DbContext.Set<M_Company>()
                                  on TRA.Company_ID equals C.Renkei_Company_ID
                              join TRAP in DbContext.Set<T_Renkei_Anken_Point>()
                                  on new { id = TRA.Renkei_Anken_ID, order = TRA.Renkei_Anken_Latest_Order } equals new { id = TRAP.Renkei_Anken_ID, order = TRAP.Renkei_Anken_Order }
                                  into TRAPs
                              from TRAP in TRAPs.DefaultIfEmpty()
                              join TRAD in DbContext.Set<T_Renkei_Anken_Detail>()
                                  on new { id = TRA.Renkei_Anken_ID, order = TRA.Renkei_Anken_Latest_Order } equals new { id = TRAD.Renkei_Anken_ID, order = TRAD.Renkei_Anken_Order }
                                  into TRADs
                              from TRAD in TRADs.DefaultIfEmpty()
                              join TRAL in DbContext.Set<T_Renkei_Anken_Luggage>()
                                  on new { id = TRA.Renkei_Anken_ID, order = TRA.Renkei_Anken_Latest_Order } equals new { id = TRAL.Renkei_Anken_ID, order = TRAL.Renkei_Anken_Order }
                              into TRALs
                              from TRAL in TRALs.DefaultIfEmpty()
                              join TRAE in DbContext.Set<T_Renkei_Anken_Equipment>()
                                  on new { id = TRA.Renkei_Anken_ID, order = TRA.Renkei_Anken_Latest_Order } equals new { id = TRAE.Renkei_Anken_ID, order = TRAE.Renkei_Anken_Order }
                              into TRAEs
                              from TRAE in TRAEs.DefaultIfEmpty()

                              join ML in DbContext.Set<M_Luggage>()
                                  on TRAL.Luggage_ID equals ML.Luggage_ID
                                  into MLs
                              from ML in MLs.DefaultIfEmpty()
                              join ME in DbContext.Set<M_Equipment>()
                                  on TRAE.Equipment_ID equals ME.Equipment_ID
                                  into MEs
                              from ME in MEs.DefaultIfEmpty()

                              join MLG in DbContext.Set<M_Luggage_Group>()
                                  on ML.Luggage_Group_ID equals MLG.Luggage_Group_ID
                                  into MLGs
                              from MLG in MLGs.DefaultIfEmpty()

                              join MEG in DbContext.Set<M_Equipment_Group>()
                                  on ME.Equipment_Group_ID equals MEG.Equipment_Group_ID
                                  into MEGs
                              from MEG in MEGs.DefaultIfEmpty()

                              where TRA.Company_ID.Equals(companyId)
                                  && ((TRAP.Kubun.Equals(9)
                                  && TRAP.SEKubun.Equals("E"))
                                  || (TRAP.Kubun.Equals(1)
                                  && TRAP.SEKubun.Equals("S")))
                                  && (oroshiAddress == null || TRAP.Address.Contains(oroshiAddress))
                              select new JoinAnken
                              {
                                  renkeiAnken = TRA,
                                  company = C,
                                  renkeiAnkenDetail = TRAD,
                                  renkeiAnkenEquipment = TRAE,
                                  renkeiAnkenLuggage = TRAL,
                                  renkeiAnkenPoint = TRAP,
                                  luggage = ML,
                                  equipment = ME,
                                  mLuggageGroup = MLG,
                                  mEquipmentGroup = MEG,
                              }
                        ).ToListAsync();
            return data;
        }

        /// <summary>
        /// 案件詳細取得
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <returns>案件詳細</returns>
        public async Task<JoinRenkeiAnken> GetDetailAsync(int id)
        {
            IQueryable<JoinRenkeiAnken> builder = from tra in DbContext.Set<T_Renkei_Anken>()
                          join trad in DbContext.Set<T_Renkei_Anken_Detail>()
                              on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                              equals new { id = trad.Renkei_Anken_ID, order = trad.Renkei_Anken_Order }

                          join trac in DbContext.Set<T_Renkei_Anken_Check>()
                              on tra.Renkei_Anken_ID equals trac.Renkei_Anken_ID into renkeiAnkenCheck
                          from trac in renkeiAnkenCheck.DefaultIfEmpty()

                          join tras in DbContext.Set<T_Renkei_Anken_Secure>()
                              on tra.Renkei_Anken_ID equals tras.Renkei_Anken_ID into renkeiAnkenSecure
                          from tras in renkeiAnkenSecure.DefaultIfEmpty()

                          join trasc in DbContext.Set<T_Renkei_Anken_Secure_Check>()
                              on tras.Renkei_Anken_ID equals trasc.Renkei_Anken_Secure_ID into renkeiAnkenSecureCheck
                          from trasc in renkeiAnkenSecureCheck.DefaultIfEmpty()

                          join company in DbContext.Set<M_Company>()
                              on tra.Company_ID equals company.Renkei_Company_ID

                          where
                              tra.Renkei_Anken_ID == id
                          orderby tra.Renkei_Anken_No ascending
                          select new JoinRenkeiAnken
                          {
                              Anken = tra,
                              AnkenDetail = trad,
                              AnkenCheck = trac,
                              AnkenSecure = tras,
                              AnkenSecureCheck = trasc,
                              Company = company
                          };
            return (await builder.ToListAsync()).FirstOrDefault();
        }


        /// <summary>
        /// 依頼案件リストの取得
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>依頼案件リスト</returns>
        public async Task<IEnumerable<JoinRenkeiAnken>> GetIraiAnkensAsync(int companyId, int branchId)
        {
            IQueryable<JoinRenkeiAnken> builder = from tra in DbContext.Set<T_Renkei_Anken>()

                          join trad in DbContext.Set<T_Renkei_Anken_Detail>()
                              on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                              equals new { id = trad.Renkei_Anken_ID, order = trad.Renkei_Anken_Order }

                          // T_Renkei_Anken_PointをSEKubun == "S"で結合
                          join trapS in DbContext.Set<T_Renkei_Anken_Point>()
                              on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                              equals new { id = trapS.Renkei_Anken_ID, order = trapS.Renkei_Anken_Order }
                              into renkeiAnkenPointS
                          from trapSNullable in renkeiAnkenPointS.DefaultIfEmpty()
                          where trapSNullable == null || trapSNullable.SEKubun == SystemConstants.Kubun.Types.TypeOne

                          // T_Renkei_Anken_PointをSEKubun == "E"で結合
                          join trapE in DbContext.Set<T_Renkei_Anken_Point>()
                              on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                              equals new { id = trapE.Renkei_Anken_ID, order = trapE.Renkei_Anken_Order }
                              into renkeiAnkenPointE
                          from trapENullable in renkeiAnkenPointE.DefaultIfEmpty()
                          where trapENullable == null || trapENullable.SEKubun == SystemConstants.Kubun.Types.TypeNine

                          join tras in DbContext.Set<T_Renkei_Anken_Secure>()
                              on tra.Renkei_Anken_ID equals tras.Renkei_Anken_ID into trass
                          from tras in trass.DefaultIfEmpty()

                          join cug in DbContext.Set<M_CompanyUser_Group>()
                              on trad.TantouID equals cug.Group_ID into cugs
                          from cug in cugs.DefaultIfEmpty()

                          where
                              tra.Company_ID == companyId
                              && tra.Branch_ID == branchId
                              && tra.Renkei_Anken_Kubun == 0

                          orderby tra.Renkei_Anken_No ascending
                          select new JoinRenkeiAnken
                          {
                              Anken = tra,
                              AnkenDetail = trad,
                              AnkenPointS = trapSNullable,
                              AnkenPointE = trapENullable,
                              AnkenSecure = tras,
                              CompanyUserGroup = cug,
                          };
            return await builder.ToListAsync();
        }

        /// <summary>
        /// 案件追加・更新
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="anken">案件</param>
        /// <returns>案件IDまたは最新受注</returns>
        public int? CreateOrUpdateWithSpRenkeiAnken(int kubun, T_Renkei_Anken anken)
        {
            SP_ResultForInt result = DbContext.SP_T_Renkei_Anken
                .FromSqlRaw("EXECUTE [dbo].[SP_T_Renkei_Anken] @KUBUN = {0}, @ANKEN_NO = {1}, @ANKEN_STATUS={2}, @ANKEN_ORDER={3}, @ANKEN_ID={4}, @COMPANY_ID={5}, @BRANCH_ID={6}",
                    kubun, anken.Renkei_Anken_No, anken.Renkei_Anken_Status, anken.Renkei_Anken_Latest_Order, anken.Renkei_Anken_ID, anken.Company_ID, anken.Branch_ID)
                .AsEnumerable()
                .FirstOrDefault();
            return result.Result;
        }

        /// <summary>
        /// 案件番号を取得
        /// </summary>
        /// <param name="fromDateTime">開始日時</param>
        /// <param name="zeroUME">ゼロ埋め</param>
        /// <returns>案件番号</returns>
        public string GetSpRenkeiAnkenNo(DateTime fromDateTime, int zeroUME)
        {
            SP_ResultForString result = DbContext.SP_T_Renkei_Anken_No
                .FromSqlRaw("EXECUTE [dbo].[SP_T_Renkei_Anken_No] @FROM_DATETIME = {0}, @ZERO_UME = {1}",
                    fromDateTime, zeroUME)
                .AsEnumerable()
                .FirstOrDefault();
            return result.Result;
        }

        /// <summary>
        /// SP_T_Renkei_Ankenの実行
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="ankenNo">案件番号</param>
        /// <param name="ankenStatus">案件ステータス</param>
        /// <param name="ankenOrder">案件注文</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>結果</returns>
        public async Task<int> CreateWithSpRenkeiAnken(RenkeiAnkenKubun kubun, string ankenNo, int ankenStatus, int ankenOrder, int companyId, int branchId)
        {
            int result = await ExecuteScalarStoredProcedureAsync<int>(
                SystemConstants.StoreProceduresName.SP_T_Renkei_Anken,
                new SqlParameter("@KUBUN", kubun),
                new SqlParameter("@ANKEN_NO", ankenNo),
                new SqlParameter("@ANKEN_STATUS", ankenStatus),
                new SqlParameter("@ANKEN_ORDER", ankenOrder),
                new SqlParameter("@COMPANY_ID", companyId),
                new SqlParameter("@BRANCH_ID", branchId)
            );
            return result;
        }

        /// <summary>
        /// SP_T_Renkei_Anken_Noの実行
        /// </summary>
        /// <param name="shareDate">共有日</param>
        /// <param name="zeroUme">ゼロ埋め</param>
        /// <returns>案件番号</returns>
        public async Task<string> GetNoSPTRenkeiAnkenNo(DateTime shareDate, SystemEnums.ZeroUme zeroUme)
        {
            string result = await ExecuteScalarStoredProcedureAsync<string>(
               SystemConstants.StoreProceduresName.SP_T_Renkei_Anken_No,
                new SqlParameter("@FROM_DATETIME", shareDate.Date),
                new SqlParameter("@ZERO_UME", zeroUme)
            );
            return result;
        }

        /// <summary>
        /// 案件変更履歴情報を取得する
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <returns>案件変更履歴情報</returns>
        public async Task<JoinAnkenChangeHistoryDto> GetHistoryChangeAnkenInfo(int id)
        {
            //Renkei_Anken_IDでT_Renkei_Anken Infoを取得する
            T_Renkei_Anken builderRenkeiAnken =
                        await (from t1 in DbContext.Set<T_Renkei_Anken>()
                               where t1.Renkei_Anken_ID == id
                               select t1).FirstOrDefaultAsync();

            //List<T_Renkei_Anken_Detail> を取得する
            List<JoinRenkeiAnkenDetailDto> builderRenkeiAnkenDetails =
                        await (from t1 in DbContext.Set<T_Renkei_Anken>()
                               join t2 in DbContext.Set<T_Renkei_Anken_Detail>()
                                   on new { id = t1.Renkei_Anken_ID }
                                   equals new { id = t2.Renkei_Anken_ID }
                               join t3 in DbContext.Set<M_CompanyUser>()
                                   on new { id = t2.Insert_User }
                                   equals new { id = t3.User_ID }
                               where t1.Renkei_Anken_ID == id
                               orderby t2.Renkei_Anken_Order ascending
                               select new JoinRenkeiAnkenDetailDto { renkeiAnkenDetail = t2, companyUser = t3 })
                               .ToListAsync();

            //List<T_Renkei_Anken_Point> を取得する
            List<T_Renkei_Anken_Point> builderRenkeiAnkenPoints =
                        await (from t1 in DbContext.Set<T_Renkei_Anken>()
                               join t3 in DbContext.Set<T_Renkei_Anken_Point>()
                                   on new { id = t1.Renkei_Anken_ID }
                                   equals new { id = t3.Renkei_Anken_ID }
                               where t1.Renkei_Anken_ID == id
                               orderby t3.Renkei_Anken_Order ascending
                               select t3).ToListAsync();

            return new JoinAnkenChangeHistoryDto
            {
                renkeiAnken = builderRenkeiAnken,
                renkeiAnkenDetails = builderRenkeiAnkenDetails,
                renkeiAnkenPoints = builderRenkeiAnkenPoints
            };
        }

        /// <summary>
        /// 案件リストの取得
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>案件リスト</returns>
        public async Task<IEnumerable<JoinAnkenCsv>> GetRenkeiAnkensAsync(int companyId, int branchId)
        {
            List<JoinAnkenCsv> data = await (from TRA in DbContext.Set<T_Renkei_Anken>()

                              join TRAP in DbContext.Set<T_Renkei_Anken_Point>()
                                  on new { id = TRA.Renkei_Anken_ID, order = TRA.Renkei_Anken_Latest_Order } equals new { id = TRAP.Renkei_Anken_ID, order = TRAP.Renkei_Anken_Order }
                                  into TRAPs
                              from TRAP in TRAPs.DefaultIfEmpty()

                              join TRAD in DbContext.Set<T_Renkei_Anken_Detail>()
                                  on new { id = TRA.Renkei_Anken_ID, order = TRA.Renkei_Anken_Latest_Order } equals new { id = TRAD.Renkei_Anken_ID, order = TRAD.Renkei_Anken_Order }
                                  into TRADs
                              from TRAD in TRADs.DefaultIfEmpty()

                              join TRAS in DbContext.Set<T_Renkei_Anken_Secure>()
                                  on TRA.Renkei_Anken_ID equals TRAS.Renkei_Anken_ID
                                  into TRASs
                              from TRAS in TRASs.DefaultIfEmpty()

                              where TRA.Company_ID.Equals(companyId)
                                  && TRA.Branch_ID.Equals(branchId)

                              orderby TRA.Renkei_Anken_No ascending

                              select new JoinAnkenCsv
                              {
                                  renkeiAnken = TRA,
                                  renkeiAnkenDetail = TRAD,
                                  renkeiAnkenPoint = TRAP,
                                  renkeiAnkenSecure = TRAS,
                              }
                        ).ToListAsync();

            return data;
        }

        /// <summary>
        /// 受注案件の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <returns>受注案件のリスト</returns>
        public async Task<IEnumerable<JoinCompanyPortalDto>> GetProjectOrdersAsync(int companyId, int branchId)
        {
            IQueryable<JoinCompanyPortalDto> builder = from tra in DbContext.Set<T_Renkei_Anken>()
                          join trad in DbContext.Set<T_Renkei_Anken_Detail>()
                              on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                              equals new { id = trad.Renkei_Anken_ID, order = trad.Renkei_Anken_Order }

                          join trapS in DbContext.Set<T_Renkei_Anken_Point>()
                              on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                              equals new { id = trapS.Renkei_Anken_ID, order = trapS.Renkei_Anken_Order }
                              into renkeiAnkenPointS
                          from trapSNullable in renkeiAnkenPointS.DefaultIfEmpty()
                          where trapSNullable == null || (trapSNullable != null && trapSNullable.SEKubun == "S")

                          // T_Renkei_Anken_PointをSEKubun != "S"で結合
                          join trapE in DbContext.Set<T_Renkei_Anken_Point>()
                              on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                              equals new { id = trapE.Renkei_Anken_ID, order = trapE.Renkei_Anken_Order }
                              into renkeiAnkenPointE
                          from trapENullable in renkeiAnkenPointE.DefaultIfEmpty()
                          where trapENullable == null || (trapENullable != null && trapENullable.SEKubun == "E")

                          where
                              tra.Company_ID == companyId
                              && tra.Branch_ID == branchId
                              && tra.Renkei_Anken_Kubun == 1

                          orderby tra.Renkei_Anken_No ascending
                          select new JoinCompanyPortalDto
                          {
                              renkeiAnken = tra,
                              renkeiAnkenDetail = trad,
                              renkeiAnkenPointE = trapENullable,
                              renkeiAnkenPointS = trapSNullable,
                          };
            return await builder.ToListAsync();
        }

        /// <summary>
        /// 案件秘密情報データ取得
        /// </summary>
        /// <param name="renkeiAnkenIDs">案件IDリスト</param>
        /// <returns>案件秘密情報のリスト</returns>
        public async Task<IEnumerable<AnkenSecureDto>> GetSecuresAsync(int[] renkeiAnkenIDs)
        {
            IQueryable<AnkenSecureDto> builder = from tras in DbContext.Set<T_Renkei_Anken_Secure>()
                          where
                              renkeiAnkenIDs.Contains(tras.Renkei_Anken_ID)
                              && tras.Del_Datetime == null
                          select new AnkenSecureDto
                          {
                              renkeiAnkenSecure = tras
                          };
            List<AnkenSecureDto> result = await builder.ToListAsync();
            return result;
        }

        /// <summary>
        /// IDでRenkei_Ankenを取得
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <returns>案件</returns>
        public async Task<T_Renkei_Anken> GetRenkeiAnkenById(int id)
        {
            return await DbContext.T_Renkei_Ankens.FirstOrDefaultAsync(anken => anken.Renkei_Anken_ID == id);
        }
    }
}
