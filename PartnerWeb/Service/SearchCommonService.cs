using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PartnerWeb.Models;

namespace PartnerWeb.Service
{
    public enum TantouLists
    {
        ALL = 0,
        Tantou = 1,
        Eigyo = 2,
    }


    public class SearchCommonService
    {

        /// <summary>
        ///  過去15ヶ月分のリストを返却
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetYearMonthSelect()
        {
            List<SelectListItem> list = new();

            //return Enumerable.Range(DateTime.Now.Month - 9, 10).Select(t => new DropDownListModel() { Value = t.ToString(), DisplayText = t.ToString() });

            for (int i = 0; i < 15; i++)
            {
                DateTime bb = DateTime.Now.AddMonths(-i);

                list.Add(new SelectListItem() { Value = bb.ToString("yyyyMM"), Text = bb.ToString("yyyy年MM月"), Selected = (0 == i) });
            }

            return list;

        }

        /// <summary>
        /// 配車担当リストを返却
        /// </summary>
        /// <returns></returns>
        public static async Task<List<SelectListItem>> GetTantouSelect(MapApiSettings _mapApiSettiong, int CompanyId, TantouLists listKubun = 0,
                                            bool notAllFlg = false, string AllDisplay = "全員")
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            List<Dto.M_CompanyUser_Local> companyUser = await api.GetCompanyUserList(CompanyId);

            if (!notAllFlg)
            {
                list.Add(new SelectListItem() { Value = "ALL", Text = AllDisplay, Selected = true });
            }
            
            switch (listKubun)
            {
                case TantouLists.ALL:
                    companyUser = companyUser.Where(m => m.Company_ID == CompanyId).ToList();
                    break;
                case TantouLists.Tantou:
                    companyUser = companyUser.Where(m => m.Company_ID == CompanyId & m.Tantou_Flg == true).ToList();
                    break;
                case TantouLists.Eigyo:
                    companyUser = companyUser.Where(m => m.Company_ID == CompanyId & m.Eigyo_Flg).ToList();
                    break;
                default:
                    break;
            }
                
            foreach (var item in companyUser)
            {
                list.Add(new SelectListItem() { Value = item.User_ID.ToString(), Text = item.Display_Name, Selected = false });
            }
            
            return list;
        }

        public static async Task<List<SelectListItem>> GetSyasyuSelect(MapApiSettings _mapApiSettiong, int CompanyId)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_Syaryo_Local> dataList = await api.GetSyaryoList(CompanyId);
            var querySyasyu = dataList.OrderBy(x => x.SYASYU).GroupBy(x => new { syasyuDisplay = x.SyasyuDisplay, syasyu = x.SYASYU, kata = x.KATA, size = x.SIZE });
            foreach (var item in querySyasyu)
            {
                list.Add(new SelectListItem() { Value = item.Key.syasyu + '-' + item.Key.kata + '-' + item.Key.size, Text = item.Key.syasyuDisplay, Selected = false });
            }
            return list;
        }

        public static async Task<List<SelectListItem>> GetKataSelect(MapApiSettings _mapApiSettiong, int CompanyId)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_Syaryo_Local> dataList = await api.GetSyaryoList(CompanyId);
            var queryKata = dataList.OrderBy(x => x.KataDisplay).GroupBy(x => new { KataDisplay = x.KataDisplay, kata = x.KATA });;
            foreach (var item in queryKata)
            {
                list.Add(new SelectListItem() { Value = item.Key.kata, Text = item.Key.KataDisplay, Selected = false });
            }
            return list;
        }

        public static async Task<List<SelectListItem>> GetEriaSelectListItem(MapApiSettings _mapApiSettiong)
        {
            List<SelectListItem> list = new();

            //API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            //IEnumerable<Dto.M_Eria_Local> dataList = await api.M_EriaList();
            //var querySyasyu = dataList.OrderBy(x => x.SortOrder);
            //foreach (var item in querySyasyu)
            //{
            //    list.Add(new SelectListItem() { Value = item.Eria, Text = item.Eria, Selected = false });
            //}
            return list;
        }

        public static async Task<List<SelectListItem>> GetPublishGroupSelectListItem(MapApiSettings _mapApiSettiong,
                                                                                int CompanyID, int BranchID, int UserID)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_PublishGroup_Local> dataList = await api.M_PublishGroupList(CompanyID, BranchID, UserID);
            var querySyasyu = dataList.OrderBy(x => x.Publish_Group_Name);
            foreach (var item in querySyasyu)
            {
                list.Add(new SelectListItem() { Value = item.PublishGroup_ID.ToString(), Text = item.Publish_Group_Name, Selected = false });
            }
            return list;
        }



        /// <summary>
        ///  過去５年分のリストを返却
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetYearSelectList()
        {
            List<SelectListItem> list = new();

            //return Enumerable.Range(DateTime.Now.Month - 9, 10).Select(t => new DropDownListModel() { Value = t.ToString(), DisplayText = t.ToString() });

            for (int i = 0; i < 5; i++)
            {
                DateTime bb = DateTime.Now.AddYears(-i);

                list.Add(new SelectListItem() { Value = bb.ToString("yyyy"), Text = bb.ToString("yyyy年"), Selected = (0 == i) });
            }

            return list;

        }


        /// <summary>
        ///  １２ヶ月のリストを返却
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetMonthSelectList()
        {
            List<SelectListItem> list = new();

            //return Enumerable.Range(DateTime.Now.Month - 9, 10).Select(t => new DropDownListModel() { Value = t.ToString(), DisplayText = t.ToString() });

            for (int i = 1; i <= 12; i++)
            {
                   list.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString("#月"), Selected = (0 == i) });
            }

            return list;

        }


    }
}
