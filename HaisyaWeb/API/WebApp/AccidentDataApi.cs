using HaisyaWeb.Models;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class AccidentDataApi : BaseHttpClient
    {

        public AccidentDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }

        /// <summary>
        /// 事故情報の取得
        /// </summary>
        /// <param name="Jiko_ID"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task<Dto.AccidentModel_Local> GetAccident(int Jiko_ID, int User_ID)
        {
            string url = _baseUrl + string.Format("Accident/GetAcciden?Jiko_ID={0}&User_ID={1}", Jiko_ID, User_ID);
            //データ取得 
            return await GetHttpData<Dto.AccidentModel_Local>(url);
        }

        /// <summary>
        /// Jiko_Statusの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> ChangeStatus(Dto.ChangeStatus data)
        {
            string url = _baseUrl + string.Format("Accident/ChangeStatu?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.ChangeStatus>(data, url);
        }

        /// <summary>
        /// ワークフローリストの取得
        /// </summary>
        /// <param name="Jiko_ID"></param>
        /// <param name="Jiko_WorkFlow_Base_ID"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task<Dto.WorkFlowList> GetWorkflowList(int Jiko_ID, int Jiko_WorkFlow_Base_ID, int User_ID)
        {
            string url = _baseUrl + string.Format("Accident/GetWorkflowLis?Jiko_ID={0}&Jiko_WorkFlow_Base_ID={1}&User_ID={2}", Jiko_ID, Jiko_WorkFlow_Base_ID, User_ID);
            //データ取得 
            return await GetHttpData<Dto.WorkFlowList>(url);
        }


        /// <summary>
        /// 差し戻し情報の取得
        /// </summary>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task<Dto.RemandDataModel> GetRemandData(int User_ID, int Jiko_WorkFlow_Status_ID)
        {
            string url = _baseUrl + string.Format("Accident/GetRemandDat?User_ID={0}&Jiko_WorkFlow_Status_ID={1}", User_ID, Jiko_WorkFlow_Status_ID);
            //データ取得 
            return await GetHttpData<Dto.RemandDataModel>(url);
        }

        /// <summary>
        /// 売上情報の登録
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> PostAccident(Dto.PostAccident data)
        {
            string url = _baseUrl + string.Format("Accident/PostAccidentData?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.PostAccident>(data, url);
        }

        /// <summary>
        /// 差し戻しの登録
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> PostRemand(Dto.PostRemand data)
        {
            string url = _baseUrl + string.Format("Accident/PostReman?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.PostRemand>(data, url);
        }

        /// <summary>
        /// 承認の登録
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> PostApproval(Dto.PostRemand data)
        {
            string url = _baseUrl + string.Format("Accident/PostApprova?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.PostRemand>(data, url);
        }

    }
}
