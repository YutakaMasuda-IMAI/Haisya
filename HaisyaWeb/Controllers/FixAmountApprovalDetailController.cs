using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.FixAmountApprovalDetailModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 変更承認画面のコントローラー
    /// </summary>
    public class FixAmountApprovalDetailController : BaseController
    {
        private readonly ILogger<FixAmountApprovalDetailController> _logger;

        // private FixAmountApprovalDetailModel.DataListModel _model { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        public FixAmountApprovalDetailController(ILogger<FixAmountApprovalDetailController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _logger = logger;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task<IActionResult> FixAmountApprovalDetailIndex(int Check_Seikyu_ID, ParamModelForFixAmountApprovalDetail Params)
        {
            try
            {
                // 選択時の行のデータ取得設定
                var modelData = new V_SeikyuCheckDataList_Local 
                { 
                    Seikyu_Month = Params.modelData.Seikyu_Month,
                    Seikyu_Date = Params.modelData.Seikyu_Date,
                    Shime_Day = Params.modelData.Shime_Day,
                    Customer_Branch_ID = Params.modelData.Customer_Branch_ID,
                    Customer_Name = Params.modelData.Customer_Name,
                    Seikyu_TantouID = Params.modelData.Seikyu_TantouID,
                    Seikyu_Tantou = Params.modelData.Seikyu_Tantou,
                    Mail_Title = Params.modelData.Mail_Title,
                    Mail_Address1 = Params.modelData.Mail_Address1,
                    Mail_Address2 = Params.modelData.Mail_Address2,
                    Phone1 = Params.modelData.Phone1,
                    Phone2 = Params.modelData.Phone2,
                    Fax1 = Params.modelData.Fax1,
                    Fax2 = Params.modelData.Fax2,
                    Zei_Kubun = Params.modelData.Zei_Kubun,
                    FROM_DATE = Params.modelData.FROM_DATE,
                    TO_DATE = Params.modelData.TO_DATE,
                    Inquiry_Status = Params.modelData.Inquiry_Status,
                    Check_Status = Params.modelData.Check_Status,
                    Seikyu_Changed = Params.modelData.Seikyu_Changed,
                    Meisai_Count = Params.modelData.Meisai_Count,
                    SeikyuUnchin = Params.modelData.SeikyuUnchin,
                    Tatekaekin = Params.modelData.Tatekaekin,
                    Anken_Count = Params.modelData.Anken_Count,
                    Kakutei_Count = Params.modelData.Kakutei_Count,
                    Zantei_Count = Params.modelData.Zantei_Count,
                    Kari_Count = Params.modelData.Kari_Count,
                    Check_Seikyu_ID = Params.modelData.Check_Seikyu_ID,
                };

                DataListModel model = await CreateModel(modelData);

                model.Search = Params.searchParams;

                return View("../FixAmountApprovalDetail/Index", model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 画面表示データの初期化
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<DataListModel> CreateModel(V_SeikyuCheckDataList_Local data)
        {
            // ログインユーザー取得
            var loguinUser = await GetLoginUser();

            return new DataListModel()
            {
                SeikyuCheckData = data,
                // 集計データの取得
                SyuukeiData = await GetSyuukeiData(data.Check_Seikyu_ID)
            };
        }

        /// <summary>
        /// JSON：案件一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetDataListForFixApprovalDetail(ParamModelForFixAmountApprovalDetail param)
        {
            var model = new DataListModel();
            try
            {
                // ログインユーザー取得
                var loguinUser = await GetLoginUser();

                int checkSeikyuID = (int)param.Check_Seikyu_ID;
                // Print_Seikyu_ID取り直し
                if (param.Print_Seikyu_ID == 0)
                {
                    T_Print_Seikyu_Local pritSeikyu = await GetPrintSeikyuData(checkSeikyuID);
                    if (pritSeikyu != null)
                    {
                        param.Print_Seikyu_ID = pritSeikyu.Print_Seikyu_ID;
                    }
                }

                List<T_Check_Seikyu_Detail_Local> checkSeikyuDetailList = await GetSeikyuDetail(checkSeikyuID);

                // 請求明細リストの取得
                model.SeikyuMeisaiList = new List<SeikyuMeisai>();
                List<T_Print_Seikyu_Detail_Local> meisaiUp = new List<T_Print_Seikyu_Detail_Local>();
                foreach (T_Check_Seikyu_Detail_Local checkSeikyuDetail in checkSeikyuDetailList)
                {
                    T_Uriage_Unchin_Local uriageUnchin = await GetUriageUnchin(checkSeikyuDetail.Uriage_Unchin_ID);
                    string workName = "";
                    string Syaban = "";
                    string SyasyuKataName = "";
                    if (uriageUnchin != null)
                    {
						// 案件情報→[T_Uriage_Unchin].「Uriage_ID」→[T_Uriage].「Anken_ID」→[T_Anken_Detail].「Work_Name」
						T_Anken_Detail_Local ankenDetail = await GetTAnkenDetailByUriageID(uriageUnchin.Uriage_ID);
						if (ankenDetail != null)
							workName = ankenDetail.Work_Name;
						// 車番→[T_Uriage_Unchin].「Uriage_ID」→[T_Uriage].「Nippou_ID」→[T_Nippou].「AnkenDisplay_ID」→[T_Haisya].「Syaryoumanagement_ID」→[M_Syaryomanagement].「Syaban_Number」
						M_SyaryoManagement_Local syaryoM = await GetMSyaryoManagementByUriageID(uriageUnchin.Uriage_ID);
						if (syaryoM != null)
							Syaban = syaryoM.Syaban_Number;
						// 車種→[T_Uriage_Unchin].「Uriage_ID」→[T_Uriage].「Nippou_ID」→[T_Nippou].「AnkenDisplay_ID」→[T_Haisya].「Syaryoumanagement_ID」→[M_Syaryomanagement].「Syaryo_ID」→[M_Syaryo].「SyasyuDisplay」
						M_Syaryo_Local syaryo = await GetMSyaryoByUriageID(uriageUnchin.Uriage_ID);
						if (syaryo != null)
							SyasyuKataName = syaryo.SyasyuDisplay;
					}
                    T_Print_Seikyu_Detail_Local printSeikyuDetail = new T_Print_Seikyu_Detail_Local()
                    {
                        //Print_Seikyu_ID
                        //Data_Kubun
                        //Data_Sort
                        Uriage_Unchin_ID = checkSeikyuDetail.Uriage_Unchin_ID,
                        Anken_ID = checkSeikyuDetail.Anken_ID,
                        //Anken_ID_Detail
                        Display_Date = uriageUnchin?.Seikyu_Date,
                        Syaban = Syaban,
                        SyasyuKataName = SyasyuKataName,
                        Tsumi = uriageUnchin?.Tsumi,
                        Oroshi = uriageUnchin?.Oroshi,
                        Luggage = uriageUnchin?.Luggage,
                        Work_Name = workName,
                        Qty = (double)(checkSeikyuDetail.Qty ?? 0),
                        Unit = (int)(checkSeikyuDetail.Unit ?? 0),
                        UnitPrice = (decimal)(checkSeikyuDetail.UnitPrice ?? 0),
                        CalcPrice = (decimal)(checkSeikyuDetail.CalcPrice ?? 0),
                        SeikyuUnchin = (decimal)(checkSeikyuDetail.SeikyuUnchin ?? 0),
                        Tatekaekin = (decimal)(checkSeikyuDetail.Tatekaekin ?? 0),
                        Warimashi1 = (decimal)(checkSeikyuDetail.Warimashi1 ?? 0),
                        Warimashi2 = (decimal)(checkSeikyuDetail.Warimashi2 ?? 0),
                        Warimashi3 = (decimal)(checkSeikyuDetail.Warimashi3 ?? 0),
                        Warimashi4 = (decimal)(checkSeikyuDetail.Warimashi4 ?? 0),
                        Warimashi5 = (decimal)(checkSeikyuDetail.Warimashi5 ?? 0),
                        SeikyuTotal = (decimal)(checkSeikyuDetail.SeikyuTotal ?? 0),
                        //Zei_Kubun
                        //YosyaDriver_ID
                        //Yosya_Branch_ID
                        //Yosya_Name
                        //Yosya_Driver_Name
                        //Remarks_ID
                        //Remaks = checkSeikyuDetail?.Remarks,
                        //FROM_DATE
                        //TO_DATE
                    };
                    meisaiUp.Add(printSeikyuDetail);
                }
                //var meisaiUp = param.Print_Seikyu_ID > 0 ? await GetSeikyuDataUpList(param.Print_Seikyu_ID) : new();
                if (meisaiUp?.Count <= 0)
                    return null;

                var meisaiDown = await GetSeikyuDataDownList(checkSeikyuID);

                model.UnitList = await GetUnit(loguinUser.Company_ID);

                model.CustomerUriageCalc = await GetCustomerUriageCalc((int)param.Customer_Branch_ID);

                var checkSeikyuDetails = await GetSeikyuDetail(checkSeikyuID);
                model.CheckSeikyuDetail = checkSeikyuDetails?.FirstOrDefault();
                model.CheckSeikyu = await GetSeikyu(checkSeikyuID);
                model.Check_Seikyu_ID = checkSeikyuID;
                model.Print_Seikyu_ID = param.Print_Seikyu_ID;

                // 入金リストの初期化
                model.NyukinList = new List<Nyukin>();

                if(model.CheckSeikyu != null)
				{
                    // 入金データの取得
                    var nyukinList = await GetNyukinDataList(model.CheckSeikyu);

                    if (nyukinList != null)
                    {
                        // `nyukinList` の各アイテムを処理
                        foreach (T_Nyukin_Local item in nyukinList)
                        {
                            string nyukinItem = null;

                            if (item.Cash_Amount != 0)
                            {
                                nyukinItem = "現金";
                            }
                            else if (item.Transfer_Amount != 0)
                            {
                                nyukinItem = "振込";
                            }
                            else if (item.Draft_Amount != 0)
                            {
                                nyukinItem = "手形";
                            }
                            else if (item.Unchin_Offset != 0)
                            {
                                nyukinItem = "運賃相殺";
                            }
                            else if (item.General_Offset != 0)
                            {
                                nyukinItem = "一般相殺";
                            }
                            else if (item.Adjustment_Amount != 0)
                            {
                                nyukinItem = "調整金";
                            }
                            // `Nyukin` クラスのインスタンスを作成し、リストに追加
                            var nyukinData = new Nyukin
                            {
                                Process_Date = item.Process_Date,
                                NyukinItem = nyukinItem,
                                Total_Amount = item.Total_Amount
                            };
                            model.NyukinList.Add(nyukinData);
                        }
                    }
                }

                // `meisaiUp` と `meisaiDown` のアイテムを `SeikyuMeisai` に変換し、リストに追加
                for (int i = 0; i <= meisaiUp.Count - 1; i++)
                {
                    // `SeikyuMeisai` クラスのインスタンスを作成
                    SeikyuMeisai data = new()
                    {
                        Up = new SeikyuMeisai.SeikyuMeisaiUp(),
                        Down = new SeikyuMeisai.SeikyuMeisaiDown()
                    };
                    // `meisaiUp` から上部情報を設定
                    data.Up.Print_Seikyu_ID = meisaiUp[i].Print_Seikyu_ID;
                    data.Up.Data_Kubun = meisaiUp[i].Data_Kubun;
                    data.Up.Data_Sort = meisaiUp[i].Data_Sort;
                    data.Up.Uriage_Unchin_ID = meisaiUp[i].Uriage_Unchin_ID;
                    data.Up.Anken_ID = meisaiUp[i].Anken_ID;
                    data.Up.Anken_ID_Detail = meisaiUp[i].Anken_ID_Detail;
                    data.Up.Display_Date = meisaiUp[i].Display_Date;
                    data.Up.Syaban = meisaiUp[i].Syaban;
                    data.Up.SyasyuKataName = meisaiUp[i].SyasyuKataName;
                    data.Up.DriverName = meisaiUp[i].DriverName;
                    data.Up.Tsumi = meisaiUp[i].Tsumi;
                    data.Up.Oroshi = meisaiUp[i].Oroshi;
                    data.Up.Luggage = meisaiUp[i].Luggage;
                    data.Up.Work_Name = meisaiUp[i].Work_Name;
                    data.Up.Qty = meisaiUp[i].Qty;
                    data.Up.Unit = meisaiUp[i].Unit;
                    data.Up.UnitData = model.UnitList.FirstOrDefault(u => u.Unit_ID == meisaiUp[i].Unit)?.Unit_Display;
                    data.Up.UnitPrice = meisaiUp[i].UnitPrice;
                    data.Up.CalcPrice = meisaiUp[i].CalcPrice;
                    data.Up.SeikyuUnchin = meisaiUp[i].SeikyuUnchin;
                    data.Up.Tatekaekin = meisaiUp[i].Tatekaekin;
                    data.Up.Warimashi1 = meisaiUp[i].Warimashi1;
                    data.Up.Warimashi2 = meisaiUp[i].Warimashi2;
                    data.Up.Warimashi3 = meisaiUp[i].Warimashi3;
                    data.Up.Warimashi4 = meisaiUp[i].Warimashi4;
                    data.Up.Warimashi5 = meisaiUp[i].Warimashi5;
                    data.Up.SeikyuTotal = meisaiUp[i].SeikyuTotal;
                    data.Up.Zei_Kubun = meisaiUp[i].Zei_Kubun;
                    data.Up.Remarks1 = meisaiUp[i].Remarks1;
                    data.Up.Remarks2 = meisaiUp[i].Remarks2;
                    data.Up.FROM_DATE = meisaiUp[i].FROM_DATE;
                    data.Up.TO_DATE = meisaiUp[i].TO_DATE;

                    // [T_Print_Seikyu_Detail]と[T_Check_Seikyu_Change]を[Uriage_Unchin_ID]で紐づけ
                    foreach (T_Check_Seikyu_Change_Local item in meisaiDown.Where(m => (m.Uriage_Unchin_ID == meisaiUp[i].Uriage_Unchin_ID)))
                    {
                        // `meisaiDown` から下部情報を設定
                        data.Down.Check_Seikyu_ID = item.Check_Seikyu_ID;
                        data.Down.Uriage_Unchin_ID = item.Uriage_Unchin_ID;
                        data.Down.Qty = item.Qty;
                        data.Down.Unit = item.Unit;
                        data.Down.UnitData = model.UnitList.FirstOrDefault(u => u.Unit_ID == item.Unit)?.Unit_Display;
                        data.Down.UnitPrice = item.UnitPrice;
                        data.Down.CalcPrice = item.CalcPrice;
                        data.Down.SeikyuUnchin = item.SeikyuUnchin;
                        data.Down.Tatekaekin = item.Tatekaekin;
                        data.Down.Warimashi1 = item.Warimashi1;
                        data.Down.Warimashi2 = item.Warimashi2;
                        data.Down.Warimashi3 = item.Warimashi3;
                        data.Down.Warimashi4 = item.Warimashi4;
                        data.Down.Warimashi5 = item.Warimashi5;
                        data.Down.SeikyuTotal = item.SeikyuTotal;
                        data.Down.Insert_Datetime = item.Insert_Datetime;
                        data.Down.Insert_User = item.Insert_User;
                        data.Down.Update_Datetime = item.Update_Datetime;
                        data.Down.Update_User = item.Update_User;
                    }

                    data.CheckDetail = checkSeikyuDetails.FirstOrDefault(x => x.Uriage_Unchin_ID == meisaiUp[i].Uriage_Unchin_ID); 
                    model.SeikyuMeisaiList.Add(data);
                }

                return await PartialViewAsJson("ClaimDataList", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// ポップアップ画面
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <param name="Uriage_Unchin_ID"></param>
        /// <returns></returns>
        public async Task<IActionResult> IndexModal(int Check_Seikyu_ID, int Uriage_Unchin_ID)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                 // 必要なデータを非同期で取得
                var printSeikyu = await GetSyuukeiData(Check_Seikyu_ID);
                var checkSeikyuChange = await GetCheckSeikyuChange(Check_Seikyu_ID, Uriage_Unchin_ID);
                var checkSeikyu = await GetSeikyu(Check_Seikyu_ID);
                var checkSeikyuDone = await GetCheckSeikyuDone(Check_Seikyu_ID);
                T_Check_Seikyu_Detail_Local checkSeikyuDetail =
                    (await GetSeikyuDetail(checkSeikyu.Check_Seikyu_ID))?.FirstOrDefault(d => d.Uriage_Unchin_ID == Uriage_Unchin_ID);
                var uriageUnchin = await GetUriageUnchin(Uriage_Unchin_ID);
                var unitList = await GetUnit(loguinUser.Company_ID);
                var customerBranch = await GetCustomerBranch(checkSeikyu.Customer_Branch_ID);
                var customerUriageCalc = await GetCustomerUriageCalc(checkSeikyu.Customer_Branch_ID);

                var model = new IndexModalModel
                {
                    // 単位リストをドロップダウンリスト用に変換
                    UnitDto = unitList.Select(item => new SelectListItem
                    {
                        Value = item.Unit_ID.ToString(),
                        Text = item.Unit_Display
                    }).ToList(),

                    // モデルに基本情報を設定
                    Check_Seikyu_ID = Check_Seikyu_ID,
                    Uriage_Unchin_ID = Uriage_Unchin_ID,
                    Change_Flg = checkSeikyuDone.Change_Flg,
                    Check_Kubun = checkSeikyu.Check_Kubun,

                    // 共通の設定
                    Tsumi = uriageUnchin.Tsumi,
                    Oroshi = uriageUnchin.Oroshi,
                    Luggage = uriageUnchin.Luggage,
                    ZeiKubun = uriageUnchin.Zei_Kubun,
                    ShimeDay = checkSeikyu.Shime_Day,
                    CustomerId = checkSeikyu.Customer_Branch_ID,
                    Customer_Code = customerBranch.Customer_Branch_Code,
                    Customer_Name = customerBranch.Customer_Branch_Name
                };

                // 請求区分に応じてモデルにデータを設定
                if (checkSeikyu.Check_Kubun == 1)
                {
                    model.DisplayDate = checkSeikyu.Seikyu_Month;
                    model.Qty = checkSeikyuChange.Qty ?? checkSeikyuDetail?.Qty;
                    model.Unit = checkSeikyuChange.Unit ?? checkSeikyuDetail?.Unit;
                    model.UnitPrice = checkSeikyuChange.UnitPrice ?? checkSeikyuDetail?.UnitPrice;
                    model.CalcPrice = checkSeikyuChange.CalcPrice ?? (model.UnitPrice != null && model.Qty != null ? model.UnitPrice.Value * (decimal)model.Qty.Value : null);
                    model.SeikyuUnchin = checkSeikyuChange.SeikyuUnchin ?? (model.CalcPrice);
                    model.Tatekaekin = checkSeikyuChange.Tatekaekin;
                    model.Warimashi1 = checkSeikyuChange.Warimashi1;
                    model.Warimashi2 = checkSeikyuChange.Warimashi2;
                    model.Warimashi3 = checkSeikyuChange.Warimashi3;
                    model.Warimashi4 = checkSeikyuChange.Warimashi4;
                    model.Warimashi5 = checkSeikyuChange.Warimashi5;
                    model.SeikyuTotal = checkSeikyuChange.SeikyuTotal;

                    // 変更前の情報を設定
                    model.PreviousQty = uriageUnchin.Qty;
                    model.PreviousUnit = uriageUnchin.Unit;
                    model.PreviousUnitPrice = uriageUnchin.UnitPrice;
                    model.PreviousCalcPrice = uriageUnchin.CalcPrice;
                    model.PreviousSeikyuUnchin = uriageUnchin.SeikyuUnchin;
                    model.PreviousTatekaekin = uriageUnchin.Tatekaekin;
                    model.PreviousWarimashi1 = uriageUnchin.Warimashi1;
                    model.PreviousWarimashi2 = uriageUnchin.Warimashi2;
                    model.PreviousWarimashi3 = uriageUnchin.Warimashi3;
                    model.PreviousWarimashi4 = uriageUnchin.Warimashi4;
                    model.PreviousWarimashi5 = uriageUnchin.Warimashi5;
                    model.PreviousSeikyuTotal = uriageUnchin.SeikyuTotal;
                }
                else if (checkSeikyu.Check_Kubun == 2)
                {
                    // 請求区分が2の場合の処理
                    model.DisplayDate = checkSeikyu.Seikyu_Month;

                    // 変更前と変更後の情報を同じにする（全てT_Uriage_Unchinから取得）
                    model.Qty = model.PreviousQty = uriageUnchin.Qty;
                    model.Unit = model.PreviousUnit = uriageUnchin.Unit;
                    model.UnitPrice = model.PreviousUnitPrice = uriageUnchin.UnitPrice;
                    model.CalcPrice = model.PreviousCalcPrice = uriageUnchin.CalcPrice;
                    model.SeikyuUnchin = model.PreviousSeikyuUnchin = uriageUnchin.SeikyuUnchin;
                    model.Tatekaekin = model.PreviousTatekaekin = uriageUnchin.Tatekaekin;
                    model.Warimashi1 = model.PreviousWarimashi1 = uriageUnchin.Warimashi1;
                    model.Warimashi2 = model.PreviousWarimashi2 = uriageUnchin.Warimashi2;
                    model.Warimashi3 = model.PreviousWarimashi3 = uriageUnchin.Warimashi3;
                    model.Warimashi4 = model.PreviousWarimashi4 = uriageUnchin.Warimashi4;
                    model.Warimashi5 = model.PreviousWarimashi5 = uriageUnchin.Warimashi5;
                    model.SeikyuTotal = model.PreviousSeikyuTotal = uriageUnchin.SeikyuTotal;
                }

                if (customerUriageCalc != null)
                {
                    model.Warimashi1_Visible = customerUriageCalc.Warimashi1_Visible;
                    model.Warimashi2_Visible = customerUriageCalc.Warimashi2_Visible;
                    model.Warimashi3_Visible = customerUriageCalc.Warimashi3_Visible;
                    model.Warimashi4_Visible = customerUriageCalc.Warimashi4_Visible;
                    model.Warimashi5_Visible = customerUriageCalc.Warimashi5_Visible;

                    model.Warimashi1_Name = customerUriageCalc.Warimashi1_Name;
                    model.Warimashi2_Name = customerUriageCalc.Warimashi2_Name;
                    model.Warimashi3_Name = customerUriageCalc.Warimashi3_Name;
                    model.Warimashi4_Name = customerUriageCalc.Warimashi4_Name;
                    model.Warimashi5_Name = customerUriageCalc.Warimashi5_Name;
                }
                return await PartialViewAsJson("IndexModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// BatchRegistrationModelの保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> BatchRegistration([FromBody] Dto.BatchRegistrationModel model)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                model.User_ID = loguinUser.User_ID;
                using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
                var result = await api.PostBatchRegistration(model);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// ModalApprovalModelの保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> PostModalApproval([FromBody] Dto.ModalApprovalModel model)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                model.User_ID = loguinUser.User_ID;
                using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
                var result = await api.PostModalApproval(model);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// ApprovalStatusModelの保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateApprovalStatus([FromBody] Dto.ApprovalStatusModel model)
        {
            try
            {
                var user = await GetLoginUser();
                model.User_ID = user.User_ID;
                using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
                var result = await api.UpdateApprovalStatus(model);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// JSON：集計データの返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="targetDate"></param>
        /// <returns></returns>
        private async Task<Dto.T_Print_Seikyu_Local> GetSyuukeiData(int? Check_Seikyu_ID)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            Dto.T_Print_Seikyu_Local SyuukeiData = await api.GetSyuukeiData(Check_Seikyu_ID);
            return SyuukeiData;
        }

        /// <summary>
        /// JSON：入金データの返却
        /// </summary>
        /// <param name="CheckSeikyu"></param>
        /// <returns></returns>
        private async Task<List<T_Nyukin_Local>> GetNyukinDataList(T_Check_Seikyu_Local CheckSeikyu)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            return await api.GetNyuukinDataList(CheckSeikyu);
        }

        /// <summary>
        /// JSON：請求明細データの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        private async Task<Dto.T_Print_Seikyu_Local> GetPrintSeikyuData(int Check_Seikyu_ID)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            Dto.T_Print_Seikyu_Local seikyuData = await api.GetT_PrintSeikyu(Check_Seikyu_ID);
            return seikyuData;
        }

        /// <summary>
        /// JSON：請求明細データの返却
        /// </summary>
        /// <param name="Print_Seikyu_ID"></param>
        /// <returns></returns>
        private async Task<List<Dto.T_Print_Seikyu_Detail_Local>> GetSeikyuDataUpList(int Print_Seikyu_ID)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.T_Print_Seikyu_Detail_Local> seikyuData = await api.GetT_PrintSeikyuDetailList(Print_Seikyu_ID);
            return seikyuData.ToList();
        }

        /// <summary>
        /// JSON：請求明細データの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        private async Task<List<Dto.T_Check_Seikyu_Change_Local>> GetSeikyuDataDownList(int Check_Seikyu_ID)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.T_Check_Seikyu_Change_Local> seikyuData = await api.GetT_Check_Seikyu_ChangeList(Check_Seikyu_ID);
            return seikyuData.ToList();
        }

        /// <summary>
        /// T_Check_Seikyu_Changeデータの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <param name="Uriage_Unchin_ID"></param>
        /// <returns></returns>
        private async Task<Dto.T_Check_Seikyu_Change_Local> GetCheckSeikyuChange(int Check_Seikyu_ID, int Uriage_Unchin_ID)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            Dto.T_Check_Seikyu_Change_Local seikyuData = await api.GetT_Check_Seikyu_Change(Check_Seikyu_ID, Uriage_Unchin_ID);
            return seikyuData;
        }

        /// <summary>
        /// JSON：請求明細データの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        private async Task<List<Dto.T_Check_Seikyu_Detail_Local>> GetSeikyuDetail(int Check_Seikyu_ID)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            List<Dto.T_Check_Seikyu_Detail_Local> seikyuData = await api.GetT_Check_Seikyu_Detail_All(Check_Seikyu_ID);
            return seikyuData;
        }

        /// <summary>
        /// T_Check_Seikyuデータの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        private async Task<Dto.T_Check_Seikyu_Local> GetSeikyu(int Check_Seikyu_ID)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            Dto.T_Check_Seikyu_Local seikyuData = await api.GetT_Check_Seikyu(Check_Seikyu_ID);
            return seikyuData;
        }

        /// <summary>
        /// T_Check_Seikyu_Doneデータの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        private async Task<Dto.T_Check_Seikyu_Done_Local> GetCheckSeikyuDone(int Check_Seikyu_ID)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            Dto.T_Check_Seikyu_Done_Local seikyuData = await api.GetT_Check_Seikyu_Done(Check_Seikyu_ID);
            return seikyuData;
        }

        /// <summary>
        /// M_Customer_Branchデータの返却
        /// </summary>
        /// <param name="Customer_Branch_ID"></param>
        /// <returns></returns>
        private async Task<Dto.M_Customer_Branch_Local> GetCustomerBranch(int Customer_Branch_ID)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            Dto.M_Customer_Branch_Local seikyuData = await api.GetM_Customer_Branch(Customer_Branch_ID);
            return seikyuData;
        }

        /// <summary>
        /// T_Uriage_Unchinデータの返却
        /// </summary>
        /// <param name="Uriage_Unchin_ID"></param>
        /// <returns></returns>
        private async Task<Dto.T_Uriage_Unchin_Local> GetUriageUnchin(int Uriage_Unchin_ID)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            Dto.T_Uriage_Unchin_Local seikyuData = await api.GetT_Uriage_Unchin(Uriage_Unchin_ID);
            return seikyuData;
        }

        /// <summary>
        /// M_Unitの取得
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <returns></returns>
        private async Task<List<Dto.M_Unit_Local>> GetUnit(int iCompanyID)
        {
            using API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_Unit_Local> unitData = await api.M_UnitList(iCompanyID);
            return unitData.ToList();
        }

        /// <summary>
        /// M_Customer_Uriage_Calcの取得
        /// </summary>
        /// <param name="Customer_Branch_ID"></param>
        /// <returns></returns>
        private async Task<Dto.M_Customer_Uriage_Calc_Local> GetCustomerUriageCalc(int Customer_Branch_ID)
        {
            using API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            Dto.M_Customer_Uriage_Calc_Local unitData = await api.GetCustomerUriageCalcData(Customer_Branch_ID);
            return unitData;
        }

        /// <summary>
        /// T_Anken_Detailの取得
        /// 案件情報→[T_Uriage_Unchin].「Uriage_ID」→[T_Uriage].「Anken_ID」→[T_Anken_Detail].「Work_Name」
        /// ※T_Print_Seikyuが無くなったことによる対応
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <returns>T_Anken_Detail_Local</returns>
        private async Task<Dto.T_Anken_Detail_Local> GetTAnkenDetailByUriageID(int Uriage_ID)
        {
            try
            {
                using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
                return await api.GetTAnkenDetailByUriageID(Uriage_ID);
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return default;
            }
        }

        /// <summary>
        /// M_SyaryoManagement_Localの取得
        /// // 車番→[T_Uriage_Unchin].「Uriage_ID」→[T_Uriage].「Nippou_ID」→[T_Nippou].「AnkenDisplay_ID」→[T_Haisya].「Syaryoumanagement_ID」→[M_Syaryomanagement].「Syaban_Number」
        /// ※T_Print_Seikyuが無くなったことによる対応
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <returns>M_SyaryoManagement_Local</returns>
        private async Task<Dto.M_SyaryoManagement_Local> GetMSyaryoManagementByUriageID(int Uriage_ID)
        {
            try
            {
                using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
                return await api.GetMSyaryoManagementByUriageID(Uriage_ID);
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return default;
            }
        }

        /// <summary>
        /// M_Syaryoの取得
        /// // 車種→[T_Uriage_Unchin].「Uriage_ID」→[T_Uriage].「Nippou_ID」→[T_Nippou].「AnkenDisplay_ID」→[T_Haisya].「Syaryoumanagement_ID」→[M_Syaryomanagement].「Syaryo_ID」→[M_Syaryo].「SyasyuDisplay」
        /// ※T_Print_Seikyuが無くなったことによる対応
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <returns>M_Syaryo_Local</returns>
        private async Task<Dto.M_Syaryo_Local> GetMSyaryoByUriageID(int Uriage_ID)
        {
            try
            {
                using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
                return await api.GetMSyaryoByUriageID(Uriage_ID);
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return default;
            }
        }

    }
}
