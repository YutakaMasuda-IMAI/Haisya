using HaisyaWeb.Dto;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.PurchasePaymentRegistrationModel_Local;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 仕入れ支払登録一覧コントローラー
    /// </summary>
    public class PurchasePaymentRegistrationController : BaseController
    {
        private readonly ILogger<PurchasePaymentRegistrationController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public PurchasePaymentRegistrationController(ILogger<PurchasePaymentRegistrationController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<HaisyaWeb.Models.MapApiSettings> mapApiSetting)
        {
            _logger = logger;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 仕入れ支払登録一覧
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(string jsondata)
        {
            try
            {
                DataListModel model = await CreateModel(jsondata);
                return View(model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 初期表示データを作成するメソッド
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns>初期表示データを含むDataListModelオブジェクト</returns> 
        private async Task<DataListModel> CreateModel(string jsondata)
        {
            DataListModel model = new();
            // JSON文字列からIndexModelオブジェクトをデシリアライズ
            IndexModel data = JsonConvert.DeserializeObject<IndexModel>(jsondata);
            model.Expense = new()
            {
                Expense_ID = data.Expense_id,
                Expense_Kubun = data.Expense_kubun,
            };
            model.Search = new()
            {
                Expense_category = data.Expense_kubun,
                Vehicle_number = data.Vehicle_number,
                Driver_code = data.Driver_code,
                Driver_name = data.Driver_name,
                Accident_year = data.Accident_year,
            };

            // ◆T_Expense．Expense_IDが渡された場合（＝Web07-0001「詳細」ボタン押下して遷移された場合）
            if (model.Expense.Expense_ID != 0)
            {
                ExpenseDataModel_Local expenseDataModel = await GetExpenseData(model.Expense.Expense_ID);

                model.Expense = expenseDataModel.TExpense;
                model.ExpensePayments = expenseDataModel.TExpensePayments;
                model.ExpenseItems = expenseDataModel.TExpenseItems;
                model.CompanyDriver = expenseDataModel.MCompanyDriver;
                model.Syaryo = expenseDataModel.MSyaryo;
                model.Venders = expenseDataModel.MVenders;
                model.PossibleExpenseItems = expenseDataModel.PossibleExpenseItems;
                model.Jiko = expenseDataModel.AccidentListItem;
            }
            // ◆T_Expense．Expense_ID＝Null、Expense_Kubunが渡された場合
            else if (model.Expense.Expense_Kubun != 0)
            {
                model.ExpensePayments = new()
                {
                    new()
                    {
                        Remarks = "",
                    }
                };
                model.Venders = new() { new() };

                List<T_Expense_Item_Local> possibleExpenseItems = await GetTExpenseItemByKubun(model.Expense.Expense_Kubun);

                if (possibleExpenseItems != null && possibleExpenseItems.Count != 0)
                { 
                    model.PossibleExpenseItems = possibleExpenseItems;

                    List<T_Expense_Item_Local> defaultExpenseItems = possibleExpenseItems;

                    model.ExpensePayments[0].Expense_Item_ID = possibleExpenseItems[0].Expense_Item_ID;

                    model.ExpenseItems = defaultExpenseItems;
                }

                model.IsNewExpense = true;
                model.Jiko = new();
            }

            return model;
        }

        /// <summary>
        /// 指定された費用IDに基づいて費用データを取得します。
        /// </summary>
        /// <param name="expenseId">取得する費用のID。</param>
        /// <returns>指定された費用IDに基づいて取得した <see cref="ExpenseDataModel_Local"/> オブジェクト。</returns>
        /// <remarks>
        /// このメソッドは、API を使用して指定された費用IDに関連する費用データを非同期に取得します
        private async Task<ExpenseDataModel_Local> GetExpenseData(int expenseId)
        {
            using API.WebApp.PurchasePaymentRegistrationApi api = new(_mapApiSettiong);
            return await api.GetExpenseData(expenseId);
        }

        /// <summary>
        /// 指定された費用区分に基づいて費用アイテムのリストを取得します。
        /// </summary>
        /// <param name="expenseKubun">取得する費用アイテムの区分。</param>
        /// <returns>指定された費用区分に基づいて取得した <see cref="List{T_Expense_Item_Local}"/> オブジェクトのリスト。</returns>
        /// <remarks>
        /// このメソッドは、API を使用して指定された費用区分に関連する費用アイテムを非同期に取得します。
        /// </remarks>
        public async Task<List<T_Expense_Item_Local>> GetTExpenseItemByKubun(int expenseKubun)
        {
            using API.WebApp.PurchasePaymentRegistrationApi api = new(_mapApiSettiong);
            return await api.GetTExpenseItemByKubun(expenseKubun);
        }

        /// <summary>
        /// 備考モーダルを表示します。
        /// </summary>
        /// <param name="remarks">表示する備考の内容。</param>
        /// <param name="index">インデックス。</param>
        /// <param name="shimeKubun">締め区分。</param>
        /// <returns>備考モーダルを含む部分ビュー。</returns>
        /// <remarks>
        /// このメソッドは、指定された備考、インデックス、および締め区分を使用して備考モーダルを非同期に表示します。
        /// </remarks>
        public async Task<IActionResult> BikouModal(string remarks, int index, int shimeKubun)
        {
            try
            {
                // 親ビューのコンテキストを使って備考モーダルを開く
                BikouModel model = new()
                {
                    ExpensePayment = new()
                    {
                        Remarks = remarks,
                        Shime_Kubun = shimeKubun
                    },
                    Index = index
                };
                return await PartialViewAsJson("BikouModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 費用アイテム追加モーダルを表示します。
        /// </summary>
        /// <param name="possibleExpenseItems">追加可能な費用アイテムのリスト。</param>
        /// <returns>費用アイテム追加モーダルを含む部分ビュー。</returns>
        /// <remarks>
        /// このメソッドは、指定された費用アイテムのリストを使用して費用アイテム追加モーダルを非同期に表示します。
        /// モーダルには、新しい費用支払いとベンダーの初期データも含まれます。
        /// </remarks>
        public async Task<IActionResult> AddExpenseItemModal(List<T_Expense_Item_Local> possibleExpenseItems)
        {
            try
            {
                DataListModel model = new()
                {
                    PossibleExpenseItems = possibleExpenseItems,
                    ExpensePayments = new()
                    {
                        new()
                        {
                            Remarks = "",
                        }
                    },
                    Venders = new() { new() }
                };
                return await PartialViewAsJson("AddExpenseItemModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 費用と関連する支払いを登録します。
        /// </summary>
        /// <param name="expense">登録する費用オブジェクト。</param>
        /// <param name="expensePayments">登録する費用支払いのリスト。</param>
        /// <returns>非同期操作を表すタスク。</returns>
        /// <remarks>
        /// このメソッドは、ログインユーザーの情報を使用して費用および関連する支払いの登録を行います。
        /// 登録処理には、API を通じて費用および支払いデータを送信します。
        /// </remarks>
        public async Task<IActionResult> RegisterExpenses(T_Expense_Local expense, List<T_Expense_Payment_Local> expensePayments)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                T_Expense_Local expenseUpdatedUser = expense;

                expenseUpdatedUser.Update_User = loguinUser.User_ID;

                expenseUpdatedUser.Company_ID = loguinUser.Company_ID;

                List<T_Expense_Payment_Local> expensePaymentsUpdatedUser = new List<T_Expense_Payment_Local>();

                foreach (var expensePayment in expensePayments)
                {
                    expensePayment.Update_User = loguinUser.User_ID;
                    expensePaymentsUpdatedUser.Add(expensePayment);
                }
                using API.WebApp.PurchasePaymentRegistrationApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.RegisterExpenses(expenseUpdatedUser, expensePaymentsUpdatedUser);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 車両管理IDに基づいて車両データを取得します。
        /// </summary>
        /// <param name="syaryoManagementId">車両管理のID。</param>
        /// <returns>指定された車両管理IDに基づいて取得した <see cref="M_Syaryo_Local"/> オブジェクト。該当する費用データが存在する場合は null を返します。</returns>
        /// <remarks>
        /// このメソッドは、指定された車両管理IDに関連する費用データの存在を確認します。
        /// もし関連する費用データが存在する場合（費用IDが0でない場合）は、null を返します。
        /// それ以外の場合は、車両データを非同期に取得します。
        /// </remarks>
        public async Task<M_Syaryo_Local> SyaryoBySyaryoManagementId(int syaryoManagementId)
        {
            try
            {
                using API.WebApp.PurchasePaymentRegistrationApi api = new(_mapApiSettiong);

                // ◆Expense_Kubun＝2：車輌経費			
                //①T_Expense．Driver_ID＝検索ダイアログで選択したM_Syaryo．Syaryo_IDの		
                //M_SyaryoManagement．SyaryoManagement_IDデータが存在	
                //・エラーメッセージ：「既に登録された情報が存在します。」
                T_Expense_Local tExpenseCheck = await api.GetTExpenseByDriverIdOrSyaryoManagementId(0, syaryoManagementId, 0);
                return tExpenseCheck.Expense_ID != 0 ? null : await api.GetSyaryoBySyaryoManagementId(syaryoManagementId);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }

        /// <summary>
        /// ドライバーIDに基づいて費用データを取得します。
        /// </summary>
        /// <param name="driverId">ドライバーのID。</param>
        /// <returns>指定されたドライバーIDに基づいて取得した <see cref="T_Expense_Local"/> オブジェクト。該当する費用データが存在する場合は null を返します。</returns>
        /// <remarks>
        /// このメソッドは、指定されたドライバーIDに関連する費用データの存在を確認します。
        /// もし関連する費用データが存在する場合（費用IDが0でない場合）は、null を返します。
        /// それ以外の場合は、ドライバーIDに基づく費用データを非同期に取得します。
        /// </remarks>
        public async Task<T_Expense_Local> TExpenseByDriverID(int driverId)
        {
            try
            {
                using API.WebApp.PurchasePaymentRegistrationApi api = new(_mapApiSettiong);

                //◆Expense_Kubun＝1：乗務員経費																
                //①T_Expense．Driver_ID＝検索ダイアログで選択したM_CompanyDriver．Driver_IDのデータが存在															
                //・エラーメッセージ：「既に登録された情報が存在します。」														

                //②T_Expense．Driver_ID＝検索ダイアログで選択したM_CompanyDriver．Driver_IDのデータが存在ないし															
                //・戻り値：M_CompanyDriver．Display_Nameを表示														

                T_Expense_Local tExpenseCheck = await api.GetTExpenseByDriverIdOrSyaryoManagementId(driverId, 0, 0);
                return tExpenseCheck.Expense_ID != 0 ? null : tExpenseCheck;
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }

        /// <summary>
        /// 事故IDに基づいて費用データを取得します。
        /// </summary>
        /// <param name="jikoId">事故のID。</param>
        /// <returns>指定された事故IDに基づいて取得した <see cref="T_Expense_Local"/> オブジェクト。該当する費用データが存在する場合は null を返します。</returns>
        /// <remarks>
        /// このメソッドは、指定された事故IDに関連する費用データの存在を確認します。
        /// もし関連する費用データが存在する場合（費用IDが0でない場合）は、null を返します。
        /// それ以外の場合は、事故IDに基づく費用データを非同期に取得します。
        public async Task<T_Expense_Local> GetTExpenseByJikoId(int jikoId)
        {
            try
            {
                using API.WebApp.PurchasePaymentRegistrationApi api = new(_mapApiSettiong);

                // ◆Expense_Kubun＝3：事故経費															
                // ①T_Expense．Jiko_ID＝検索ダイアログで選択したT_Jiko．Jiko_IDのデータが存在する場合														
                // ・エラーメッセージ：「既に登録された情報が存在します。」													
                T_Expense_Local tExpenseCheck = await api.GetTExpenseByDriverIdOrSyaryoManagementId(0, 0, jikoId);
                if (tExpenseCheck.Expense_ID != 0)
                {
                    return null;
                }

                return tExpenseCheck;
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }
    }
}
