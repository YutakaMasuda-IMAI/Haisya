using HaisyaWeb.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class NippouDataApi : BaseHttpClient
    {
        public NippouDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
            httpClient.BaseAddress = new Uri(_baseUrl);
        }

        /// <summary>
        /// 新しいコメントを登録
        /// </summary>
        /// <param name="t_Nippou">コメントを挿入するT_Nippouオブジェクト</param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> InsertCommentAsync(Dto.T_Nippou_Local t_Nippou)
        {
            string url = string.Format("NippouData/InsertCommentAsync");
            //データ更新
            return await ExecHttpData(t_Nippou, url);
        }

        /// <summary>
        /// 新しい受領を登録
        /// </summary>
        /// <param name="t_Nippou"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> InsertReceiptAsync(Dto.T_Nippou_Local t_Nippou)
        {
            string url = string.Format("NippouData/InsertReceiptAsync");
            //データ更新
            return await ExecHttpData(t_Nippou, url);
        }

        /// <summary>
        /// T_Nippouのリストを検索して返却
        /// </summary>
        /// <param name="t_Nippous"></param>
        /// <returns></returns>
        public async Task<List<Dto.T_Nippou_Local>> SearchTNippouAsync(List<Dto.T_Nippou_Local> t_Nippous)
        {
            ApiResponse<List<Dto.T_Nippou_Local>> response = await MakePostGetRequestAsync<List<Dto.T_Nippou_Local>, List<Dto.T_Nippou_Local>>(t_Nippous, _baseUrl, "NippouData/SearchTNippouAsync");
            if (!response.Success)
            {
                throw new HttpRequestException(response.Message);
            }
            return response.Data;
        }
    }
}
