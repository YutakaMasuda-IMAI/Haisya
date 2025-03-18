using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Repositories;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    public class CompanyPortalService : ICompanyPortalService
    {
        private readonly ICompanyPortalRepository _db;

        public CompanyPortalService(ICompanyPortalRepository db) => _db = db;

        /// <summary>
        /// カレンダーを取得する
        /// </summary>
        /// <param name="company_id">ログインユーザーの会社ID</param>
        /// <param name="branch_id">ログインユーザーのブランチID</param>
        /// <param name="from_date">開始日</param>
        /// <param name="to_date">終了日</param>
        /// <returns>
        /// </returns>
        public async Task<IEnumerable<CountByDateString>> get_calendars(int company_id, int branch_id, DateTime from_date, DateTime to_date)
            => (await _db.Get_calendars(company_id, branch_id, from_date, to_date)).Select(di => new CountByDateString { date = di.Date.ToString("yyyy/MM/dd"), cnt = di.Count });

        /// <summary>
        /// ポータルカウンタ取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>
        /// </returns>
        public async Task<CompanyPortalCountsDto> GetPortalCountsAsync(int companyId, int branchId, string date)
        {
            DateTime dateVal = Convert.ToDateTime(date);

            int shareSyaryoCount = await _db.GetSyaryosAsync(companyId, branchId) != null ? (await _db.GetSyaryosAsync(companyId, branchId)).Where(x => x.shareSyaryo.Share_Syaryo_Status == 0
                                        && x.shareSyaryoDetail.Empty_Car_Day.Date == dateVal.Date).Count() : 0;

            int syaSyaryoKakuhoCount = await _db.GetSyaryosAsync(companyId, branchId) != null ? (await _db.GetSyaryosAsync(companyId, branchId)).Where(x => x.shareSyaryo.Share_Syaryo_Status == 1
                                        && x.shareSyaryoSecure?.Company_ID == companyId
                                        && x.shareSyaryoSecure?.Branch_ID == branchId
                                        && x.shareSyaryoDetail.Empty_Car_Day.Date == dateVal.Date).Count() : 0;

            int shareLuggageCount = await _db.GetLuggagesAsync(companyId, branchId) != null ? (await _db.GetLuggagesAsync(companyId, branchId)).Where(x => x.shareLuggage.Share_Luggage_Status == 0
                                        && x.shareLuggageDetail.Tumi_Datetime?.Date == dateVal.Date).Count() : 0;

            int shareLuggageKakuhoCount = await _db.GetShareLuggageKakuhoCnt(companyId, branchId, dateVal);
            int shareLuggageMitourokuCnt = _db.GetShareLuggageMitourokuCnt(companyId, branchId, dateVal);
            int iraiAnkenCount = await _db.GetIraiAnkensAsync(companyId, branchId) != null ? (await _db.GetIraiAnkensAsync(companyId, branchId)).Where(x => x.renkeiAnkenPointS.PointDate?.Date == dateVal.Date).Count() : 0;
            int juchuAnkenCount = await _db.GetJuchuAnkensAsync(companyId, branchId) != null ? (await _db.GetJuchuAnkensAsync(companyId, branchId)).Where(x => x.renkeiAnkenPointS.PointDate?.Date == dateVal.Date).Count() : 0;

            CompanyPortalCountsDto result = new()
            {
                shareSyaryoCnt = shareSyaryoCount,
                syaSyaryoKakuhoCnt = syaSyaryoKakuhoCount,
                shareLuggageCnt = shareLuggageCount,
                shareLuggageKakuhoCnt = shareLuggageKakuhoCount,
                shareLuggageMitourokuCnt = shareLuggageMitourokuCnt,
                iraiAnkenCnt = iraiAnkenCount,
                iraiAnkenSyabanKakuteiCnt = 0,
                iraiAnkenSyabanMikakuteiCnt = 0,
                juchuAnkenCnt = juchuAnkenCount,
                juchuAnkenSyabanTourokuCnt = 0,
                juchuAnkenSyabanMitourokuCnt = 0,
            };

            return result;
        }

        /// <summary>
        /// ポータルカウンタ取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>
        /// </returns>
        public async Task<CompanyPortalsDto> GetPortalsAsync(int companyId, int branchId)
        {
            CompanyPortalsDto result = new();

            IEnumerable<JoinCompanyPortalDto> iraiAnkenData = await _db.GetIraiAnkensAsync(companyId, branchId);
            IEnumerable<JoinCompanyPortalDto> juchuAnkenData = await _db.GetJuchuAnkensAsync(companyId, branchId);

            int[] iraiAnkenIds = iraiAnkenData
                .Select(d => d.renkeiAnken.Renkei_Anken_ID).Distinct()
            .ToArray();

            int[] juchuAnkenIds = juchuAnkenData
                .Select(d => d.renkeiAnken.Renkei_Anken_ID).Distinct()
            .ToArray();

            IEnumerable<AnkenSecureDto> ankenSecures = await _db.GetSecuresAsync(iraiAnkenIds.Concat(juchuAnkenIds).ToArray());
            IEnumerable<JoinShareSyaryoDto> shareSyaryos = await _db.GetSyaryosAsync(companyId, branchId);
            IEnumerable<JoinShareLuggageDto> shareLuggages = await _db.GetLuggagesAsync(companyId, branchId);

            result.shareLuggages = shareLuggages.Where(x =>
                new int[] { SystemConstants.ShareLuggageStatus.公開中, SystemConstants.ShareLuggageStatus.確保 }.Contains(x.shareLuggage.Share_Luggage_Status)
            ).Where(x => (x.shareLuggage.Share_Luggage_Status == SystemConstants.ShareLuggageStatus.公開中 && x.shareLuggageDetail.Tumi_Datetime?.Date >= DateTime.Now.Date)
                    || (x.shareLuggage.Share_Luggage_Status == SystemConstants.ShareLuggageStatus.確保 && x.shareLuggageSecure.Company_ID == companyId && x.shareLuggageSecure.Branch_ID == branchId)
            ).Select(x => Mapper.ConvertToShareLuggagePortalEntity(x));
            result.shareSyaryos = shareSyaryos.Where(x => x.shareSyaryo.Share_Syaryo_Status == 0 && x.shareSyaryoDetail.Empty_Car_Day.Date >= DateTime.Now.Date).
                Select(x => Mapper.ConvertToShareSyaryoPortalEntity(x));

            result.shareSyaryoKakuhos = shareSyaryos.Where(x => x.shareSyaryo.Share_Syaryo_Status == 1 && x.shareSyaryoSecure.Company_ID == companyId && x.shareSyaryoSecure.Branch_ID == branchId).
                Select(x => Mapper.ConvertToShareSyaryoKakuhoPortalEntity(x));

            result.iraiAnkens = iraiAnkenData.
                Select(x => Mapper.ConvertToIraiAnkenEntity(
                    x,
                    ankenSecures.FirstOrDefault(y => y.renkeiAnkenSecure?.Renkei_Anken_ID == x.renkeiAnken.Renkei_Anken_ID)
                ));
            result.juchuAnkens = juchuAnkenData.
                Select(x => Mapper.ConvertToJuchuAnkenEntity(
                    x,
                    ankenSecures.FirstOrDefault(y => y.renkeiAnkenSecure?.Renkei_Anken_ID == x.renkeiAnken.Renkei_Anken_ID)
                ));
            return result;
        }
    }
}
