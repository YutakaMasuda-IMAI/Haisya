using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Common;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.InfoDto;
using SeikyuWeb.Dto.PortalDto;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using SeikyuWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeikyuWeb.Services
{
    /// <summary>
    /// ポータル情報サービス
    /// </summary>
    public class PortalInfoService : IPortalInfoService
    {
        private readonly IPortalInfoRepository _repository;
        private readonly ICheckSeikyuRepository _checkSeikyuRepository;
        private readonly ICheckShitabaraiRepository _checkShitabaraiRepository;
        private readonly IPrintSeikyuRepository _printSeikyuRepository;
        private readonly IPrintParameterRepository _printParameterRepository;
        private readonly ISeikyuRepository _seikyuRepository;
        private readonly IPrintShitabaraiRepository _printShitabaraiRepository;
        private readonly ICustomerBranchRepository _customerBranchRepository;
        public PortalInfoService(IPortalInfoRepository repository,
            ICheckSeikyuRepository checkSeikyuRepository,
            ICheckShitabaraiRepository checkShitabaraiRepository,
            IPrintSeikyuRepository printSeikyuRepository,
            IPrintParameterRepository printParameterRepository,
            ISeikyuRepository seikyuRepository,
            IPrintShitabaraiRepository printShitabaraiRepository,
            ICustomerBranchRepository customerBranchRepository
            )
        {
            _repository = repository;
            _checkSeikyuRepository = checkSeikyuRepository;
            _checkShitabaraiRepository = checkShitabaraiRepository;
            _printSeikyuRepository = printSeikyuRepository;
            _printParameterRepository = printParameterRepository;
            _seikyuRepository = seikyuRepository;
            _printShitabaraiRepository = printShitabaraiRepository;
            _customerBranchRepository = customerBranchRepository;
        }

        /// <summary>
        /// ログイン者情報に該当するお知らせ情報を取得する
        /// </summary>
        /// <param name="id">id （業者）/（業者以外）</param>
        /// <param name="isCompany">True: （業者）/False:（業者以外）</param>
        /// <returns>お知らせ情報のリスト</returns>
        /// <exception cref="System.NotImplementedException">未実装の例外</exception>
        public async Task<IEnumerable<PortalInfoDto>> GetNotification(int id, bool isCompany)
        {
            IEnumerable<TPortalInfo> portalInfos = isCompany ? await _repository.GetNotificationContractor(id) : await _repository.GetNotificationWithoutContractor(id);
            IEnumerable<int> portalInfoIds = portalInfos.Select(pi => pi.PrintId);

            IEnumerable<TPrintParameter> printParameters = await _printParameterRepository.GetByCodeKubunsAsync();

            IEnumerable<TPrintParameter> printParameterSeikyu = FilterPrintParameters(printParameters, portalInfoIds, SystemConstants.CodeData.請求書);
            IEnumerable<TPrintParameter> printParameterCheckSeikyu = FilterPrintParameters(printParameters, portalInfoIds, SystemConstants.CodeData.請求問合せ);
            IEnumerable<TPrintParameter> printParameterCheckShitabarais = FilterPrintParameters(printParameters, portalInfoIds, SystemConstants.CodeData.支払問合せ);

            //T_Check_Seikyu
            IEnumerable<TCheckSeikyu> checkSeikyus = await _checkSeikyuRepository.GetTCheckSeikyuAsync();
            //T_Seikyu
            IEnumerable<TSeikyu> seikyus = await _seikyuRepository.GetTSeikyuAsync();
            //T_Check_Shitabarai
            IEnumerable<TCheckShitabarai> checkShitabarais = await _checkShitabaraiRepository.GetTCheckSeikyuAsync();

            //inner join
            IEnumerable<dynamic> printParameterCheckSeikyuData = JoinPrintParametersWithCheckSeikyus(printParameterCheckSeikyu, checkSeikyus);

            //inner join
            IEnumerable<dynamic> printParameterSeikyuData = JoinPrintParametersWithSeikyus(printParameterSeikyu, seikyus);

            //Left join
            IEnumerable<dynamic> printParameterCheckShitabaraisData = JoinPrintParametersWithCheckShitabarais(printParameterCheckShitabarais, checkShitabarais);

            List<PortalInfoDto> result = new List<PortalInfoDto>();
            result.AddRange(CreatePortalInfoDtos(portalInfos, printParameterCheckSeikyuData, (item) => item.CheckSeikyu.CheckSeikyuId));
            result.AddRange(CreatePortalInfoDtos(portalInfos, printParameterSeikyuData, (item) => item.Seikyu.SeikyuId));
            result.AddRange(CreatePortalInfoDtos(portalInfos, printParameterCheckShitabaraisData, (item) => item.CheckShitabarai?.CheckShitabaraiId ?? null));

            return result;
        }

        /// <summary>
        /// 指定されたポータル情報と印刷パラメータデータに基づいてPortalInfoDtoオブジェクトのリストを作成する
        /// </summary>
        /// <param name="portalInfos">TPortalInfoオブジェクトのリスト</param>
        /// <param name="printParameterData">印刷パラメータデータを含む動的オブジェクトのリスト</param>
        /// <param name="getDataId">動的オブジェクトからデータIDを取得する関数</param>
        /// <returns>PortalInfoDtoオブジェクトのリスト</returns>
        private static IEnumerable<PortalInfoDto> CreatePortalInfoDtos(IEnumerable<TPortalInfo> portalInfos,
            IEnumerable<dynamic> printParameterData,
            Func<dynamic, int?> getDataId)
        {
            return from pi in portalInfos
                   join ppd in printParameterData on pi.PrintId equals ppd.PrintParameter.PrintId
                   select new PortalInfoDto
                   {
                       title = pi.Title,
                       detail = pi.Detail,
                       dateLimit = pi.LimitDate,
                       dataId = getDataId != null ? getDataId(ppd) : null,
                       printKubun = ppd.PrintParameter.PrintKubun,
                       action = pi.Action,
                       controller = pi.Controller,
                       criticalKubun = pi.CriticalKubun,
                       id = pi.PortalInfoId,
                   };
        }

        /// <summary>
        /// Joins the print parameters with the seikyus.
        /// </summary>
        /// <param name="printParameters">The list of print parameters.</param>
        /// <param name="seikyus">The list of seikyus.</param>
        /// <returns>The joined data containing the print parameter and seikyu.</returns>
        private static IEnumerable<dynamic> JoinPrintParametersWithSeikyus(IEnumerable<TPrintParameter> printParameters, IEnumerable<TSeikyu> seikyus)
        {
            return from pp in printParameters
                   join s in seikyus on pp.DataId equals s.SeikyuId
                   where pp.PrintKubun == SystemConstants.CodeData.請求書
                   select new { PrintParameter = pp, Seikyu = s };
        }

        /// <summary>
        /// Joins the print parameters with check shitabarais.
        /// </summary>
        /// <param name="printParameters">The print parameters.</param>
        /// <param name="checkShitabarais">The check shitabarais.</param>
        /// <returns>The joined print parameters with check shitabarais.</returns>
        private static IEnumerable<dynamic> JoinPrintParametersWithCheckShitabarais(IEnumerable<TPrintParameter> printParameters, IEnumerable<TCheckShitabarai> checkShitabarais)
        {
            return from pp in printParameters
                   join cs in checkShitabarais on pp.DataId equals cs.CheckShitabaraiId into csGroup
                   from cs in csGroup.DefaultIfEmpty()
                   where pp.PrintKubun == SystemConstants.CodeData.支払問合せ
                   select new { PrintParameter = pp, CheckShitabarai = cs };
        }

        /// <summary>
        /// Joins the print parameters with check seikyus.
        /// </summary>
        /// <param name="printParameters">The print parameters.</param>
        /// <param name="checkSeikyus">The check seikyus.</param>
        /// <returns>The joined print parameters with check seikyus.</returns>
        private static IEnumerable<dynamic> JoinPrintParametersWithCheckSeikyus(IEnumerable<TPrintParameter> printParameters, IEnumerable<TCheckSeikyu> checkSeikyus)
        {
            return from pp in printParameters
                   join cs in checkSeikyus
                      on pp.DataId equals cs.CheckSeikyuId
                   where pp.PrintKubun == SystemConstants.CodeData.請求問合せ
                   select new { PrintParameter = pp, CheckSeikyu = cs };
        }

        /// <summary>
        /// Filters the print parameters based on the given portal info IDs and print kubun.
        /// </summary>
        /// <param name="printParameters">The collection of print parameters.</param>
        /// <param name="portalInfoIds">The collection of portal info IDs.</param>
        /// <param name="printKubun">The print kubun.</param>
        /// <returns>The filtered print parameters.</returns>
        private static IEnumerable<TPrintParameter> FilterPrintParameters(IEnumerable<TPrintParameter> printParameters, IEnumerable<int> portalInfoIds, int printKubun)
        {
            return printParameters.Where(x => portalInfoIds.Contains(x.PrintId) && x.PrintKubun.Equals(printKubun));
        }

        #region Postals
        /// <summary>
        /// ポータルの取得
        /// </summary>
        /// <param name="id">id (company or branch id)</param>
        /// <param name="isCompany">True: company / False: branch</param>
        /// <returns>PortalDtoリスト</returns>
        public async Task<IEnumerable<PortalDto>> GetPortals(int id, bool isCompany)
            => isCompany ? await GetPortalsByCompany(id) : await GetPortalsByBranch(id);

        /// <summary>
        /// 表示フラグの更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task UpdateDisplayFlag(List<int> ids)
        {
            try
            {
                await _repository.BeginTransactionAsync();
                IEnumerable<TPortalInfo> dataToUpdate = await _repository.GetAllByIdsAsync(ids);

                List<TPortalInfo> updatedData = dataToUpdate
                    .Where(item => item.DisplayFlg != SystemConstants.DisplayFlag.YES)
                    .Select(item =>
                    {
                        item.DisplayFlg = SystemConstants.DisplayFlag.YES;
                        item.UpdateDatetime = DateTime.Now;
                        return item;
                    })
                    .ToList();

                await _repository.UpdateListAsync(updatedData);

                await _repository.EndTransactionAsync();
            }
            catch
            {
                await _repository.RollbackTransactionAsync();
                throw;
            }
        }

        /// <summary>
        /// idでポータル情報の表示フラグを更新
        /// </summary>
        /// <param name="id">ポータル情報id</param>
        /// <param name="displayFlg">リクエストからの値true/false</param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdateDisplayFlagById(int id, bool? displayFlg)
        {
            try
            {
                await _repository.BeginTransactionAsync();
                TPortalInfo dataToUpdate = await _repository.GetNotificationById(id);

                if (dataToUpdate == null)
                {
                    return new ApiResponse { code = StatusCodes.Status404NotFound, message = SystemConstants.Message.DataNotFound };
                }

                if (displayFlg == true)
                {
                    dataToUpdate.DisplayFlg = SystemConstants.DisplayFlag.YES;
                    dataToUpdate.UpdateDatetime = DateTime.Now;
                    await _repository.UpdateAsync(dataToUpdate);
                }

                await _repository.EndTransactionAsync();
                return new ApiResponse() { code = StatusCodes.Status200OK };
            }
            catch (Exception)
            {
                await _repository.RollbackTransactionAsync();
                return new ApiResponse() { code = StatusCodes.Status500InternalServerError, message = SystemConstants.Message.InternalServerError };
            }
        }

        /// <summary>
        /// ポータルの取得（業者）
        /// </summary>
        /// <param name="id">company id</param>
        /// <returns>PortalDtoリスト</returns>
        private async Task<List<PortalDto>> GetPortalsByCompany(int id)
        {
            IEnumerable<TPrintParameter> printParameters = await _printParameterRepository.GetByCodeKubunsAsync();

            IEnumerable<TCheckSeikyu> checkSeikyus = await _checkSeikyuRepository.GetByCompanyIdAsync(id);

            IEnumerable<TCheckShitabarai> checkShitabarais = await _checkShitabaraiRepository.GetByCompanyIdAsync(id);

            IEnumerable<TSeikyu> sekyus = await _seikyuRepository.GetByCompanyAsync(id);

            IEnumerable<TPrintSeikyu> printSeikyus = await _printSeikyuRepository.GetAllAsync();

            List<PortalDto> result = new();
            List<(int, int)> itemContain = new List<(int, int)>();
            foreach (var item in printParameters)
            {
                PortalDto portalDto = null;
                // 重複チェック
                if (itemContain.Any(x => x.Item1 == item.DataId && x.Item2 == item.PrintKubun))
                {
                    continue;
                }
                itemContain.Add((item.DataId, item.PrintKubun));
                switch (item.PrintKubun)
                {
                    case SystemConstants.CodeData.請求問合せ:
                        portalDto = await CreatePortalDtoForPrintKubun10(item, checkSeikyus);
                        break;
                    case SystemConstants.CodeData.支払問合せ:
                        portalDto = await CreatePortalDtoForPrintKubun11(item, checkShitabarais);
                        break;
                    case SystemConstants.CodeData.請求書:
                        portalDto = await CreatePortalDtoForPrintKubun12(item, sekyus, printSeikyus);
                        break;
                }

                if (portalDto != null)
                {
                    result.Add(portalDto);
                }
            }

            return result.OrderBy(r => r.month).ToList();
        }

        /// <summary>
        /// ポータルの取得（荷主）
        /// </summary>
        /// <param name="customerBranchId">branch id</param>
        /// <returns>PortalDtoリスト</returns>
        private async Task<List<PortalDto>> GetPortalsByBranch(int customerBranchId)
        {
            IEnumerable<dynamic> printParameters = await _printParameterRepository.GetByCodeKubunsAsync();

            IEnumerable<TCheckSeikyu> checkSeikyus = await _checkSeikyuRepository.GetByCustomerBranchIdAsync(customerBranchId);

            IEnumerable<TCheckShitabarai> checkShitabarais = await _checkShitabaraiRepository.GetByBranchIdAsync(customerBranchId);

            IEnumerable<TSeikyu> sekyus = await _seikyuRepository.GetByBranchAsync(customerBranchId);

            IEnumerable<TPrintSeikyu> printSeikyus = await _printSeikyuRepository.GetAllAsync();

            List<PortalDto> result = new();
            List<(int, int)> itemContain = new List<(int, int)>();
            foreach (var item in printParameters)
            {
                PortalDto portalDto = null;
                // 重複チェック
                if (itemContain.Any(x => x.Item1 == item.DataId && x.Item2 == item.PrintKubun))
                {
                    continue;
                }
                itemContain.Add((item.DataId, item.PrintKubun));
                switch (item.PrintKubun)
                {
                    case SystemConstants.CodeData.請求問合せ:
                        portalDto = await CreatePortalDtoForPrintKubun10(item, checkSeikyus);
                        break;
                    case SystemConstants.CodeData.支払問合せ:
                        portalDto = await CreatePortalDtoForPrintKubun11(item, checkShitabarais);
                        break;
                    case SystemConstants.CodeData.請求書:
                        portalDto = await CreatePortalDtoForPrintKubun12(item, sekyus, printSeikyus);
                        break;
                }

                if (portalDto != null)
                {
                    result.Add(portalDto);
                }
            }
            return result.OrderBy(r => r.month).ToList();
        }

        /// <summary>
        /// ポータル情報（請求問合せ）の作成
        /// </summary>
        /// <param name="item"></param>
        /// <param name="checkSeikyus"></param>
        /// <returns></returns>
        private async Task<PortalDto> CreatePortalDtoForPrintKubun10(
            TPrintParameter item,
            IEnumerable<TCheckSeikyu> checkSeikyus)
        {
            TCheckSeikyu checkSeikyu = (from cs in checkSeikyus
                               where cs.CheckSeikyuId.Equals(item.DataId)
                               select cs).FirstOrDefault();
            if (checkSeikyu == null) return null;
            MCustomerBranch customerBranch = checkSeikyu != null ? await _customerBranchRepository.GetByIdAsync(checkSeikyu.CustomerBranchId) : null;

            return new PortalDto
            {
                printKubun = SetPrintKubun(item.PrintKubun),
                month = SetMonth(item.PrintKubun, checkSeikyu, null, null),
                date = SetDate(item.PrintKubun, checkSeikyu, null, null, null),
                totalAmount = SetTotalAmount(item.PrintKubun, checkSeikyu, null, null),
                staffName = SetStaffName(item.PrintKubun, checkSeikyu, null, null, customerBranch),
            };
        }

        /// <summary>
        /// ポータル情報（支払問合せ）の作成
        /// </summary>
        /// <param name="item"></param>
        /// <param name="checkShitabarais"></param>
        /// <returns></returns>
        private async Task<PortalDto> CreatePortalDtoForPrintKubun11(
            TPrintParameter item,
            IEnumerable<TCheckShitabarai> checkShitabarais)
        {
            TCheckShitabarai checkShitabarai = checkShitabarais.FirstOrDefault(cs => cs.CheckShitabaraiId == item.DataId);
            if (checkShitabarai == null) return null;
            MCustomerBranch customerBranch = checkShitabarai != null ? await _customerBranchRepository.GetByIdAsync(checkShitabarai.YosyaBranchId) : null;

            return new PortalDto
            {
                printKubun = SetPrintKubun(item.PrintKubun),
                month = SetMonth(item.PrintKubun, null, checkShitabarai, null),
                date = SetDate(item.PrintKubun, null, checkShitabarai, null, null),
                totalAmount = SetTotalAmount(item.PrintKubun, null, checkShitabarai, null),
                staffName = SetStaffName(item.PrintKubun, null, checkShitabarai, null, customerBranch),
            };
        }

        /// <summary>
        /// ポータル情報（請求書）の作成
        /// </summary>
        /// <param name="item"></param>
        /// <param name="checkSeikyus"></param>
        /// <param name="printSeikyus"></param>
        /// <returns></returns>
        private async Task<PortalDto> CreatePortalDtoForPrintKubun12(
            TPrintParameter item,
            IEnumerable<TSeikyu> sekyus,
            IEnumerable<TPrintSeikyu> printSeikyus)
        {
            dynamic printSeikyuAndSekyus = (from p in printSeikyus
                                        join s in sekyus on p.SeikyuId equals s.SeikyuId
                                        where p.SeikyuId.Equals(item.DataId)
                                        select new
                                        {
                                            printSeikyu = p,
                                            sekyus = s,
                                        }).FirstOrDefault();

            if (printSeikyuAndSekyus == null) return null;

            TPrintSeikyu printSeikyu = printSeikyuAndSekyus.printSeikyu;
            TSeikyu sekyu = printSeikyuAndSekyus.sekyus;
            MCustomerBranch customerBranch = printSeikyu != null ? await _customerBranchRepository.GetByIdAsync(printSeikyu.CustomerBranchId) : null;

            return new PortalDto
            {
                printKubun = SetPrintKubun(item.PrintKubun),
                month = SetMonth(item.PrintKubun, null, null, printSeikyu),
                date = SetDate(item.PrintKubun, null, null, null, sekyu),
                totalAmount = SetTotalAmount(item.PrintKubun, null, null, printSeikyu),
                staffName = SetStaffName(item.PrintKubun, null, null, printSeikyu, customerBranch),
            }; ;
        }
        #endregion Portals

        #region common function
        /// <summary>
        /// プリント区分の設定
        /// </summary>
        /// <param name="printKubun">The print kubun code</param>
        /// <returns>The print kubun value as a string</returns>
        private static string SetPrintKubun(int printKubun)
            => printKubun switch
            {
                SystemConstants.CodeData.請求問合せ => "10",
                SystemConstants.CodeData.支払問合せ => "11",
                SystemConstants.CodeData.請求書 => "12",
                SystemConstants.CodeData.免税請求書 => "13",
                _ => "",
            };

        /// <summary>
        /// 年月情報の設定
        /// </summary>
        /// <param name="printKubun"></param>
        /// <param name="cse"></param>
        /// <param name="csh"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        private static string SetMonth(int printKubun, TCheckSeikyu cse,
            TCheckShitabarai csh, TPrintSeikyu ps)
            => printKubun switch
            {
                SystemConstants.CodeData.請求問合せ => cse?.SeikyuMonth.ToString(SystemConstants.DateFormat.YEAR_MONTH),
                SystemConstants.CodeData.支払問合せ => csh?.ShiharaiMonth.ToString(SystemConstants.DateFormat.YEAR_MONTH),
                SystemConstants.CodeData.請求書 => ps?.SeikyuMonth.ToString(SystemConstants.DateFormat.YEAR_MONTH),
                _ => "",
            };

        /// <summary>
        /// 年月日情報の設定
        /// </summary>
        /// <param name="printKubun"></param>
        /// <param name="cse"></param>
        /// <param name="csh"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        private static string SetDate(int printKubun, TCheckSeikyu cse,
            TCheckShitabarai csh, TPrintSeikyu ps, TSeikyu ts = null)
        {
            switch (printKubun)
            {
                case SystemConstants.CodeData.請求問合せ:
                    return cse?.PrintDate.ToString(SystemConstants.DateFormat.DATE);
                case SystemConstants.CodeData.支払問合せ:
                    return csh?.PrintDate.ToString(SystemConstants.DateFormat.DATE);
                case SystemConstants.CodeData.請求書:
                    string dateStr = ts?.PrintDate.ToString();
                    DateTime? result;
                    _ = CommonHelper.TryParseDate(dateStr, out result, "Date", out _);
                    return result?.ToString(SystemConstants.DateFormat.DATE);
                default:
                    return "";
            }
        }

        /// <summary>
        /// 合計金額の設定
        /// </summary>
        /// <param name="printKubun"></param>
        /// <param name="cse"></param>
        /// <param name="csh"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        private static string SetTotalAmount(int printKubun, TCheckSeikyu cse,
            TCheckShitabarai csh, TPrintSeikyu ps)
            => printKubun switch
            {
                SystemConstants.CodeData.請求問合せ => "",
                SystemConstants.CodeData.支払問合せ => "",
                SystemConstants.CodeData.請求書 => ps?.SeikyuTotalAmount?.ToString(),
                SystemConstants.CodeData.免税請求書 => "",
                _ => "",
            };

        /// <summary>
        /// スタッフ名の設定
        /// </summary>
        /// <param name="printKubun"></param>
        /// <param name="cse"></param>
        /// <param name="csh"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        private static string SetStaffName(int printKubun, TCheckSeikyu cse,
            TCheckShitabarai csh, TPrintSeikyu ps, MCustomerBranch cb)
            => printKubun switch
            {
                SystemConstants.CodeData.請求問合せ => cb?.SeikyuCustomerId > 0 ? cb?.CustomerBranchNameAbbr : null,
                SystemConstants.CodeData.支払問合せ => cb?.ShiharaiTantouId > 0 ? cb?.CustomerBranchNameAbbr : null,
                SystemConstants.CodeData.請求書 => cb?.SeikyuCustomerId > 0 ? cb?.CustomerBranchNameAbbr : null,
                SystemConstants.CodeData.免税請求書 => cb?.ShiharaiTantouId > 0 ? cb?.CustomerBranchNameAbbr : null,
                _ => null,
            };
        #endregion common function
    }
}
