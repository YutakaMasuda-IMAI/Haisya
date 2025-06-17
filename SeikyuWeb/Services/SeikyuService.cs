using SeikyuWeb.Common;
using SeikyuWeb.Dto.ReportLayoutDto;
using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using SeikyuWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Services
{
    /// <summary>
    /// 請求サービスクラス
    /// </summary>
    public class SeikyuService : ISeikyuService
    {
        private readonly ICheckSeikyuRepository _checkSeikyuRepository;
        private readonly IPrintSeikyuRepository _printSeikyuRepository;
        private readonly ISeikyuRepository _seikyuRepository;
        private readonly IReportLayoutRepository _reportLayoutRepository;
        private readonly IPrintParameterRepository _printParameterRepository;
        private readonly IPortalInfoRepository _portalInfoRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="checkSeikyuRepository">請求確認リポジトリ</param>
        /// <param name="printSeikyuRepository">請求印刷リポジトリ</param>
        /// <param name="seikyuRepository">請求リポジトリ</param>
        /// <param name="reportLayoutRepository">レポートレイアウトリポジトリ</param>
        /// <param name="printParameterRepository">印刷パラメータリポジトリ</param>
        /// <param name="portalInfoRepository">ポータル情報リポジトリ</param>
        public SeikyuService(
            ICheckSeikyuRepository checkSeikyuRepository,
            IPrintSeikyuRepository printSeikyuRepository,
            ISeikyuRepository seikyuRepository,
            IReportLayoutRepository reportLayoutRepository,
            IPrintParameterRepository printParameterRepository,
            IPortalInfoRepository portalInfoRepository
        )
        {
            _checkSeikyuRepository = checkSeikyuRepository;
            _printSeikyuRepository = printSeikyuRepository;
            _seikyuRepository = seikyuRepository;
            _reportLayoutRepository = reportLayoutRepository;
            _printParameterRepository = printParameterRepository;
            _portalInfoRepository = portalInfoRepository;
        }

        /// <summary>
        /// 請求問合せ一覧の取得
        /// </summary>
        /// <param name="kubun">区分（1：確認済）/（2：確認中, 未確認）</param>
        /// <param name="idInt">ID（会社ID）/（担当ID）</param>
        /// <param name="isCompany">True:（会社ID）/False:（担当ID）</param>
        /// <returns>請求問合せ一覧</returns>
        public async Task<IEnumerable<CheckSeikyuDto>> GetBillingInqueryList(int kubun, int idInt, bool isCompany)
        {
            List<CheckSeikyuDto> checkSeikyu = new List<CheckSeikyuDto>();
            int[] status = { 0, 1 };
            if (kubun == 1)
            {
                status = new int[] { 2 };
            }

            IEnumerable<SeikyuDto> data = await _checkSeikyuRepository.GetBillingInqueryList(kubun, status, idInt, isCompany);

            if (!data.Any())
            {
                return checkSeikyu;
            }

            int[] checkSeikyuIds = data
                .Select(d => d.checkSeikyu.CheckSeikyuId).Distinct()
                .ToArray();

            IEnumerable<SeikyuTantouQueryDto> seikyuTantou = await _checkSeikyuRepository.GetSeikyuTantou(checkSeikyuIds);
            IEnumerable<ShiharaiTantouQueryDto> shiharaiTantou = await _checkSeikyuRepository.GetShiharaiTantou(checkSeikyuIds);

            List<SeikyuTantouDto> formatSeikyuTantou = seikyuTantou.GroupBy(s => new
            {
                s.mCompanyUserGroup.GroupId,
                MCompanyUserGroup = s.mCompanyUserGroup,
            })
            .Select(g => new SeikyuTantouDto
            {
                id = g.Key.MCompanyUserGroup.GroupId,
                displayName = g.Key.MCompanyUserGroup.DisplayName,
                groupName = g.Key.MCompanyUserGroup.GroupName,
                phone = g.Key.MCompanyUserGroup.Phone,
                users = g.Select(u => new UserDto
                {
                    id = u.mCompanyUser.UserId,
                    displayName = u.mCompanyUser.DisplayName,
                })
                .GroupBy(u => u.id)
                .Select(group => group.First())
                .ToList()
            }).ToList();

            List<ShiharaiTantouDto> formatShiharaiTantou = shiharaiTantou.GroupBy(s => new
            {
                s.mCompanyUserGroup.GroupId,
                MCompanyUserGroup = s.mCompanyUserGroup,
            })
            .Select(g => new ShiharaiTantouDto
            {
                id = g.Key.MCompanyUserGroup.GroupId,
                displayName = g.Key.MCompanyUserGroup.DisplayName,
                groupName = g.Key.MCompanyUserGroup.GroupName,
                phone = g.Key.MCompanyUserGroup.Phone,
                users = g.Select(u => new UserDto
                {
                    id = u.mCompanyUser.UserId,
                    displayName = u.mCompanyUser.DisplayName,
                })
                .GroupBy(u => u.id)
                .Select(group => group.First())
                .ToList()
            }).ToList();

            foreach (var item in data)
            {
                CheckSeikyuDto existingObj = checkSeikyu.FirstOrDefault(o => o.id == item.checkSeikyu.CheckSeikyuId);

                if (existingObj == null)
                {
                    checkSeikyu.Add(new CheckSeikyuDto
                    {
                        id = item.checkSeikyu.CheckSeikyuId,
                        checkStatus = item.checkSeikyu.CheckStatus,
                        seikyuMonth = item.checkSeikyu.SeikyuMonth.ToString(DateFormat.MONTH_JP),
                        shimeDay = item.checkSeikyu.ShimeDay,
                        zeiKubun = item.checkSeikyu.ZeiKubun,
                        customerBranch = new CustomerBranchDto
                        {
                            id = item.customerBranch != null ? item.customerBranch.CustomerBranchId : 0,
                            uriageCalc = item.customerUriageCalc != null ? new CustomerUriageCalcDto
                            {
                                id = item.customerUriageCalc?.CustomerBranchId ?? 0,
                                seikyuUnchinName = item.customerUriageCalc?.SeikyuUnchinName,
                                tatekaekinName = item.customerUriageCalc?.TatekaekinName,
                                warimashi1Name = item.customerUriageCalc?.Warimashi1Name,
                                warimashi2Name = item.customerUriageCalc?.Warimashi2Name,
                                warimashi3Name = item.customerUriageCalc?.Warimashi3Name,
                                warimashi4Name = item.customerUriageCalc?.Warimashi4Name,
                                warimashi5Name = item.customerUriageCalc?.Warimashi5Name,
                                seikyuTotalname = item.customerUriageCalc?.SeikyuTotalName,
                                warimashi1Visible = item.customerUriageCalc?.Warimashi1Visible ?? false,
                                warimashi2Visible = item.customerUriageCalc?.Warimashi2Visible ?? false,
                                warimashi3Visible = item.customerUriageCalc?.Warimashi3Visible ?? false,
                                warimashi4Visible = item.customerUriageCalc?.Warimashi4Visible ?? false,
                                warimashi5Visible = item.customerUriageCalc?.Warimashi5Visible ?? false,
                                warimashi1Calc = item.customerUriageCalc?.Warimashi1Calc,
                                warimashi2Calc = item.customerUriageCalc?.Warimashi2Calc,
                                warimashi3Calc = item.customerUriageCalc?.Warimashi3Calc,
                                warimashi4Calc = item.customerUriageCalc?.Warimashi4Calc,
                                warimashi5Calc = item.customerUriageCalc?.Warimashi5Calc,
                                seikyuTotalCalc = item.customerUriageCalc?.SeikyuTotalCalc,
                            } : null,
                            seikyuTantou = formatSeikyuTantou.Find(x => x.id == item.customerBranch.SeikyuTantouId),
                            shiharaiTantou = formatShiharaiTantou.Find(x => x.id == item.customerBranch.ShiharaiTantouId),
                        },
                        done = new CheckSeikyuDoneDto
                        {
                            checkDatetime = (item.done != null && item.done.CheckSeikyuId != 0) ? item.done.CheckDatetime.ToString("yyyy/MM/dd") : null,
                            checkUser = (item.done != null && item.done.CheckSeikyuId != 0) ? item?.customerTantou?.TantouNameAbbr : null,
                            changeFlg = (item.done != null && item.done.CheckSeikyuId != 0) ? item.done.ChangeFlg.HasValue && item.done.ChangeFlg != 0 : null,
                            checkReault = (item.done != null && item.done.CheckSeikyuId != 0) ? item.done.CheckReault : null,
                        },
                        changeCount = 0,
                        ankenCount = 0,
                        detailCount = 0,
                        beforeSeikyuUnchin = 0,
                        beforeTatekaekin = 0,
                        afterSeikyuUnchin = 0,
                        afterTatekaekin = 0,
                        status = item.checkSeikyu.CheckStatus switch
                        {
                            0 => StatusCheckSeikyu.Unconfirmed,
                            1 => StatusCheckSeikyu.Checking,
                            _ => StatusCheckSeikyu.Confirmed
                        },
                        details = new List<CheckSeikyuDetailDto> {
                            new()
                            {
                                checkSeikyuId = item.detail.CheckSeikyuId,
                                uriageUnchinId = item.detail.UriageUnchinId,
                                change = item.change != null ? new CheckSeikyuChangeDto
                                {
                                    checkSeikyuId = item.change.CheckSeikyuId,
                                    uriageUnchinId = item.change.UriageUnchinId,
                                    qty = item.change.Qty,
                                    unit = item.change.Unit,
                                    unitPrice = item.change.UnitPrice,
                                    calcPrice = item.change.CalcPrice,
                                    seikyuUnchin = item.change.SeikyuUnchin,
                                    tatekaekin = item.change.Tatekaekin,
                                    warimashi1 = item.change.Warimashi1,
                                    warimashi2 = item.change.Warimashi2,
                                    warimashi3 = item.change.Warimashi3,
                                    warimashi4 = item.change.Warimashi4,
                                    warimashi5 = item.change.Warimashi5,
                                    seikyuTotal = item.change.SeikyuTotal,
                                } : null,
                            }
                        }
                    });
                }
                else
                {
                    existingObj.details = existingObj.details.Append(
                        new CheckSeikyuDetailDto
                        {
                            checkSeikyuId = item.detail.CheckSeikyuId,
                            uriageUnchinId = item.detail.UriageUnchinId,
                            change = item.change != null ? new CheckSeikyuChangeDto
                            {
                                checkSeikyuId = item.change.CheckSeikyuId,
                                uriageUnchinId = item.change.UriageUnchinId,
                                qty = item.change.Qty,
                                unit = item.change.Unit,
                                unitPrice = item.change.UnitPrice,
                                calcPrice = item.change.CalcPrice,
                                seikyuUnchin = item.change.SeikyuUnchin,
                                tatekaekin = item.change.Tatekaekin,
                                warimashi1 = item.change.Warimashi1,
                                warimashi2 = item.change.Warimashi2,
                                warimashi3 = item.change.Warimashi3,
                                warimashi4 = item.change.Warimashi4,
                                warimashi5 = item.change.Warimashi5,
                                seikyuTotal = item.change.SeikyuTotal,
                            } : null,
                        }
                    );
                }
            }

            foreach (var item in checkSeikyu)
            {
                int id = item.id;
                List<SeikyuDto> result = data.Where(d => d.checkSeikyu.CheckSeikyuId == id).ToList();

                // changeが存在するかどうか
                bool isExistChange = result.Any(item => item.change != null);

                int changeCount = isExistChange ? result.Where(d => d.change != null).Select(d => d.change.UriageUnchinId).Distinct().Count() : 0;
                int ankenCount = result.Select(d => d.detail.AnkenId).Distinct().Count();
                int detailCount = result.Count(d => d.detail != null);

                decimal beforeSeikyuUnchin = result.Sum(d => d.detail.SeikyuUnchin ?? 0);
                decimal beforeTatekaekin = result.Sum(d => d.detail.Tatekaekin ?? 0);

                decimal afterSeikyuUnchin = beforeSeikyuUnchin;
                decimal afterTatekaekin = beforeTatekaekin;

                // changeが存在する場合
                if (isExistChange)
                {
                    List<int> urigeUnchinIds = isExistChange ? result.Where(d => d.change != null && d.change.SeikyuUnchin != null).Select(d => d.change.UriageUnchinId).ToList() : new List<int>();
                    // T_Check_Seikyu_Change　データが存在する場合、afterSeikyuUnchin（変更後運賃計）＝①＋②を返却
                    decimal totalSeikyuUnchinChange = result.Where(d => d.change != null).Sum(d => d.change.SeikyuUnchin ?? 0);
                    decimal totalSeikyuUnchinDetail = result.Where(d => !urigeUnchinIds.Contains(d.detail.UriageUnchinId)).Sum(d => d.detail.SeikyuUnchin ?? 0);
                    afterSeikyuUnchin = totalSeikyuUnchinChange + totalSeikyuUnchinDetail;

                    List<int> urigeUnchinIds2 = isExistChange ? result.Where(d => d.change != null && d.change.Tatekaekin != null).Select(d => d.change.UriageUnchinId).ToList() : new List<int>();
                    // T_Check_Seikyu_Change　データが存在する場合、afterTatekaekin（変更後立替計）＝①＋②を返却
                    decimal totalTatekaekinChange = result.Where(d => d.change != null).Sum(d => d.change.Tatekaekin ?? 0);
                    decimal totalTatekaekinDetail = result.Where(d => !urigeUnchinIds2.Contains(d.detail.UriageUnchinId)).Sum(d => d.detail.Tatekaekin ?? 0);
                    afterTatekaekin = totalTatekaekinChange + totalTatekaekinDetail;
                }
                // changeが存在しない場合
                else
                {
                    // T_Check_Seikyu_Change　データが存在しない場合、T_Check_Seikyu_Detail．SeikyuUnchinのSumを返却
                    decimal totalSeikyuUnchinDetail = result.Sum(d => d.detail.SeikyuUnchin ?? 0);
                    afterSeikyuUnchin = totalSeikyuUnchinDetail;

                    // T_Check_Seikyu_Change　データが存在しない場合、T_Check_Seikyu_Detail．TatekaekinのSumを返却
                    decimal totalTatekaekinDetail = result.Sum(d => d.detail.Tatekaekin ?? 0);
                    afterTatekaekin = totalTatekaekinDetail;
                }

                item.changeCount = changeCount;
                item.ankenCount = ankenCount;
                item.detailCount = detailCount;
                item.beforeSeikyuUnchin = beforeSeikyuUnchin;
                item.beforeTatekaekin = beforeTatekaekin;
                item.afterSeikyuUnchin = afterSeikyuUnchin;
                item.afterTatekaekin = afterTatekaekin;
            }

            return checkSeikyu;
        }

        /// <summary>
        /// 請求書一覧の取得
        /// </summary>
        /// <param name="idInt">ID（会社ID）/（担当ID）</param>
        /// <param name="isCompany">True:（会社ID）/False:（担当ID）</param>
        /// <param name="fromYm">請求年月（開始）</param>
        /// <param name="toYm">請求年月（終了）</param>
        /// <param name="zeiKubunIntValue">税区分</param>
        /// <param name="statusIntValue">ステータス（1: 全て 2: 未印刷 3: 印刷済）</param>
        /// <returns>請求書一覧</returns>
        public async Task<IEnumerable<InvoiceDto>> GetInvoiceList(int idInt, bool isCompany, DateTime? fromYm, DateTime? toYm, int? shimeDay, int zeiKubunIntValue, int statusIntValue)
        {
            List<InvoiceDto> invoices = new List<InvoiceDto>();

            int[] zeiKubun = { ZeiKubun.課税, ZeiKubun.非課税 };
            if (zeiKubunIntValue == ZeiKubun.課税)
            {
                zeiKubun = new int[] { ZeiKubun.課税 };
            }
            if (zeiKubunIntValue == ZeiKubun.非課税)
            {
                zeiKubun = new int[] { ZeiKubun.非課税 };
            }

            DateTime? lastDayOfToYm = toYm != null ? toYm?.AddMonths(1).AddDays(-1) : null;

            IEnumerable<InvoiceQueryDto> data = await _printSeikyuRepository.GetInvoiceList(idInt, isCompany, fromYm, lastDayOfToYm, zeiKubun, shimeDay);

            if (!data.Any())
            {
                return invoices;
            }

            if (statusIntValue == PrintKubun.未印刷)
            {
                data = data.Where(d => d.printRireki == null || d.printRireki.PrintRirekiId == 0).ToList();
            }
            if (statusIntValue == PrintKubun.印刷済)
            {
                data = data.Where(d => d.printRireki != null && d.printRireki.PrintRirekiId != 0).ToList();
            }

            foreach (var item in data)
            {
                InvoiceDto existingObj = invoices.FirstOrDefault(o => o.id == item.printSeikyu.PrintSeikyuId);

                if (existingObj == null)
                {
                    invoices.Add(new InvoiceDto
                    {
                        id = item.printSeikyu.PrintSeikyuId,
                        details = new List<InvoiceDetailDto> {
                            new()
                            {
                                displayDate = item.printSeikyuDetail.DisplayDate?.ToString(DateFormat.DATE_JP),
                                syaban = item.printSeikyuDetail.Syaban,
                                syasyuKataName = item.printSeikyuDetail.SyasyuKataName,
                                tsumi = item.printSeikyuDetail.Tsumi,
                                oroshi = item.printSeikyuDetail.Oroshi,
                                workName = item.printSeikyuDetail.WorkName,
                                luggage = item.printSeikyuDetail.Luggage,
                                qty = item.printSeikyuDetail.Qty.ToString(),
                                unit = item.printSeikyuDetail.Unit.ToString(),
                                unitPrice = item.printSeikyuDetail.UnitPrice ?? 0,
                                seikyuUnchin = item.printSeikyuDetail.SeikyuUnchin ?? 0,
                                warimashi1 = item.printSeikyuDetail.Warimashi1 ?? 0,
                                warimashi2 = item.printSeikyuDetail.Warimashi2 ?? 0,
                                warimashi3 = item.printSeikyuDetail.Warimashi3 ?? 0,
                                warimashi4 = item.printSeikyuDetail.Warimashi4 ?? 0,
                                warimashi5 = item.printSeikyuDetail.Warimashi5 ?? 0,
                                tatekaekin = item.printSeikyuDetail.Tatekaekin ?? 0,
                                seikyuTotal = item.printSeikyuDetail.SeikyuTotal ?? 0,
                                remarks = string.Join(" ", new[] { item.printSeikyuDetail.Remarks1, item.printSeikyuDetail.Remarks2 }.Where(s => !string.IsNullOrWhiteSpace(s))),
                            }
                        },
                        ankenCount = 0,
                        detailCount = 0,
                        printDatetime = item.printRireki?.PrintRirekiId != 0 ? item.printRireki?.PrintDatetime.ToString(DateFormat.DATE_JP) : null,
                        printPattern = item.printSeikyu.PrintPattern,
                        printUser = item?.printRireki?.PrintRirekiId != 0 ? item?.customerTantou.TantouNameAbbr : null,
                        seikuyuAmount = item.printSeikyu.SeikyuAmount,
                        seikuyuTotalAmount = item.printSeikyu.SeikyuTotalAmount,
                        seikyuMonth = item.printSeikyu.SeikyuMonth.ToString(DateFormat.MONTH_JP),
                        shimeDay = item.printSeikyu.ShimeDay.ToString(),
                        status = item?.printRireki?.PrintRirekiId != 0 ? StatusPrint.Printed : StatusPrint.Unprinted,
                        zeiKubun = item.printSeikyu.ZeiKubun,
                    });
                }
                else
                {
                    if (item.printRireki?.PrintRirekiId != 0)
                    {
                        DateTime beforeDate = DateTime.ParseExact(existingObj.printDatetime, DateFormat.DATE_JP, CultureInfo.InvariantCulture);
                        DateTime afterDate = DateTime.ParseExact(item.printRireki?.PrintDatetime.ToString(DateFormat.DATE_JP), DateFormat.DATE_JP, CultureInfo.InvariantCulture);
                        if (beforeDate < afterDate)
                        {
                            existingObj.printDatetime = afterDate.ToString(DateFormat.DATE_JP);
                            existingObj.printUser = item?.printRireki?.PrintRirekiId != 0 ? item?.customerTantou.TantouNameAbbr : null;
                        }
                    }
                    existingObj.details.Add(
                        new InvoiceDetailDto
                        {
                            displayDate = item.printSeikyuDetail.DisplayDate?.ToString(DateFormat.DATE_JP),
                            syaban = item.printSeikyuDetail.Syaban,
                            syasyuKataName = item.printSeikyuDetail.SyasyuKataName,
                            tsumi = item.printSeikyuDetail.Tsumi,
                            oroshi = item.printSeikyuDetail.Oroshi,
                            workName = item.printSeikyuDetail.WorkName,
                            luggage = item.printSeikyuDetail.Luggage,
                            qty = item.printSeikyuDetail.Qty.ToString(),
                            unit = item.printSeikyuDetail.Unit.ToString(),
                            unitPrice = item.printSeikyuDetail.UnitPrice ?? 0,
                            seikyuUnchin = item.printSeikyuDetail.SeikyuUnchin ?? 0,
                            warimashi1 = item.printSeikyuDetail.Warimashi1 ?? 0,
                            warimashi2 = item.printSeikyuDetail.Warimashi2 ?? 0,
                            warimashi3 = item.printSeikyuDetail.Warimashi3 ?? 0,
                            warimashi4 = item.printSeikyuDetail.Warimashi4 ?? 0,
                            warimashi5 = item.printSeikyuDetail.Warimashi5 ?? 0,
                            tatekaekin = item.printSeikyuDetail.Tatekaekin ?? 0,
                            seikyuTotal = item.printSeikyuDetail.SeikyuTotal ?? 0,
                            remarks = string.Join(" ", new[] { item.printSeikyuDetail.Remarks1, item.printSeikyuDetail.Remarks2 }.Where(s => !string.IsNullOrWhiteSpace(s))),
                        }
                    );
                }
            }

            foreach (var item in invoices)
            {
                int id = item.id;
                List<InvoiceQueryDto> result = data.Where(d => d.printSeikyu.PrintSeikyuId == id).ToList();

                int ankenCount = result.Select(d => d.printSeikyuDetail.AnkenId).Distinct().Count();
                int detailCount = result.Count(d => d.printSeikyuDetail != null);

                item.ankenCount = ankenCount;
                item.detailCount = detailCount;
            }

            return invoices;
        }

        /// <summary>
        /// 請求書発行の取得
        /// </summary>
        /// <param name="printSeikyuId">請求印刷ID</param>
        /// <param name="id">ID</param>
        /// <param name="isCompany">True:（会社ID）/False:（担当ID）</param>
        /// <returns>請求書とレポートレイアウトのDTO</returns>
        public async Task<InvoiceAndReportLayoutDto> GetInvoiceDetailAsync(int printSeikyuId, int id, bool isCompany)
        {
            IEnumerable<TReportLayout> reportLayout = await _reportLayoutRepository.GetReportLayoutListAsync(CodeData.請求書);

            InvoiceAndReportLayoutDto invoiceAndReportLayout = new()
            {
                printSeikyu = null,
                reportLayout = new List<ReportLayoutDto>(),
                portalInfoIds = new List<int>(),
            };

            IEnumerable<TPrintSeikyu> dataByPrintSeikyuId = await _printSeikyuRepository.GetByprintSeikyuIdAsync(printSeikyuId);

            if (!dataByPrintSeikyuId.Any())
            {
                return invoiceAndReportLayout;
            }

            IEnumerable<InvoiceQueryDto> printSeikyu = await _seikyuRepository.GetInvoiceListByPrintSeikyuIdAsync(printSeikyuId);

            int seikyuId = printSeikyu.FirstOrDefault()?.printSeikyu?.SeikyuId ?? 0;
            IEnumerable<TPrintParameter> printParameters = await _printParameterRepository.GetListBySeikyuIdAsync(seikyuId);

            IEnumerable<TPortalInfo> portalInfos = await GetTPortalInfoByPrintParameterAsync(printParameters, id, isCompany);

            if (portalInfos.Any())
            {
                invoiceAndReportLayout.portalInfoIds = portalInfos.Select(x => x.PortalInfoId).ToList();
            }

            for (int i = 0; i < reportLayout.Count(); i++)
            {
                invoiceAndReportLayout.reportLayout.Add(ReportLayoutDto.FromEntity(reportLayout.ElementAt(i)));
            }

            if (!printSeikyu.Any())
            {
                return invoiceAndReportLayout;
            }

            InvoiceDto invoice = SetInvoiceQueryToDto(printSeikyu.FirstOrDefault());
            if (invoice != null)
            {
                foreach (var item in printSeikyu)
                {
                    invoice.details.Add(InvoiceDetailDto.FromEntity(item.printSeikyuDetail));
                }

                List<InvoiceQueryDto> result = printSeikyu.Where(d => d.printSeikyu.PrintSeikyuId == printSeikyuId).ToList();
                int ankenCount = result.Select(d => d.printSeikyuDetail.AnkenId).Distinct().Count();
                int detailCount = result.Count(d => d.printSeikyuDetail != null);

                invoice.ankenCount = ankenCount;
                invoice.detailCount = detailCount;
                invoiceAndReportLayout.printSeikyu = invoice;
            }
            
            return invoiceAndReportLayout;
        }

        /// <summary>
        /// データ変換（InvoiceQueryDto→InvoiceDto）
        /// </summary>
        /// <param name="dto">InvoiceQueryDto</param>
        /// <returns>InvoiceDto</returns>
        private static InvoiceDto SetInvoiceQueryToDto(InvoiceQueryDto dto)
            => dto == null ? null : new()
            {
                id = dto.printSeikyu.PrintSeikyuId,
                details = new List<InvoiceDetailDto> { },
                ankenCount = 0,
                detailCount = 0,
                printDatetime = dto.printRireki?.PrintRirekiId != 0 ? dto.printRireki?.PrintDatetime.ToString(DateFormat.DATE_JP) : null,
                printPattern = dto.printSeikyu.PrintPattern,
                printUser = dto.printRireki?.PrintRirekiId != 0 ? dto.customerTantou.TantouNameAbbr : null,
                seikuyuAmount = dto.printSeikyu.SeikyuAmount,
                seikuyuTotalAmount = dto.printSeikyu.SeikyuTotalAmount,
                seikyuMonth = dto.printSeikyu.SeikyuMonth.ToString(DateFormat.MONTH_JP),
                shimeDay = dto.printSeikyu.ShimeDay.ToString(),
                status = dto.printRireki?.PrintRirekiId != 0 ? StatusPrint.Printed : StatusPrint.Unprinted,
                zeiKubun = dto.printSeikyu.ZeiKubun,
                seikyuId = dto.printSeikyu.SeikyuId,
            };

        /// <summary>
        /// ポータル情報の取得
        /// </summary>
        /// <param name="printParameters">印刷パラメータのリスト</param>
        /// <param name="idInt">ID</param>
        /// <param name="isCompany">True:（会社ID）/False:（担当ID）</param>
        /// <returns>ポータル情報のリスト</returns>
        private async Task<IEnumerable<TPortalInfo>> GetTPortalInfoByPrintParameterAsync(IEnumerable<TPrintParameter> printParameters, int idInt, bool isCompany)
        {
            List<int> printParameterIds = printParameters.Select(x => x.PrintId).ToList();

            return isCompany ?
                await _portalInfoRepository.GetTPortalInfoByPrintIdAndCompanyId(printParameterIds, idInt) :
                await _portalInfoRepository.GetTPortalInfoByPrintIdAndCustomerTantouId(printParameterIds, idInt);
        }
    }
}
