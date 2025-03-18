using DinkToPdf;
using Microsoft.AspNetCore.Http;
using SeikyuWeb.Common;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.CheckShiharaisDto;
using SeikyuWeb.Dto.ReportDto;
using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories;
using SeikyuWeb.Repositories.Interfaces;
using SeikyuWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SeikyuWeb.Common.SystemConstants;
using static SeikyuWeb.Common.SystemEnums;

namespace SeikyuWeb.Services
{
    /// <summary>
    /// 請求確認サービス
    /// </summary>
    public class CheckSeikyuService : ICheckSeikyuService
    {
        private readonly ICheckSeikyuRepository _checkSeikyuRepository;
        private readonly ICheckSeikyuDetailRepository _checkSeikyuDetailRepository;
        private readonly ICheckSeikyuDoneRepository _checkSeikyuDoneRepository;
        private readonly IPrintSeikyuRepository _printSeikyuRepository;
        private readonly IPrintSeikyuDetailRepository _printSeikyuDetailRepository;
        private readonly ICustomerBranchRepository _customerBranchRepository;
        private readonly ICompanyUserGroupRepository _companyUserGroupRepository;
        private readonly IUriageUnchinRepository _uriageUnchinRepository;
        private readonly ICheckSeikyuChangeRepository _checkSeikyuChangeRepository;
        private readonly ICompanyUserGroupUserRepository _companyUserGroupUserRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly ICustomerUriageCalcRepository _customerUriageCalcRepository;
        private readonly IPrintParameterRepository _printParameterRepository;
        private readonly IPortalInfoRepository _portalInfoRepository;
        private readonly IReportCommonRepository _reportCommonRepository;
        private readonly IUriageRepository _uriageRepository;
        private readonly IAnkenDetailRepository _ankenDetailRepository;
        private readonly INippouRepository _nippouRepository;
        private readonly IHaisyaRepository _haisyaRepository;
        private readonly ISyaryoManagementRepository _syaryoManagementRepository;
        private readonly ISyaryoRepository _syaryoRepository;
        public CheckSeikyuService(ICheckSeikyuRepository checkSeikyuRepository,
            ICheckSeikyuDetailRepository checkSeikyuDetailRepository,
            ICheckSeikyuDoneRepository checkSeikyuDoneRepository,
            IPrintSeikyuRepository printSeikyuRepository,
            IPrintSeikyuDetailRepository printSeikyuDetailRepository,
            ICustomerBranchRepository customerBranchRepository,
            ICompanyUserGroupRepository companyUserGroupRepository,
            IUriageUnchinRepository uriageUnchinRepository,
            ICheckSeikyuChangeRepository checkSeikyuChangeRepository,
            ICompanyUserRepository companyUserRepository,
            IUnitRepository unitRepository,
            ICustomerUriageCalcRepository customerUriageCalcRepository,
            ICompanyUserGroupUserRepository companyUserGroupUserRepository,
            IPrintParameterRepository printParameterRepository,
            IPortalInfoRepository portalInfoRepository,
            IReportCommonRepository reportCommonRepository,
            IUriageRepository uriageRepository,
            IAnkenDetailRepository ankenDetailRepository,
            INippouRepository nippouRepository,
            IHaisyaRepository haisyaRepository,
            ISyaryoManagementRepository syaryoManagementRepository,
            ISyaryoRepository syaryoRepository
            )
        {
            _checkSeikyuRepository = checkSeikyuRepository;
            _checkSeikyuDetailRepository = checkSeikyuDetailRepository;
            _checkSeikyuDoneRepository = checkSeikyuDoneRepository;
            _printSeikyuRepository = printSeikyuRepository;
            _printSeikyuDetailRepository = printSeikyuDetailRepository;
            _customerBranchRepository = customerBranchRepository;
            _companyUserGroupRepository = companyUserGroupRepository;
            _uriageUnchinRepository = uriageUnchinRepository;
            _checkSeikyuChangeRepository = checkSeikyuChangeRepository;
            _companyUserRepository = companyUserRepository;
            _unitRepository = unitRepository;
            _customerUriageCalcRepository = customerUriageCalcRepository;
            _companyUserGroupUserRepository = companyUserGroupUserRepository;
            _printParameterRepository = printParameterRepository;
            _portalInfoRepository = portalInfoRepository;
            _reportCommonRepository = reportCommonRepository;
            _uriageRepository = uriageRepository;
            _ankenDetailRepository = ankenDetailRepository;
            _nippouRepository = nippouRepository;
            _haisyaRepository = haisyaRepository;
            _syaryoManagementRepository = syaryoManagementRepository;
            _syaryoRepository = syaryoRepository;
        }

        /// <summary>
        /// 請求問合せ入力＆取得
        /// </summary>
        /// <param name="id">パラメータのcheck_Seikyu_id</param>
        /// <param name="idLogin">idLogin （companyId）/（tantouId）</param>
        /// <param name="isCompany">True: （companyId）/False:（tantouId）</param>
        /// <returns>請求問合せDTO</returns>
        public async Task<BillingInquiryDto> GetBillingInquiriesById(int id, int idLogin, bool isCompany)
        {
            TCheckSeikyu checkSeikyu = await _checkSeikyuRepository.GetCheckSeikyuByIdAsync(id);
            if (checkSeikyu == null) return null;

            IEnumerable<TCheckSeikyuDetail> checkSeikyuDetail = await GetCheckSeikyuDetailsAsync(checkSeikyu.CheckSeikyuId);
            List<int> checkSeikyuIds = checkSeikyuDetail.Select(x => x.CheckSeikyuId).ToList();
            List<int> uriageUnchinIds = checkSeikyuDetail.Select(x => x.UriageUnchinId).ToList();

            IEnumerable<TCheckSeikyuChange> checkSeikyuChanges = await _checkSeikyuChangeRepository.GetByIdsAsync(checkSeikyuIds, uriageUnchinIds);
            TCheckSeikyuDone checkSeikyuDone = await _checkSeikyuDoneRepository.GetByIdAsync(checkSeikyu.CheckSeikyuId);

            // T_Uriage_Unchin
            IEnumerable<TUriageUnchin> uriageUnchins = await _uriageUnchinRepository.GetByIdsAsync(uriageUnchinIds) ?? new List<TUriageUnchin>();
            List<int> uriageIds = uriageUnchins.Select(x => x.UriageId).ToList();

            // T_Uriage
            IEnumerable<TUriage> uriages = await _uriageRepository.GetByIdsAsync(uriageIds) ?? new List<TUriage>();

            // List AnkenIds
            List<int> ankenIds = uriages
                                .Where(x => x.AnkenId != 0 && x.NippouId != 0)
                                .Select(x => x.AnkenId)
                                .ToList();
            // List NippouId
            List<int> nippouIds = uriages.Select(x => x.NippouId).ToList();

            // T_Anken_Detail
            IEnumerable<TAnkenDetail> ankenDetails = await _ankenDetailRepository.GetByIdsAsync(ankenIds) ?? new List<TAnkenDetail>();

            // T_Nippou
            IEnumerable<TNippou> nippous = await _nippouRepository.GetByIdsAsync(nippouIds) ?? new List<TNippou>();

            // List AnkenDisplay_ID
            List<int> ankenDisplayIds = nippous.Select(x => x.AnkenDisplayId).ToList();

            // T_Haisya
            IEnumerable<THaisya> haisyas = await _haisyaRepository.GetByIdsAsync(ankenDisplayIds) ?? new List<THaisya>();

            // List SyaryoManagement_ID
            List<int> syaryoManagementIds = haisyas.Select(x => x.SyaryoManagementId).ToList();

            // M_SyaryoManagement
            IEnumerable<MSyaryoManagement> mSyaryoManagements = await _syaryoManagementRepository.GetByIdsAsync(syaryoManagementIds) ?? new List<MSyaryoManagement>();

            // List Syaryo_ID
            List<int> syaryoIds = mSyaryoManagements
                                .Where(x => x.SyaryoId.HasValue)
                                .Select(x => x.SyaryoId.Value)
                                .ToList();

            // M_Syaryo
            IEnumerable<MSyaryo> mSyaryos = await _syaryoRepository.GetByIdsAsync(syaryoIds);

            MCustomerBranch customerBranch = await _customerBranchRepository.GetByIdAsync(checkSeikyu.CustomerBranchId);

            MCompanyUserGroup companyGroupSeikyuTantou = null;
            MCompanyUserGroup companyGroupShiharaiTantou = null;
            MCustomerUriageCalc customerUriageCalc = null;
            if (customerBranch != null)
            {
                companyGroupSeikyuTantou = await GetCompanyUserGroupBySeikyuTantouIdAsync(customerBranch);
                companyGroupShiharaiTantou = await GetCompanyUserGroupByShiharaiTantouIdAsync(customerBranch);
                customerUriageCalc = await _customerUriageCalcRepository.GetByIdAsync(customerBranch.CustomerBranchId);
            }

            IEnumerable<MCompanyUserGroupUser> companyGroupUserSeikyuTantou = await GetCompanyUserGroupUserAsync(companyGroupSeikyuTantou);
            IEnumerable<MCompanyUser> companyUserSeikyuTantou = await GetCompanyUser(companyGroupUserSeikyuTantou);

            IEnumerable<MCompanyUserGroupUser> companyGroupUserShiharaiTantou = await GetCompanyUserGroupUserAsync(companyGroupShiharaiTantou);
            IEnumerable<MCompanyUser> companyUserShiharaiTantou = await GetCompanyUser(companyGroupUserShiharaiTantou);

            IEnumerable<MUnit> unit = await GetUnitAsync(checkSeikyuDetail);

            IEnumerable<TPrintParameter> printParameters = await _printParameterRepository.GetListByCheckSeikyuIdAsync(checkSeikyu.CheckSeikyuId);

            IEnumerable<TPortalInfo> portalInfos = await GetTPortalInfoByPrintParameterAsync(printParameters, idLogin, isCompany);
            List<int> listIdsPortalInfo = new();
            if (portalInfos.Any())
            {
                listIdsPortalInfo = portalInfos.Select(x => x.PortalInfoId).ToList();
            }

            BillingInquiryDto dto = new BillingInquiryDto
            {
                seikyuDetails = CreateSeikyuDetails(uriageUnchins, uriages, ankenDetails, nippous, haisyas, mSyaryoManagements, mSyaryos, checkSeikyuChanges, checkSeikyuDetail),
                checkSeikyu = CreateCheckSeikyuDto(checkSeikyu, checkSeikyuDetail,
                checkSeikyuChanges, checkSeikyuDone, customerBranch,
                customerUriageCalc, companyGroupSeikyuTantou,
                companyUserSeikyuTantou, companyGroupShiharaiTantou,
                companyUserShiharaiTantou),
                portalInfoIds = listIdsPortalInfo,
            };

            return dto;
        }

        /// <summary>
        /// ポータル情報の取得（業者はcompanyID、荷主はcustomerTantouIdより取得）
        /// </summary>
        /// <param name="printParameters">List ids of PrintParameter</param>
        /// <param name="idInt">idLogin （companyId）/（tantouId）</param>
        /// <param name="isCompany">True: （companyId）/False:（tantouId）</param>
        /// <returns></returns>
        private async Task<IEnumerable<TPortalInfo>> GetTPortalInfoByPrintParameterAsync(IEnumerable<TPrintParameter> printParameters, int idInt, bool isCompany)
        {
            List<int> printParameterIds = printParameters.Select(x => x.PrintId).ToList();

            if (isCompany)
            {
                return await _portalInfoRepository.GetTPortalInfoByPrintIdAndCompanyId(printParameterIds, idInt);
            }
            else
            {
                return await _portalInfoRepository.GetTPortalInfoByPrintIdAndCustomerTantouId(printParameterIds, idInt);
            }
        }

        /// <summary>
        /// 請求問合せ入力（サマリ）の作成
        /// </summary>
        /// <param name="printSeikyu"></param>
        /// <param name="checkSeikyuChanges"></param>
        /// <param name="printSeikyuDetails"></param>
        /// <returns></returns>
        private static SummaryDto CreateSummaryDto(IEnumerable<TPrintSeikyu> printSeikyu,
            IEnumerable<TCheckSeikyuChange> checkSeikyuChanges,
            IEnumerable<TPrintSeikyuDetail> printSeikyuDetails,
            MCustomerUriageCalc customerUriageCalc)
        {
            decimal seikyuUnchin = SumSeikyuUnchin(checkSeikyuChanges, printSeikyuDetails);
            decimal warimashi = SumWarimashi(printSeikyuDetails, customerUriageCalc);
            decimal tatekaekin = SumTatekaekin(checkSeikyuChanges, printSeikyuDetails);
            long tax = CommonHelper.CeilingDecimalToLong((seikyuUnchin + warimashi) * 10 / 100);
            long seikyuTotal = CommonHelper.RoundDecimalToLong(seikyuUnchin + warimashi + tatekaekin + tax);

            return new SummaryDto
            {
                seikyuPrevious = CommonHelper.RoundDecimalToLong(printSeikyu.Sum(s => s.SeikyuPrevious)),
                receivedAmountThis = CommonHelper.RoundDecimalToLong(printSeikyu.Sum(s => s.ReceivedAmountThis)),
                discount = 0, // Q&A OUT_IMAI-333
                balanceForward = CommonHelper.RoundDecimalToLong(printSeikyu.Sum(s => s.BalanceForward)),
                seikyuUnchin = CommonHelper.RoundDecimalToLong(seikyuUnchin),
                warimashi = CommonHelper.RoundDecimalToLong(warimashi),
                tax = tax,
                tatekaekin = CommonHelper.RoundDecimalToLong(tatekaekin),
                seikyuTotal = seikyuTotal
            };
        }

        /// <summary>
        /// 請求問合せ一覧の作成
        /// </summary>
        /// <param name="checkSeikyu"></param>
        /// <param name="details"></param>
        /// <param name="changes"></param>
        /// <param name="done"></param>
        /// <param name="branch"></param>
        /// <param name="uriageCalc"></param>
        /// <param name="companyGroupSeikyuTantou"></param>
        /// <param name="companyUserSeikyuTantou"></param>
        /// <param name="companyGroupShiharaiTantou"></param>
        /// <param name="companyUserShiharaiTantou"></param>
        /// <returns></returns>
        private static CheckSeikyuDto CreateCheckSeikyuDto(TCheckSeikyu checkSeikyu,
            IEnumerable<TCheckSeikyuDetail> details,
            IEnumerable<TCheckSeikyuChange> changes,
            TCheckSeikyuDone done,
            MCustomerBranch branch,
            MCustomerUriageCalc uriageCalc,
            MCompanyUserGroup companyGroupSeikyuTantou,
            IEnumerable<MCompanyUser> companyUserSeikyuTantou,
            MCompanyUserGroup companyGroupShiharaiTantou,
            IEnumerable<MCompanyUser> companyUserShiharaiTantou)
        {
            if (checkSeikyu == null)
            {
                return null;
            }
            decimal afterSeikyuUnchin = SetAfterSeikyuUnchin(changes, details);
            decimal afterTatekaekin = SetAfterTatekaekin(changes, details);
            return new()
            {
                id = checkSeikyu.CheckSeikyuId,
                checkStatus = checkSeikyu.CheckStatus,
                seikyuMonth = checkSeikyu.SeikyuMonth.ToString("yyyy年MM月"),
                shimeDay = checkSeikyu.ShimeDay,
                zeiKubun = checkSeikyu.ZeiKubun,
                customerBranch = SetCustomerBranch(branch, uriageCalc, companyGroupSeikyuTantou, companyUserSeikyuTantou, companyGroupShiharaiTantou, companyUserShiharaiTantou),
                done = SetCheckSeikyuDoneDto(done),
                changeCount = SetChangeCount(changes),
                ankenCount = SetAnkenCount(details),
                detailCount = SetDetailCount(details),
                beforeSeikyuUnchin = SetBeforeSeikyuUnchinSum(details),
                beforeTatekaekin = SetBeforeTatekaekinSum(details),
                afterSeikyuUnchin = CommonHelper.RoundDecimalToLong(afterSeikyuUnchin),
                afterTatekaekin = CommonHelper.RoundDecimalToLong(afterTatekaekin),
                status = SetStatusCheckSeikyu(checkSeikyu.CheckStatus),
                details = SetDetails(changes, details),
            };
        }

        /// <summary>
        /// ステータスの設定
        /// </summary>
        /// <param name="checkStatus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static string SetStatusCheckSeikyu(int checkStatus)
        {
            switch (checkStatus)
            {
                case 0:
                    return SystemConstants.StatusCheckSeikyu.Unconfirmed;
                case 1:
                    return SystemConstants.StatusCheckSeikyu.Checking;
                case 2:
                    return SystemConstants.StatusCheckSeikyu.Confirmed;
                default:
                    break;
            }
            return checkStatus.ToString();
        }

        /// <summary>
        /// 請求問合せ入力（詳細）の作成
        /// </summary>
        /// <param name="checkSeikyuDetail"></param>
        /// <param name="checkSeikyuChanges"></param>
        /// <param name="printSeikyus"></param>
        /// <param name="printSeikyuDetails"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        private static List<SeikyuDetailDto> CreateSeikyuDetails(
            IEnumerable<TUriageUnchin> uriageUnchins,
            IEnumerable<TUriage> uriages,
            IEnumerable<TAnkenDetail> ankenDetails,
            IEnumerable<TNippou> nippous,
            IEnumerable<THaisya> haisyas,
            IEnumerable<MSyaryoManagement> mSyaryoManagements,
            IEnumerable<MSyaryo> mSyaryos,
            IEnumerable<TCheckSeikyuChange> checkSeikyuChanges,
            IEnumerable<TCheckSeikyuDetail> checkSeikyuDetails)
        {
            List<SeikyuDetailDto> seikyuDetails = new List<SeikyuDetailDto>();

            foreach (var item in uriageUnchins)
            {
                CheckSeikyuDetailResDto checkSeikyuDetail = SetCheckSeikyuDetail(item, uriages, ankenDetails, nippous, haisyas, mSyaryoManagements, mSyaryos, checkSeikyuDetails);
                CheckSeikyuChangeDto checkSeikyuChange = SetCheckSeikyuChangeDetailDto(checkSeikyuChanges.FirstOrDefault(c => c.UriageUnchinId == item.UriageUnchinId));

                SeikyuDetailDto seikyuDetail = new SeikyuDetailDto()
                {
                    checkSeikyuDetail = checkSeikyuDetail,
                    checkSeikyuChange = checkSeikyuChange,
                };

                seikyuDetails.Add(seikyuDetail);
            }

            return seikyuDetails;
        }

        /// <summary>
        /// 請求問合せ一覧（詳細）の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static List<CheckSeikyuDetailDto> SetDetails(IEnumerable<TCheckSeikyuChange> changes, IEnumerable<TCheckSeikyuDetail> details)
        {
            return details.Select(detail => new CheckSeikyuDetailDto
            {
                checkSeikyuId = detail.CheckSeikyuId,
                uriageUnchinId = detail.UriageUnchinId,
                change = SetCheckSeikyuChangeDto(changes.FirstOrDefault(c => c.CheckSeikyuId == detail.CheckSeikyuId
                && c.UriageUnchinId == detail.UriageUnchinId))
            }).ToList();
        }

        /// <summary>
        /// 請求明細の設定
        /// </summary>
        /// <param name="model"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        private static PrintSeikyuDto SetPrintSeikyuDto(TPrintSeikyuDetail model, IEnumerable<MUnit> units)
        {
            if (model == null) return null;

            string unitDisplay = units.FirstOrDefault(a => a.UnitId == model.Unit)?.UnitDisplay;

            return new()
            {
                displayDate = model.DisplayDate?.ToString("yyyy/MM/dd") ?? string.Empty,
                syaban = model.Syaban,
                syasyuKataName = model.SyasyuKataName,
                tsumi = model.Tsumi,
                oroshi = model.Oroshi,
                workName = model.WorkName,
                luggage = model.Luggage,
                qty = model.Qty ?? 0,
                unit = unitDisplay,
                unitPrice = model.UnitPrice ?? 0,
                seikyuUnchin = model.SeikyuUnchin ?? 0,
                warimashi1 = model.Warimashi1 ?? 0,
                warimashi2 = model.Warimashi2 ?? 0,
                warimashi3 = model.Warimashi3 ?? 0,
                warimashi4 = model.Warimashi4 ?? 0,
                warimashi5 = model.Warimashi5 ?? 0,
                tatekaekin = model.Tatekaekin ?? 0,
                seikyuTotal = model.SeikyuTotal ?? 0,
                remaks = string.Join(" ", new[] { model.Remarks1, model.Remarks2 }.Where(s => !string.IsNullOrWhiteSpace(s))),
            };
        }

        private static CheckSeikyuDetailResDto SetCheckSeikyuDetail(
            TUriageUnchin tuu,
            IEnumerable<TUriage> uriages,
            IEnumerable<TAnkenDetail> ankenDetails,
            IEnumerable<TNippou> nippous,
            IEnumerable<THaisya> haisyas,
            IEnumerable<MSyaryoManagement> mSyaryoManagements,
            IEnumerable<MSyaryo> mSyaryos,
            IEnumerable<TCheckSeikyuDetail> checkSeikyuDetails
            )
        {
            if (tuu == null) return null;

            string displayDate = tuu.SeikyuDate.ToString("yyyy年MM月dd日", System.Globalization.CultureInfo.InvariantCulture);

            // Get T_Uriage
            TUriage tu = uriages.Where(x => x.UriageId == tuu.UriageId).FirstOrDefault();

            // Get T_Check_Seikyu_Detail
            TCheckSeikyuDetail csd = checkSeikyuDetails.Where(x => x.UriageUnchinId == tuu.UriageUnchinId).FirstOrDefault();

            string syabanNumber = null;
            string syasyuKataName = null;
            string workName = null;

            if (tu?.NippouId != 0)
            {
                TNippou nippou = nippous.Where(x => x.NippouId == tu.NippouId).FirstOrDefault();
                THaisya haisya = haisyas.Where(x => x.AnkenDisplayId == nippou.AnkenDisplayId).FirstOrDefault();

                MSyaryoManagement syaryoManagement = mSyaryoManagements.Where(x => x.SyaryoManagementId == haisya?.SyaryoManagementId).FirstOrDefault();
                MSyaryo syaryo = mSyaryos.Where(x => x.SyaryoId == syaryoManagement?.SyaryoId).FirstOrDefault();
                
                syabanNumber = syaryoManagement?.SyabanNumber;
                syasyuKataName = syaryo?.SyasyuDisplay;
            }

            if (tu?.AnkenId != 0)
            {
                TAnkenDetail ankenDetail = ankenDetails.Where(x => x.AnkenId == tu.AnkenId).FirstOrDefault();
                workName = ankenDetail?.WorkName;
            }

            return new CheckSeikyuDetailResDto() {
                displayDate = displayDate,
                syaban = syabanNumber,
                syasyuKataName = syasyuKataName,
                tsumi = tuu?.Tsumi,
                oroshi = tuu?.Oroshi,
                workName = workName,
                luggage = tuu?.Luggage,
                qty = csd.Qty ?? 0,
                unit = csd.Unit.ToString(),
                unitPrice = csd.UnitPrice ?? 0,
                seikyuUnchin = csd.SeikyuUnchin ?? 0,
                warimashi1 = csd.Warimashi1 ?? 0,
                warimashi2 = csd.Warimashi2 ?? 0,
                warimashi3 = csd.Warimashi3 ?? 0,
                warimashi4 = csd.Warimashi4 ?? 0,
                warimashi5 = csd.Warimashi5 ?? 0,
                tatekaekin = csd.Tatekaekin ?? 0,
                seikyuTotal = csd.SeikyuTotal ?? 0,
                remarks = string.Join(" ", new[] { tuu?.Remarks1, tuu?.Remarks2 }.Where(s => !string.IsNullOrWhiteSpace(s))),
            };
        }

        /// <summary>
        /// 変更内容入力の設定（請求問合せ入力）
        /// </summary>
        /// <param name="change"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        private static CheckSeikyuChangeDto SetCheckSeikyuChangeDetailDto(TCheckSeikyuChange change)
        {
            if (change == null) return null;
            return new CheckSeikyuChangeDto()
            {
                checkSeikyuId = change.CheckSeikyuId,
                uriageUnchinId = change.UriageUnchinId,
                qty = change.Qty,
                unit = change.Unit,
                unitPrice = change.UnitPrice,
                calcPrice = change.CalcPrice,
                seikyuUnchin = change.SeikyuUnchin,
                tatekaekin = change.Tatekaekin,
                warimashi1 = change.Warimashi1,
                warimashi2 = change.Warimashi2,
                warimashi3 = change.Warimashi3,
                warimashi4 = change.Warimashi4,
                warimashi5 = change.Warimashi5,
                seikyuTotal = change.SeikyuTotal,
            };
        }

        /// <summary>
        /// 変更内容入力の設定（請求問合せ一覧）
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        private static CheckSeikyuChangeDto SetCheckSeikyuChangeDto(TCheckSeikyuChange change)
        {
            if (change == null) return null;
            return new CheckSeikyuChangeDto()
            {
                checkSeikyuId = change.CheckSeikyuId,
                uriageUnchinId = change.UriageUnchinId,
                qty = change.Qty,
                unit = change.Unit,
                unitPrice = change.UnitPrice,
                calcPrice = change.CalcPrice,
                seikyuUnchin = change.SeikyuUnchin,
                tatekaekin = change.Tatekaekin,
                warimashi1 = change.Warimashi1,
                warimashi2 = change.Warimashi2,
                warimashi3 = change.Warimashi3,
                warimashi4 = change.Warimashi4,
                warimashi5 = change.Warimashi5,
                seikyuTotal = change.SeikyuTotal,
            };
        }

        /// <summary>
        /// 変更後立替計の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static decimal SetAfterTatekaekin(IEnumerable<TCheckSeikyuChange> changes, IEnumerable<TCheckSeikyuDetail> details)
        {
            decimal sum = 0M;
            if (changes != null && changes.Any())
            {
                decimal changesSum = changes?.Sum(c => c.Tatekaekin) ?? 0;
                decimal detailsSum = 0M;

                if (details != null && details.Any())
                {
                    HashSet<int> changeUriageUnchinIds = new HashSet<int>(changes?.Where(x => x.Tatekaekin != null).Select(c => c.UriageUnchinId) ?? Enumerable.Empty<int>());
                    detailsSum = details
                        .Where(detail => !changeUriageUnchinIds.Contains(detail.UriageUnchinId))
                        .Sum(c => c.Tatekaekin) ?? 0;
                }

                sum = changesSum + detailsSum;
            }
            else
            {
                if (details != null && details.Any())
                    sum = details.Sum(c => c.Tatekaekin) ?? 0;
            }
            return sum;
        }

        /// <summary>
        /// 変更後運賃計の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static decimal SetAfterSeikyuUnchin(IEnumerable<TCheckSeikyuChange> changes,
            IEnumerable<TCheckSeikyuDetail> details)
        {
            decimal sum = 0M;
            if (changes != null && changes.Any())
            {
                decimal changesSum = changes?.Sum(c => c.SeikyuUnchin) ?? 0;
                decimal detailsSum = 0M;

                if (details != null && details.Any())
                {
                    HashSet<int> changeUriageUnchinIds = new HashSet<int>(changes?.Where(x => x.SeikyuUnchin != null).Select(c => c.UriageUnchinId) ?? Enumerable.Empty<int>());
                    detailsSum = details
                        .Where(detail => !changeUriageUnchinIds.Contains(detail.UriageUnchinId))
                        .Sum(c => c.SeikyuUnchin) ?? 0;
                }

                sum = changesSum + detailsSum;
            }
            else
            {
                if (details != null && details.Any())
                    sum = details.Sum(c => c.SeikyuUnchin) ?? 0;
            }
            return sum;
        }

        /// <summary>
        /// 変更前運賃計の設定
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        private static long SetBeforeTatekaekinSum(IEnumerable<TCheckSeikyuDetail> details)
            => (details != null && details.Any()) ? CommonHelper.RoundDecimalToLong(details.Sum(c => c.Tatekaekin)) : 0;

        /// <summary>
        /// 変更前運賃計の設定
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        private static long SetBeforeSeikyuUnchinSum(IEnumerable<TCheckSeikyuDetail> details)
            => (details != null && details.Any()) ? CommonHelper.RoundDecimalToLong(details.Sum(c => c.SeikyuUnchin)) : 0;

        /// <summary>
        /// 明細件数の設定
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        private static int SetDetailCount(IEnumerable<TCheckSeikyuDetail> details) => details?.Count() ?? 0;

        /// <summary>
        /// 案件件数の設定
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        private static int SetAnkenCount(IEnumerable<TCheckSeikyuDetail> details) => details?.GroupBy(c => c.AnkenId).Count() ?? 0;

        /// <summary>
        /// 変更件数の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <returns></returns>
        private static int SetChangeCount(IEnumerable<TCheckSeikyuChange> changes)
        {
            int sum = 0;
            if (changes != null && changes.Any())
            {
                sum = changes.GroupBy(c => new { c.CheckSeikyuId, c.UriageUnchinId }).Count();
            }
            return sum;
        }

        /// <summary>
        /// 確認情報の設定
        /// </summary>
        /// <param name="done"></param>
        /// <returns></returns>
        private static CheckSeikyuDoneDto SetCheckSeikyuDoneDto(TCheckSeikyuDone done)
        {
            // Q&A OUT_IMAI-169
            //if (done == null)
            //{
            //    return null; 
            //}
            return new()
            {
                checkDatetime = done?.CheckDatetime != null ? done.CheckDatetime.ToString("yyyy/MM/dd") : null,
                checkUser = done?.CheckUser.ToString(),
                changeFlg = done?.ChangeFlg.Equals(SystemConstants.Flag.TRUE),
                checkReault = done?.CheckReault
            };
        }

        /// <summary>
        /// 請求先の設定
        /// </summary>
        /// <param name="customerBranch"></param>
        /// <param name="customerUriageCalc"></param>
        /// <param name="companyUserGroupUserBySeikyuTantouId"></param>
        /// <param name="companyUserBySeikyuTantouId"></param>
        /// <param name="companyUserGroupUserByShiharaiTantouId"></param>
        /// <param name="companyUserByShiharaiTantouId"></param>
        /// <returns></returns>
        private static CustomerBranchDto SetCustomerBranch(MCustomerBranch customerBranch,
            MCustomerUriageCalc customerUriageCalc,
            MCompanyUserGroup companyUserGroupUserBySeikyuTantouId,
            IEnumerable<MCompanyUser> companyUserBySeikyuTantouId,
            MCompanyUserGroup companyUserGroupUserByShiharaiTantouId,
            IEnumerable<MCompanyUser> companyUserByShiharaiTantouId)
            => new()
            {
                id = customerBranch != null ? customerBranch.CustomerBranchId : 0,
                uriageCalc = SetCustomerUriageCalcDto(customerUriageCalc),
                seikyuTantou = SetSeikyuTantouDto(customerBranch, companyUserGroupUserBySeikyuTantouId, companyUserBySeikyuTantouId),
                shiharaiTantou = SetShiharaiTantouDto(customerBranch, companyUserGroupUserByShiharaiTantouId, companyUserByShiharaiTantouId)
            };

        /// <summary>
        /// 請求先担当者の設定
        /// </summary>
        /// <param name="customerBranch"></param>
        /// <param name="companyUserGroup"></param>
        /// <param name="companyUser"></param>
        /// <returns></returns>
        private static SeikyuTantouDto SetSeikyuTantouDto(MCustomerBranch customerBranch, MCompanyUserGroup companyUserGroup,
            IEnumerable<MCompanyUser> companyUser)
            => companyUserGroup == null ? null : new()
            {
                id = customerBranch != null ? customerBranch.SeikyuTantouId : 0,
                groupName = companyUserGroup?.GroupName,
                displayName = companyUserGroup?.DisplayName,
                phone = companyUserGroup?.Phone,
                users = global::SeikyuWeb.Services.CheckSeikyuService.SetCompanyUser(companyUser),
            };

        /// <summary>
        /// 支払先担当者の設定
        /// </summary>
        /// <param name="customerBranch"></param>
        /// <param name="companyUserGroup"></param>
        /// <param name="companyUser"></param>
        /// <returns></returns>
        private static ShiharaiTantouDto SetShiharaiTantouDto(MCustomerBranch customerBranch, MCompanyUserGroup companyUserGroup,
            IEnumerable<MCompanyUser> companyUser)
            => companyUserGroup == null ? null : new()
            {
                id = customerBranch != null ? customerBranch.ShiharaiTantouId : 0,
                groupName = companyUserGroup?.GroupName,
                displayName = companyUserGroup?.DisplayName,
                phone = companyUserGroup?.Phone,
                users = global::SeikyuWeb.Services.CheckSeikyuService.SetCompanyUser(companyUser)
            };

        /// <summary>
        /// 業者ユーザの設定
        /// </summary>
        /// <param name="companyUsers"></param>
        /// <returns></returns>
        private static List<UserDto> SetCompanyUser(IEnumerable<MCompanyUser> companyUsers)
            => companyUsers == null || !companyUsers.Any() ? new() :
            companyUsers.Select(user => new UserDto
            {
                id = user.UserId,
                displayName = user.DisplayName
            }).ToList();

        /// <summary>
        /// M_Customer_Uriage_Calcの設定
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static CustomerUriageCalcDto SetCustomerUriageCalcDto(MCustomerUriageCalc model)
            => new()
            {
                id = model?.CustomerBranchId ?? 0,
                seikyuUnchinName = model?.SeikyuUnchinName,
                tatekaekinName = model?.TatekaekinName,
                warimashi1Name = model?.Warimashi1Name,
                warimashi2Name = model?.Warimashi2Name,
                warimashi3Name = model?.Warimashi3Name,
                warimashi4Name = model?.Warimashi4Name,
                warimashi5Name = model?.Warimashi5Name,
                seikyuTotalname = model?.SeikyuTotalName,
                warimashi1Visible = model?.Warimashi1Visible ?? false,
                warimashi2Visible = model?.Warimashi2Visible ?? false,
                warimashi3Visible = model?.Warimashi3Visible ?? false,
                warimashi4Visible = model?.Warimashi4Visible ?? false,
                warimashi5Visible = model?.Warimashi5Visible ?? false,
                warimashi1Calc = model?.Warimashi1Calc,
                warimashi2Calc = model?.Warimashi2Calc,
                warimashi3Calc = model?.Warimashi3Calc,
                warimashi4Calc = model?.Warimashi4Calc,
                warimashi5Calc = model?.Warimashi5Calc,
                seikyuTotalCalc = model?.SeikyuTotalCalc
            };

        /// <summary>
        /// 立替計の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static decimal SumTatekaekin(IEnumerable<TCheckSeikyuChange> changes,
            IEnumerable<TPrintSeikyuDetail> details)
        {
            decimal sum = 0M;
            if (changes != null && changes.Any())
            {
                decimal changesSum = changes?.Sum(c => c.Tatekaekin) ?? 0;
                decimal detailsSum = 0M;

                if (details != null && details.Any())
                {
                    HashSet<int> changeUriageUnchinIds = new HashSet<int>(changes?.Where(x => x.Tatekaekin != null).Select(c => c.UriageUnchinId) ?? Enumerable.Empty<int>());
                    detailsSum = details
                        .Where(detail => detail.UriageUnchinId.HasValue && !changeUriageUnchinIds.Contains(detail.UriageUnchinId.Value))
                        .Sum(c => c.Tatekaekin) ?? 0;
                }

                sum = changesSum + detailsSum;
            }
            else
            {
                if (details != null && details.Any())
                    sum = details.Sum(c => c.Tatekaekin ?? 0);
            }

            return sum;
        }

        /// <summary>
        /// 運賃計の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static decimal SumSeikyuUnchin(IEnumerable<TCheckSeikyuChange> changes,
            IEnumerable<TPrintSeikyuDetail> details)
        {
            decimal sum = 0M;
            if (changes != null && changes.Any())
            {
                decimal changesSum = changes?.Sum(c => c.SeikyuUnchin) ?? 0;
                decimal detailsSum = 0M;

                if (details != null && details.Any())
                {
                    HashSet<int> changeUriageUnchinIds = new HashSet<int>(changes?.Where(x => x.SeikyuUnchin != null).Select(c => c.UriageUnchinId) ?? Enumerable.Empty<int>());
                    detailsSum = details
                        .Where(detail => detail.UriageUnchinId.HasValue && !changeUriageUnchinIds.Contains(detail.UriageUnchinId.Value))
                        .Sum(c => c.SeikyuUnchin) ?? 0;
                }

                sum = changesSum + detailsSum;
            }
            else
            {
                if (details != null && details.Any())
                    sum = details.Sum(c => c.SeikyuUnchin ?? 0);
            }

            return sum;
        }

        /// <summary>
        /// 割増の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static decimal SumWarimashi(IEnumerable<TPrintSeikyuDetail> details, MCustomerUriageCalc mCustomerUriageCalc)
        {
            decimal sum = 0M;
            if (details != null && details.Any() && mCustomerUriageCalc != null)
            {
                foreach (var detail in details)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        bool? isVisible = (bool?)typeof(MCustomerUriageCalc).GetProperty($"Warimashi{i}Visible").GetValue(mCustomerUriageCalc);
                        if (isVisible == true)
                        {
                            sum += (decimal?)typeof(TPrintSeikyuDetail).GetProperty($"Warimashi{i}").GetValue(detail) ?? 0;
                        }
                    }
                }
            }
            return sum;
        }

        /// <summary>
        /// 単位の取得
        /// </summary>
        /// <param name="checkSeikyuDetails"></param>
        /// <returns></returns>
        private async Task<IEnumerable<MUnit>> GetUnitAsync(IEnumerable<TCheckSeikyuDetail> checkSeikyuDetails)
        {
            List<int> ids = new();
            if (checkSeikyuDetails != null && checkSeikyuDetails.Any())
            {
                ids.AddRange(checkSeikyuDetails.Where(x => x.Unit.HasValue).Select(x => x.Unit.Value));
            }
            if (ids.Count > 0)
            {
                return await _unitRepository.GetByIdsAsync(ids.Distinct().ToList());
            }
            return new List<MUnit>();
        }

        /// <summary>
        /// 請求問合せ入力（詳細）の取得
        /// </summary>
        /// <param name="checkSeikyuId"></param>
        /// <returns></returns>
        private async Task<IEnumerable<TCheckSeikyuDetail>> GetCheckSeikyuDetailsAsync(int checkSeikyuId)
            => await _checkSeikyuDetailRepository.GetByCheckSeikyuId(checkSeikyuId) ?? new List<TCheckSeikyuDetail>();

        /// <summary>
        /// 請求明細の取得
        /// </summary>
        /// <param name="checkSeikyuId">checkSeikyuId</param>
        /// <returns></returns>
        private async Task<IEnumerable<TPrintSeikyu>> GetPrintSeikyuAsync(int checkSeikyuId)
            => await _printSeikyuRepository.GetBycheckSeikyuIdAsync(checkSeikyuId);

        /// <summary>
        /// 請求明細の取得
        /// </summary>
        /// <param name="printSeikyu"></param>
        /// <returns></returns>
        private async Task<IEnumerable<TPrintSeikyuDetail>> GetPrintSeikyuDetailsAsync(IEnumerable<TPrintSeikyu> printSeikyu)
        {
            List<int> printSeikyuIds = printSeikyu.Select(x => x.PrintSeikyuId).ToList();
            return await _printSeikyuDetailRepository.GetTPrintSeikyuDetailByKubunAsync(printSeikyuIds, SystemConstants.DataKubun.案件明細);
        }

        /// <summary>
        /// 請求担当IDから業者ユーザグループの取得
        /// </summary>
        /// <param name="customerBranch"></param>
        /// <returns></returns>
        private async Task<MCompanyUserGroup> GetCompanyUserGroupBySeikyuTantouIdAsync(MCustomerBranch customerBranch)
        {
            return await _companyUserGroupRepository.GetByIdAsync(customerBranch.SeikyuTantouId);
        }

        /// <summary>
        /// 支払担当IDから業者ユーザグループの取得
        /// </summary>
        /// <param name="customerBranch"></param>
        /// <returns></returns>
        private async Task<MCompanyUserGroup> GetCompanyUserGroupByShiharaiTantouIdAsync(MCustomerBranch customerBranch)
        {
            return await _companyUserGroupRepository.GetByIdAsync(customerBranch.ShiharaiTantouId);
        }

        /// <summary>
        /// 業者ユーザグループユーザの取得
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<IEnumerable<MCompanyUserGroupUser>> GetCompanyUserGroupUserAsync(MCompanyUserGroup model)
        {
            if (model == null)
            {
                return Enumerable.Empty<MCompanyUserGroupUser>();
            }
            return await _companyUserGroupUserRepository.GetByGroupIdAsync(model.GroupId);
        }

        /// <summary>
        /// 業者ユーザの取得
        /// </summary>
        /// <param name="companyUserGroupUser"></param>
        /// <returns></returns>
        private async Task<IEnumerable<MCompanyUser>> GetCompanyUser(IEnumerable<MCompanyUserGroupUser> companyUserGroupUser)
        {
            List<int> userIds = companyUserGroupUser.Select(x => x.UserId).ToList();
            return await _companyUserRepository.GetByIdsAsync(userIds);
        }

        /// <summary>
        /// 請求問合せ入力 確認中または確認済み登録ボタン
        /// </summary>
        /// <param name="id">請求確認ID</param>
        /// <param name="tantouId">担当者ID</param>
        /// <param name="dto">リクエストDTO</param>
        /// <returns>APIレスポンス</returns>
        public async Task<ApiResponse> UpdateCheckSeikyu(int id, int tantouId, RequestUpdateCheckSeikyusDto dto)
        {
            TCheckSeikyu checkSeikyu = await _checkSeikyuRepository.GetByIdAsync(id);
            if (checkSeikyu == null)
            {
                return new ApiResponse { code = StatusCodes.Status404NotFound, message = SystemConstants.Message.DataNotFound };
            }
            IEnumerable<TCheckSeikyuDetail> checkSeikyuDetail = await GetCheckSeikyuDetailsAsync(checkSeikyu.CheckSeikyuId);
            List<int> checkSeikyuIds = checkSeikyuDetail.Select(x => x.CheckSeikyuId).ToList();
            List<int> uriageUnchinIds = checkSeikyuDetail.Select(x => x.UriageUnchinId).ToList();

            foreach (var item in dto.seikyuChanges)
            {
                if (checkSeikyuDetail.Any(x => ((x.CheckSeikyuId != item.checkSeikyuId && x.UriageUnchinId != item.uriageUnchinId) ||
                (x.CheckSeikyuId == item.checkSeikyuId && x.UriageUnchinId == item.uriageUnchinId))) && item.checkSeikyuId == id)
                {
                    continue;
                }
                return new ApiResponse { code = StatusCodes.Status400BadRequest, message = SystemConstants.Message.DataNotFound };
            }


            IEnumerable<TCheckSeikyuChange> checkSeikyuChanges = await GetCheckSeikyuChanges(checkSeikyuIds, uriageUnchinIds);

            TCheckSeikyuDone checkSeikyuDone = await _checkSeikyuDoneRepository.GetByIdAsync(id);

            try
            {
                await _checkSeikyuRepository.BeginTransactionAsync();
                int changeFlg = await HandleSeikyuChanges(dto.seikyuChanges, checkSeikyu,
                    checkSeikyuChanges, tantouId);
                await HandleCheckSeikyuDone(checkSeikyuDone, checkSeikyu, dto.checkStatus ?? 0, changeFlg, tantouId);
                await UpdateCheckSeikyuStatus(checkSeikyu, dto.checkStatus ?? 0, tantouId);

                await _checkSeikyuRepository.EndTransactionAsync();
            }
            catch
            {
                await _checkSeikyuRepository.RollbackTransactionAsync();
                return new ApiResponse { code = StatusCodes.Status500InternalServerError, message = SystemConstants.Message.InternalServerError };
            }

            return new ApiResponse { code = StatusCodes.Status200OK };
        }

        /// <summary>
        /// 請求問合せ入力の挿入・更新・削除処理
        /// </summary>
        /// <param name="seikyuChanges"></param>
        /// <param name="checkSeikyu"></param>
        /// <param name="printSeikyus"></param>
        /// <param name="printSeikyuDetails"></param>
        /// <param name="checkSeikyuChanges"></param>
        /// <param name="tantouId"></param>
        /// <returns></returns>
        private async Task<int> HandleSeikyuChanges(List<SeikyuChangeDto> seikyuChanges,
            TCheckSeikyu checkSeikyu,
            IEnumerable<TCheckSeikyuChange> checkSeikyuChanges,
            int tantouId)
        {
            int changeFlg = 0;

            (List<SeikyuChangeDto> insertList, List<SeikyuChangeDto> updateList, List<SeikyuChangeDto> deleteList) = CategorizeSeikyuChanges(seikyuChanges, checkSeikyuChanges);

            if (deleteList.Any())
            {
                await DeleteSeikyuChanges(deleteList, checkSeikyuChanges);
                changeFlg = 0;
            }

            if (insertList.Any())
            {
                await InsertSeikyuChanges(insertList, checkSeikyu, tantouId);
                changeFlg = 1;
            }

            if (updateList.Any())
            {
                await UpdateSeikyuChanges(updateList, checkSeikyuChanges, tantouId);
                changeFlg = 1;
            }

            return changeFlg;
        }

        /// <summary>
        /// 請求問合せ入力の挿入・更新・削除処理リストの取得
        /// </summary>
        /// <param name="seikyuChanges"></param>
        /// <param name="checkSeikyuChanges"></param>
        /// <returns></returns>
        private static (List<SeikyuChangeDto> insertList, List<SeikyuChangeDto> updateList,
            List<SeikyuChangeDto> deleteList) CategorizeSeikyuChanges(List<SeikyuChangeDto> seikyuChanges,
            IEnumerable<TCheckSeikyuChange> checkSeikyuChanges)
        {
            List<SeikyuChangeDto> insertList = new List<SeikyuChangeDto>();
            List<SeikyuChangeDto> updateList = new List<SeikyuChangeDto>();
            List<SeikyuChangeDto> deleteList = new List<SeikyuChangeDto>();

            if (seikyuChanges != null && seikyuChanges.Any())
            {
                foreach (var change in seikyuChanges)
                {
                    if (checkSeikyuChanges.Any(c => c.UriageUnchinId == change.uriageUnchinId
                    && c.CheckSeikyuId == change.checkSeikyuId))
                    {
                        if (change.delFlg.Equals(SystemConstants.DelFlag.YES))
                        {
                            deleteList.Add(change);
                        }
                        else
                        {
                            updateList.Add(change);
                        }
                    }
                    else
                    {
                        if (!change.delFlg.Equals(SystemConstants.DelFlag.YES))
                        {
                            insertList.Add(change);
                        }
                    }
                }
            }

            return (insertList, updateList, deleteList);
        }

        /// <summary>
        /// 請求問合せ入力の挿入処理
        /// </summary>
        /// <param name="insertList"></param>
        /// <param name="checkSeikyu"></param>
        /// <param name="printSeikyus"></param>
        /// <param name="printSeikyuDetails"></param>
        /// <param name="tantouId"></param>
        /// <returns></returns>
        private async Task InsertSeikyuChanges(List<SeikyuChangeDto> insertList, TCheckSeikyu checkSeikyu, int tantouId)
        {
            List<TCheckSeikyuChange> items = new List<TCheckSeikyuChange>();

            foreach (var change in insertList)
            {
                TCheckSeikyuChange checkSeikyuChange = new TCheckSeikyuChange
                {
                    CheckSeikyuId = checkSeikyu.CheckSeikyuId,
                    UriageUnchinId = change.uriageUnchinId ?? 0,
                    Qty = change.qty,
                    Unit = null,
                    UnitPrice = change.unitPrice,
                    CalcPrice = change == null ? null : (change.qty != null && change.unitPrice != null ? (decimal)change.qty * change.unitPrice : null),
                    SeikyuUnchin = change.seikyuUnchin,
                    Tatekaekin = change.tatekaekin,
                    Warimashi1 = null,
                    Warimashi2 = null,
                    Warimashi3 = null,
                    Warimashi4 = null,
                    Warimashi5 = null,
                    SeikyuTotal = change.seikyuTotal,
                    InsertDatetime = DateTime.Now,
                    InsertUser = tantouId,
                    UpdateDatetime = DateTime.Now,
                    UpdateUser = tantouId
                };
                items.Add(checkSeikyuChange);
            }

            await _checkSeikyuChangeRepository.CreatListAsync(items);
        }

        /// <summary>
        /// 請求問合せ入力の更新処理
        /// </summary>
        /// <param name="updateList"></param>
        /// <param name="checkSeikyu"></param>
        /// <param name="printSeikyus"></param>
        /// <param name="printSeikyuDetails"></param>
        /// <param name="checkSeikyuChanges"></param>
        /// <param name="tantouId"></param>
        /// <returns></returns>
        private async Task UpdateSeikyuChanges(List<SeikyuChangeDto> updateList, IEnumerable<TCheckSeikyuChange> checkSeikyuChanges, int tantouId)
        {
            List<TCheckSeikyuChange> items = new List<TCheckSeikyuChange>();

            foreach (var change in updateList)
            {
                TCheckSeikyuChange checkSeikyuChange = checkSeikyuChanges.FirstOrDefault(c => c.UriageUnchinId == change.uriageUnchinId
                && c.CheckSeikyuId == change.checkSeikyuId);
                if (checkSeikyuChange != null)
                {
                    checkSeikyuChange.Qty = change.qty;
                    checkSeikyuChange.Unit = null;
                    checkSeikyuChange.UnitPrice = change.unitPrice;
                    checkSeikyuChange.CalcPrice = change == null ? null : (change.qty != null && change.unitPrice != null ? (decimal)change.qty * change.unitPrice : null);
                    checkSeikyuChange.SeikyuUnchin = change.seikyuUnchin;
                    checkSeikyuChange.Tatekaekin = change.tatekaekin;
                    checkSeikyuChange.Warimashi1 = null;
                    checkSeikyuChange.Warimashi2 = null;
                    checkSeikyuChange.Warimashi3 = null;
                    checkSeikyuChange.Warimashi4 = null;
                    checkSeikyuChange.Warimashi5 = null;
                    checkSeikyuChange.SeikyuTotal = change.seikyuTotal;
                    checkSeikyuChange.UpdateDatetime = DateTime.Now;
                    checkSeikyuChange.UpdateUser = tantouId;
                    items.Add(checkSeikyuChange);
                }
            }

            await _checkSeikyuChangeRepository.UpdateListAsync(items);
        }

        /// <summary>
        /// 請求問合せ入力の削除処理
        /// </summary>
        /// <param name="deleteList"></param>
        /// <param name="checkSeikyuChanges"></param>
        /// <returns></returns>
        private async Task DeleteSeikyuChanges(List<SeikyuChangeDto> deleteList, IEnumerable<TCheckSeikyuChange> checkSeikyuChanges)
        {
            List<TCheckSeikyuChange> items = new List<TCheckSeikyuChange>();

            foreach (var change in deleteList)
            {
                TCheckSeikyuChange checkSeikyuChange = checkSeikyuChanges.FirstOrDefault(c => c.UriageUnchinId == change.uriageUnchinId
                && c.CheckSeikyuId == change.checkSeikyuId);
                if (checkSeikyuChange != null)
                {
                    items.Add(checkSeikyuChange);
                }
            }

            await _checkSeikyuChangeRepository.DeleteListAsync(items);
        }

        /// <summary>
        /// 確認情報の更新・登録処理
        /// </summary>
        /// <param name="checkSeikyuDone"></param>
        /// <param name="checkSeikyu"></param>
        /// <param name="checkStatus"></param>
        /// <param name="changeFlg"></param>
        /// <param name="tantouId"></param>
        /// <returns></returns>
        private async Task HandleCheckSeikyuDone(TCheckSeikyuDone checkSeikyuDone, TCheckSeikyu checkSeikyu,
            int checkStatus, int changeFlg, int tantouId)
        {
            TCheckSeikyuDone done = new TCheckSeikyuDone
            {
                CheckSeikyuId = checkSeikyu.CheckSeikyuId,
                CheckDatetime = DateTime.Now,
                CheckUser = tantouId,
                CheckReault = checkStatus.Equals(SystemConstants.CheckStatus.確認中) ? SystemConstants.CheckStatus.発行済 : SystemConstants.CheckStatus.確認中,
                ChangeFlg = changeFlg,
                UpdateDatetime = DateTime.Now,
                UpdateUser = tantouId,
                InsertDatetime = DateTime.Now,
                InsertUser = tantouId
            };

            if (checkSeikyuDone != null)
            {
                done.InsertUser = checkSeikyuDone.InsertUser;
                done.InsertDatetime = checkSeikyuDone.InsertDatetime;
                await _checkSeikyuDoneRepository.UpdateAsync(done);
            }
            else
            {
                await _checkSeikyuDoneRepository.CreateAsync(done);
            }
        }

        /// <summary>
        /// ステータスの更新処理
        /// </summary>
        /// <param name="checkSeikyu"></param>
        /// <param name="checkStatus"></param>
        /// <param name="tantouId"></param>
        /// <returns></returns>
        private async Task UpdateCheckSeikyuStatus(TCheckSeikyu checkSeikyu, int checkStatus, int tantouId)
        {
            checkSeikyu.CheckStatus = checkStatus.Equals(SystemConstants.CheckStatus.確認中) ? SystemConstants.CheckStatus.確認中 : SystemConstants.CheckStatus.確認済;
            checkSeikyu.UpdateDatetime = DateTime.Now;
            checkSeikyu.UpdateUser = tantouId;

            await _checkSeikyuRepository.UpdateAsync(checkSeikyu);
        }


        /// <summary>
        /// 請求明細の取得
        /// </summary>
        /// <param name="checkSeikyuId"></param>
        /// <param name="printSeikyus"></param>
        /// <param name="printSeikyuDetails"></param>
        /// <param name="change"></param>
        /// <returns></returns>
        private static async Task<TPrintSeikyuDetail> GetPrintSeikyuDetailByCheckSeikyuId(int checkSeikyuId,
            IEnumerable<TPrintSeikyu> printSeikyus,
            IEnumerable<TPrintSeikyuDetail> printSeikyuDetails,
            SeikyuChangeDto change)
        {
            IEnumerable<TPrintSeikyuDetail> filteredPrintSeikyuDetail = from ps in printSeikyus
                                            join psd in printSeikyuDetails
                                                on new
                                                {
                                                    ps.PrintSeikyuId,
                                                    DataKubun = SystemConstants.DataKubun.案件明細,
                                                }
                                                equals new
                                                {
                                                    psd.PrintSeikyuId,
                                                    psd.DataKubun
                                                }
                                            where
                                                ps.CheckSeikyuId == change.checkSeikyuId &&
                                                psd.UriageUnchinId == change.uriageUnchinId &&
                                                ps.DelDatetime == null
                                            select psd;
            return await Task.FromResult(filteredPrintSeikyuDetail.FirstOrDefault());
        }

        /// <summary>
        /// 変更内容入力の取得
        /// </summary>
        /// <param name="checkSeikyuId"></param>
        /// <returns></returns>
        private async Task<IEnumerable<TCheckSeikyuChange>> GetCheckSeikyuChanges(List<int> checkSeikyuIds, List<int> uriageUnchinIds)
        {
            return await _checkSeikyuChangeRepository.GetBySeikyuIdAndUnchinId(checkSeikyuIds, uriageUnchinIds)
                ?? new List<TCheckSeikyuChange>();
        }

        /// <summary>
        /// 案件一覧の出力データを取得します
        /// </summary>
        /// <param name="kubun">kubun （1：確認済）/（2：確認中, 未確認）</param>
        /// <param name="checkSeikyuId">checkseikyuのidです</param>
        /// <param name="companyId">会社のidです</param>
        /// <param name="customerId">顧客のidです</param>
        /// <param name="branchId">支店のidです</param>
        /// <returns>案件一覧の出力データです。</returns>
        public async Task<ReportCommonDto> GetDataExport(int kubun, int checkSeikyuId)
        {
            ReportCommonDto model = new (){};

            // 帳票検索データを取得します
            model.ReportSearch = await _reportCommonRepository.GetReportSearchAsync((int)ReportType.ANKEN_DATA_LIST);

            // プロシージャパラメータを取得します
            Dictionary<string, object> paramProc = new Dictionary<string, object>();

            paramProc.Add(ProcParamName.KUBUN, kubun);
            paramProc.Add(ProcParamName.CHECK_ID, checkSeikyuId);

            List<string> sortList = new();
            string sortType = "ASC";

            // 案件一覧のデータを取得します
            IEnumerable<object> dataModel = await _reportCommonRepository.GetReportCommonAsync(ProcName.AnkenList, ProcModelName.AnkenList, paramProc, sortList, sortType);
            
            foreach (var item in dataModel)
            {
                V_ReportCommon_Local reportItem = new V_ReportCommon_Local();

                // リフレクションを使用してプロパティとその値を取得します
                foreach (var property in item.GetType().GetProperties())
                {
                    object value = property.GetValue(item);
                    reportItem[property.Name] = value;
                }

                model.ReportCommonList.Add(reportItem); 
            }

            return model;
        }
    }
}
