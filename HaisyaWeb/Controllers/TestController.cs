using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static HaisyaWeb.Models.TestModel;

namespace HaisyaWeb.Controllers
{
    public class TestController : BaseController
    {



        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public TestController(ILogger<TestController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }


        public async Task<IActionResult> Index(AnkenModel.SearchModelForAnkenList param)
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";

                if (param.SelectDay != null)
                {
                    model.Search.SelectDay = param.SelectDay;
                    model.Search.SelectTantou = param.SelectTantou;
                    model.Search.SelectSyasyu = param.SelectSyasyu;
                    model.Search.SelectKata = param.SelectKata;
                    model.Search.SelectGroup = param.SelectGroup;
                    model.Search.BackMenuAction = "AnkenList";
                }

                return View(model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new();
                errorModel.RequestId = ex.HelpLink ?? GetErrorRequestId();
                errorModel.Message = ex.Message;
                return View("Error", errorModel);
            }
        }


        /// <summary>
        /// Index2処理
        /// </summary>
        /// <param name="param"></param>                     
        /// <returns></returns>
        public IActionResult Index2()
        {
            try
            {
                return View("Index2");
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new();
                errorModel.RequestId = ex.HelpLink ?? GetErrorRequestId();
                errorModel.Message = ex.Message;
                return View("Error", errorModel);
            }
        }

        /// <summary>
        /// Index3処理
        /// </summary>
        /// <param name="param"></param>                     
        /// <returns></returns>
        public IActionResult Index3()
        {
            try
            {
                return View("Index3");
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new();
                errorModel.RequestId = ex.HelpLink ?? GetErrorRequestId();
                errorModel.Message = ex.Message;
                return View("Error", errorModel);
            }
        }

        /// <summary>
        /// DataListModelの作成＆返却
        /// </summary>
        /// <returns></returns>
        private async Task<DataListModel> CreateModel()
        {
            SearchModelForTestList param = null;
            if (param == null)
            {
                param = new()
                {
                    SelectGroup = 0,
                    SelectDay = DateTime.Now.ToString("yyyy/MM/dd"),
                };
            }
            if (param.SelectTantou == null) { param.SelectTantou = "ALL"; }

            SearchModelForTestList search = new();

            DataListModel model = new() { };

            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            search = new()
            {
                //MonthSelectList = SearchCommonService.GetYearMonthSelect(),
                TantouSelectList = await SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, TantouLists.Tantou),
                SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                KataSelectList = await SearchCommonService.GetKataSelect(_mapApiSettiong, loguinUser.Company_ID),
                SelectTantou = param.SelectTantou,
                SelectTab = param.SelectTab,
                SelectDay = param.SelectDay,
                SelectGroup = param.SelectGroup,
            };

            model.Search = search;
            return model;
        }


        /// <summary>
        /// JSON：案件一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetDataList(SearchModelForTestList param)
        {
            try
            {
                DataListModel model = new() { };

                if (param == null) { return null; }

                IEnumerable<Dto.V_AnkenDataList_Local> list = await GetAnkenDataList(DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), null, null, 1, 0, 0);

                List<AnkenDataList> listData = new();

                //Parallel.ForEach(list, data =>
                //{
                //    listData.Add(new AnkenDataList(data));
                //});

                foreach (var data in list)
                {
                    listData.Add(new AnkenDataList(data));
                }

                if (!"ALL".Equals(param.SelectTantou))
                {
                    listData = listData.Where(m => m.TantouID == int.Parse(param.SelectTantou)).ToList();
                }

                if (param.SelectSyasyu != null)
                {
                    listData = listData.Where(m => m.SyasyuDisplay.Contains(param.SelectSyasyu)).ToList();
                }

                if (param.SelectKata != null)
                {
                    listData = listData.Where(m => m.Kata.Contains(param.SelectKata)).ToList();
                }

                if (param.SelectDay != null)
                {
                    listData = listData.Where(m => m.START_PointDate == DateTime.Parse(param.SelectDay)).ToList();
                }

                model.DataDataLists = listData;


                string html = "DataListForNone";

                //switch (param.SelectGroup)
                //{
                //    case 1:
                //        html += "DataListForStatus";
                //        break;
                //    case 2:
                //        html += "DataListForStep";
                //        break;
                //    case 3:
                //        html += "DataListForSyasyu";
                //        break;
                //    case 4:
                //        html += "DataListForTantou";
                //        break;
                //    default:
                //        html += "DataListForNone";
                //        break;

                //}
                return await PartialViewAsJson(html, model, true);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new();
                errorModel.RequestId = ex.HelpLink ?? GetErrorRequestId();
                errorModel.Message = ex.Message;
                return await PartialViewAsJson("Error", errorModel, true);
            }
        }

        /// <summary>
        /// JSON：案件一覧データの返却
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="CcompanyID"></param>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Dto.V_AnkenDataList_Local>> GetAnkenDataList(string targetDate, string targetDateFrom, string targetDateTo, int iCcompanyID, int iCustomerID, int iBranchID)
        {
            using API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.V_AnkenDataList_Local> ankenDataList = await api.GetAnkenDataList(targetDate, null, null, iCcompanyID, iCustomerID, iBranchID);
            return ankenDataList;
        }



    }
}
