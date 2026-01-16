using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Model;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class DailyReportRegistrationDetailService : IDailyReportRegistrationDetailService
    {
        private readonly IDailyReportRegistrationDetailRepository _dailyReportRegistrationDetailRepository;

        public DailyReportRegistrationDetailService(IDailyReportRegistrationDetailRepository dailyReportRegistrationDetailRepository)
        {
            _dailyReportRegistrationDetailRepository = dailyReportRegistrationDetailRepository;
        }

        /// <summary>
        /// 日報登録の詳細情報を取得します。
        /// </summary>
        /// <param name="Anken_ID">案件の一意の識別子。</param>
        /// <param name="AnkenDisplay_ID">案件配車用ID。</param>
        /// <param name="DriverCd">乗務員CD（デジタコデータ取得で使用）。</param>
        /// <param name="Syaban">車番CD（デジタコデータ取得で使用）。</param>
        /// <param name="KokyakuId">顧客ID（nullable）。</param>
        /// <param name="Driver_ID">運転手ID（nullable）。</param>
        /// <param name="Haisya_Kubun">配車区分（nullable）。</param>
        /// <param name="SyaryoManagement_ID">車両管理ID（nullable）。</param>
        /// <param name="Haisya_ID">配車ID（nullable）。</param>
        /// <param name="StartDatetime">レポートの開始日時。</param>
        /// <param name="EndDatetime">レポートの終了日時。</param>
        /// <returns>非同期操作を表すタスク。結果として<see cref="DailyReportRegistrationDetailModel"/>を返します。</returns>
        public async Task<DailyReportRegistrationDetailModel> GetDailyReportRegistrationAnken(int Anken_ID, int AnkenDisplay_ID, int DriverCd, int Syaban,
                            int? KokyakuId, int? Driver_ID, int? Haisya_Kubun,
                            int? SyaryoManagement_ID, int? Haisya_ID, DateTime StartDatetime, DateTime EndDatetime)
        {
            T_Nippou nippou = await _dailyReportRegistrationDetailRepository.GetNippou(AnkenDisplay_ID) ?? new T_Nippou();

            T_Anken Anken = await _dailyReportRegistrationDetailRepository.GetAnken(Anken_ID);

            T_Anken_Display AnkenDisplay = await _dailyReportRegistrationDetailRepository.GetAnkenDisplay(AnkenDisplay_ID);

            T_Anken_Detail AnkenDetail = await _dailyReportRegistrationDetailRepository.GetAnkenDetail(Anken_ID, Anken.Anken_Latest_Order);

            List<T_Anken_Point> AnkenPoints = await _dailyReportRegistrationDetailRepository.GetAnkenPoint(Anken_ID, Anken.Anken_Latest_Order);

            string SeikyuRemarks = await _dailyReportRegistrationDetailRepository.GetSeikyuRemarks(KokyakuId);

            List<M_Customer_TollSeikyuKubun> TollSeikyuKubun = await _dailyReportRegistrationDetailRepository.GetTollSeikyuKubun(KokyakuId);

            string DisplayName = await _dailyReportRegistrationDetailRepository.GetDisplayName(AnkenDisplay_ID, Haisya_Kubun, Driver_ID);

            string SyabanNumber = await _dailyReportRegistrationDetailRepository.GetSyabanNumber(Haisya_Kubun, SyaryoManagement_ID, Haisya_ID);

            List<T_KUDGIVT> DegitakoData = await _dailyReportRegistrationDetailRepository.GetDegitakoData(AnkenDisplay_ID, DriverCd, Syaban, StartDatetime, EndDatetime);

            T_Nippou_Anken nippouAnken = await _dailyReportRegistrationDetailRepository.GetNippouAnken(nippou.Nippou_ID) ?? new T_Nippou_Anken();

            List<int> nippouAnkenDegitakoIdList = await _dailyReportRegistrationDetailRepository.GetNippouAnkenDegitako(nippou.Nippou_ID) ?? new List<int>();

            int nippouId = nippou != null ? nippou.Nippou_ID : 0;

            return new DailyReportRegistrationDetailModel
            {
                AnkenDisplay = AnkenDisplay,
                AnkenDetail = AnkenDetail,
                PointLists = AnkenPoints,
                TollSeikyuKubun = TollSeikyuKubun,
                DisplayName = DisplayName,
                SeikyuRemarks = SeikyuRemarks,
                SyabanNumber = SyabanNumber,
                DegitakoData = DegitakoData,
                Nippou_ID = nippouId,
                Nippou = nippou,
                Nippou_Anken = nippouAnken,
                NippouAnkenDegitakoIdList = nippouAnkenDegitakoIdList,

            };
        }

        /// <summary>
        /// 日報登録の詳細情報を取得します。
        /// </summary>
        /// <param name="Anken_ID">案件の一意の識別子。</param>
        /// <param name="AnkenDisplay_ID">案件配車用ID。</param>
        /// <param name="DriverCd">乗務員CD（デジタコデータ取得で使用）。</param>
        /// <param name="Syaban">車番CD（デジタコデータ取得で使用）。</param>
        /// <param name="KokyakuId">顧客ID（nullable）。</param>
        /// <param name="Driver_ID">運転手ID（nullable）。</param>
        /// <param name="Haisya_Kubun">配車区分（nullable）。</param>
        /// <param name="SyaryoManagement_ID">車両管理ID（nullable）。</param>
        /// <param name="Haisya_ID">配車ID（nullable）。</param>
        /// <param name="StartDatetime">レポートの開始日時。</param>
        /// <param name="EndDatetime">レポートの終了日時。</param>
        /// <returns>非同期操作を表すタスク。結果として<see cref="DailyReportRegistrationDetailModel"/>を返します。</returns>
        public async Task<DailyReportRegistrationDetailModel> GetDailyReportRegistrationDetail(int Anken_ID, int AnkenDisplay_ID, int DriverCd, int Syaban, 
                            int? KokyakuId, int? Driver_ID, int? Haisya_Kubun, 
                            int? SyaryoManagement_ID, int? Haisya_ID, DateTime StartDatetime, DateTime EndDatetime)
        {
            T_Nippou nippou = await _dailyReportRegistrationDetailRepository.GetNippou(AnkenDisplay_ID) ?? new T_Nippou();

            //T_Uriage uriage = await _dailyReportRegistrationDetailRepository.GetUriage(Anken_ID) ??  new T_Uriage();

            //T_Anken Anken = await _dailyReportRegistrationDetailRepository.GetAnken(Anken_ID);

            //T_Anken_Display AnkenDisplay = await _dailyReportRegistrationDetailRepository.GetAnkenDisplay(AnkenDisplay_ID);

            //T_Anken_Detail AnkenDetail = await _dailyReportRegistrationDetailRepository.GetAnkenDetail(Anken_ID, Anken.Anken_Latest_Order);

            //List<T_Anken_Point> AnkenPoints = await _dailyReportRegistrationDetailRepository.GetAnkenPoint(Anken_ID, Anken.Anken_Latest_Order);

            string SeikyuRemarks = await _dailyReportRegistrationDetailRepository.GetSeikyuRemarks(KokyakuId);

            List<M_Customer_TollSeikyuKubun> TollSeikyuKubun = await _dailyReportRegistrationDetailRepository.GetTollSeikyuKubun(KokyakuId);

            string DisplayName = await _dailyReportRegistrationDetailRepository.GetDisplayName(AnkenDisplay_ID, Haisya_Kubun, Driver_ID);

            string SyabanNumber = await _dailyReportRegistrationDetailRepository.GetSyabanNumber(Haisya_Kubun, SyaryoManagement_ID, Haisya_ID);

            List<T_KUDGIVT> DegitakoData = await _dailyReportRegistrationDetailRepository.GetDegitakoData(AnkenDisplay_ID, DriverCd, Syaban, StartDatetime, EndDatetime);

            List<T_KUDGSIR> HighwayData = await _dailyReportRegistrationDetailRepository.GetHighwayData(AnkenDisplay_ID, DriverCd, Syaban, StartDatetime, EndDatetime);

            //T_Nippou_Anken nippouAnken = await _dailyReportRegistrationDetailRepository.GetNippouAnken(nippou.Nippou_ID) ?? new T_Nippou_Anken();

            //List<int> nippouAnkenDegitakoIdList = await _dailyReportRegistrationDetailRepository.GetNippouAnkenDegitako(nippou.Nippou_ID) ?? new List<int>();

            T_Nippou_Stay nippouStay = await _dailyReportRegistrationDetailRepository.GetNippouStay(nippou.Nippou_ID) ??  new T_Nippou_Stay();

            List<int> nippouStayDegitakoIdList = await _dailyReportRegistrationDetailRepository.GetNippouStayDegitako(nippou.Nippou_ID) ?? new List<int>();

            T_Nippou_Kaiso nippouKaiso = await _dailyReportRegistrationDetailRepository.GetNippouKaiso(nippou.Nippou_ID) ??  new T_Nippou_Kaiso();

            List<int> nippouKaisoDegitakoIdList = await _dailyReportRegistrationDetailRepository.GetNippouKaisoDegitako(nippou.Nippou_ID) ?? new List<int>();

            List<T_Nippou_Toll_Other> NippouTollOther = await _dailyReportRegistrationDetailRepository.GetNippouTollOther(nippou.Nippou_ID) ?? new List<T_Nippou_Toll_Other>();

            List<T_Nippou_Toll> NippouToll = await _dailyReportRegistrationDetailRepository.GetNippouToll(nippou.Nippou_ID) ?? new List<T_Nippou_Toll>();

            T_Nippou_Approval NippouApproval = await _dailyReportRegistrationDetailRepository.GetNippouApproval(nippou.Nippou_ID) ??  new T_Nippou_Approval();

            int nippouId = nippou != null ? nippou.Nippou_ID : 0;

            return new DailyReportRegistrationDetailModel
            {
                //AnkenDisplay = AnkenDisplay,
                //AnkenDetail = AnkenDetail,
                //PointLists = AnkenPoints,
                //Uriage = uriage,
                TollSeikyuKubun = TollSeikyuKubun,
                DisplayName = DisplayName,
                SeikyuRemarks = SeikyuRemarks,
                SyabanNumber = SyabanNumber,
                DegitakoData = DegitakoData,
                HighwayData = HighwayData,
                Nippou_ID = nippouId,
                Nippou = nippou,
                //Nippou_Anken = nippouAnken,
                //NippouAnkenDegitakoIdList = nippouAnkenDegitakoIdList,
                Nippou_Stay = nippouStay,
                NippouStayDegitakoIdList = nippouStayDegitakoIdList,
                Nippou_Kaiso = nippouKaiso,
                NippouKaisoDegitakoIdList = nippouKaisoDegitakoIdList,
                NippouTollOther = NippouTollOther,
                NippouToll = NippouToll,
                NippouApproval = NippouApproval
            };
        }

        /// <summary>
        /// GetCertificationRequestModelリクエストモデルを取得します。
        /// </summary>
        /// <param name="Nippou_Approval_ID">日報承認ID</param>
        /// <returns>非同期操作を表すタスク。結果として<see cref="CertificationRequestModel"/>を返します。</returns>
        public async Task<CertificationRequestModel> GetCertificationRequestModel(int Nippou_Approval_ID)
        {
            T_Nippou_Approval NippouApprovalData = await _dailyReportRegistrationDetailRepository.GetNippouApprovalData(Nippou_Approval_ID) ??  new T_Nippou_Approval();;

            List<GroupUserDto> GroupUserList = await _dailyReportRegistrationDetailRepository.GetGroupUser();

            string GroupName = await _dailyReportRegistrationDetailRepository.GetSelectGroupName(NippouApprovalData);

            string Comment = await _dailyReportRegistrationDetailRepository.GetComment(10);

            return new CertificationRequestModel
            {
                NippouApprovalData = NippouApprovalData,
                GroupUserList = GroupUserList,
                GroupName = GroupName,
                Comment = Comment
            };
        }

        /// <summary>
        /// PostCertificationRequestリクエストを送信します。
        /// </summary>
        /// <param name="data">送信する認証リクエストモデル。</param>
        /// <returns>非同期操作を表すタスク。操作が成功した場合はtrueを返します。</returns>
        public async Task<bool> PostCertificationRequest(CertificationRequestModel data)
        {
            await _dailyReportRegistrationDetailRepository.PostCertificationRequest(data);

            return true;
        }

        /// <summary>
        /// 日報の仮登録を行います。
        /// </summary>
        /// <param name="data">仮登録する日報の詳細モデル。</param>
        /// <returns>非同期操作を表すタスク。操作が成功した場合はtrueを返します。</returns>
        public async Task<bool> ProvisionalRegistration(DailyReportRegistrationDetailModel data)
        {
            bool result = await _dailyReportRegistrationDetailRepository.ProvisionalRegistration(data);

            return result;
        }
    }
    public interface IDailyReportRegistrationDetailService
    {

        Task<DailyReportRegistrationDetailModel> GetDailyReportRegistrationAnken(int Anken_ID, int AnkenDisplay_ID, int DriverCd, int Syaban, int? KokyakuId, int? Driver_ID, int? Haisya_Kubun, int? SyaryoManagement_ID, int? Haisya_ID, DateTime StartDatetime, DateTime EndDatetime);
        Task<DailyReportRegistrationDetailModel> GetDailyReportRegistrationDetail(int Anken_ID, int AnkenDisplay_ID, int DriverCd, int Syaban, int? KokyakuId, int? Driver_ID, int? Haisya_Kubun, int? SyaryoManagement_ID, int? Haisya_ID, DateTime StartDatetime, DateTime EndDatetime);
        Task<CertificationRequestModel> GetCertificationRequestModel(int Nippou_Approval_ID);
        Task<bool> PostCertificationRequest(CertificationRequestModel data);
        Task<bool> ProvisionalRegistration(DailyReportRegistrationDetailModel data);
    }

}