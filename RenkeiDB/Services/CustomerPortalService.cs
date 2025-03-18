using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemConstants;

namespace RenkeiDB.Services
{
    /// <summary>
    /// 顧客ポータルサービスを提供します。
    /// </summary>
    public class CustomerPortalService : ICustomerPortalService
    {
        private readonly ICustomerPortalRepository _customerPortalRepository;

        public CustomerPortalService(ICustomerPortalRepository customerPortalRepository)
        {
            _customerPortalRepository = customerPortalRepository;
        }

        /// <summary>
        /// ポータル情報（荷主）の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーの会社ID</param>
        /// <param name="branchId">ログインユーザーの支店ID</param>
        /// <returns>案件のリストを含むタスク</returns>
        public async Task<IEnumerable<AnkenDto>> GetPortalsAsync(int companyId, int branchId)
        {
            IEnumerable<JoinCustomerPortalDto> data = await _customerPortalRepository.GetPortalsAsync(companyId, branchId);

            Dictionary<int, AnkenDto> ankenDict = new ();

            List<AnkenDto> ankens = new ();

            int[] renkeiAnkenIds = data
                .Select(d => d.renkeiAnken.Renkei_Anken_ID).Distinct()
                .ToArray();

            IEnumerable<AnkenSecureDto> ankenSecures = await _customerPortalRepository.GetSecuresAsync(renkeiAnkenIds);

            foreach (var item in data)
            {
                int renkeiAnkenId = item.renkeiAnken.Renkei_Anken_ID;

                DateTime? pointDate = item.renkeiAnkenPoint?.PointDate;
                string pointTimeStr = item.renkeiAnkenPoint?.PointTime;
                string oroshiDatetime = null;
                string transportationDatetime = null;
                if (pointDate == null || string.IsNullOrWhiteSpace(pointTimeStr))
                {
                    oroshiDatetime = null;
                    transportationDatetime = null;
                } else
                {
                    if (!TimeSpan.TryParseExact(pointTimeStr, "hh\\:mm", null, out TimeSpan pointTime))
                    {
                        oroshiDatetime = null;
                        transportationDatetime = null;
                    }
                    else
                    {
                        DateTime combinedDateTime = pointDate.Value.Date + pointTime;
                        oroshiDatetime = combinedDateTime.ToString(Format.DateTime);
                        transportationDatetime = combinedDateTime.ToString(Format.DateTime);
                    }
                }

                if (ankenDict.ContainsKey(renkeiAnkenId))
                {
                    if (item.renkeiAnkenPoint.SEKubun == Kubun.Types.TypeOne)
                    {
                        ankenDict[renkeiAnkenId].tumi = item.renkeiAnkenPoint.Address;
                        ankenDict[renkeiAnkenId].transportationDate = transportationDatetime;
                    }

                    if (item.renkeiAnkenPoint.SEKubun == Kubun.Types.TypeNine)
                    {
                        ankenDict[renkeiAnkenId].oroshi = item.renkeiAnkenPoint.Address;
                        ankenDict[renkeiAnkenId].oroshiDatetime = oroshiDatetime;
                    }
                    continue;
                }

                oroshiDatetime = item.renkeiAnkenPoint?.SEKubun == Kubun.Types.TypeNine ? oroshiDatetime : null;
                transportationDatetime = item.renkeiAnkenPoint?.SEKubun == Kubun.Types.TypeOne ? transportationDatetime : null;

                AnkenSecureDto ankenSecure = ankenSecures.Where(item => item.renkeiAnkenSecure.Renkei_Anken_ID.Equals(renkeiAnkenId)).FirstOrDefault();

                // ステータスを検出
                string status = null;
                if (item.renkeiAnken.Renkei_Anken_Status == NumberAnkenStatus.Confirmed || item.renkeiAnken.Renkei_Anken_Status == NumberAnkenStatus.Temporarily)
                {
                    if (ankenSecure?.renkeiAnkenSecure == null)
                    {
                        status = RenkeiAnkenStatus.Ordered;
                    }
                    else
                    {
                        status = item.renkeiAnken.Renkei_Anken_Status == NumberAnkenStatus.Confirmed ? RenkeiAnkenStatus.Confirmed : RenkeiAnkenStatus.Temporarily;
                    }
                }

                if (ankenSecure?.renkeiAnkenSecure?.Renkei_Anken_Secure_Status == NumberAnkenStatus.Others)
                {
                    status = RenkeiAnkenStatus.Others;
                }
                if (item.renkeiAnken.Renkei_Anken_Status == NumberAnkenStatus.Cancel)
                {
                    status = RenkeiAnkenStatus.Cancel;
                }
                if (item.renkeiAnken.Renkei_Anken_Status == NumberAnkenStatus.ChangeRequest)
                {
                    status = RenkeiAnkenStatus.ChangeRequest;
                }

                if ((item.renkeiAnken.Renkei_Anken_Status == NumberAnkenStatus.ChangeRequest && item.renkeiAnkenCheck != null)
                    || (ankenSecure?.renkeiAnkenSecure?.Renkei_Anken_Secure_Status == NumberAnkenStatus.ChangeRequest && ankenSecure?.renkeiAnkenSecureCheck != null))
                {
                    status = RenkeiAnkenStatus.ChangeConfirmation;
                }

                string dispatchStatus = string.Empty;
                if (ankenSecure?.renkeiAnkenSecurePrint?.Print_Kubun == NumberDispatchStatus.DispatchInProgress)
                {
                    dispatchStatus = DispatchStatus.DispatchInProgress;
                }
                if (ankenSecure?.renkeiAnkenSecure?.Renkei_Anken_Secure_Status == NumberDispatchStatus.TemporaryVehicleAllocation)
                {
                    dispatchStatus = DispatchStatus.TemporaryVehicleAllocation;
                }
                if (ankenSecure?.renkeiAnkenSecure?.Renkei_Anken_Secure_Status == NumberDispatchStatus.ConfirmedDispatch)
                {
                    dispatchStatus = DispatchStatus.ConfirmedDispatch;
                }

                string syaban = ankenSecure?.renkeiAnkenSecure != null ? ankenSecure?.renkeiAnkenSecure?.Syaban_Number : null;

                AnkenDto anken = Mapper.ConvertToAnkenEntity(item, status, dispatchStatus, syaban, oroshiDatetime, transportationDatetime);
                anken.id = renkeiAnkenId;
                ankens.Add(anken);
                ankenDict[renkeiAnkenId] = anken;
            }

            return ankens;
        }

        /// <summary>
        /// ポータル/荷主カレンダー数の取得
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="from_date">開始日</param>
        /// <param name="to_date">終了日</param>
        /// <returns>カウントのリストを含むタスク</returns>
        public async Task<IEnumerable<CountByDateString>> GetCalendars(int companyId, int branchId, DateTime fromDate, DateTime toDate)
            => (await _customerPortalRepository.GetCalendars(companyId, branchId, fromDate, toDate))
                .Select(di => new CountByDateString { date = di.Date.ToString("yyyy/MM/dd"), cnt = di.Count });

        /// <summary>
        /// ポータルのカウントを取得
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>ポータルカウントのDTOを含むタスク</returns>
        public async Task<CustomerPortalCountsDto> Get_portal_counts(int company_id, int branch_id, DateTime date)
            => await _customerPortalRepository.Get_portal_counts(company_id, branch_id, date);
    }
}
