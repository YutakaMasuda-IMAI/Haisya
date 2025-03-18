using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class CompletedPaymentsApi : BaseHttpClient
    {
        public CompletedPaymentsApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }

        /// <summary>
        /// T_Nyukin_Localリストのデータの追加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertTNyukin(List<T_Nyukin_Local> list)
        {
            string url = _baseUrl + string.Format("CompletedPayments/InsertTNyukin?jsonString={0}", "A");
            return await ExecHttpData(list, url);
        }

        /// <summary>
        /// T_Nyukinリストのデータの追加・更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertOrUpdateTNyukin(List<T_Nyukin_Local> list)
        {
            string url = _baseUrl + string.Format("CompletedPayments/InsertOrUpdateTNyukin?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData(list, url);
        }

        /// <summary>
        /// PaymentInputDataListデータの返却
        /// </summary>
        /// <param name="Seikyu_ID"></param>
        /// <returns></returns>
        public async Task<PaymentInputDataListModel> GetPaymentInputDataList(int Seikyu_ID)
        {
            string url = _baseUrl + string.Format("CompletedPayments/GetPaymentInputDataLis?Seikyu_ID={0}", Seikyu_ID);
            return await GetHttpData<PaymentInputDataListModel>(url);
        }

        /// <summary>
        ///  入金情報と返金情報の登録・更新・削除
        /// </summary>
        /// <param name="combinedData"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> PostPaymentInputData(PostPaymentInputDataModel combinedData)
        {
            string url = _baseUrl + string.Format("CompletedPayments/PostPaymentInputDat?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.PostPaymentInputDataModel>(combinedData, url);
        }
    }
}
