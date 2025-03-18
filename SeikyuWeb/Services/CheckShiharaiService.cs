using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Common;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.CheckShiharaisDto;
using SeikyuWeb.Dto.Shitabarai;
using SeikyuWeb.Models;
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
    /// 支払確認サービス
    /// </summary>
    public class CheckShiharaiService : ICheckShiharaiService
    {
        private readonly ICheckShitabaraiRepository _repository;
        private readonly ICheckShitabaraiDetailRepository _checkShitabaraiDetailRepository;
        private readonly ICheckShitabaraiChangeRepository _checkShitabaraiChangeRepository;
        private readonly ICheckShitabaraiDonerepository _checkShitabaraiDoneRepository;
        private readonly IUriageShitabaraiRepository _uriageShitabaraiRepository;
        private readonly IPrintShitabaraiRepository _printShitabaraiRepository;
        private readonly IPrintShitabaraiDetailRepository _printShitabaraiDetailRepository;
        private readonly ICustomerTantouRepository _customerTantouRepository;
        private readonly ICustomerBranchRepository _customerBranchRepository;
        private readonly ICompanyUserGroupRepository _companyUserGroupRepository;
        private readonly ICompanyUserGroupUserRepository _companyUserGroupUserRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IPrintParameterRepository _printParameterRepository;
        private readonly IPortalInfoRepository _portalInfoRepository;
        private readonly ICustomerUriageCalcRepository _customerUriageCalcRepository;
        private readonly IUriageRepository _uriageRepository;
        private readonly IAnkenDetailRepository _ankenDetailRepository;
        private readonly IHaisyaRepository _haisyaRepository;
        private readonly INippouRepository _nippouRepository;
        private readonly ISyaryoManagementRepository _syaryoManagementRepository;
        private readonly ISyaryoRepository _syaryoRepository;
        public CheckShiharaiService(
            ICheckShitabaraiRepository repository,
            ICheckShitabaraiDetailRepository checkShitabaraiDetailRepository,
            ICheckShitabaraiChangeRepository checkShitabaraiChangeRepository,
            ICheckShitabaraiDonerepository checkShitabaraiDoneRepository,
            IUriageShitabaraiRepository uriageShitabaraiRepository,
            IPrintShitabaraiRepository printShitabaraiRepository,
            IPrintShitabaraiDetailRepository printShitabaraiDetailRepository,
            ICustomerTantouRepository customerTantouRepository,
            ICustomerBranchRepository customerBranchRepository,
            ICompanyUserGroupRepository companyUserGroupRepository,
            ICompanyUserGroupUserRepository companyUserGroupUserRepository,
            ICompanyUserRepository companyUserRepository,
            IUnitRepository unitRepository,
            IPrintParameterRepository printParameterRepository,
            IPortalInfoRepository portalInfoRepository,
            ICustomerUriageCalcRepository customerUriageCalcRepository,
            IUriageRepository uriageRepository,
            IAnkenDetailRepository ankenDetailRepository,
            IHaisyaRepository haisyaRepository,
            INippouRepository nippouRepository,
            ISyaryoManagementRepository syaryoManagementRepository,
            ISyaryoRepository syaryoRepository
            )
        {
            _repository = repository;
            _checkShitabaraiDetailRepository = checkShitabaraiDetailRepository;
            _checkShitabaraiChangeRepository = checkShitabaraiChangeRepository;
            _checkShitabaraiDoneRepository = checkShitabaraiDoneRepository;
            _uriageShitabaraiRepository = uriageShitabaraiRepository;
            _printShitabaraiRepository = printShitabaraiRepository;
            _printShitabaraiDetailRepository = printShitabaraiDetailRepository;
            _customerTantouRepository = customerTantouRepository;
            _customerBranchRepository = customerBranchRepository;
            _companyUserGroupRepository = companyUserGroupRepository;
            _companyUserGroupUserRepository = companyUserGroupUserRepository;
            _companyUserRepository = companyUserRepository;
            _unitRepository = unitRepository;
            _printParameterRepository = printParameterRepository;
            _portalInfoRepository = portalInfoRepository;
            _customerUriageCalcRepository = customerUriageCalcRepository;
            _uriageRepository = uriageRepository;
            _ankenDetailRepository = ankenDetailRepository;
            _haisyaRepository = haisyaRepository;
            _nippouRepository = nippouRepository;
            _syaryoManagementRepository = syaryoManagementRepository;
            _syaryoRepository = syaryoRepository;
        }

        /// <summary>
        /// 変更内容入力の更新
        /// </summary>
        /// <param name="checkShitabaraiId">支払確認ID</param>
        /// <param name="tantouId">担当者ID</param>
        /// <param name="dto">変更内容DTO</param>
        /// <param name="checkShitabaraiChange">支払確認変更</param>
        /// <param name="printDetail">印刷詳細</param>
        /// <returns>更新された支払確認変更</returns>
        async Task<TCheckShitabaraiChange> UpdateCheckShibaraiChange(int checkShitabaraiId, int tantouId, ShitabaraichangeDto dto, TCheckShitabaraiChange checkShitabaraiChange)
        {
            // 削除処理
            if (dto.delFlg == 1)
            {
                await _checkShitabaraiChangeRepository.DeleteAsync(checkShitabaraiChange);
                await _checkShitabaraiChangeRepository.SaveChangeAsync();

                return null;
            }
            TCheckShitabaraiChange dataChange = new TCheckShitabaraiChange()
            {
                CheckShitabaraiId = checkShitabaraiId,
                UriageShiharaiId = (int)dto.uriageShiharaiId,
                Qty = dto.qty,
                Unit = null,
                UnitPrice = dto.unitPrice,
                CalcPrice = dto == null ? null : (dto.qty != null && dto.unitPrice != null ? (decimal)dto.qty * dto.unitPrice : null),
                ShiharaiUnchin = dto.shiharaiUnchin,
                Tatekaekin = dto.tatekaekin,
                Warimashi1 = null,
                Warimashi2 = null,
                Warimashi3 = null,
                Warimashi4 = null,
                Warimashi5 = null,
                ShiharaiTotal = dto.shiharaiTotal,
                InsertDatetime = DateTime.Now,
                InsertUser = tantouId,
                UpdateDatetime = DateTime.Now,
                UpdateUser = tantouId,
            };

            if (checkShitabaraiChange == null)
            {
                // 挿入処理
                await _checkShitabaraiChangeRepository.CreateAsync(dataChange);
                return dataChange;
            }
            else
            {
                // 更新処理
                dataChange.CheckShitabaraiId = checkShitabaraiChange.CheckShitabaraiId;
                dataChange.UriageShiharaiId = checkShitabaraiChange.UriageShiharaiId;
                dataChange.InsertUser = checkShitabaraiChange.InsertUser;
                dataChange.InsertDatetime = checkShitabaraiChange.InsertDatetime;

                await _checkShitabaraiChangeRepository.UpdateAsync(dataChange);
                return dataChange;
            }
        }

        /// <summary>
        /// 支払問合せ入力 確認中または確認済み登録ボタン
        /// </summary>
        /// <param name="id">支払確認ID</param>
        /// <param name="tantouId">担当者ID</param>
        /// <param name="dto">リクエストDTO</param>
        /// <returns>APIレスポンス</returns>
        public async Task<ApiResponse> UpdateCheckShiharai(int id, int tantouId, RequestUpdateCheckShiharaisDto dto)
        {
            TCheckShitabarai checkShibarai = await _repository.GetDetailCheckShibaraiById(id);
            TCheckShitabaraiDone checkShibaraiDone = await _checkShitabaraiDoneRepository.FindByCondition(d => d.CheckShitabaraiId == id).FirstOrDefaultAsync();

            if (checkShibarai == null)
            {
                return new ApiResponse { code = StatusCodes.Status404NotFound, message = SystemConstants.Message.DataNotFound };
            }

            try
            {
                await _repository.BeginTransactionAsync();

                int ChangeFlg = 0;
                Dictionary<int?, bool> idsUriageUpdated = new() { };

                foreach (var shitabaraiChange in (dto.shitabaraiChanges ?? Array.Empty<ShitabaraichangeDto>()))
                {
                    // 重複チェック
                    if (idsUriageUpdated.ContainsKey(shitabaraiChange.uriageShiharaiId))
                    {
                        return new ApiResponse { code = StatusCodes.Status400BadRequest, message = string.Format(SystemConstants.Message.AllowValue, "下払い変更") };
                    }

                    TCheckShitabaraiDetail dataDetail = checkShibarai.CheckShitabaraiDetails.FirstOrDefault(d => d.UriageShiharaiId == shitabaraiChange.uriageShiharaiId);

                    if (dataDetail == null)
                    {
                        await _repository.RollbackTransactionAsync();
                        return new ApiResponse { code = StatusCodes.Status404NotFound, message = SystemConstants.Message.DataNotFound };
                    }

                    TCheckShitabaraiChange dataChange = await _checkShitabaraiChangeRepository.FindByCondition(c => c.CheckShitabaraiId == id && c.UriageShiharaiId == dataDetail.UriageShiharaiId).FirstOrDefaultAsync();

                    if (shitabaraiChange.delFlg == 1 && dataChange == null)
                    {
                        continue;
                    }

                    if (dataChange == null)
                    {
                        ChangeFlg = 1;
                    }
                    // 更新処理
                    await UpdateCheckShibaraiChange(id, tantouId, shitabaraiChange, dataChange);

                    idsUriageUpdated.Add(shitabaraiChange.uriageShiharaiId, true);
                }

                // Register data T_Check_Sitabarai_Done
                TCheckShitabaraiDone dataDone = new()
                {
                    CheckShitabaraiId = id,
                    CheckDatetime = DateTime.Now,
                    CheckUser = tantouId,
                    CheckReault = dto.checkStatus == 1 ? 0 : 1,
                    InsertDatetime = DateTime.Now,
                    InsertUser = tantouId,
                    UpdateDatetime = DateTime.Now,
                    UpdateUser = tantouId,
                    ChangeFlg = ChangeFlg,
                };

                if (checkShibaraiDone == null)
                {
                    // 挿入処理
                    await _checkShitabaraiDoneRepository.CreateAsync(dataDone);
                }

                else
                {
                    // 更新処理
                    dataDone.InsertUser = checkShibaraiDone.InsertUser;
                    dataDone.InsertDatetime = checkShibaraiDone.InsertDatetime;

                    await _checkShitabaraiDoneRepository.UpdateAsync(dataDone);
                }

                // 更新処理
                checkShibarai.CheckStatus = (int)dto.checkStatus;
                checkShibarai.UpdateDatetime = DateTime.Now;
                checkShibarai.UpdateUser = tantouId;

                await _repository.UpdateAsync(checkShibarai);

                await _repository.EndTransactionAsync();
            }
            catch
            {
                await _repository.RollbackTransactionAsync();
                return new ApiResponse { code = 500, message = SystemConstants.Message.InternalServerError };
            }

            return new ApiResponse { code = 200 };
        }

        /// <summary>
        /// 支払問合せ一覧
        /// </summary>
        /// <param name="tantouId">担当者ID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="kubun">抽出区分（1：確認済、2：確認中/未確認）</param>
        /// <returns>支払問合せDTOのリスト</returns>
        public async Task<IEnumerable<CheckShiharaisDto>> GetListCheckShiharaisAsync(int tantouId, int companyId, int kubun)
        {
            int[] status = { (int)CheckShitabaraiStatus.発行済み, (int)CheckShitabaraiStatus.確認中 };

            if (kubun == (int)CheckShitabaraiKubun.確認済)
            {
                status = new int[] { (int)CheckShitabaraiStatus.確認済 };
            }

            int customerBranchId = tantouId != 0 ? (await _customerTantouRepository.FindByCondition(t => t.TantouId == tantouId).Select(t => t.CustomerBranchId).FirstOrDefaultAsync()) : 0;
            IEnumerable<TCheckShitabarai> data = await _repository.GetListCheckShiharaisAsync(customerBranchId, companyId, status);

            return data.Select(d => CheckShiharaisDto.FromEntity(d));
        }

        /// <summary>
        /// 支払問合せ入力＆取得
        /// </summary>
        /// <param name="id">支払確認ID</param>
        /// <param name="idInt">ID</param>
        /// <param name="isCompany">会社かどうか</param>
        /// <returns>支払問合せDTO</returns>
        public async Task<PaymentInquiryDto> GetPaymentInquiriesById(int id, int idInt, bool isCompany)
        {
            TCheckShitabarai checkShitabarai = await _repository.GetByIdAsync(id);
            if (checkShitabarai == null) return null;

            IEnumerable<TCheckShitabaraiDetail> checkShitabaraiDetail = await GetCheckShitabaraiDetailsAsync(checkShitabarai.CheckShitabaraiId);
            List<int> checkShitabaraiIds = checkShitabaraiDetail.Select(x => x.CheckShitabaraiId).ToList();
            List<int> uriageShiharaiIds = checkShitabaraiDetail.Select(x => x.UriageShiharaiId).ToList();

            IEnumerable<TUriageShitabarai> uriageShitabarai = await _uriageShitabaraiRepository.GetByIdsAsync(uriageShiharaiIds);
            IEnumerable<TCheckShitabaraiChange> checkShitabaraiChanges = await _checkShitabaraiChangeRepository.GetByIdsAsync(checkShitabaraiIds, uriageShiharaiIds);
            TCheckShitabaraiDone checkShitabaraiDone = await _checkShitabaraiDoneRepository.GetByIdAsync(checkShitabarai.CheckShitabaraiId);
            MCustomerBranch customerBranch = await _customerBranchRepository.GetByIdAsync(checkShitabarai.YosyaBranchId);

            MCompanyUserGroup companyGroupShitabaraiTantou = await GetCompanyUserGroupByShitabaraiTantouIdAsync(customerBranch);
            IEnumerable<MCompanyUserGroupUser> companyGroupUserShitabaraiTantou = await GetCompanyUserGroupUserAsync(companyGroupShitabaraiTantou);
            IEnumerable<MCompanyUser> companyUserShitabaraiTantou = await GetCompanyUser(companyGroupUserShitabaraiTantou);

            MCompanyUserGroup companyGroupSeikyuTantou = await GetCompanyUserGroupBySeikyuTantouIdAsync(customerBranch);
            IEnumerable<MCompanyUserGroupUser> companyGroupUserSeikyuTantou = await GetCompanyUserGroupUserAsync(companyGroupSeikyuTantou);
            IEnumerable<MCompanyUser> companyUserSeikyuTantou = await GetCompanyUser(companyGroupUserSeikyuTantou);

            IEnumerable<MUnit> unit = await GetUnitAsync(checkShitabaraiChanges);
            MCustomerUriageCalc customerUriageCalc = await _customerUriageCalcRepository.GetByIdAsync(customerBranch.CustomerBranchId);

            IEnumerable<TPrintParameter> printParameters = await _printParameterRepository.GetByCheckShitabaraiIdAsync(checkShitabarai.CheckShitabaraiId);
            IEnumerable<TPortalInfo> portalInfos = await GetTPortalInfoByPrintParameterAsync(printParameters, idInt, isCompany);

            PaymentInquiryDto dto = new PaymentInquiryDto
            {
                shitabaraiDetails = await CreateShitabaraiDetails(checkShitabaraiDetail, checkShitabaraiChanges, uriageShitabarai),
                checkShitabarai = CreateCheckShitabaraiDto(checkShitabarai, checkShitabaraiDetail,
                checkShitabaraiChanges, checkShitabaraiDone, customerBranch,
                customerUriageCalc, companyGroupSeikyuTantou,
                companyUserSeikyuTantou, companyGroupShitabaraiTantou,
                companyUserShitabaraiTantou, unit),
                portalInfoIds = portalInfos.Select(p => p.PortalInfoId).ToList()
            };

            return dto;
        }

        /// <summary>
        /// 支払問合せ一覧の作成
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
        private static CheckShitabaraiDto CreateCheckShitabaraiDto(TCheckShitabarai checkShitabarai,
            IEnumerable<TCheckShitabaraiDetail> details,
            IEnumerable<TCheckShitabaraiChange> changes,
            TCheckShitabaraiDone done,
            MCustomerBranch branch,
            MCustomerUriageCalc uriageCalc,
            MCompanyUserGroup companyGroupSeikyuTantou,
            IEnumerable<MCompanyUser> companyUserSeikyuTantou,
            MCompanyUserGroup companyGroupShiharaiTantou,
            IEnumerable<MCompanyUser> companyUserShiharaiTantou,
            IEnumerable<MUnit> unit)
        {
            if (checkShitabarai == null)
            {
                return null;
            }
            decimal afterSeikyuUnchin = SetAfterShiharaiUnchin(changes, details);
            decimal afterTatekaekin = SetAfterTatekaekin(changes, details);
            return new CheckShitabaraiDto
            {
                id = checkShitabarai.CheckShitabaraiId,
                checkStatus = checkShitabarai.CheckStatus,
                shiharaiMonth = checkShitabarai.ShiharaiMonth.ToString("yyyy年MM月"),
                shimeDay = checkShitabarai.ShimeDay,
                zeiKubun = checkShitabarai.ZeiKubun,
                ankenCount = SetAnkenCount(details),
                changeCount = SetChangeCount(changes),

                detailCount = SetDetailCount(details),

                beforeShiharaiUnchin = SetBeforeShiharaiUnchinSum(details),
                beforeTatekaekin = SetBeforeTatekaekinSum(details),
                afterShiharaiUnchin = CommonHelper.RoundDecimalToLong(afterSeikyuUnchin),
                afterTatekaekin = CommonHelper.RoundDecimalToLong(afterTatekaekin),

                done = SetCheckShiharaisDoneDto(done),
                details = SetDetails(changes, details, unit),
                customerBranch = SetCustomerBranch(branch, uriageCalc, companyGroupSeikyuTantou, companyUserSeikyuTantou,
                companyGroupShiharaiTantou, companyUserShiharaiTantou)
            };
        }

        /// <summary>
        /// 支払先の設定
        /// </summary>
        /// <param name="customerBranch"></param>
        /// <param name="customerUriageCalc"></param>
        /// <param name="companyUserGroupUserBySeikyuTantouId"></param>
        /// <param name="companyUserBySeikyuTantouId"></param>
        /// <param name="companyUserGroupUserByShiharaiTantouId"></param>
        /// <param name="companyUserByShiharaiTantouId"></param>
        /// <returns></returns>
        private static CustomerBranchShitabaraiDto SetCustomerBranch(MCustomerBranch customerBranch,
            MCustomerUriageCalc customerUriageCalc,
            MCompanyUserGroup companyUserGroupUserBySeikyuTantouId,
            IEnumerable<MCompanyUser> companyUserBySeikyuTantouId,
            MCompanyUserGroup companyUserGroupUserByShiharaiTantouId,
            IEnumerable<MCompanyUser> companyUserByShiharaiTantouId)
            => new()
            {
                id = customerBranch.CustomerBranchId,
                uriageCalc = SetCustomerUriageCalcDto(customerUriageCalc),
                seikyuTantou = SetSeikyuTantouDto(customerBranch, companyUserGroupUserBySeikyuTantouId, companyUserBySeikyuTantouId),
                shiharaiTantou = SetShiharaiTantouDto(customerBranch, companyUserGroupUserByShiharaiTantouId, companyUserByShiharaiTantouId)
            };

        /// <summary>
        /// 支払先担当者の設定
        /// </summary>
        /// <param name="customerBranch"></param>
        /// <param name="companyUserGroup"></param>
        /// <param name="companyUser"></param>
        /// <returns></returns>
        private static ShiharaiTantouShitabaraiDto SetShiharaiTantouDto(MCustomerBranch customerBranch, MCompanyUserGroup companyUserGroup,
            IEnumerable<MCompanyUser> companyUser)
        {
            if (companyUserGroup == null)
            {
                return null;
            }

            ShiharaiTantouShitabaraiDto dto = new ShiharaiTantouShitabaraiDto
            {
                id = customerBranch.ShiharaiTantouId,
                groupName = companyUserGroup?.GroupName,
                displayName = companyUserGroup?.DisplayName,
                phone = companyUserGroup?.Phone,
                users = SetCompanyUser(companyUser)
            };

            return dto;
        }

        /// <summary>
        /// 請求先担当者の設定
        /// </summary>
        /// <param name="customerBranch"></param>
        /// <param name="companyUserGroup"></param>
        /// <param name="companyUser"></param>
        /// <returns></returns>
        private static SeikyuTantouShitabaraiDto SetSeikyuTantouDto(MCustomerBranch customerBranch, MCompanyUserGroup companyUserGroup,
            IEnumerable<MCompanyUser> companyUser)
        {
            if (companyUserGroup == null)
            {
                return null;
            }
            SeikyuTantouShitabaraiDto dto = new SeikyuTantouShitabaraiDto
            {
                id = customerBranch.SeikyuTantouId,
                groupName = companyUserGroup?.GroupName,
                displayName = companyUserGroup?.DisplayName,
                phone = companyUserGroup?.Phone,
                users = SetCompanyUser(companyUser),
            };
            return dto;
        }

        /// <summary>
        /// 業者ユーザの設定
        /// </summary>
        /// <param name="companyUsers"></param>
        /// <returns></returns>
        private static List<UserShitabaraiDto> SetCompanyUser(IEnumerable<MCompanyUser> companyUsers)
            => companyUsers == null || !companyUsers.Any() ? new() : companyUsers.Select(user => new UserShitabaraiDto
            {
                id = user.UserId,
                displayName = user.DisplayName
            }).ToList();

        /// <summary>
        /// M_Customer_Uriage_Calcの設定
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static CustomerUriageCalcShitabaraiDto SetCustomerUriageCalcDto(MCustomerUriageCalc model)
            => model == null ? null : new()
            {
                id = model.CustomerBranchId,
                seikyuUnchinName = model.SeikyuUnchinName,
                tatekaekinName = model.TatekaekinName,
                warimashi1Name = model.Warimashi1Name,
                warimashi2Name = model.Warimashi2Name,
                warimashi3Name = model.Warimashi3Name,
                warimashi4Name = model.Warimashi4Name,
                warimashi5Name = model.Warimashi5Name,
                seikyuTotalname = model.SeikyuTotalName,
                /*warimashi1Visible = (bool)model.Warimashi1Visible,
                warimashi2Visible = (bool)model.Warimashi2Visible,
                warimashi3Visible = (bool)model.Warimashi3Visible,
                warimashi4Visible = (bool)model.Warimashi4Visible,
                warimashi5Visible = (bool)model.Warimashi5Visible,*/
                warimashi1Calc = model.Warimashi1Calc,
                warimashi2Calc = model.Warimashi2Calc,
                warimashi3Calc = model.Warimashi3Calc,
                warimashi4Calc = model.Warimashi4Calc,
                warimashi5Calc = model.Warimashi5Calc,
                seikyuTotalCalc = model.SeikyuTotalCalc
            };

        /// <summary>
        /// 支払問合せ一覧（詳細）の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static List<CheckShitabaraiDetailDto> SetDetails(IEnumerable<TCheckShitabaraiChange> changes,
            IEnumerable<TCheckShitabaraiDetail> details, IEnumerable<MUnit> unit)
            => details.Select(detail => new CheckShitabaraiDetailDto
            {
                checkShitabaraiId = detail.CheckShitabaraiId,
                uriageShiharaiId = detail.UriageShiharaiId,
                change = SetCheckShiharaisChangeDto(changes.FirstOrDefault(c => c.CheckShitabaraiId == detail.CheckShitabaraiId
                && c.UriageShiharaiId == detail.UriageShiharaiId))
            }).ToList();

        /// <summary>
        /// 確認情報の設定
        /// </summary>
        /// <param name="done"></param>
        /// <returns></returns>
        private static CheckShitabaraiDoneDto SetCheckShiharaisDoneDto(TCheckShitabaraiDone done)
        {
            // Q&A OUT_IMAI-169
            //if (done == null)
            //{
            //    return null; 
            //}
            return new()
            {
                checkDatetime = done?.CheckDatetime != null ? done.CheckDatetime.ToString("yyyy年MM月dd日") : null,
                checkUser = done?.CheckUser.ToString(),
                changeFlg = done?.ChangeFlg.Equals(SystemConstants.Flag.TRUE),
                checkReault = done?.CheckReault
            };
        }

        /// <summary>
        /// 変更後立替計の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static decimal SetAfterTatekaekin(IEnumerable<TCheckShitabaraiChange> changes, IEnumerable<TCheckShitabaraiDetail> details)
        {
            decimal sum = 0M;
            if (changes != null && changes.Any())
            {
                decimal changesSum = changes?.Sum(c => c.Tatekaekin) ?? 0;
                decimal detailsSum = 0M;

                if (details != null && details.Any())
                {
                    HashSet<int> changeUriageUnchinIds = new HashSet<int>(changes?.Where(x => x.Tatekaekin != null).Select(c => c.UriageShiharaiId) ?? Enumerable.Empty<int>());
                    detailsSum = details
                        .Where(detail => !changeUriageUnchinIds.Contains(detail.UriageShiharaiId))
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
        private static decimal SetAfterShiharaiUnchin(IEnumerable<TCheckShitabaraiChange> changes, IEnumerable<TCheckShitabaraiDetail> details)
        {
            decimal sum = 0M;
            if (changes != null && changes.Any())
            {
                decimal changesSum = changes?.Sum(c => c.ShiharaiUnchin) ?? 0;
                decimal detailsSum = 0M;

                if (details != null && details.Any())
                {
                    HashSet<int> changeUriageUnchinIds = new HashSet<int>(changes?.Where(x => x.ShiharaiUnchin != null).Select(c => c.UriageShiharaiId) ?? Enumerable.Empty<int>());
                    detailsSum = details
                        .Where(detail => !changeUriageUnchinIds.Contains(detail.UriageShiharaiId))
                        .Sum(c => c.ShiharaiUnchin) ?? 0;
                }

                sum = changesSum + detailsSum;
            }
            else
            {
                if (details != null && details.Any())
                    sum = details.Sum(c => c.ShiharaiUnchin) ?? 0;
            }
            return sum;
        }

        /// <summary>
        /// 変更前立替計の設定
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        private static long SetBeforeTatekaekinSum(IEnumerable<TCheckShitabaraiDetail> details)
            => (details != null && details.Any()) ? CommonHelper.RoundDecimalToLong(details.Sum(c => c.Tatekaekin)) : 0;

        /// <summary>
        /// 変更前運賃計の設定
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        private static long SetBeforeShiharaiUnchinSum(IEnumerable<TCheckShitabaraiDetail> details)
            => (details != null && details.Any()) ? CommonHelper.RoundDecimalToLong(details.Sum(c => c.ShiharaiUnchin)) : 0;

        /// <summary>
        /// 明細件数の設定
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        private static int SetDetailCount(IEnumerable<TCheckShitabaraiDetail> details) => details?.Count() ?? 0;

        /// <summary>
        /// 変更件数の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <returns></returns>
        private static int SetChangeCount(IEnumerable<TCheckShitabaraiChange> changes)
        {
            int sum = 0;
            if (changes != null && changes.Any())
            {
                sum = changes.GroupBy(c => new { c.CheckShitabaraiId, c.UriageShiharaiId }).Count();
            }
            return sum;
        }

        /// <summary>
        /// 案件件数の設定
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        private static int SetAnkenCount(IEnumerable<TCheckShitabaraiDetail> details)
            => details?.GroupBy(c => c.AnkenId).Count() ?? 0;

        /// <summary>
        /// 支払問合せ入力（詳細）の作成
        /// </summary>
        /// <param name="checkSeikyuDetail"></param>
        /// <param name="checkSeikyuChanges"></param>
        /// <param name="printSeikyus"></param>
        /// <param name="printSeikyuDetails"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        private async Task<List<ShitabaraiDetailDto>> CreateShitabaraiDetails(IEnumerable<TCheckShitabaraiDetail> checkShitabaraiDetails, IEnumerable<TCheckShitabaraiChange> checkShitabaraiChanges, IEnumerable<TUriageShitabarai> uriageShitabarais)
        {
            IEnumerable<TUriage> uriages = await _uriageRepository.GetByIdsAsync(uriageShitabarais.Select(item => item.UriageId).ToList());
            IEnumerable<TAnkenDetail> ankenDetails = await _ankenDetailRepository.GetLatestByIdsAsync(uriages.Select(item => item.AnkenId).ToList());
            IEnumerable<TNippou> nippous = await _nippouRepository.GetByIdsAsync(uriages.Select(item => item.NippouId).ToList());
            IEnumerable<THaisya> haisyas = await _haisyaRepository.GetByIdsAsync(nippous.Select(item => item.AnkenDisplayId).ToList());
            IEnumerable<MSyaryoManagement> syaryoManagements = await _syaryoManagementRepository.GetByIdsAsync(haisyas.Select(item => item.SyaryoManagementId).ToList());
            IEnumerable<MSyaryo> syaryos = await _syaryoRepository.GetByIdsAsync(syaryoManagements.Select(item => item.SyaryoId ?? 0).ToList());

            IEnumerable<JoinCheckShitabaraiDto> details = from csd in checkShitabaraiDetails
                          join us in uriageShitabarais on csd.UriageShiharaiId equals us.UriageShiharaiId
                          join u in uriages on us.UriageId equals u.UriageId

                          join ad in ankenDetails on u.AnkenId equals ad.AnkenId into ankenDetail
                          from adNullable in ankenDetail.DefaultIfEmpty()

                          join n in nippous on u.NippouId equals n.NippouId into nippou
                          from nNullable in nippou.DefaultIfEmpty()

                          join h in haisyas on nNullable?.AnkenDisplayId equals h.AnkenDisplayId into haisya
                          from hNullable in haisya.DefaultIfEmpty()

                          join sm in syaryoManagements on hNullable?.SyaryoManagementId equals sm.SyaryoManagementId into syaryoManagement
                          from smNullable in syaryoManagements.DefaultIfEmpty()

                          join s in syaryos on smNullable?.SyaryoId equals s.SyaryoId into syaryo
                          from sNullable in syaryo.DefaultIfEmpty()

                          select  new JoinCheckShitabaraiDto
                          {
                              detail = csd,
                              uriageShirabarai = us,
                              uriage = u,
                              ankenDetail = adNullable,
                              syaryo = sNullable,
                          };

            details = details.ToList();

            List<ShitabaraiDetailDto> shitabaraiDetails = new List<ShitabaraiDetailDto>();

            foreach (var item in details)
            {
                TCheckShitabaraiDetail detail = item.detail;
                TUriageShitabarai uriageShirabarai = item.uriageShirabarai;
                TUriage uriage = item.uriage;
                TAnkenDetail ankendetail = item.ankenDetail;
                MSyaryo syaryo = item.syaryo;

                PrintShitabaraiDetailDto checkShitabaraiDetail = SetCheckShitabaraiDetailDto(detail, uriageShirabarai, uriage, syaryo, ankendetail);
                CheckShitabaraiChangeDto checkShitabaraiChange = SetCheckShiharaisChangeDto(checkShitabaraiChanges.FirstOrDefault(c => c.CheckShitabaraiId == detail.CheckShitabaraiId
                                            && c.UriageShiharaiId == detail.UriageShiharaiId));
                if (checkShitabaraiDetail == null && checkShitabaraiChange == null)
                {
                    continue;
                }
                ShitabaraiDetailDto shitabaraiDetailDto = new ShitabaraiDetailDto
                {
                    checkShitabaraiDetail = checkShitabaraiDetail,
                    checkShitabaraiChange = checkShitabaraiChange
                };

                shitabaraiDetails.Add(shitabaraiDetailDto);
            }

            return shitabaraiDetails;
        }

        /// <summary>
        /// 変更内容入力の設定（支払問合せ入力）
        /// </summary>
        /// <param name="change"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        private static CheckShitabaraiChangeDto SetCheckShiharaisChangeDto(TCheckShitabaraiChange change)
        {
            if (change == null) return null;
            return new CheckShitabaraiChangeDto()
            {
                checkShitabaraiId = change.CheckShitabaraiId,
                uriageShiharaiId = change.UriageShiharaiId,
                qty = change.Qty,
                unit = change.Unit,
                calcPrice = change.CalcPrice,
                shiharaiUnchin = change.ShiharaiUnchin,
                tatekaekin = change.Tatekaekin,
                warimashi1 = change.Warimashi1,
                warimashi2 = change.Warimashi2,
                warimashi3 = change.Warimashi3,
                warimashi4 = change.Warimashi4,
                warimashi5 = change.Warimashi5,
                shiharaiTotal = change.ShiharaiTotal,
                unitPrice = change.UnitPrice ?? 0
            };
        }

        /// <summary>
        /// 支払明細の設定
        /// </summary>
        /// <param name="model"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        private static PrintShitabaraiDetailDto SetCheckShitabaraiDetailDto(TCheckShitabaraiDetail detail, TUriageShitabarai uriageShitabarai, TUriage uriage, MSyaryo syaryo, TAnkenDetail ankenDetail)
        {

            return new PrintShitabaraiDetailDto()
            {
                displayDate = uriageShitabarai?.ShiharaiDate.ToString(DateFormat.DATE) ?? string.Empty,
                syaban = uriageShitabarai?.Syaban,
                syasyuKataName = uriage?.NippouId != 0 ? syaryo?.SyasyuDisplay : null,
                tsumi = uriageShitabarai?.Tsumi,
                oroshi = uriageShitabarai?.Oroshi,
                workName = uriage?.AnkenId != 0 ? ankenDetail?.WorkName : null,
                luggage = uriageShitabarai?.Luggage,
                qty = detail.Qty ?? 0,
                unit = detail.Unit.ToString(),
                unitPrice = detail.UnitPrice ?? 0,
                shiharaiUnchin = detail.ShiharaiUnchin ?? 0,
                warimashi1 = detail.Warimashi1 ?? 0,
                warimashi2 = detail.Warimashi2 ?? 0,
                warimashi3 = detail.Warimashi3 ?? 0,
                warimashi4 = detail.Warimashi4 ?? 0,
                warimashi5 = detail.Warimashi5 ?? 0,
                tatekaekin = detail.Tatekaekin ?? 0,
                shiharaiTotal = detail.ShiharaiTotal ?? 0,
                remarks = uriageShitabarai?.Remarks
            };
        }

        /// <summary>
        /// 支払問合せ入力（サマリ）の作成
        /// </summary>
        /// <param name="printShitabarais"></param>
        /// <param name="checkShitabaraiChanges"></param>
        /// <param name="printShitabaraiDetails"></param>
        /// <returns></returns>
        private static SummaryShitabaraiDto CreateSummaryDto(IEnumerable<TPrintShitabarai> printShitabarais, IEnumerable<TCheckShitabaraiChange> checkShitabaraiChanges,
            IEnumerable<TPrintShitabaraiDetail> printShitabaraiDetails)
        {
            decimal shitabaraiUnchin = SumShitabaraiUnchin(checkShitabaraiChanges, printShitabaraiDetails);
            decimal warimashi = SumWarimashi(printShitabaraiDetails);
            decimal tatekaekin = SumTatekaekin(checkShitabaraiChanges, printShitabaraiDetails);
            long tax = CommonHelper.CeilingDecimalToLong((shitabaraiUnchin + warimashi) * 10 / 100);
            long shitabaraiTotal = CommonHelper.RoundDecimalToLong(shitabaraiUnchin + warimashi + tatekaekin + tax);

            return new SummaryShitabaraiDto
            {
                shitabaraiPrevious = CommonHelper.RoundDecimalToLong(printShitabarais.Sum(s => s.前月残)),
                receivedAmountThis = 0,// Q&A OUT_IMAI-628
                discount = 0, // Q&A OUT_IMAI-628
                balanceForward = CommonHelper.RoundDecimalToLong(printShitabarais.Sum(s => s.繰越金額)),
                shitabaraiUnchin = CommonHelper.RoundDecimalToLong(shitabaraiUnchin),
                warimashi = CommonHelper.RoundDecimalToLong(warimashi),
                tax = tax,
                tatekaekin = CommonHelper.RoundDecimalToLong(tatekaekin),
                shitabaraiTotal = shitabaraiTotal
            };
        }

        /// <summary>
        /// 立替計の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static decimal SumTatekaekin(IEnumerable<TCheckShitabaraiChange> changes, IEnumerable<TPrintShitabaraiDetail> details)
        {
            decimal sum = 0M;
            if (changes != null && changes.Any())
            {
                decimal changesSum = changes?.Sum(c => c.Tatekaekin) ?? 0;
                decimal detailsSum = 0M;

                if (details != null && details.Any())
                {
                    HashSet<int> changeUriageUnchinIds = new HashSet<int>(changes?.Where(x => x.Tatekaekin != null).Select(c => c.UriageShiharaiId) ?? Enumerable.Empty<int>());
                    detailsSum = details
                        .Where(detail => !changeUriageUnchinIds.Contains(detail.UriageShiharaiId))
                        .Sum(c => c.Tatekaekin);
                }

                sum = changesSum + detailsSum;
            }
            else
            {
                if (details != null && details.Any())
                    sum = details.Sum(c => c.Tatekaekin);
            }

            return sum;
        }

        /// <summary>
        /// 割増の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static decimal SumWarimashi(IEnumerable<TPrintShitabaraiDetail> details)
        {
            decimal sum = 0M;
            if (details != null && details.Any())
            {
                sum = details.Sum(x => x.Warimashi1);
            }
            return sum;
        }

        /// <summary>
        /// 運賃計の設定
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private static decimal SumShitabaraiUnchin(IEnumerable<TCheckShitabaraiChange> changes, IEnumerable<TPrintShitabaraiDetail> details)
        {
            decimal sum = 0M;
            if (changes != null && changes.Any())
            {
                decimal changesSum = changes?.Sum(c => c.ShiharaiUnchin) ?? 0;
                decimal detailsSum = 0M;

                if (details != null && details.Any())
                {
                    HashSet<int> changeUriageUnchinIds = new HashSet<int>(changes?.Where(c => c.ShiharaiUnchin != null).Select(c => c.UriageShiharaiId) ?? Enumerable.Empty<int>());
                    detailsSum = details
                        .Where(detail => !changeUriageUnchinIds.Contains(detail.UriageShiharaiId))
                        .Sum(c => c.ShiharaiUnchin);
                }

                sum = changesSum + detailsSum;
            }
            else
            {
                if (details != null && details.Any())
                    sum = details.Sum(c => c.ShiharaiUnchin);
            }

            return sum;
        }

        /// <summary>
        /// ポータル情報の取得（業者はcompanyID、荷主はcustomerTantouIdより取得）
        /// </summary>
        /// <param name="printParameters">List ids of PrintParameter</param>
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
        /// 単位の取得
        /// </summary>
        /// <param name="printSeikyuDetails"></param>
        /// <param name="checkSeikyuChanges"></param>
        /// <returns></returns>
        private async Task<IEnumerable<MUnit>> GetUnitAsync(IEnumerable<TCheckShitabaraiChange> checkShitabaraiChanges)
        {
            List<int> ids = new();
            if (checkShitabaraiChanges != null && checkShitabaraiChanges.Any())
            {
                ids.AddRange(checkShitabaraiChanges.Where(x => x.Unit.HasValue && x.Unit.Value > 0).Select(x => x.Unit.Value));
            }
            if (ids.Count > 0)
            {
                return await _unitRepository.GetByIdsAsync(ids.Distinct().ToList());
            }
            return new List<MUnit>();
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
        /// 請求担当IDより業者ユーザグループの取得
        /// </summary>
        /// <param name="customerBranch"></param>
        /// <returns></returns>
        private async Task<MCompanyUserGroup> GetCompanyUserGroupBySeikyuTantouIdAsync(MCustomerBranch customerBranch)
        {
            return await _companyUserGroupRepository.GetByIdAsync(customerBranch.SeikyuTantouId);
        }
        /// <summary>
        /// 支払担当IDより業者ユーザグループの取得
        /// </summary>
        /// <param name="customerBranch"></param>
        /// <returns></returns>
        private async Task<MCompanyUserGroup> GetCompanyUserGroupByShitabaraiTantouIdAsync(MCustomerBranch customerBranch)
        {
            return await _companyUserGroupRepository.GetByIdAsync(customerBranch.ShiharaiTantouId);
        }

        /// <summary>
        /// 支払問合せ入力（詳細）の取得
        /// </summary>
        /// <param name="checkShitabaraiId"></param>
        /// <returns></returns>
        private async Task<IEnumerable<TCheckShitabaraiDetail>> GetCheckShitabaraiDetailsAsync(int checkShitabaraiId)
        {
            return await _checkShitabaraiDetailRepository.GetByCheckShitabaraiId(checkShitabaraiId)
                ?? new List<TCheckShitabaraiDetail>();
        }
    }
}
