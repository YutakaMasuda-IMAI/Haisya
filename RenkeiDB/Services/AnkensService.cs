using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.AnkenDto.AnkenChangeHistoryDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemConstants;

namespace RenkeiDB.Services
{
    public class AnkensService : IAnkensService
    {
        private readonly IRenkeiAnkenRepository _ankensRepo;
        private readonly IRenkeiAnkenLuggageRepository _ankenLuggageRepo;
        private readonly IRenkeiAnkenEquipmentRepository _ankenEquipmentRepo;
        private readonly IRenkeiAnkenPointRepository _ankenPointRepo;
        private readonly ICompanyRepository _companyRepo;
        private readonly IRenkeiAnkenDetailRepository _ankenDetailRepo;
        private readonly ISyaryoRepository _syaryoRepository;
        private readonly IRenkeiAnkenCheckRepository _ankenCheckRepository;
        private readonly IRenkeiAnkenSecureCheckRepository _ankenSecureCheckRepository;
        private readonly IRenkeiAnkenRepository _renkeiAnkenRepository;
        private readonly IRenkeiAnkenDetailRepository _renkeiAnkenDetailRepository;
        private readonly IRenkeiAnkenPointRepository _renkeiAnkenPointRepository;
        private readonly IRenkeiAnkenLuggageRepository _renkeiAnkenLuggageRepository;
        private readonly IRenkeiAnkenEquipmentRepository _renkeiAnkenEquipmentRepository;
        private readonly ICompanyPortalRepository _companyPortalRepo;
        private readonly IShareSyaryoRepository _shareSyaryoRepo;
        private readonly ICustomerPortalRepository _customerPortalRepo;

        public AnkensService(IRenkeiAnkenRepository ankenRepo, IRenkeiAnkenLuggageRepository ankenLuggageRepo,
            IRenkeiAnkenEquipmentRepository ankenEquipmentRepo, IRenkeiAnkenPointRepository ankenPointRepo,
            ICompanyRepository companyRepo, IRenkeiAnkenDetailRepository ankenDetailRepo, ISyaryoRepository syaryoRepository,
            IRenkeiAnkenCheckRepository ankenCheckRepository, IRenkeiAnkenSecureCheckRepository ankenSecureCheckRepository,
            IRenkeiAnkenRepository renkeiAnkenRepository,
            IRenkeiAnkenDetailRepository renkeiAnkenDetailRepository,
            IRenkeiAnkenPointRepository renkeiAnkenPointRepository,
            IRenkeiAnkenLuggageRepository renkeiAnkenLuggageRepository,
            IRenkeiAnkenEquipmentRepository renkeiAnkenEquipmentRepository,
            ICompanyPortalRepository companyPortalRepo,
            IShareSyaryoRepository shareSyaryoRepo,
            ICustomerPortalRepository customerPortalRepo)
        {
            _ankensRepo = ankenRepo;
            _ankenEquipmentRepo = ankenEquipmentRepo;
            _ankenLuggageRepo = ankenLuggageRepo;
            _ankenPointRepo = ankenPointRepo;
            _companyRepo = companyRepo;
            _ankenDetailRepo = ankenDetailRepo;
            _syaryoRepository = syaryoRepository;
            _ankenCheckRepository = ankenCheckRepository;
            _ankenSecureCheckRepository = ankenSecureCheckRepository;
            _renkeiAnkenRepository = renkeiAnkenRepository;
            _renkeiAnkenDetailRepository = renkeiAnkenDetailRepository;
            _renkeiAnkenPointRepository = renkeiAnkenPointRepository;
            _renkeiAnkenLuggageRepository = renkeiAnkenLuggageRepository;
            _renkeiAnkenEquipmentRepository = renkeiAnkenEquipmentRepository;
            _companyPortalRepo = companyPortalRepo;
            _shareSyaryoRepo = shareSyaryoRepo;
            _customerPortalRepo = customerPortalRepo;
        }

        /// <summary>
        /// 案件詳細の取得
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <returns>案件詳細</returns>
        public async Task<RenkeiAnkenDto> GetAnkenDetailAsync(int id, int userId)
        {
            JoinRenkeiAnken anken = await _ankensRepo.GetDetailAsync(id);
            if (anken?.Anken != null)
            {
                IList<T_Renkei_Anken_Luggage> ankenLuggages = await _ankenLuggageRepo.GetLuggages(id, anken.Anken.Renkei_Anken_Latest_Order);
                IList<T_Renkei_Anken_Equipment> ankenEquipments = await _ankenEquipmentRepo.GetEquipments(id, anken.Anken.Renkei_Anken_Latest_Order);
                IList<T_Renkei_Anken_Point> ankenPoints = await _ankenPointRepo.GetPoints(id, anken.Anken.Renkei_Anken_Latest_Order);

                if (anken.Anken.Renkei_Anken_Status == 7)
                {
                    if (anken.AnkenCheck == null ||
                        (anken.AnkenCheck != null && anken.AnkenDetail.Insert_Datetime > anken.AnkenCheck.Insert_Datetime))
                    {
                        T_Renkei_Anken_Check ankenCheck = new()
                        {
                            Renkei_Anken_ID = anken.Anken.Renkei_Anken_ID,
                            Check_Status = 1,
                            Insert_Datetime = DateTime.Now,
                            Insert_User = userId,
                        };

                        if (anken.AnkenCheck != null)
                            await _ankenCheckRepository.DeleteAsync(anken.AnkenCheck);

                        await _ankenCheckRepository.CreateAsync(ankenCheck);
                    }
                }

                if (anken.AnkenSecure?.Renkei_Anken_Secure_Status == 7 && anken.AnkenSecureCheck == null)
                {
                    T_Renkei_Anken_Secure_Check ankenSecureCheck = new()
                    {
                        Renkei_Anken_Secure_ID = anken.AnkenSecure.Renkei_Anken_Secure_ID,
                        Check_Status = 1,
                        Insert_Datetime = DateTime.Now,
                        Insert_User = userId,
                    };

                    await _ankenSecureCheckRepository.CreateAsync(ankenSecureCheck);
                }
                return Mapper.ConvertToRenkeiAnkenEntity(anken, ankenLuggages, ankenEquipments, ankenPoints);
            }
            return null;
        }

        /// <summary>
        /// 案件を更新します。
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="dto">更新データ</param>
        /// <returns>APIレスポンスを含むタスク</returns>
        public async Task<ApiResponse> UpdateAnkenAsync(int id, int userId, int companyId, UpdateAnkenDto dto)
        {
            T_Renkei_Anken anken = await _ankensRepo.FindByCondition(x => x.Renkei_Anken_ID == id).FirstOrDefaultAsync();
            M_Company company = await _companyRepo.FindByCondition(x => x.Renkei_Company_ID == companyId).FirstOrDefaultAsync();
            M_Syaryo syaryo = await _syaryoRepository.FindByCondition(x => x.Syaryo_ID == dto.syasyu).FirstOrDefaultAsync();
            if (anken == null || company == null)
            {
                return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
            }

            try
            {
                await _ankensRepo.BeginTransactionAsync();

                int? latestOrder = _ankensRepo.CreateOrUpdateWithSpRenkeiAnken(2, new T_Renkei_Anken() { Renkei_Anken_ID = id });

                if (latestOrder == null)
                {
                    await _ankensRepo.RollbackTransactionAsync();
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                T_Renkei_Anken_Detail ankenDetail = Mapper.ConvertToRenkeiAnkenDetailEntity(dto, id, userId, companyId, latestOrder.Value, company, syaryo);
                await _ankenDetailRepo.CreateAsync(ankenDetail);

                List<T_Renkei_Anken_Point> ankenPoints = new List<T_Renkei_Anken_Point>();
                UpdateAnkenPointDto firstPoint = dto.ankenPoints.FirstOrDefault(x => x.kubun == 1);

                if (firstPoint != null)
                    ankenPoints.Add(Mapper.ConvertToRenkeiAnkenPointEntity(firstPoint, 1, id, userId, latestOrder.Value));

                ankenPoints.AddRange(dto.ankenPoints.Where(x => x.kubun != 1 && x.kubun != 9)
                    .Select((x, i) => Mapper.ConvertToRenkeiAnkenPointEntity(x, i + 2, id, userId, latestOrder.Value)));

                UpdateAnkenPointDto lastPoint = dto.ankenPoints.FirstOrDefault(x => x.kubun == 9);
                if (lastPoint != null)
                    ankenPoints.Add(Mapper.ConvertToRenkeiAnkenPointEntity(lastPoint, ankenPoints.Last().Point_Order + 1, id, userId, latestOrder.Value));

                await _ankenPointRepo.CreatListAsync(ankenPoints);

                if (dto.ankenLuggages != null)
                    await _ankenLuggageRepo.CreatListAsync(dto.ankenLuggages.Select(x => Mapper.ConvertToRenkeiAnkenLuggageEntity(x, id, userId, latestOrder.Value)));

                if (dto.ankenEquipments != null)
                    await _ankenEquipmentRepo.CreatListAsync(dto.ankenEquipments.Select(x => Mapper.ConvertToRenkeiAnkenEquipmentEntity(x, id, userId, latestOrder.Value)));

                await _ankensRepo.EndTransactionAsync();

                return new() { Code = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _ankensRepo.RollbackTransactionAsync();
                return new() { Code = StatusCodes.Status500InternalServerError, Message = SystemConstants.Message.InternalServerError };
            }
        }

        /// <summary>
        /// 案件一覧の取得
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="oroshiAddress">卸し住所</param>
        /// <returns>案件一覧を含むタスク</returns>
        public async Task<IEnumerable<AnkensDto>> GetAnkenListAsync(int companyId, string oroshiAddress)
        {
            IEnumerable<JoinAnken> ankenList = await _ankensRepo.GetAnkensAsync(companyId, oroshiAddress);
            IEnumerable<AnkenGroupDto> ankenGroupById = ankenList
            .GroupBy(s => s.renkeiAnkenDetail?.Renkei_Anken_ID)
            .Select(g =>
            {
                JoinAnken firstAnken = g.FirstOrDefault();
                return new AnkenGroupDto
                {
                    renkeiAnken = firstAnken?.renkeiAnken,
                    company = firstAnken?.company,
                    renkeiAnkenPoint = g.Select(e => e.renkeiAnkenPoint).Distinct().ToList(),
                    renkeiAnkenDetail = firstAnken?.renkeiAnkenDetail,
                    renkeiAnkenLuggage = g.Select(e => e.renkeiAnkenLuggage).Distinct().ToList(),
                    renkeiAnkenEquipment = g.Select(e => e.renkeiAnkenEquipment).Distinct().ToList(),
                    mEquipment = g.Select(e => e.equipment).Distinct().ToList(),
                    mLuggage = g.Select(e => e.luggage).Distinct().ToList(),
                    mLuggageGroups = g.Select(e => e.mLuggageGroup).Distinct().ToList(),
                    mEquipmentGroups = g.Select(e => e.mEquipmentGroup).Distinct().ToList(),
                };
            })
            .AsEnumerable();
            return Mapper.ConvertToAnkenListEntity(ankenGroupById);
        }

        /// <summary>
        /// 案件変更履歴情報を取得する
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <returns>案件変更履歴情報を含むタスク</returns>
        public async Task<GetAnkenChangeDto> GetHistoryChangeAnkenInfo(int id)
        {
            //RepositoryからChangeAnkenInfoを取得する
            JoinAnkenChangeHistoryDto data = await _ankensRepo.GetHistoryChangeAnkenInfo(id);

            //renkeiAnkenがNULLの場合、空オブジェクトを返す
            if (data.renkeiAnken == null)
            {
                return new GetAnkenChangeDto();
            }

            //ローカル変数を作成する
            Dictionary<string, List<ChangeDto>> dict_result = new Dictionary<string, List<ChangeDto>>();
            ChangeDto item = null;

            #region Create data for T_Renkei_Anken_Detail
            //T_Renkei_Anken_Detail DBに新しい辞書マッピングを作成する
            T_Renkei_Anken_Detail a = new();
            Dictionary<string, string> dict_defineNameDetailDB = new Dictionary<string, string>
            {
                {KeyHistoryChangeAnken.WorkName, NameHistoryChangeAnken.WorkName },
                {KeyHistoryChangeAnken.SyasyuDisplay, NameHistoryChangeAnken.SyasyuDisplay },
                {KeyHistoryChangeAnken.Daisuu, NameHistoryChangeAnken.Daisuu },
                {KeyHistoryChangeAnken.RouteTypeDisplay, NameHistoryChangeAnken.RouteTypeDisplay },
                {KeyHistoryChangeAnken.RouteTotalTime, NameHistoryChangeAnken.RouteTotalTime },
                {KeyHistoryChangeAnken.RouteTotalDistance, NameHistoryChangeAnken.RouteTotalDistance },
                {KeyHistoryChangeAnken.RouteTotalToll, NameHistoryChangeAnken.RouteTotalToll },
                {KeyHistoryChangeAnken.RouteStdAllFreight, NameHistoryChangeAnken.RouteStdAllFreight },
                {KeyHistoryChangeAnken.SeikyuKubun, NameHistoryChangeAnken.SeikyuKubun },
                {KeyHistoryChangeAnken.BaseFee, NameHistoryChangeAnken.BaseFee },
                {KeyHistoryChangeAnken.ExtraCharge, NameHistoryChangeAnken.ExtraCharge },
                {KeyHistoryChangeAnken.Toll, NameHistoryChangeAnken.Toll },
                {KeyHistoryChangeAnken.Discount, NameHistoryChangeAnken.Discount },
                {KeyHistoryChangeAnken.GrossAmount, NameHistoryChangeAnken.GrossAmount },
                {KeyHistoryChangeAnken.TollKubun, NameHistoryChangeAnken.TollKubun },
                {KeyHistoryChangeAnken.TollMoney, NameHistoryChangeAnken.TollMoney },
                {KeyHistoryChangeAnken.TollRemarks, NameHistoryChangeAnken.TollRemarks },
                {KeyHistoryChangeAnken.LuggageDisplay, NameHistoryChangeAnken.LuggageDisplay },
                {KeyHistoryChangeAnken.EquipmentDisplay, NameHistoryChangeAnken.EquipmentDisplay },
                {KeyHistoryChangeAnken.SyabanrenrakuRemarks, NameHistoryChangeAnken.SyabanrenrakuRemarks },
            };

            // 高速金額のvalは高速代(Toll_Kubun)が一部or金額の場合のみセットして
            List<string> tollKubunPartAndAmount = new List<string> { TollKubun.一部, TollKubun.金額 };

            foreach (JoinRenkeiAnkenDetailDto joinDetail in data.renkeiAnkenDetails)
            {
                T_Renkei_Anken_Detail detail = joinDetail.renkeiAnkenDetail;
                M_CompanyUser companyUser = joinDetail.companyUser;

                // T_Renkei_Anken_Detailの値を取得する
                // detail.Renkei_Anken_Order == Renkei_Anken_Order - 1
                T_Renkei_Anken_Detail pre_detail = data.renkeiAnkenDetails
                    .Where(x => detail.Renkei_Anken_Order - 1 == x.renkeiAnkenDetail.Renkei_Anken_Order).FirstOrDefault()?.renkeiAnkenDetail;

                # region T_Renkei_Anken_Detail．TsumiTaskTime、T_Renkei_Anken_Detail．OroshiTaskTime
                string waitingLoadingTime = (!string.IsNullOrEmpty(detail.TsumiTaskTime) ? "積み：" : "") + detail.TsumiTaskTime +
                    ((string.IsNullOrEmpty(detail.TsumiTaskTime) || string.IsNullOrEmpty(detail.OroshiTaskTime)) ? "" : ";")
                    + (!string.IsNullOrEmpty(detail.OroshiTaskTime) ? "卸し：" : "") + detail.OroshiTaskTime;
                string pre_waitingLoadingTime = pre_detail == null ? "" :
                    ((!string.IsNullOrEmpty(pre_detail.TsumiTaskTime) ? "積み：" : "") + pre_detail.TsumiTaskTime +
                        ((string.IsNullOrEmpty(pre_detail.TsumiTaskTime) || string.IsNullOrEmpty(pre_detail.OroshiTaskTime)) ? "" : ";")
                        + (!string.IsNullOrEmpty(pre_detail.OroshiTaskTime) ? "卸し：" : "") + pre_detail.OroshiTaskTime);
                if (string.IsNullOrEmpty(pre_waitingLoadingTime))
                {
                    item = new ChangeDto()
                    {
                        val = waitingLoadingTime,
                        change = false
                    };
                }
                else
                {
                    item = new ChangeDto()
                    {
                        val = waitingLoadingTime,
                        change = CompareTwoObject(waitingLoadingTime, pre_waitingLoadingTime)
                    };
                }
                if (dict_result.ContainsKey(NameHistoryChangeAnken.WaitingTime))
                {

                    dict_result[NameHistoryChangeAnken.WaitingTime].Add(item);
                }
                else
                {
                    dict_result[NameHistoryChangeAnken.WaitingTime] = new List<ChangeDto> { item };
                }
                #endregion

                # region M_CompanyUser.Display_Name 
                item = new ChangeDto()
                {
                    val = companyUser.Display_Name,
                    change = false
                };

                if (dict_result.ContainsKey(NameHistoryChangeAnken.InsertUser))
                {

                    dict_result[NameHistoryChangeAnken.InsertUser].Add(item);
                }
                else
                {
                    dict_result[NameHistoryChangeAnken.InsertUser] = new List<ChangeDto> { item };
                }
                #endregion

                # region M_CompanyUser.Renkei_Anken_Order 
                item = new ChangeDto()
                {
                    val = detail.Renkei_Anken_Order.ToString(),
                    change = false
                };

                if (dict_result.ContainsKey(NameHistoryChangeAnken.HistoryNumber))
                {

                    dict_result[NameHistoryChangeAnken.HistoryNumber].Add(item);
                }
                else
                {
                    dict_result[NameHistoryChangeAnken.HistoryNumber] = new List<ChangeDto> { item };
                }
                #endregion

                # region M_CompanyUser.InsertDateTime 
                item = new ChangeDto()
                {
                    val = detail.Insert_Datetime.ToString(Format.DateTime),
                    change = false
                };

                if (dict_result.ContainsKey(NameHistoryChangeAnken.InsertDateTime))
                {

                    dict_result[NameHistoryChangeAnken.InsertDateTime].Add(item);
                }
                else
                {
                    dict_result[NameHistoryChangeAnken.InsertDateTime] = new List<ChangeDto> { item };
                }
                #endregion

                //他のフィールドを設定する
                foreach (var prop in detail.GetType().GetProperties())
                {
                    if (!dict_defineNameDetailDB.ContainsKey(prop.Name.ToString()))
                    {
                        continue;
                    }

                    item = null;
                    if (pre_detail == null)
                    {
                        item = new ChangeDto()
                        {
                            val = prop.GetValue(detail) == null ? "" : (prop.Name.ToString() == SeikyuKubun.ItemName ? (prop.GetValue(detail).ToString() == SeikyuKubun.Value.Temporary ? SeikyuKubun.LabelValue.Temporary : SeikyuKubun.LabelValue.Final) : prop.GetValue(detail).ToString()),
                            change = false
                        };
                    }
                    else
                    {
                        PropertyInfo pre_prop = pre_detail.GetType().GetProperties()
                            .Where(x => x.Name.ToString() == prop.Name.ToString()).FirstOrDefault();
                        bool isTollMoneyEmpty = prop.Name.ToString() == KeyHistoryChangeAnken.TollMoney && !tollKubunPartAndAmount.Contains(detail.Toll_Kubun.ToString());
                        bool isPreTollMoneyEmpty = prop.Name.ToString() == KeyHistoryChangeAnken.TollMoney && !tollKubunPartAndAmount.Contains(pre_detail.Toll_Kubun.ToString());
                        item = new ChangeDto()
                        {
                            val = prop.GetValue(detail) == null ? "" : (prop.Name.ToString() == SeikyuKubun.ItemName ? (prop.GetValue(detail).ToString() == SeikyuKubun.Value.Temporary ? SeikyuKubun.LabelValue.Temporary : SeikyuKubun.LabelValue.Final) : prop.GetValue(detail).ToString()),
                            change = CompareTwoObject(!isTollMoneyEmpty ? prop.GetValue(detail) : "", !isPreTollMoneyEmpty ? pre_prop.GetValue(pre_detail) : "")
                        };
                    }

                    string propName = dict_defineNameDetailDB[prop.Name.ToString()];

                    string newVal = item.val;

                    switch (propName)
                    {
                        case NameHistoryChangeAnken.TollKubun:
                            {
                                switch (item.val)
                                {
                                    case TollKubun.全高:
                                        newVal = TollKubun.Label.全高;
                                        break;
                                    case TollKubun.一部:
                                        newVal = TollKubun.Label.一部;
                                        break;
                                    case TollKubun.金額:
                                        newVal = TollKubun.Label.金額;
                                        break;
                                    case TollKubun.無し:
                                        newVal = TollKubun.Label.無し;
                                        break;
                                    case TollKubun.他:
                                        newVal = TollKubun.Label.他;
                                        break;
                                    default:
                                        newVal = "";
                                        break;
                                }

                                item.val = newVal;
                            }
                            break;
                        case NameHistoryChangeAnken.RouteTotalDistance:
                            if (!string.IsNullOrEmpty(newVal))
                                newVal += "km";
                            break;
                        case NameHistoryChangeAnken.TollMoney:
                            if (double.TryParse(item.val, out double moneyVal) && tollKubunPartAndAmount.Contains(detail.Toll_Kubun.ToString()))
                            {
                                newVal = moneyVal.ToString("#,0");
                                newVal += "円";
                            }
                            else
                                newVal = "";
                            break;
                        case NameHistoryChangeAnken.Discount:
                        case NameHistoryChangeAnken.RouteTotalToll:
                        case NameHistoryChangeAnken.Toll:
                        case NameHistoryChangeAnken.BaseFee:
                        case NameHistoryChangeAnken.ExtraCharge:
                        case NameHistoryChangeAnken.RouteStdAllFreight:
                        case NameHistoryChangeAnken.GrossAmount:
                            if (double.TryParse(item.val, out double tempVal))
                            {
                                newVal = tempVal.ToString("#,0");
                                newVal += "円";
                            }
                            else
                                newVal = "";
                            break;
                    }

                    item.val = newVal;

                    if (dict_result.ContainsKey(propName))
                    {

                        dict_result[propName].Add(item);
                    }
                    else
                    {
                        dict_result[propName] = new List<ChangeDto> { item };
                    }
                }
            }
            #endregion

            #region Create data for T_Renkei_Anken_Point
            //ローカル変数を作成する
            string key_buildingName = NameHistoryChangeAnken.StartBuildingName;
            string key_address = NameHistoryChangeAnken.StartAddress;
            string key_pointDateTime = NameHistoryChangeAnken.StartPointDateTime;
            string buildingName = "";
            string address = "";
            string pointDatetime = "";
            string pre_buildingName = "";
            string pre_address = "";
            string pre_pointDatetime = "";
            ChangeDto item_buidingName = null;
            ChangeDto item_address = null;
            ChangeDto item_pointDateTime = null;

            foreach (T_Renkei_Anken_Point point in data.renkeiAnkenPoints)
            {
                buildingName = point.BuildingName;
                address = point.Address;
                pointDatetime = (point.PointDate == null ? "" : DateTime.Parse(point.PointDate.ToString()).ToString("yyyy-MM-dd"))
                    + " " + point.PointTime;

                // T_Renkei_Anken_Pointの値を取得する
                // point.Renkei_Anken_Order == x.Renkei_Anken_Order - 1
                T_Renkei_Anken_Point pre_point = data.renkeiAnkenPoints
                    .Where(x => point.Renkei_Anken_Order - 1 == x.Renkei_Anken_Order && point.Kubun == x.Kubun).FirstOrDefault();

                pre_buildingName = pre_point == null ? buildingName : pre_point.BuildingName;
                pre_address = pre_point == null ? address : pre_point.Address;
                pre_pointDatetime = pre_point == null ? pointDatetime
                    : ((pre_point.PointDate == null ? "" : DateTime.Parse(pre_point.PointDate.ToString()).ToString("yyyy-MM-dd"))
                        + " " + pre_point.PointTime);

                //buidingName・address・pointDatetime用のChangeDtoを作成する
                item_buidingName = new ChangeDto
                {
                    val = buildingName,
                    change = CompareTwoObject(buildingName, pre_buildingName),
                };
                item_address = new ChangeDto
                {
                    val = address,
                    change = CompareTwoObject(address, pre_address)
                };
                item_pointDateTime = new ChangeDto
                {
                    val = pointDatetime,
                    change = CompareTwoObject(pointDatetime, pre_pointDatetime)
                };

                //ローカル変数の値を設定するために、Kubun と SEKubunをチェックする
                if (point.Kubun == 1 && point.SEKubun == Kubun.Types.TypeOne)
                {
                    key_buildingName = NameHistoryChangeAnken.StartBuildingName;
                    key_address = NameHistoryChangeAnken.StartAddress;
                    key_pointDateTime = NameHistoryChangeAnken.StartPointDateTime;
                }
                else if (point.Kubun == 9 && point.SEKubun == Kubun.Types.TypeNine)
                {
                    key_buildingName = NameHistoryChangeAnken.EndBuildingName;
                    key_address = NameHistoryChangeAnken.EndAddress;
                    key_pointDateTime = NameHistoryChangeAnken.EndPointDateTime;
                }
                else
                {
                    key_buildingName = "";
                    key_address = "";
                    key_pointDateTime = "";
                }

                //BuildingNameの値を設定する
                if (!string.IsNullOrEmpty(key_buildingName))
                {
                    if (dict_result.ContainsKey(key_buildingName))
                    {

                        dict_result[key_buildingName].Add(item_buidingName);
                    }
                    else
                    {
                        dict_result[key_buildingName] = new List<ChangeDto> { item_buidingName };
                    }
                }

                //addressの値を設定する
                if (!string.IsNullOrEmpty(key_address))
                {
                    if (dict_result.ContainsKey(key_address))
                    {

                        dict_result[key_address].Add(item_address);
                    }
                    else
                    {
                        dict_result[key_address] = new List<ChangeDto> { item_address };
                    }
                }

                //pointDateTimeを設定する
                if (!string.IsNullOrEmpty(key_pointDateTime))
                {
                    if (dict_result.ContainsKey(key_pointDateTime))
                    {

                        dict_result[key_pointDateTime].Add(item_pointDateTime);
                    }
                    else
                    {
                        dict_result[key_pointDateTime] = new List<ChangeDto> { item_pointDateTime };
                    }
                }
            }
            #endregion

            return new GetAnkenChangeDto
            {
                renkeiAnkenNo = data.renkeiAnken.Renkei_Anken_No,
                changes = dict_result
                    .OrderBy(dict => dict.Key)
                    .Select(dict => new HistoryChangeAnkenDto { name = Regex.Replace(dict.Key.Substring(3), @"\([^)]*\)", ""), values = dict.Value.ToList() })
                    .ToList(),
            };
        }

        /// <summary>
        /// 比較処理
        /// </summary>
        /// <param name="a">Object a</param>
        /// <param name="b">Object b</param>
        /// <returns>
        /// true: 同じ値ではない場合
        /// false:同じ値の場合
        /// </returns>
        private bool CompareTwoObject(object a, object b)
        {
            string str_a = a == null ? "" : a.ToString();
            string str_b = b == null ? "" : b.ToString();
            return !str_a.Equals(str_b);
        }

        /// <summary>
        /// 案件情報　新規登録
        /// </summary>
        /// <param name="idInt"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ApiResponse> CreateAnken(UserLoginDto user, AnkenCreateDto dto)
        {
            string ankenNo = await _renkeiAnkenRepository.GetNoSPTRenkeiAnkenNo(DateTime.Now.Date, SystemEnums.ZeroUme.CREATE_RENKEI_ANKEN);
            //string ankenNo = "A20240000010";
            await _renkeiAnkenRepository.BeginTransactionAsync();

            M_Company company = await _companyRepo.GetByIdAsync(user.CompanyId);
            M_Syaryo syaryo = await _syaryoRepository.GetByIdAsync(dto.syasyu.Value);

            try
            {
                if (dto.syasyu == 0 || syaryo == null) return await RollbackWithResponse(StatusCodes.Status400BadRequest, SystemConstants.Message.DataNotFound);

                if (ankenNo == null) return await RollbackWithResponse(StatusCodes.Status400BadRequest, SystemConstants.Message.DataNotFound);

                T_Renkei_Anken entity = Mapper.CreateRenkeiAnkenEntity(user, ankenNo);
                T_Renkei_Anken anken = await _renkeiAnkenRepository.CreateEntityAsync(entity);

                T_Renkei_Anken_Detail detail = Mapper.CreateRenkeiAnkenDetail(user, dto, company, syaryo, anken.Renkei_Anken_ID);

                List<T_Renkei_Anken_Point> pointsKubun1 = FilterAndMapPoints(dto.ankenPoints, SystemConstants.Kubun.One,
                    entity.Renkei_Anken_ID, user);

                List<T_Renkei_Anken_Point> pointsKubun2To4 = FilterAndMapPoints(dto.ankenPoints, SystemConstants.Kubun.Two,
                    SystemConstants.Kubun.Four, entity.Renkei_Anken_ID, user);

                //上記の＜T_Renkei_Anken_Point＞--INS②で登録したデータのMAX　Point_Order＋1
                int maxPointOrder = pointsKubun2To4.Any() ? pointsKubun2To4.Max(x => x.Point_Order) + 1 : (pointsKubun1.Any() ? pointsKubun1.Max(x => x.Point_Order) + 1 : 1);
                List<T_Renkei_Anken_Point> pointsKubun9 = FilterAndMapPoints(dto.ankenPoints, SystemConstants.Kubun.Nine,
                    entity.Renkei_Anken_ID, user, maxPointOrder);

                if (HasDuplicates(pointsKubun1) || HasDuplicates(pointsKubun2To4) || HasDuplicates(pointsKubun9))
                {
                    return await RollbackWithResponse(StatusCodes.Status400BadRequest, SystemConstants.Message.AnkenPointDuplicate);
                }

                List<T_Renkei_Anken_Luggage> luggages = dto.ankenLuggages.Select(x => Mapper.MappingDataAnkenLuggage(x, entity.Renkei_Anken_ID, user)).ToList();

                List<T_Renkei_Anken_Equipment> equipments = dto.ankenEquipments.Select(x => Mapper.MappingDataAnkenEquipment(x, entity.Renkei_Anken_ID, user)).ToList();

                await _renkeiAnkenDetailRepository.CreateAsync(detail);
                await _renkeiAnkenPointRepository.CreatListAsync(pointsKubun1);
                await _renkeiAnkenPointRepository.CreatListAsync(pointsKubun2To4);
                await _renkeiAnkenPointRepository.CreatListAsync(pointsKubun9);
                await _renkeiAnkenLuggageRepository.CreatListAsync(luggages);
                await _renkeiAnkenEquipmentRepository.CreatListAsync(equipments);

                await _renkeiAnkenRepository.EndTransactionAsync();
                return new ApiResponse { Code = StatusCodes.Status200OK };
            }
            catch (Exception)
            {
                return await RollbackWithResponse(StatusCodes.Status500InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 重複チェック
        /// </summary>
        /// <param name="points">T_Renkei_Anken_Pointリスト</param>
        /// <returns></returns>
        private bool HasDuplicates(IEnumerable<T_Renkei_Anken_Point> points)
        {
            return points
                .GroupBy(p => new { p.Renkei_Anken_ID, p.Renkei_Anken_Order, p.Kubun, p.Point_Order }).Count() != points.Count();
        }

        /// <summary>
        /// 地図ポイントの取得
        /// </summary>
        /// <param name="points"></param>
        /// <param name="kubun"></param>
        /// <param name="renkeiAnkenId"></param>
        /// <param name="user"></param>
        /// <param name="maxPointOrder"></param>
        /// <returns></returns>
        private List<T_Renkei_Anken_Point> FilterAndMapPoints(IEnumerable<AnkenPointCreateDto> points, int kubun, int renkeiAnkenId,
            UserLoginDto user, int maxPointOrder = 0)
        {
            return points
                .Where(x => x.kubun == kubun)
                .Select(x => Mapper.MappingDataAnkenPoint(x, renkeiAnkenId, user, maxPointOrder))
                .ToList();
        }

        /// <summary>
        /// 地図ポイントの取得
        /// </summary>
        /// <param name="points"></param>
        /// <param name="minKubun"></param>
        /// <param name="maxKubun"></param>
        /// <param name="renkeiAnkenId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private List<T_Renkei_Anken_Point> FilterAndMapPoints(IEnumerable<AnkenPointCreateDto> points,
            int minKubun, int maxKubun, int renkeiAnkenId, UserLoginDto user)
        {
            return points
                .Where(x => x.kubun >= minKubun && x.kubun <= maxKubun)
                .Select(x => Mapper.MappingDataAnkenPoint(x, renkeiAnkenId, user))
                .ToList();
        }


        /// <summary>
        /// ロールバック
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<ApiResponse> RollbackWithResponse(int statusCode, string message)
        {
            await _renkeiAnkenRepository.RollbackTransactionAsync();
            return new ApiResponse
            {
                Code = statusCode,
                Message = message
            };
        }

        /// <summary>
        /// 空車車両一覧の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <returns></returns>
        public async Task<CompanyPortalsDto> GetInfoListKeepEmptyCarAsync(int companyId, int branchId)
        {
            CompanyPortalsDto result = new();

            IEnumerable<JoinAnkenCsv> renkeiAnkens = await _ankensRepo.GetRenkeiAnkensAsync(companyId, branchId);
            IEnumerable<JoinShareSyaryoDto> shareSyaryos = await _shareSyaryoRepo.GetKeepEmptyCarAsync(companyId, branchId);

            result.keepEmptyCars = shareSyaryos.Where(x => x.shareSyaryo.Share_Syaryo_Status == 1).
                Select(x => Mapper.ConvertToKeepEmptyCarEntity(x));

            return result;
        }

        /// <summary>
        /// 受注案件CSVの取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <returns></returns>
        public async Task<List<JuchuAnkenDto>> GetOrdersAsync(int companyId, int branchId)
        {
            IEnumerable<JoinCompanyPortalDto> data = await _companyPortalRepo.GetJuchuAnkensAsync(companyId, branchId);
            IEnumerable<JoinShareSyaryoDto> syaryos = await _shareSyaryoRepo.GetKeepEmptyCarAsync(companyId, branchId);
            IEnumerable<AnkenSecureDto> secures = await _customerPortalRepo.GetSecuresAsync(
                data.Select(d => d.renkeiAnken.Renkei_Anken_ID).Distinct().ToArray());

            List<JuchuAnkenDto> ankens = new List<JuchuAnkenDto>();
            foreach (var item in data)
            {
                T_Renkei_Anken ra = item.renkeiAnken;
                JoinShareSyaryoDto syaryo = syaryos?.FirstOrDefault(y => y.shareSyaryoSecure?.Renkei_Anken_ID == ra.Renkei_Anken_ID);
                AnkenSecureDto secure = secures.Where(item => item.renkeiAnkenSecure.Renkei_Anken_ID == ra.Renkei_Anken_ID).FirstOrDefault();

                #region Detect status
                string status = null;
                int ras = ra.Renkei_Anken_Status;
                if (ras == NumberAnkenStatus.Confirmed || ras == NumberAnkenStatus.Temporarily)
                {
                    status =
                        secure?.renkeiAnkenSecure == null ? RenkeiAnkenStatus.Ordered :
                        ras == NumberAnkenStatus.Confirmed ? RenkeiAnkenStatus.Confirmed :
                        RenkeiAnkenStatus.Temporarily;
                }

                if (secure?.renkeiAnkenSecure?.Renkei_Anken_Secure_Status == NumberAnkenStatus.Others)
                {
                    status = RenkeiAnkenStatus.Others;
                }
                if (ras == NumberAnkenStatus.Cancel)
                {
                    status = RenkeiAnkenStatus.Cancel;
                }
                if (ras == NumberAnkenStatus.ChangeRequest)
                {
                    status = RenkeiAnkenStatus.ChangeRequest;
                }

                if ((ras == NumberAnkenStatus.ChangeRequest && item.renkeiAnkenCheck != null)
                    || (secure?.renkeiAnkenSecure?.Renkei_Anken_Secure_Status == NumberAnkenStatus.ChangeRequest && secure?.renkeiAnkenSecureCheck != null))
                {
                    status = RenkeiAnkenStatus.ChangeConfirmation;
                }
                #endregion

                T_Renkei_Anken_Detail ad = item.renkeiAnkenDetail;
                T_Renkei_Anken_Point ps = item.renkeiAnkenPointS;
                T_Renkei_Anken_Point pe = item.renkeiAnkenPointE;
                T_Share_Luggage sl = item.shareLuggage;

                ankens.Add(new JuchuAnkenDto()
                {
                    status = status,
                    id = ra.Renkei_Anken_ID,
                    tumiAddress = ps?.Address,
                    oroshiAddress = pe?.Address,
                    ankenNo = ra.Renkei_Anken_No,
                    syasyuDisplay = ad?.SyasyuDisplay,
                    syaban = secure?.renkeiAnkenSecure?.Syaban_Number,
                    driverName = secure?.renkeiAnkenSecure?.Display_Name,
                    shareLuggageNo = ra.Renkei_Anken_Kubun == SystemConstants.Kubun.One ? null : sl?.Share_Luggage_No,
                    vehicleRentalDestination = null,
                    tumiDatetime = $"{ps?.PointDate:yyyy/MM/dd} {ps?.PointTime}",
                    kokyakuName = ad?.KokyakuName,
                });
            }
            return ankens;
        }
    }
}
