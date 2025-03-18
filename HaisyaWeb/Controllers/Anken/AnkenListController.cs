using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.AnkenModel;

namespace HaisyaWeb.Controllers.Anken
{
    [Authorize]
    public class AnkenListController : BaseController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public AnkenListController(ILogger<AnkenListController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// Index処理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(SearchModelForAnkenList param)
        {
            try
            {
                AnkenListModel model = await CreateModel();
                if (param.SelectDay != null)
                {
                    model.Search.SelectDay = param.SelectDay;
                    model.Search.SelectTantou = param.SelectTantou;
                    model.Search.SelectSyasyu = param.SelectSyasyu;
                    model.Search.SelectKata = param.SelectKata;
                    model.Search.SelectGroup = param.SelectGroup;
                }
                model.Search.BackMenuAction = "AnkenList";

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// AnkenListModelの作成＆返却
        /// </summary>
        /// <returns></returns>
        private async Task<AnkenListModel> CreateModel()
        {
            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            SearchModelForAnkenList param = new()
            {
                SelectGroup = 1,
                SelectDay = DateTime.Now.ToString("yyyy/MM/dd"),
                SelectTantou = await SearchCommonService.GetUserGroupDefaultVal(_mapApiSettiong, loguinUser.Company_ID,
                                                UserGroupLists.Haisya, loguinUser.User_ID) ?? "ALL",
            };
            AnkenListModel model = new()
            {
                SortParam = new DataListSortModel() { SortItemParam = "Anken_No", SortOrder = "asc" }
            };

            SearchModelForAnkenList search = new()
            {
                //MonthSelectList = SearchCommonService.GetYearMonthSelect(),
                TantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya),
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
        /// 案件一覧データの返却
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Dto.V_AnkenDataList_Local>> GetAnkenDataList(string targetDate, string targetDateFrom, string targetDateTo)
        {
            return await GetAnkenDataList(targetDate, targetDateFrom, targetDateTo, 1, 0, 0);
        }

        /// <summary>
        /// JSON：案件一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetDataList(SearchModelForAnkenList param, DataListSortModel sortParam)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                AnkenListModel model = new() { 
                    SortParam = sortParam,
                };

                if (param == null) { return null; }

                IEnumerable<Dto.V_AnkenDataList_Local> listData = await GetAnkenDataList(DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), null, null);

                if (listData != null)
                {
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
                }

                model.AnkenDataLists = listData;

                //配車データの取得
                using API.WebApp.HaisyaDataApi apiH = new(_mapApiSettiong);
                model.HaisyaDataLists = await apiH.GetHaisyaDataList(loguinUser.Company_ID, param.SelectDay, null,null, 0, 0 , 0);

                return await PartialViewAsJson("AnkenListForStatus", model, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// JSON：案件一覧データの返却
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="iCcompanyID"></param>
        /// <param name="iCustomerID"></param>
        /// <param name="iBranchID"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Dto.V_AnkenDataList_Local>> GetAnkenDataList(string targetDate, string targetDateFrom,
                                                string targetDateTo, int iCcompanyID, int iCustomerID, int iBranchID)
        {
            using API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
            return await api.GetAnkenDataList(targetDate, null, null, iCcompanyID, iCustomerID, iBranchID);
        }
    }
}
