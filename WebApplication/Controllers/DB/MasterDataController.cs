using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Dto;
using WebApplication.Model;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// マスターデータを管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MasterDataController : MyBaseController
    {
        private readonly ILogger<MasterDataController> _logger;

        /// <summary>
        /// MasterDataControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="context">データベースコンテキスト</param>
        public MasterDataController(ILogger<MasterDataController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        #region M_Company
        /*****************************************************************************
          M_Company
          *****************************************************************************/
        /// <summary>
        /// M_Companyの取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>M_Companyデータ</returns>
        [HttpGet("M_CompanyData")]
        public async Task<IActionResult> M_CompanyData(int CompanyID)
        {
            try
            {
                IQueryable<Data.M_Company> query = _context.M_Companies.Where(m => m.Company_ID == CompanyID);
                Data.M_Company res = await query.FirstOrDefaultAsync();
                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// M_Companyの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式の会社データ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCompanyData")]
        public async Task<IActionResult> InsertUpdateCompanyData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                Data.M_Company dto = GetMultipartFormDataContentData<Data.M_Company>();

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCompanyData(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Company

        #region M_CompanyUser
        /*****************************************************************************
          M_CompanyUser
          *****************************************************************************/
        /// <summary>
        /// M_CompanyUserリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="kubun">"eigyo"：営業担当、"tantou"：配車担当、"seikyu"：請求、"etc"：その他</param>
        /// <param name="NotDel">削除フラグ</param>
        /// <returns>M_CompanyUserリスト</returns>
        [HttpGet("M_CompanyUsersList")]
        public async Task<List<M_CompanyUser>> M_CompanyUsersList(int CompanyID, string kubun, int NotDel = 0)
        {
            IQueryable<M_CompanyUser> list = _context.M_CompanyUsers;

            try
            {
                if ("eigyo".Equals(kubun.ToLower()))
                {
                    list = list.Where(m => m.Company_ID == CompanyID && m.Eigyo_Flg);
                }
                else if ("tantou".Equals(kubun.ToLower()))
                {
                    list = list.Where(m => m.Company_ID == CompanyID && m.Tantou_Flg);
                }
                else if ("seikyu".Equals(kubun.ToLower()))
                {
                    list = list.Where(m => m.Company_ID == CompanyID && m.Seikyu_Flg);
                }
                else
                {
                    list = list.Where(m => m.Company_ID == CompanyID);
                }

                if (NotDel == 1)
                {
                    list = list.Where(m => m.Del_Flg == false);
                }

                return await list.OrderBy(m => m.Employee_Number).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// M_CompanyUserのデータ取得
        /// </summary>
        /// <param name="UserID">ユーザーID</param>
        /// <returns>M_CompanyUserデータ</returns>
        [HttpGet("M_CompanyUsersData")]
        public async Task<Data.M_CompanyUser> M_CompanyUsersData(int UserID)
        {
            try
            {
                Data.M_CompanyUser m_CompanyUser = await _context.M_CompanyUsers.FirstOrDefaultAsync(m => m.User_ID == UserID);
                return m_CompanyUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// M_CompanyUserの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のユーザーデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCompanyUserMasterData")]
        public async Task<IActionResult> InsertUpdateCompanyUserMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_CompanyUser m_CompanyUser = System.Text.Json.JsonSerializer.Deserialize<Data.M_CompanyUser>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCompanyUserMasterData(m_CompanyUser);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_CompanyUser

        #region M_CompanyUser_Group
        /*****************************************************************************
          M_CompanyUser_Group
          *****************************************************************************/
        /// <summary>
        /// M_CompanyUser_Groupリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="GroupKubun">グループ区分</param>
        /// <param name="GroupID">グループID</param>
        /// <returns>M_CompanyUser_Groupリスト</returns>
        [HttpGet("M_CompanyUserGroupList")]
        public async Task<IActionResult> M_CompanyUserGroupList(int CompanyID, int GroupKubun = 0, int GroupID = 0)
        {
            IQueryable<Data.M_CompanyUser_Group> data = _context.M_CompanyUser_Groups;
            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                data = data.Where(m => m.Company_ID == CompanyID);

                if (GroupID > 0) data = data.Where(m => m.Group_ID == GroupID);
                if (GroupKubun > 0) data = data.Where(m => m.Group_Kubun == GroupKubun);

                IEnumerable<Data.M_CompanyUser_Group> res = await data.OrderBy(m => m.SortOrder).ToListAsync();
                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// ユーザーIDに基づいてM_CompanyUser_Groupリストを取得
        /// </summary>
        /// <param name="UserID">ユーザーID</param>
        /// <returns>M_CompanyUser_Groupリスト</returns>
        [HttpGet("CompanyUserGroupListFromUserID")]
        public async Task<IActionResult> CompanyUserGroupListFromUserID(int UserID)
        {
            List<Data.M_CompanyUser_Group> data = null;
            try
            {
                if (UserID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                data = await _context.M_CompanyUser_Groups.Select(s => s)
                                    .Where(t1 => _context.M_CompanyUser_GroupUsers.Where(m => m.User_ID == UserID).Select(m => m.Group_ID).AsQueryable().Contains(t1.Group_ID)).ToListAsync();

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_CompanyUser_Groupの並び順変更処理
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("UpdateCompanyUserGroupListSortOrder")]
        public async Task<IActionResult> UpdateCompanyUserGroupListSortOrder()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                Dto.CompanyUserGroupDto dataDto = GetMultipartFormDataContentData<Dto.CompanyUserGroupDto>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.UpdateCompanyUserGroupListSortOrder(dataDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_CompanyUser_Groupの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCompanyUserGroupMasterData")]
        public async Task<IActionResult> InsertUpdateCompanyUserGroupMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                Dto.CompanyUserGroupDto dataDto = GetMultipartFormDataContentData<Dto.CompanyUserGroupDto>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCompanyUserGroupMasterData(dataDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタの削除
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("DeleteCompanyUserGroupMasterData")]
        public async Task<IActionResult> DeleteCompanyUserGroupMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                Data.M_CompanyUser_Group dataDto = GetMultipartFormDataContentData<Data.M_CompanyUser_Group>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.DeleteCompanyUserGroupMasterData(dataDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_CompanyUser_Group

        #region M_CompanyUser_GroupUser
        /*****************************************************************************
          M_CompanyUser_GroupUser
          *****************************************************************************/
        /// <summary>
        /// M_CompanyUser_GroupUserリストのデータ返却
        /// </summary>
        /// <param name="GroupID">グループID</param>
        /// <returns>M_CompanyUser_GroupUserリスト</returns>
        [HttpGet("M_CompanyUserGroupUserList")]
        public async Task<IActionResult> M_CompanyUserGroupUserList(int GroupID)
        {
            IEnumerable<Data.M_CompanyUser_GroupUser> data = null;

            try
            {
                if (GroupID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                data = await _context.M_CompanyUser_GroupUsers.Where(m => m.Group_ID == GroupID).OrderBy(m => m.User_ID).ToListAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }
        #endregion M_CompanyUser_GroupUser

        #region M_CompanyDriver
        /*****************************************************************************
          M_CompanyDriver
          *****************************************************************************/
        /// <summary>
        /// M_CompanyDriverリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="NotDel">削除フラグ</param>
        /// <param name="UserID">ユーザーID</param>
        /// <returns>M_CompanyDriverリスト</returns>
        [HttpGet("M_CompanyDriversList")]
        public async Task<IActionResult> M_CompanyDriversList(int CompanyID, int NotDel = 0, int UserID = 0)
        {
            try
            {
                IQueryable<Data.M_CompanyDriver> list = _context.M_CompanyDrivers.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.Employee_Number);

                if (NotDel == 1)
                {
                    list = list.Where(m => m.Del_Flg == false);
                }

                if (UserID > 0)
                {
                    IQueryable<int> subquery = _context.M_CompanyUser_GroupUsers.Where(m => m.User_ID == UserID).Select(m => m.Group_ID);
                    IQueryable<int> subquery2 = _context.M_CompanyDriver_Syaryos.Where(m => subquery.AsQueryable().Contains(m.Group_ID) && ((m.End_Date == null ? DateTime.Parse("2999/01/01") : m.End_Date) >= DateTime.Now)).Select(m => m.Driver_ID);
                    list = list.Where(m => subquery2.AsQueryable().Contains(m.Driver_ID));
                }

                List<Data.M_CompanyDriver> result = await list.OrderBy(m => m.Employee_Number).ToListAsync();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }

        }

        /// <summary>
        /// M_CompanyDriverのデータ取得
        /// </summary>
        /// <param name="DriverID">ドライバーID</param>
        /// <returns>M_CompanyDriverデータ</returns>
        [HttpGet("M_CompanyDriversData")]
        public async Task<Data.M_CompanyDriver> M_CompanyDriversData(int DriverID)
        {
            try
            {
                Data.M_CompanyDriver m_CompanyDriver = await _context.M_CompanyDrivers.FirstOrDefaultAsync(m => m.Driver_ID == DriverID);
                return m_CompanyDriver;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// M_CompanyDriverの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のドライバーデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCompanyDriverMasterData")]
        public async Task<IActionResult> InsertUpdateCompanyDriverMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                Data.M_CompanyDriver dataDto = GetMultipartFormDataContentData<Data.M_CompanyDriver>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCompanyDriverMasterData(dataDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_CompanyDriver

        #region V_CompanyDriver
        /// <summary>
        /// V_CompanyDriverリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="NotDel">削除フラグ</param>
        /// <param name="haisyaGroupID">配車グループID</param>
        /// <param name="driverId">ドライバーID</param>
        /// <returns>V_CompanyDriverリスト</returns>
        [HttpGet("GetVCompanyDriversList")]
        public async Task<IActionResult> GetVCompanyDriversList(int CompanyID, int NotDel = 0,
                                                                    int haisyaGroupID = 0, int driverId = 0)
        {
            try
            {
                IQueryable<V_CompanyDriver> builder = _context.V_CompanyDrivers.Where(m => m.Company_ID == CompanyID);

                if (driverId > 0) builder = builder.Where(m => m.Driver_ID == driverId);

                if (haisyaGroupID > 0) builder = builder.Where(m => m.Group_ID == haisyaGroupID);

                List<V_CompanyDriver> list = await builder.ToListAsync();

                return new OkObjectResult(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }
        #endregion V_CompanyDriver

        #region M_Role
        /*****************************************************************************
          M_Role
          *****************************************************************************/
        /// <summary>
        /// M_Roleリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="Role">役割</param>
        /// <returns>M_Roleリスト</returns>
        [HttpGet("M_RoleList")]
        public async Task<IActionResult> M_RoleList(int CompanyID, int Role)
        {
            try
            {
                if (CompanyID == 0 || Role == 0) { throw new Exception("パラメーターエラー：CompanyID,Role"); }

                IEnumerable<Data.M_Role> list = null;
                list = await _context.M_Roles.Where(m => m.Company_ID == CompanyID && m.Role == Role).ToListAsync();
                return new OkObjectResult(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }
        #endregion M_Role

        #region M_CompanyBranch
        /*****************************************************************************
          M_CompanyBranch
          *****************************************************************************/
        /// <summary>
        /// M_CompanyBranchリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="kubun">区分</param>
        /// <returns>M_CompanyBranchリスト</returns>
        [HttpGet("M_CompanyBranchList")]
        public async Task<IEnumerable<Data.M_CompanyBranch>> M_CompanyBranchList(int CompanyID, string kubun)
        {
            try
            {
                IEnumerable<Data.M_CompanyBranch> list = null;
                list = await _context.M_CompanyBranches.Where(m => m.Company_ID == CompanyID).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_CompanyBranchのデータ取得
        /// </summary>
        /// <param name="BranchID">支店ID</param>
        /// <returns>M_CompanyBranchデータ</returns>
        [HttpGet("M_CompanyBranch")]
        public async Task<Data.M_CompanyBranch> M_CompanyBranch(int BranchID)
        {
            try
            {
                Data.M_CompanyBranch data = null;
                data = await _context.M_CompanyBranches.FirstOrDefaultAsync(m => m.Branch_ID == BranchID);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_CompanyBranchの並び順変更処理
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("UpdateSortOrderForCompanyBranch")]
        public async Task<IActionResult> UpdateSortOrderForCompanyBranch()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_CompanyBranch> param = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_CompanyBranch>>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.UpdateSortOrderForCompanyBranch(param);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_CompanyBranchの追加更新処理
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateForCompanyBranch")]
        public async Task<IActionResult> InsertUpdateForCompanyBranch()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_CompanyBranch param = System.Text.Json.JsonSerializer.Deserialize<Data.M_CompanyBranch>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateDataForCompanyBranch(param);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_CompanyBranch

        #region M_Kata
        /*****************************************************************************
          M_Kata
          *****************************************************************************/
        /// <summary>
        /// M_Kataリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>M_Kataリスト</returns>
        [HttpGet("KataList")]
        public async Task<IActionResult> KataList(int CompanyID)
        {
            IEnumerable<Data.M_Katum> data = null;
            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }
                data = await _context.M_Kata.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.SortOrder).ToListAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_Kataの並び順変更処理
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("UpdateKataSortOrder")]
        public async Task<IActionResult> UpdateKataSortOrder()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_Katum> dataDto = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_Katum>>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.UpdateKataListSortOrder(dataDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_Kataの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateKataMasterData")]
        public async Task<IActionResult> InsertUpdateKataMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_Katum m_Kata = System.Text.Json.JsonSerializer.Deserialize<Data.M_Katum>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateKataMasterData(m_Kata);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Kata

        #region M_SyaryoSize
        /*****************************************************************************
          M_SyaryoSize
          *****************************************************************************/
        /// <summary>
        /// M_SyaryoSizeリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="size">サイズ</param>
        /// <returns>M_SyaryoSizeリスト</returns>
        [HttpGet("M_SyaryoSizeList")]
        public async Task<IActionResult> M_SyaryoSizeList(int CompanyID, string size)
        {
            IEnumerable<Data.M_SyaryoSize> data = null;
            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                if (size != null)
                {
                    data = await _context.M_SyaryoSizes.Where(m => m.Company_ID == CompanyID && m.SIZE == size).OrderBy(m => m.SortOrder).ToListAsync();
                }
                else
                {
                    data = await _context.M_SyaryoSizes.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.SortOrder).ToListAsync();
                }

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }

        }

        /// <summary>
        /// M_SyaryoSizeの並び順変更処理
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("UpdateSyaryoSizeSortOrder")]
        public async Task<IActionResult> UpdateSyaryoSizeSortOrder()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Dto.SaryoSizeListDto dataDto = System.Text.Json.JsonSerializer.Deserialize<Dto.SaryoSizeListDto>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.UpdateSyaryoSizeListSortOrder(dataDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_SyaryoSizeの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateSyaryoSizeMasterData")]
        public async Task<IActionResult> InsertUpdateSyaryoSizeMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_SyaryoSize m_SyaryoSize = System.Text.Json.JsonSerializer.Deserialize<Data.M_SyaryoSize>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateSyaryoSizeMasterData(m_SyaryoSize);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_SyaryoSize

        #region M_Syaryo
        /*****************************************************************************
          M_Syaryo
          *****************************************************************************/
        /// <summary>
        /// M_Syaryoリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="Syasyu">車種</param>
        /// <param name="Kata">型</param>
        /// <param name="Size">サイズ</param>
        /// <param name="SyaryoID">車両ID</param>
        /// <returns>M_Syaryoリスト</returns>
        [HttpGet("M_SyaryoList")]
        public async Task<IActionResult> M_SyaryoList(int CompanyID, string Syasyu, string Kata, string Size, int SyaryoID = 0)
        {
            IQueryable<Data.M_Syaryo> data = _context.M_Syaryos;
            try
            {
                data = data.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.SortOrder);

                if (Syasyu != null)
                {
                    data = data.Where(m => m.SYASYU == Syasyu).OrderBy(m => m.SortOrder);
                }
                if (Kata != null)
                {
                    data = data.Where(m => m.KATA == Kata).OrderBy(m => m.SortOrder);
                }
                if (Size != null)
                {
                    data = data.Where(m => m.SIZE == Size).OrderBy(m => m.SortOrder);
                }

                if (SyaryoID > 0)
                {
                    data = data.Where(m => m.Syaryo_ID == SyaryoID).OrderBy(m => m.SortOrder);
                }

                IEnumerable<Data.M_Syaryo> res = await data.ToListAsync();
                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_Syaryoの並び順変更処理
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("UpdateSyaryoListSortOrder")]
        public async Task<IActionResult> UpdateSyaryoListSortOrder()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Dto.SaryoListDto dataDto = System.Text.Json.JsonSerializer.Deserialize<Dto.SaryoListDto>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.UpdateSyaryoListSortOrder(dataDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_Syaryoの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateSyaryoMasterData")]
        public async Task<IActionResult> InsertUpdateSyaryoMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_Syaryo m_Syaryo = System.Text.Json.JsonSerializer.Deserialize<Data.M_Syaryo>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateSyaryoMasterData(m_Syaryo);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_Syaryoマスタの削除
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("DeleteSyaryoMasterData")]
        public async Task<IActionResult> DeleteSyaryoMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_Syaryo m_Syaryo = System.Text.Json.JsonSerializer.Deserialize<Data.M_Syaryo>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.DeleteSyaryoMasterData(m_Syaryo);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);

        }
        #endregion M_Syaryo

        #region M_SyaryoCost
        /*****************************************************************************
          M_SyaryoCost
          *****************************************************************************/
        /// <summary>
        /// 車輌原価計算マスタ
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="SyaryoID">車両ID</param>
        /// <returns>M_SyaryoCostリスト</returns>
        [HttpGet("M_SyaryoCostList")]
        public async Task<IActionResult> M_SyaryoCostList(int CompanyID, int SyaryoID)
        {
            List<Data.M_SyaryoCost> data = null;
            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                data = await _context.M_SyaryoCosts.Where(m => m.Company_ID == CompanyID).ToListAsync();

                if (SyaryoID > 0)
                {
                    data = data.Where(m => m.Syaryo_ID == SyaryoID).ToList();
                }

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_SyaryoCostのデータ更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateSyaryoCostData")]
        public async Task<IActionResult> InsertUpdateSyaryoCostData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_SyaryoCost> m_SyaryoCost = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_SyaryoCost>>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateSyaryoCostData(m_SyaryoCost);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_SyaryoCost

        #region M_DefaultMoney
        /*****************************************************************************
          M_DefaultMoney
          *****************************************************************************/
        /// <summary>
        /// M_DefaultMoneyリストのデータ返却
        /// </summary>
        /// <param name="Area">エリア</param>
        /// <param name="SyasyuSize">車種サイズ</param>
        /// <returns>M_DefaultMoneyリスト</returns>
        [HttpGet("M_DefaultMoneyList")]
        public async Task<List<Data.M_DefaultMoney>> M_DefaultMoneyList(string Area, string SyasyuSize)
        {
            try
            {
                List<Data.M_DefaultMoney> data = null;

                if (SyasyuSize == null && Area == null)
                {
                    data = await _context.M_DefaultMoneys.ToListAsync();
                }
                else if (SyasyuSize == null && Area != null)
                {
                    data = await _context.M_DefaultMoneys.Where(m => m.Area == Area).ToListAsync();
                }
                else if (SyasyuSize != null && Area == null)
                {
                    data = await _context.M_DefaultMoneys.Where(m => m.SyasyuSize == SyasyuSize).ToListAsync();
                }
                else
                {
                    data = await _context.M_DefaultMoneys.Where(m => m.SyasyuSize == SyasyuSize && m.Area == Area).ToListAsync();
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_DefaultMoneyのデータ更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateDefaultMoneyData")]
        public async Task<IActionResult> InsertUpdateDefaultMoneyData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_DefaultMoney> m_DefaultMoney = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_DefaultMoney>>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateDefaultMoneyData(m_DefaultMoney);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_DefaultMoney

        #region M_DefaultMoney_WaitTimeForArea
        /*****************************************************************************
          M_DefaultMoney_WaitTimeForArea
          *****************************************************************************/
        /// <summary>
        /// M_DefaultMoney_WaitTimeForAreaのデータ取得
        /// </summary>
        /// <param name="Area">エリア</param>
        /// <param name="SyasyuSize">車種サイズ</param>
        /// <returns>M_DefaultMoney_WaitTimeForAreaデータ</returns>
        [HttpGet("M_DefaultMoneyWaitTimeForAreaData")]
        public async Task<IActionResult> M_DefaultMoneyWaitTimeForAreaData(string Area, string SyasyuSize)
        {
            Data.M_DefaultMoney_WaitTimeForArea data = null;
            try
            {
                if (SyasyuSize == null || Area == null) { throw new Exception("パラメーターエラー：SyasyuSize,Area"); }

                data = await _context.M_DefaultMoney_WaitTimeForAreas.FirstOrDefaultAsync(m => m.SyasyuSize == SyasyuSize && m.Area == Area);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_DefaultMoney_WaitTimeForAreaリストのデータ返却
        /// </summary>
        /// <param name="Area">エリア</param>
        /// <param name="SyasyuSize">車種サイズ</param>
        /// <returns>M_DefaultMoney_WaitTimeForAreaリスト</returns>
        [HttpGet("M_DefaultMoneyWaitTimeForAreaList")]
        public async Task<List<Data.M_DefaultMoney_WaitTimeForArea>> M_DefaultMoneyWaitTimeForAreaList(string Area, string SyasyuSize)
        {
            try
            {
                List<Data.M_DefaultMoney_WaitTimeForArea> data = null;

                if (SyasyuSize == null && Area == null)
                {
                    data = await _context.M_DefaultMoney_WaitTimeForAreas.ToListAsync();
                }
                else if (SyasyuSize == null && Area != null)
                {
                    data = await _context.M_DefaultMoney_WaitTimeForAreas.Where(m => m.Area == Area).ToListAsync();
                }
                else if (SyasyuSize != null && Area == null)
                {
                    data = await _context.M_DefaultMoney_WaitTimeForAreas.Where(m => m.SyasyuSize == SyasyuSize).ToListAsync();
                }
                else
                {
                    data = await _context.M_DefaultMoney_WaitTimeForAreas.Where(m => m.SyasyuSize == SyasyuSize && m.Area == Area).ToListAsync();
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_DefaultMoneyWaitTimeForEriaのデータ更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateDefaultMoneyWaitTimeForAreaData")]
        public async Task<IActionResult> InsertUpdateDefaultMoneyWaitTimeForAreaData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_DefaultMoney_WaitTimeForArea> m_DefaultMoneyWaitTimeForEria = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_DefaultMoney_WaitTimeForArea>>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateDefaultMoneyWaitTimeForEriaData(m_DefaultMoneyWaitTimeForEria);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_DefaultMoney_WaitTimeForErium

        #region M_DefaultMoney_WaitTimeForCompany
        /*****************************************************************************
          M_DefaultMoney_WaitTimeForCompany
          *****************************************************************************/
        /// <summary>
        /// M_DefaultMoney_WaitTimeForCompanyリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="Area">エリア</param>
        /// <param name="SyasyuSize">車種サイズ</param>
        /// <returns>M_DefaultMoney_WaitTimeForCompanyリスト</returns>
        [HttpGet("M_DefaultMoneyWaitTimeForCompanyList")]
        public async Task<List<Data.M_DefaultMoney_WaitTimeForCompany>> M_DefaultMoneyWaitTimeForCompanyList(int CompanyID, string Area, string SyasyuSize)
        {
            try
            {
                List<Data.M_DefaultMoney_WaitTimeForCompany> data = null;

                if (SyasyuSize == null && Area == null)
                {
                    data = await _context.M_DefaultMoney_WaitTimeForCompanies.Where(m => m.Company_ID == CompanyID).ToListAsync();
                }
                else if (SyasyuSize == null && Area != null)
                {
                    data = await _context.M_DefaultMoney_WaitTimeForCompanies.Where(m => m.Company_ID == CompanyID && m.Area == Area).ToListAsync();
                }
                else if (SyasyuSize != null && Area == null)
                {
                    data = await _context.M_DefaultMoney_WaitTimeForCompanies.Where(m => m.Company_ID == CompanyID && m.SyasyuSize == SyasyuSize).ToListAsync();
                }
                else
                {
                    data = await _context.M_DefaultMoney_WaitTimeForCompanies.Where(m => m.SyasyuSize == SyasyuSize && m.Area == Area && m.Company_ID == CompanyID).ToListAsync();
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }

        }

        /// <summary>
        /// M_DefaultMoneyWaitTimeForCompanyのデータ更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateDefaultMoneyWaitTimeForCompanyData")]
        public async Task<IActionResult> InsertUpdateDefaultMoneyWaitTimeForCompanyData(int CompanyID)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_DefaultMoney_WaitTimeForCompany> m_DefaultMoneyWaitTimeForCompany = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_DefaultMoney_WaitTimeForCompany>>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateDefaultMoneyWaitTimeForCompanyData(CompanyID, m_DefaultMoneyWaitTimeForCompany);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_DefaultMoney_WaitTimeForCompany

        #region M_PersonnelExpense
        /*****************************************************************************
          M_PersonnelExpense
          *****************************************************************************/
        /// <summary>
        /// M_PersonnelExpenseリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="Syasyu">車種</param>
        /// <param name="Kata">型</param>
        /// <returns>M_PersonnelExpenseリスト</returns>
        [HttpGet("M_PersonnelExpenseList")]
        public async Task<IActionResult> M_PersonnelExpenseList(int CompanyID, string Syasyu, string Kata)
        {
            List<Data.M_PersonnelExpense> data = null;
            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                data = await _context.M_PersonnelExpenses.Where(m => m.Company_ID == CompanyID).ToListAsync();

                if (Syasyu != null)
                {
                    data = data.Where(m => m.Syasyu == Syasyu).ToList();
                }

                if (Kata != null)
                {
                    data = data.Where(m => m.Kata == Kata).ToList();
                }

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_PersonnelExpenseのデータ更新
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdatePersonnelExpenseData")]
        public async Task<IActionResult> InsertUpdatePersonnelExpenseData(int CompanyID)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_PersonnelExpense> m_PersonnelExpense = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_PersonnelExpense>>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdatePersonnelExpenseData(CompanyID, m_PersonnelExpense);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_PersonnelExpense

        #region M_FuelCost
        /*****************************************************************************
          M_FuelCost
          *****************************************************************************/
        /// <summary>
        /// M_FuelCostリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>M_FuelCostリスト</returns>
        [HttpGet("M_FuelCostList")]
        public async Task<IActionResult> M_FuelCostList(int CompanyID)
        {
            List<Data.M_FuelCost> data = null;
            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                data = await _context.M_FuelCosts.Where(m => m.Company_ID == CompanyID).ToListAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_FuelCostのデータ更新
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateFuelCostData")]
        public async Task<IActionResult> InsertUpdateFuelCostData(int CompanyID)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_FuelCost> m_FuelCost = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_FuelCost>>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateFuelCostData(CompanyID, m_FuelCost);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_FuelCost

        #region M_PostCode
        /// <summary>
        /// 指定県リストの住所データを返却する
        /// </summary>
        /// <param name="AreaList">エリアリスト</param>
        /// <returns>住所データリスト</returns>
        [HttpGet("M_AddressToShiKuChoList")]
        public async Task<IActionResult> M_AddressToShiKuChoList(string AreaList)
        {
            IEnumerable<Data.M_PostCode> data = null;
            try
            {
                if (AreaList == null) { throw new Exception("パラメーターエラー：AreaList"); }

                string[] target = AreaList.Split(",");

                data = await _context.M_PostCodes.Where(m => target.AsQueryable().Contains(m.KEN)).ToListAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }
        #endregion M_PostCode

        #region M_PostCode
        /*****************************************************************************
          M_PostCode
          *****************************************************************************/
        /// <summary>
        /// エリアに基づいて住所リストを取得
        /// </summary>
        /// <param name="iArea">エリア</param>
        /// <returns>住所リスト</returns>
        [HttpGet("GetAddressList")]
        public async Task<List<Data.M_PostCode>> GetAddressList(Dto.AddressArea iArea)
        {
            try
            {
                List<Data.M_PostCode> returnVal = new();

                Dto.AddressListDto dto = Dto.AddressListDto.GetInstance();

                switch (iArea)
                {
                    case Dto.AddressArea.Hokkaido:
                        returnVal = dto.HokkaidoAddressItem.ToList();
                        break;
                    case Dto.AddressArea.Tohoku:
                        returnVal = dto.TohokuAddressItem.ToList();
                        break;
                    case Dto.AddressArea.Hokuriku:
                        returnVal = dto.HokurikuAddressItem.ToList();
                        break;
                    case Dto.AddressArea.Chubu:
                        returnVal = dto.ChubuAddressItem.ToList();
                        break;
                    case Dto.AddressArea.Kanto:
                        returnVal = dto.KantoAddressItem.ToList();
                        break;
                    case Dto.AddressArea.Kinki:
                        returnVal = dto.KinkiAddressItem.ToList();
                        break;
                    case Dto.AddressArea.Chugoku:
                        returnVal = dto.ChugokuAddressItem.ToList();
                        break;
                    case Dto.AddressArea.Shikoku:
                        returnVal = dto.ShikokuAddressItem.ToList();
                        break;
                    case Dto.AddressArea.Kyusyu:
                        returnVal = dto.KyusyuAddressItem.ToList();
                        break;
                    case Dto.AddressArea.Okinawa:
                        returnVal = dto.OkinawaAddressItem.ToList();
                        break;
                    default:
                        returnVal = null;
                        break;
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }
        #endregion M_PostCode

        #region V_LoginUser
        /*****************************************************************************
          V_LoginUser
          *****************************************************************************/
        /// <summary>
        /// V_LoginUserリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>V_LoginUserリスト</returns>
        [HttpGet("V_LoginUserList")]
        public async Task<IActionResult> V_LoginUserList(int CompanyID)
        {
            List<Data.V_LoginUser> data = null;
            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                data = await _context.V_LoginUsers.Where(m => m.Company_ID == CompanyID).ToListAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// V_LoginUserのデータ取得
        /// </summary>
        /// <param name="CompanyCode">会社コード</param>
        /// <param name="LoginID">ログインID</param>
        /// <param name="loginUserID">ログインユーザーID</param>
        /// <returns>V_LoginUserデータ</returns>
        [HttpGet("V_LoginUser")]
        public async Task<IActionResult> V_LoginUser(string CompanyCode, string LoginID, int loginUserID = 0)
        {
            Data.V_LoginUser data = null;
            try
            {
                if ((CompanyCode == null && LoginID == null) && (loginUserID == 0))
                {
                    throw new Exception("パラメーターエラー：CompanyCode,LoginID,loginUserID");
                }

                if (CompanyCode != null && LoginID != null)
                {
                    data = await _context.V_LoginUsers.FirstOrDefaultAsync(m => m.Company_Code == CompanyCode && m.LoginID == LoginID);
                }
                else
                {
                    data = await _context.V_LoginUsers.FirstOrDefaultAsync(m => m.LoginUser_ID == loginUserID);
                }

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }
        #endregion V_LoginUser

        #region M_PublishGroup
        /*****************************************************************************
          M_PublishGroup
          *****************************************************************************/
        /// <summary>
        /// M_PublishGroupリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="BranchID">支店ID</param>
        /// <param name="UserID">ユーザーID</param>
        /// <returns>M_PublishGroupリスト</returns>
        [HttpGet("M_PublishGroupList")]
        public async Task<IActionResult> M_PublishGroupList(int CompanyID, int BranchID, int UserID)
        {
            List<Data.M_PublishGroup> data = null;
            try
            {
                if (CompanyID == 0 && BranchID == 0 && UserID == 0)
                {
                    throw new Exception("パラメーターエラー：CompanyID,BranchID,UserID");
                }

                data = await _context.M_PublishGroups.Where(m => (m.Company_ID == CompanyID && m.Branch_ID == 0) ||
                                                                (m.Branch_ID == BranchID) ||
                                                                (m.Company_ID == 0 && m.Branch_ID == 0 && m.User_ID == UserID)).ToListAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }
        #endregion M_PublishGroup

        #region M_Customer
        /*****************************************************************************
          M_Customer
          *****************************************************************************/
        /// <summary>
        /// M_Customerリストの返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="Cd">コード</param>
        /// <param name="Key">キー</param>
        /// <param name="Phone">電話番号</param>
        /// <returns>M_Customerリスト</returns>
        [HttpGet("CustomerList")]
        public async Task<IActionResult> CustomerList(int CompanyID, string Cd = null, string Key = null, string Phone = null)
        {
            try
            {
                if (CompanyID is 0 or < 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                IQueryable<Data.M_Customer> dataList = _context.M_Customers;

                if (Cd != null && Cd.Length > 0)
                {
                    dataList = dataList.Where(m => m.Customer_Code.Substring(0, Cd.Length) == Cd);
                }

                if (Key != null && Key.Length > 0)
                {
                    dataList = dataList.Where(m => (m.Customer_Name.Substring(0, Key.Length) == Key ||
                                            m.Customer_Name_Abbr.Substring(0, Key.Length) == Key ||
                                            m.Customer_Name_Kana.Substring(0, Key.Length) == Key));
                }

                if (Phone != null && Phone.Length > 0)
                {
                    dataList = dataList.Where(m => (m.Phone1.Contains(Phone) || m.Phone2.Contains(Phone) ||
                                                    m.Fax1.Contains(Phone) || m.Fax2.Contains(Phone)));
                }

                List<Data.M_Customer> result = await dataList.OrderBy(m => m.Customer_Code).Take(100).ToListAsync();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_Customerの取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="CustomerCode">顧客コード</param>
        /// <returns>M_Customerデータ</returns>
        [HttpGet("CustomerData")]
        public async Task<IActionResult> CustomerData(int CustomerID, string CustomerCode)
        {
            IQueryable<Data.M_Customer> query = null;
            try
            {
                if (!(CustomerID > 0 || CustomerCode != null)) { throw new Exception("パラメーターエラー：CompanyID"); }

                query = _context.M_Customers;

                if (CustomerID > 0) query = query.Where(m => m.Customer_ID == CustomerID);

                if (CustomerCode != null) query = query.Where(m => m.Customer_Code == CustomerCode);

                Data.M_Customer res = await query.FirstOrDefaultAsync();
                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }

        }

        /// <summary>
        /// M_Customerの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerData")]
        public async Task<IActionResult> InsertUpdateCustomerData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Dto.CustomerModalDto dto = System.Text.Json.JsonSerializer.Deserialize<Dto.CustomerModalDto>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerDto(dto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        #endregion M_Customer

        #region M_Customer_Branch
        /*****************************************************************************
          M_Customer_Branch
          *****************************************************************************/
        /// <summary>
        /// M_Customer_Branchリストの返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="CustomerID">顧客ID</param>
        /// <param name="YosyaFlg">他社フラグ</param>
        /// <param name="Cd">コード</param>
        /// <param name="Key">キー</param>
        /// <param name="Phone">電話番号</param>
        /// <param name="SelectRowCount">選択行数</param>
        /// <returns>M_Customer_Branchリスト</returns>
        [HttpGet("CustomerBranchList")]
        public async Task<IActionResult> CustomerBranchList(int CompanyID, int CustomerID = 0, int YosyaFlg = 0, string Cd = null, string Key = null, string Phone = null, int SelectRowCount = 100)
        {
            try
            {
                if (CompanyID is 0 or < 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                IQueryable<Data.M_Customer_Branch> dataList = _context.M_Customer_Branches;

                if (CustomerID > 0)
                {
                    dataList = dataList.Where(m => m.Customer_ID == CustomerID);
                }

                if (YosyaFlg > 0)
                {
                    IQueryable<int> subquery = _context.M_Customers.Where(m => m.Yosya_Flg == 1).Select(m => m.Customer_ID);
                    dataList = dataList.Where(m => subquery.AsQueryable().Contains(m.Customer_ID));
                }

                if (Cd != null && Cd.Length > 0)
                {
                    dataList = dataList.Where(m => m.Customer_Branch_Code.Substring(0, Cd.Length) == Cd);
                }

                if (Key != null && Key.Length > 0)
                {
                    dataList = dataList.Where(m => (m.Customer_Branch_Name.Substring(0, Key.Length) == Key ||
                                            m.Customer_Branch_Name_Abbr.Substring(0, Key.Length) == Key ||
                                            m.Customer_Branch_Name_Kana.Substring(0, Key.Length) == Key));
                }

                if (Phone != null && Phone.Length > 0)
                {
                    dataList = dataList.Where(m => (m.Customer_Branch_Phone1.Contains(Phone) || m.Customer_Branch_Phone2.Contains(Phone) ||
                                                    m.Customer_Branch_Fax1.Contains(Phone) || m.Customer_Branch_Fax2.Contains(Phone)));
                }

                List<Data.M_Customer_Branch> result = await dataList.OrderBy(m => m.Customer_Branch_Code).Take(SelectRowCount).ToListAsync();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_Customer_Branchの取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>M_Customer_Branchデータ</returns>
        [HttpGet("CustomerBranchData")]
        public async Task<IActionResult> CustomerBranchData(int Customer_BranchID)
        {
            Data.M_Customer_Branch data = null;
            try
            {
                if (Customer_BranchID == 0 || Customer_BranchID < 0) { throw new Exception("パラメーターエラー：Customer_BranchID"); }

                data = await _context.M_Customer_Branches.FirstOrDefaultAsync(m => m.Customer_Branch_ID == Customer_BranchID);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_Customer_Branchの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerBranchData")]
        public async Task<IActionResult> InsertUpdateCustomerBranchData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                /// パラメーターの取得
                Dto.CustomerBranchModalDto dto = GetMultipartFormDataContentData<Dto.CustomerBranchModalDto>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerBranchDto(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_Customer_Branch使用率TOPXXリスト
        /// XXは引数指定
        /// </summary>
        /// <param name="Customer_BranchID">顧客支店ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="Top">上位件数</param>
        /// <returns>M_Customer_Branchリスト</returns>
        [HttpGet("GetCustomerBranchListForUtilizationRate")]
        public async Task<List<M_Customer_Branch>> GetCustomerBranchListForUtilizationRate(int CompanyID, int userId, int Top = 0)
        {
            string sql = "";
            sql += " SELECT";
            if (Top > 0) { sql += " TOP " + Top.ToString(); }
            sql += " C.* ";
            sql += " FROM";
            sql += " [M_Customer_Branch] C";
            sql += " INNER JOIN";
            sql += " (";
            sql += " SELECT ";
            sql += " D.[KokyakuId] AS [Customer_Branch_ID]";
            sql += " ,COUNT(*) AS AA";
            sql += " FROM ";
            sql += " [T_Anken_Detail] D";
            sql += " INNER JOIN [T_Anken] A ON D.[Anken_ID] = A.[Anken_ID]";
            sql += " WHERE";
            sql += " A.[Company_ID] = {1}";
            sql += " AND";
            sql += " (";
            sql += " D.[Insert_User] = {0}";
            sql += " OR";
            sql += " D.[Update_User] = {0}";
            sql += " )";
            sql += " GROUP BY";
            sql += " D.[KokyakuId]";
            sql += " )X ON";
            sql += " C.[Customer_Branch_ID] = X.[Customer_Branch_ID]";
            sql += " ORDER BY";
            sql += " X.AA DESC";

            try
            {
                List<M_Customer_Branch> result = await _context.M_Customer_Branches.FromSqlRaw(sql, userId, CompanyID).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(e.Message);
                throw;
            }


        }

        /// <summary>
        /// M_Customer_Branch使用履歴TOPXXリスト
        /// XXは引数指定
        /// </summary>
        /// <param name="Customer_BranchID">顧客支店ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="Top">上位件数</param>
        /// <returns>M_Customer_Branchリスト</returns>
        [HttpGet("GetCustomerBranchListForRireki")]
        public async Task<IActionResult> GetCustomerBranchListForRireki(int CompanyID, int userId, int Top)
        {
            string sql = "";
            sql += " SELECT";
            if (Top > 0) { sql += " TOP " + Top.ToString(); }
            sql += " C.* ";
            sql += " FROM";
            sql += " [dbo].[M_Customer_Branch] C";
            sql += " INNER JOIN";
            sql += " (";
            sql += " ";
            sql += " SELECT";
            sql += " [Customer_Branch_ID]";
            sql += " ,MAX([Insert_Datetime]) AS [Sort_Datetime]";
            sql += " FROM";
            sql += " (";
            sql += " SELECT ";
            sql += " D.[KokyakuId] AS [Customer_Branch_ID]";
            sql += " ,D.[Insert_Datetime]";
            sql += " FROM ";
            sql += " [T_Anken_Detail] D";
            sql += " INNER JOIN [T_Anken] A ON D.[Anken_ID] = A.[Anken_ID]";
            sql += " WHERE";
            sql += " A.[Company_ID] = {1}";
            sql += " AND";
            sql += " D.[Insert_User] = {0}";
            sql += " ";
            sql += " UNION ALL";
            sql += " ";
            sql += " SELECT ";
            sql += " D.[KokyakuId] AS [Customer_Branch_ID]";
            sql += " ,[Update_Datetime] AS [Sort_Datetime]";
            sql += " FROM ";
            sql += " [T_Anken_Detail] D";
            sql += " INNER JOIN [T_Anken] A ON D.[Anken_ID] = A.[Anken_ID]";
            sql += " WHERE";
            sql += " A.[Company_ID] = {1}";
            sql += " AND";
            sql += " D.[Insert_User] = {0}";
            sql += " )Z";
            sql += " GROUP BY";
            sql += " [Customer_Branch_ID]";
            sql += " ";
            sql += " )X ON";
            sql += " C.[Customer_Branch_ID] = X.[Customer_Branch_ID]";
            sql += " ORDER BY";
            sql += " X.[Sort_Datetime] DESC";

            try
            {
                List<M_Customer_Branch> result = await _context.M_Customer_Branches.FromSqlRaw(sql, userId, CompanyID).ToListAsync();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 他社の顧客支店リストを取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="Cd">コード</param>
        /// <param name="Key">キー</param>
        /// <param name="Phone">電話番号</param>
        /// <returns>他社の顧客支店リスト</returns>
        [HttpGet("GetCustomerBranchListForYosya")]
        public async Task<List<Data.M_Customer_Branch>> GetCustomerBranchListForYosya(int CompanyID, string Cd = null, string Key = null, string Phone = null)
        {
            try
            {
                IQueryable<Data.M_Customer_Branch> query = null;

                query = _context.M_Customer_Branches.Join(
                    _context.M_Customers, cb => cb.Customer_ID, c => c.Customer_ID,
                    (cb, c) => new { CustomerBranch = cb, Customer = c })
                    .Where(m => m.Customer.Customer_ID == m.CustomerBranch.Customer_ID && m.Customer.Yosya_Flg == 1)
                    .Select(m => m.CustomerBranch);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }

        }


        #endregion M_Customer_Branch

        #region M_Customer_Tantou
        /*****************************************************************************
          M_Customer_Tantou
          *****************************************************************************/
        /// <summary>
        /// M_Customer_Tantouリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="CustomerBranchID">顧客支店ID</param>
        /// <returns>M_Customer_Tantouリスト</returns>
        [HttpGet("CustomerTantouList")]
        public async Task<List<Data.M_Customer_Tantou>> CustomerTantouList(int CompanyID, int CustomerBranchID)
        {
            try
            {
                List<Data.M_Customer_Tantou> list = null;

                if (CustomerBranchID > 0)
                {
                    list = await _context.M_Customer_Tantous.Where(m => m.Company_ID == CompanyID && m.Customer_Branch_ID == CustomerBranchID).ToListAsync();
                }

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Customer_Tantouの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerTantouData")]
        public async Task<IActionResult> InsertUpdateCustomerTantouData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_Customer_Tantou m_Customer_Tantou = System.Text.Json.JsonSerializer.Deserialize<Data.M_Customer_Tantou>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerTantouData(m_Customer_Tantou);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Customer_Tantou

        #region M_Customer_TantouHaisyaGroup
        /*****************************************************************************
          M_Customer_TantouHaisyaGroup
          *****************************************************************************/
        /// <summary>
        /// M_Customer_TantouHaisyaGroupリストのデータ返却
        /// </summary>
        /// <param name="CustomerID">顧客ID</param>
        /// <returns>M_Customer_TantouHaisyaGroupリスト</returns>
        [HttpGet("GetCustomerTantouHaisyaGroupList")]
        public async Task<List<Data.M_Customer_TantouHaisyaGroup>> GetCustomerTantouHaisyaGroupList(int CustomerID)
        {
            try
            {
                List<Data.M_Customer_TantouHaisyaGroup> list = null;

                if (CustomerID > 0)
                {
                    list = await _context.M_Customer_TantouHaisyaGroups.Where(m => m.Customer_ID == CustomerID).ToListAsync();
                }

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Customer_TantouHaisyaGroupの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerTantouHaisyaGroupData")]
        public async Task<IActionResult> InsertUpdateCustomerTantouHaisyaGroupData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                List<Data.M_Customer_TantouHaisyaGroup> m_Customer_TantouHaisyaGroup = GetMultipartFormDataContentData<List<Data.M_Customer_TantouHaisyaGroup>>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerTantouHaisyaGroupData(m_Customer_TantouHaisyaGroup);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Customer_TantouHaisyaGroup

        #region M_Customer_Driver
        /*****************************************************************************
          M_Customer_Driver
          *****************************************************************************/
        /// <summary>
        /// M_Customer_Driverリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="CustomerBranchID">顧客支店ID</param>
        /// <returns>M_Customer_Driverリスト</returns>
        [HttpGet("CustomerDriverList")]
        public async Task<List<Data.M_Customer_Driver>> CustomerDriverList(int CompanyID, int CustomerBranchID)
        {
            try
            {
                IQueryable<Data.M_Customer_Driver> list = null;

                IQueryable<int> subquery = _context.M_Customers.Where(m => m.Yosya_Flg == 1 && m.Company_ID == CompanyID).Select(m => m.Customer_ID);
                list = _context.M_Customer_Drivers.Where(m => subquery.Contains(m.Customer_ID));

                if (CustomerBranchID > 0)
                {
                    list = _context.M_Customer_Drivers.Where(m => m.Customer_Branch_ID == CustomerBranchID);
                }

                List<Data.M_Customer_Driver> result = await list.OrderBy(m => m.Employee_Number).ThenBy(m => m.Insert_Datetime).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Customer_Driverの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerDriverData")]
        public async Task<IActionResult> InsertUpdateCustomerDriverData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_Customer_Driver m_Customer_Driver = System.Text.Json.JsonSerializer.Deserialize<Data.M_Customer_Driver>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerDriverData(m_Customer_Driver);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Customer_Driver

        #region M_Customer_Driver_Syaryo
        /*****************************************************************************
          M_Customer_Driver_Syaryo
          *****************************************************************************/
        /// <summary>
        /// M_Customer_Driver_Syaryoリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="CustomerBranchID">顧客支店ID</param>
        /// <returns>M_Customer_Driver_Syaryoリスト</returns>
        [HttpGet("CustomerDriverSyaryoList")]
        public async Task<List<Data.M_Customer_Driver_Syaryo>> CustomerDriverSyaryoList(int CompanyID, int CustomerBranchID)
        {
            try
            {
                IQueryable<Data.M_Customer_Driver_Syaryo> list = null;

                IQueryable<int> subquery = _context.M_Customers.Where(m => m.Yosya_Flg == 1 && m.Company_ID == CompanyID).Select(m => m.Customer_ID);
                list = _context.M_Customer_Driver_Syaryos.Where(m => subquery.Contains(m.Customer_ID));

                if (CustomerBranchID > 0)
                {
                    list = _context.M_Customer_Driver_Syaryos.Where(m => m.Customer_Branch_ID == CustomerBranchID);
                }

                List<Data.M_Customer_Driver_Syaryo> result = await list.OrderBy(m => m.Syasyu).ThenBy(m => m.Kata).ThenBy(m => m.Syaban_Number).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Customer_Driver_Syaryoの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerDriverSyaryoData")]
        public async Task<IActionResult> InsertUpdateCustomerDriverSyaryoData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_Customer_Driver_Syaryo m_Customer_Driver_Syaryo = System.Text.Json.JsonSerializer.Deserialize<Data.M_Customer_Driver_Syaryo>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerDriverSyaryoData(m_Customer_Driver_Syaryo);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Customer_Driver_Syaryo

        #region V_Customer_Syaryo
        /*****************************************************************************
          V_Customer_Syaryo
          *****************************************************************************/
        /// <summary>
        /// V_Customer_Syaryoリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="CustomerBranchID">顧客支店ID</param>
        /// <returns>V_Customer_Syaryoリスト</returns>
        [HttpGet("CustomerSyaryoList")]
        public async Task<List<Data.V_Customer_Syaryo>> CustomerSyaryoList(int CompanyID, int CustomerBranchID)
        {
            try
            {
                IQueryable<Data.V_Customer_Syaryo> list = _context.V_Customer_Syaryos.Where(m => m.Company_ID == CompanyID);

                if (CustomerBranchID > 0)
                {
                    list = list.Where(m => m.Customer_Branch_ID == CustomerBranchID);
                }

                List<Data.V_Customer_Syaryo> result = await list.OrderBy(m => m.Syasyu).ThenBy(m => m.Kata).ThenBy(m => m.Syaban_Number).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        #endregion V_Customer_Syaryo

        #region M_Customer_TollSeikyuKubun
        /*****************************************************************************
          M_Customer_TollSeikyuKubun
          *****************************************************************************/
        /// <summary>
        /// M_Customer_TollSeikyuKubunリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</        /// <param name="CustomerBranchID">顧客支店ID</param>
        /// <returns>M_Customer_TollSeikyuKubunリスト</returns>
        [HttpGet("CustomerTollSeikyuKubunList")]
        public async Task<List<Data.M_Customer_TollSeikyuKubun>> CustomerTollSeikyuKubunList(int CompanyID, int CustomerBranchID)
        {
            try
            {
                List<Data.M_Customer_TollSeikyuKubun> list = null;

                if (CustomerBranchID > 0)
                {
                    list = await _context.M_Customer_TollSeikyuKubuns.Where(m => m.Customer_Branch_ID == CustomerBranchID).ToListAsync();
                }

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Customer_TollSeikyuKubunの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerTollSeikyuKubunData")]
        public async Task<IActionResult> InsertUpdateCustomerTollSeikyuKubunData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                List<Data.M_Customer_TollSeikyuKubun> m_Customer_TollSeikyuKubun = GetMultipartFormDataContentData<List<Data.M_Customer_TollSeikyuKubun>>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerTollSeikyuKubunData(m_Customer_TollSeikyuKubun);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Customer_TollSeikyuKubun

        #region M_Customer_ICSeikyuKubun
        /*****************************************************************************
          M_Customer_ICSeikyuKubun
          *****************************************************************************/
        /// <summary>
        /// M_Customer_ICSeikyuKubunリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="CustomerBranchID">顧客支店ID</param>
        /// <returns>M_Customer_ICSeikyuKubunリスト</returns>
        [HttpGet("CustomerICSeikyuKubunList")]
        public async Task<List<Data.M_Customer_ICSeikyuKubun>> CustomerICSeikyuKubunList(int CompanyID, int CustomerBranchID)
        {
            try
            {
                List<Data.M_Customer_ICSeikyuKubun> list = null;

                if (CustomerBranchID > 0)
                {
                    list = await _context.M_Customer_ICSeikyuKubuns.Where(m => m.Customer_Branch_ID == CustomerBranchID).ToListAsync();
                }

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Customer_ICSeikyuKubunの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerICSeikyuKubunData")]
        public async Task<IActionResult> InsertUpdateCustomerICSeikyuKubunData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                List<Data.M_Customer_ICSeikyuKubun> m_Customer_ICSeikyuKubun = GetMultipartFormDataContentData<List<Data.M_Customer_ICSeikyuKubun>>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerICSeikyuKubunData(m_Customer_ICSeikyuKubun);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Customer_ICSeikyuKubun

        #region M_Customer_Uriage_Calc
        /*****************************************************************************
          M_Customer_Uriage_Calc
          *****************************************************************************/
        /// <summary>
        /// M_Customer_Uriage_Calcのデータ取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="CustomerBranchID">顧客支店ID</param>
        /// <returns>M_Customer_Uriage_Calcデータ</returns>
        [HttpGet("CustomerUriageCalcData")]
        public async Task<Data.M_Customer_Uriage_Calc> CustomerUriageCalcData(int CompanyID, int CustomerBranchID)
        {
            try
            {
                Data.M_Customer_Uriage_Calc list = null;
                if (CustomerBranchID > 0)
                {
                    list = await _context.M_Customer_Uriage_Calcs.FirstOrDefaultAsync(m => m.Customer_Branch_ID == CustomerBranchID);
                }
                return list ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Customer_Uriage_Calcの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerUriageCalcData")]
        public async Task<IActionResult> InsertUpdateCustomerUriageCalcData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                Data.M_Customer_Uriage_Calc m_Customer_Uriage_Calc = GetMultipartFormDataContentData<Data.M_Customer_Uriage_Calc>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerUriageCalcData(m_Customer_Uriage_Calc);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Customer_Uriage_Calc

        #region M_Customer_Shiharai_Calc
        /*****************************************************************************
          M_Customer_Shiharai_Calc
          *****************************************************************************/
        /// <summary>
        /// M_Customer_Shiharai_Calcのデータ取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="CustomerBranchID">顧客支店ID</param>
        /// <returns>M_Customer_Shiharai_Calcデータ</returns>
        [HttpGet("CustomerShiharaiCalcData")]
        public async Task<Data.M_Customer_Shiharai_Calc> CustomerShiharaiCalcData(int CompanyID, int CustomerBranchID)
        {
            try
            {
                Data.M_Customer_Shiharai_Calc list = null;
                if (CustomerBranchID > 0)
                {
                    list = await _context.M_Customer_Shiharai_Calcs.FirstOrDefaultAsync(m => m.Customer_Branch_ID == CustomerBranchID);
                }
                return list ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Customer_Shiharai_Calcの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerShiharaiCalcData")]
        public async Task<IActionResult> InsertUpdateCustomerShiharaiCalcData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                Data.M_Customer_Shiharai_Calc m_Customer_Shiharai_Calc = GetMultipartFormDataContentData<Data.M_Customer_Shiharai_Calc>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerShiharaiCalcData(m_Customer_Shiharai_Calc);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Customer_Shiharai_Calc

        #region M_Customer TracmateLink
        /// <summary>
        /// トラックメイトからの連携データを更新する
        /// </summary>
        /// <param name="customerTruckmeteModalDto">トラックメイト連携データ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateCustomerTracmateData")]
        public async Task<IActionResult> InsertUpdateCustomerTracmateData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                Dto.CustomerTruckmeteModalDto dto = GetMultipartFormDataContentData<Dto.CustomerTruckmeteModalDto>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCustomerTracmateData(dto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Customer TracmateLink

        #region M_Unit
        /// <summary>
        /// M_Unitリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>M_Unitリスト</returns>
        [HttpGet("M_Unit")]
        public async Task<List<Data.M_Unit>> M_Unit(int CompanyID)
        {

            // if (CompanyID == 0 || CompanyID < 0) { return null; }
            try
            {
                List<M_Unit> data = await _context.M_Units
                                .Where(m => m.Company_ID == CompanyID)
                                .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        #endregion M_Unit

        #region M_Code
        /*****************************************************************************
          M_Code
          *****************************************************************************/
        /// <summary>
        /// M_Codeのデータ取得
        /// </summary>
        /// <param name="CodeID">コードID</param>
        /// <returns>M_Codeデータ</returns>
        [HttpGet("M_CodeData")]
        public async Task<Data.M_Code> M_CodeData(int CodeID)
        {
            try
            {
                Data.M_Code list = null;

                if (CodeID > 0)
                {
                    list = await _context.M_Codes.FirstOrDefaultAsync(m => m.Code_ID == CodeID);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }
        /// <summary>
        /// M_Codeリストのデータ返却
        /// </summary>
        /// <returns>M_Codeリスト</returns>
        [HttpGet("M_CodeList")]
        public async Task<List<Data.M_Code>> M_CodeList()
        {
            try
            {
                List<Data.M_Code> list = null;
                list = await _context.M_Codes.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }
        #endregion M_Code

        #region M_Code_Data
        /*****************************************************************************
          M_Code_Data
          *****************************************************************************/
        /// <summary>
        /// M_Code_Dataリストのデータ返却
        /// </summary>
        /// <param name="CodeID">コードID</param>
        /// <returns>M_Code_Dataリスト</returns>
        [HttpGet("M_Code_DataList")]
        public async Task<List<Data.M_Code_Datum>> M_Code_DataList(int CodeID)
        {
            try
            {
                IQueryable<Data.M_Code_Datum> list = _context.M_Code_Data;

                if (CodeID > 0)
                {
                    list = list.Where(m => m.Code_ID == CodeID);
                }

                return await list.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }
        #endregion M_Code_Data

        #region M_Anken_Excharge
        /*****************************************************************************
          M_Anken_Excharge
          *****************************************************************************/
        /// <summary>
        /// M_Anken_Exchargeリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="SyasyuSize">車種サイズ</param>
        /// <returns>M_Anken_Exchargeリスト</returns>
        [HttpGet("M_Anken_ExchargeList")]
        public async Task<List<Data.M_Anken_Excharge>> M_Anken_ExchargeList(int CompanyID, string SyasyuSize)
        {
            try
            {
                List<Data.M_Anken_Excharge> list = null;

                if (SyasyuSize != null && SyasyuSize.Length > 0)
                {
                    list = await _context.M_Anken_Excharges.Where(m => m.Company_ID == CompanyID && m.SIZE == SyasyuSize).OrderBy(m => m.SortOrder).ToListAsync();
                }
                else
                {
                    list = await _context.M_Anken_Excharges.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.SortOrder).ToListAsync();
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Anken_Exchargeの並び順変更処理
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("UpdateAnkenExchargeSortOrder")]
        public async Task<IActionResult> UpdateAnkenExchargeSortOrder()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Dto.AnkenExchargeListDto dataDto = System.Text.Json.JsonSerializer.Deserialize<Dto.AnkenExchargeListDto>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.UpdateAnkenExchargeSortOrder(dataDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_Anken_Exchargeの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateAnkenExchargeMasterData")]
        public async Task<IActionResult> InsertUpdateAnkenExchargeMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_Anken_Excharge m_AnkenExcharge = System.Text.Json.JsonSerializer.Deserialize<Data.M_Anken_Excharge>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateAnkenExchargeMasterData(m_AnkenExcharge);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Anken_Excharge

        #region M_LoginUser_Role
        /*****************************************************************************
          M_LoginUser_Role
          *****************************************************************************/
        /// <summary>
        /// M_LoginUser_Roleリストのデータ返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>M_LoginUser_Roleリスト</returns>
        [HttpGet("M_LoginUserRoleList")]
        public async Task<List<Data.M_LoginUser_Role>> M_LoginUserRoleList(int CompanyID)
        {
            try
            {
                List<Data.M_LoginUser_Role> list = await _context.M_LoginUser_Roles.Where(m => m.Company_ID == CompanyID).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_LoginUser_Roleのデータ取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="Role">役割</param>
        /// <returns>M_LoginUser_Roleデータ</returns>
        [HttpGet("M_LoginUserRoleData")]
        public async Task<Data.M_LoginUser_Role> M_LoginUserRoleData(int CompanyID, int Role)
        {
            try
            {
                Data.M_LoginUser_Role M_LoginUser_Role = await _context.M_LoginUser_Roles.FirstOrDefaultAsync(m => m.Company_ID == CompanyID && m.Role == Role);
                return M_LoginUser_Role;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_LoginUser_Roleの追加更新
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateLoginUserRoleMasterData")]
        public async Task<IActionResult> InsertUpdateLoginUserRoleMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_LoginUser_Role m_LoginUser_Role = System.Text.Json.JsonSerializer.Deserialize<Data.M_LoginUser_Role>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateLoginUserRoleMasterData(m_LoginUser_Role);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_LoginUser_Role

        #region M_LoginUser
        /*****************************************************************************
          M_LoginUser
          *****************************************************************************/
        /// <summary>
        /// M_LoginUserのデータ取得
        /// </summary>
        /// <param name="LoginUserID">ログインユーザーID</param>
        /// <returns>M_LoginUserデータ</returns>
        [HttpGet("M_LoginUserData")]
        public async Task<Data.M_LoginUser> M_LoginUserData(int LoginUserID)
        {
            try
            {
                Data.M_LoginUser M_LoginUser = await _context.M_LoginUsers.FirstOrDefaultAsync(m => m.LoginUser_ID == LoginUserID);
                return M_LoginUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_LoginUserマスタの登録更新処理
        /// </summary>
        /// <param name="jsonString">JSON形式のデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertUpdateLoginUserMasterData")]
        public async Task<IActionResult> InsertUpdateLoginUserMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_LoginUser M_LoginUser = System.Text.Json.JsonSerializer.Deserialize<Data.M_LoginUser>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateLoginUserMasterData(M_LoginUser);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_LoginUserマスタのパスワード更新
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("UpdateLoginUserPass")]
        public async Task<IActionResult> UpdateLoginUserPass()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_LoginUser M_LoginUser = System.Text.Json.JsonSerializer.Deserialize<Data.M_LoginUser>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.UpdateLoginUserPass(M_LoginUser);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_LoginUser

        #region M_Customer
        /*****************************************************************************
        M_Customer
        *****************************************************************************/
        [HttpGet("M_CustomerListForTruckmeteNotConnect")]
        public Data.M_Customer M_CustomerListForTruckmeteNotConnect(int CompanyID)
        {
            //try
            //{
            //    var aa = await _context.V_Tokuisakis.Select(m => m.コード.ToString()).ToListAsync();


            ////Data.M_Customer M_LoginUser = await _context.M_Customers.Select(s => s)
            ////    .Where(t1 => _context.V_Tokuisakis.Select(m => m.コード).Contains(t1.Customer_Code)).ToListAsync();
            ////return M_LoginUser;
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exception: " + ex.Message);
            //    Response.StatusCode = StatusCodes.Status400BadRequest;
            //    throw;
            //}
            //finally
            //{

            //}
            return null;
        }

        #endregion M_Customer

        #region M_SyaryoManagement
        /*****************************************************************************
          M_SyaryoManagement
          *****************************************************************************/
        [HttpGet("M_SyaryoManagementList")]
        public async Task<IActionResult> M_SyaryoManagementList(int CompanyID, string Syaban = null, int UserID = 0, string Syasyu = null, string Kata = null)
        {
            try
            {
                if (CompanyID is 0 or < 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                IQueryable<Data.M_SyaryoManagement> dataList = _context.M_SyaryoManagements;

                dataList = dataList.Where(m => m.Company_ID == CompanyID);

                if (Syaban != null)
                {
                    dataList = dataList.Where(m => m.Syaban_Number == Syaban);
                }

                if (Syasyu != null)
                {
                    dataList = dataList.Where(m => m.Syasyu == Syasyu);
                }

                if (Kata != null)
                {
                    dataList = dataList.Where(m => m.Kata == Kata);
                }

                if (UserID > 0)
                {
                    IQueryable<int> subquery = _context.M_CompanyUser_GroupUsers.Where(m => m.User_ID == UserID).Select(m => m.Group_ID);
                    dataList = dataList.Where(m => subquery.Contains(m.Group_ID));
                }

                List<Data.M_SyaryoManagement> result = await dataList.OrderBy(m => m.Syasyu).ThenBy(m => m.Kata).ToListAsync();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
        }

        [HttpGet("M_SyaryoManagementsData")]
        public async Task<Data.M_SyaryoManagement> M_SyaryoManagementsData(int SyaryoManagementID)
        {
            try
            {
                Data.M_SyaryoManagement m_SyaryoManagement = await _context.M_SyaryoManagements.FirstOrDefaultAsync(m => m.SyaryoManagement_ID == SyaryoManagementID);
                return m_SyaryoManagement;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }


        /// <summary>
        /// 車両管理マスタの追加更新
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("InsertUpdateSyaryoManagementMasterData")]
        public async Task<IActionResult> InsertUpdateSyaryoManagementMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_SyaryoManagement m_SyaryoManagement = System.Text.Json.JsonSerializer.Deserialize<Data.M_SyaryoManagement>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateSyaryoManagementMasterData(m_SyaryoManagement);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);

        }
        #endregion M_SyaryoManagement

        #region M_CompanyDriver_Syaryo
        /*****************************************************************************
          M_CompanyDriver_Syaryo
          *****************************************************************************/
        /// <summary>
        /// M_CompanyDriver_Syaryoリストのデータ返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="NotDel"></param>
        /// <returns></returns>
        [HttpGet("M_CompanyDriverSyaryosList")]
        public async Task<List<Data.M_CompanyDriver_Syaryo>> M_CompanyDriverSyaryosList(int CompanyID, int DriverID = 0, int NotDel = 0)
        {
            try
            {
                List<Data.M_CompanyDriver_Syaryo> list = null;

                list = await _context.M_CompanyDriver_Syaryos.Where(m => m.Company_ID == CompanyID).ToListAsync();

                if (DriverID > 0)
                {
                    list = list.Where(m => m.Driver_ID == DriverID).ToList();
                }

                if (NotDel == 1)
                {
                    list = list.Where(m => m.Del_Flg == false).ToList();
                }

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        [HttpGet("M_CompanyDriverSyaryosData")]
        public async Task<Data.M_CompanyDriver_Syaryo> M_CompanyDriverSyaryosData(int DriverSyaryoID)
        {
            try
            {
                Data.M_CompanyDriver_Syaryo m_CompanyDriver_Syaryo = await _context.M_CompanyDriver_Syaryos.FirstOrDefaultAsync(m => m.DriverSyaryo_ID == DriverSyaryoID);
                return m_CompanyDriver_Syaryo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("InsertUpdateCompanyDriverSyaryoMasterData")]
        public async Task<IActionResult> InsertUpdateCompanyDriverSyaryoMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_CompanyDriver_Syaryo> m_CompanyDriver_Syaryos = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_CompanyDriver_Syaryo>>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateCompanyDriverSyaryoMasterData(m_CompanyDriver_Syaryos);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_CompanyDriver_Syaryo

        #region M_Yosya
        /*****************************************************************************
          M_Yosya
          *****************************************************************************/
        /// <summary>
        /// M_Yosyaリストの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [HttpGet("M_YosyaList")]
        public async Task<IEnumerable<Data.M_Yosya>> M_YosyaList(int CompanyID)
        {
            try
            {
                IEnumerable<Data.M_Yosya> list = null;

                if (CompanyID > 0)
                {
                    list = await _context.M_Yosyas.Where(m => m.Company_ID == CompanyID).ToListAsync();
                }

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Yosyaデータの取得
        /// </summary>
        /// <param name="YosyaID"></param>
        /// <returns></returns>
        [HttpGet("M_YosyaData")]
        public async Task<Data.M_Customer> M_YosyaData(int YosyaID)
        {
            try
            {
                Data.M_Customer list = null;

                if (YosyaID > 0)
                {
                    list = await _context.M_Customers.FirstOrDefaultAsync(m => m.Customer_ID == YosyaID);
                }

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }



        /// <summary>
        /// M_Yosyaの追加更新
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("InsertUpdateYosyaData")]
        public async Task<IActionResult> InsertUpdateYosyaData(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Dto.YosyaModalDto dto = System.Text.Json.JsonSerializer.Deserialize<Dto.YosyaModalDto>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateYosyaData(dto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Yosya

        #region M_SyasyuKubun
        /*****************************************************************************
          M_SyasyuKubun
          *****************************************************************************/
        [HttpGet("SyasyuKubunList")]
        public async Task<IActionResult> SyasyuKubunList(int CompanyID, string Size, string KataID, int SyasyuKubunID = 0)
        {
            IQueryable<Data.M_SyasyuKubun> data = null;

            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                data = _context.M_SyasyuKubuns.Where(m => m.Company_ID == CompanyID);

                if (SyasyuKubunID > 0)
                {
                    data = data.Where(m => m.SyasyuKubun_ID == SyasyuKubunID);
                }

                if (Size != null)
                {
                    data = data.Where(m => m.SIZE == Size);
                }

                if (KataID != null)
                {
                    data = data.Where(m => m.Kata_ID == KataID);
                }

                IEnumerable<Data.M_SyasyuKubun> res = await data.OrderBy(m => m.SortOrder).ToListAsync();
                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("UpdateSyasyuKubunListSortOrder")]
        public async Task<IActionResult> UpdateSyasyuKubunListSortOrder()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Dto.SyasyuKubunListDto dataDto = System.Text.Json.JsonSerializer.Deserialize<Dto.SyasyuKubunListDto>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.UpdateSyasyuKubunListSortOrder(dataDto);
                return new OkObjectResult(resultVal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("InsertUpdateSyasyuKubunMasterData")]
        public async Task<IActionResult> InsertUpdateSyasyuKubunMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_SyasyuKubun M_SyasyuKubun = System.Text.Json.JsonSerializer.Deserialize<Data.M_SyasyuKubun>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateSyasyuKubunMasterData(M_SyasyuKubun);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// M_SyasyuKubunマスタの削除
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("DeleteSyasyuKubunMasterData")]
        public async Task<IActionResult> DeleteSyasyuKubunMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_SyasyuKubun M_SyasyuKubun = System.Text.Json.JsonSerializer.Deserialize<Data.M_SyasyuKubun>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.DeleteSyasyuKubunMasterData(M_SyasyuKubun);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_SyasyuKubun

        #region M_Senzoku
        /*****************************************************************************
          M_Senzoku
          *****************************************************************************/
        /// <summary>
        /// M_Senzokuリストの取得
        /// </summary>
        /// <param name="YosyaID"></param>
        /// <returns></returns>
        [HttpGet("GetSenzokuList")]
        public async Task<IEnumerable<Data.M_Senzoku>> GetSenzokuList(int CompanyID)
        {
            try
            {
                IEnumerable<Data.M_Senzoku> list = null;
                list = await _context.M_Senzokus.Where(m => m.Company_ID == CompanyID).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Senzokuリストの取得
        /// </summary>
        /// <param name="YosyaID"></param>
        /// <returns></returns>
        [HttpGet("GetSenzokuViewList")]
        public async Task<IEnumerable<Data.V_Senzoku>> GetSenzokuViewList(int CompanyID)
        {
            try
            {
                IEnumerable<Data.V_Senzoku> list = null;
                list = await _context.V_Senzokus.Where(m => m.Company_ID == CompanyID).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Senzokuの登録更新処理
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("InsertUpdateSenzokuData")]
        public async Task<IActionResult> InsertUpdateSenzokuData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                Data.M_Senzoku dto = GetMultipartFormDataContentData<Data.M_Senzoku>();

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateSenzokuData(dto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Senzoku

        #region M_Senzoku_Driver
        /*****************************************************************************
          M_Senzoku_Driver
          *****************************************************************************/
        /// <summary>
        /// M_Senzoku_Driverリストの取得
        /// </summary>
        /// <param name="YosyaID"></param>
        /// <returns></returns>
        [HttpGet("GetSenzokuDriverList")]
        public async Task<IEnumerable<Data.M_Senzoku_Driver>> GetSenzokuDriverList(int CompanyID, DateOnly? targetMonth)
        {
            try
            {
                IQueryable<Data.M_Senzoku_Driver> list = _context.M_Senzoku_Drivers.Where(m => m.Company_ID == CompanyID);

                if (targetMonth != null)
                {
                    list = list.Where(m => (m.To_Date == null ||
                                            (m.To_Date != null && (m.To_Date.Value.AddMonths(-1) >= targetMonth))));
                }

                //list = list.Where(m => m.To_Date == null ||
                //                            (m.To_Date != null && DateTime.Parse(((DateTime)m.To_Date).ToString("yyyy/MM/01")) >= targetMonth));


                return await list.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// V_Senzoku_Driverリストの取得
        /// </summary>
        /// <param name="YosyaID"></param>
        /// <returns></returns>
        [HttpGet("GetSenzokuDriverViewList")]
        public async Task<IEnumerable<Data.V_Senzoku_Driver>> GetSenzokuDriverViewList(int CompanyID, DateOnly? targetMonth, int SenzokuDriverID = 0)
        {
            try
            {
                IQueryable<Data.V_Senzoku_Driver> list = _context.V_Senzoku_Drivers;

                list = list.Where(m => m.Company_ID == CompanyID);

                if (targetMonth != null)
                {
                    list = list.Where(m => m.To_Date == null ||
                                            (m.To_Date != null && (m.To_Date.Value.AddMonths(-1) >= targetMonth)));
                    list = list.Where(m => m.To_Date == null ||
                                            (m.To_Date != null && ((m.To_Date) >= targetMonth)));
                    ////&& DateTime.Parse(((DateTime)m.To_Date).ToString("yyyy/MM/01")) >= (DateTime)targetMonth

                }

                if (SenzokuDriverID > 0)
                {
                    list = list.Where(m => m.Senzoku_Driver_ID == SenzokuDriverID);
                }

                return await list.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// M_Senzoku_Driverの登録更新処理
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("InsertUpdateSenzokuDriverData")]
        public async Task<IActionResult> InsertUpdateSenzokuDriverData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                Data.M_Senzoku_Driver dto = GetMultipartFormDataContentData<Data.M_Senzoku_Driver>();

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateSenzokuDriverData(dto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Senzoku_Driver

        #region M_Luggage
        /*****************************************************************************
          M_Luggage
          *****************************************************************************/
        /// <summary>
        /// LuggageListの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns>M_LuggageList</returns>
        [HttpGet("LuggageList")]
        public async Task<IEnumerable<Data.M_Luggage>> LuggageList(int CompanyID)
        {
            try
            {
                IEnumerable<Data.M_Luggage> data = null;
                if (CompanyID == 0) { return null; }
                data = await _context.M_Luggages.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.SortOrder).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        #region M_Burden
        /*****************************************************************************
          M_Burden
          *****************************************************************************/
        /// <summary>
        /// BurdenGroupの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns>data</returns>
        [HttpGet("BurdenList")]
        public async Task<IEnumerable<Data.M_Burden>> BurdenList(int CompanyID)
        {
            try
            {
                IEnumerable<Data.M_Burden> data = null;
                if (CompanyID == 0) { return null; }
                data = await _context.M_Burdens.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.SortOrder).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// BurdenGroupの保存
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("InsertUpdateBurdenMasterData")]
        public async Task<IActionResult> InsertUpdateBurdenMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_Burden m_Burden = System.Text.Json.JsonSerializer.Deserialize<Data.M_Burden>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateBurdenMasterData(m_Burden);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        #endregion M_Burden

        #region M_Burden_Group
        /*****************************************************************************
          M_Burden_Group
          *****************************************************************************/
        /// <summary>
        /// BurdenGroupListの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns>BurdenGroupList</returns>
        [HttpGet("BurdenGroupList")]
        public async Task<IEnumerable<Data.M_Burden_Group>> BurdenGroupList(int CompanyID)
        {
            try
            {
                IEnumerable<Data.M_Burden_Group> data = null;
                if (CompanyID == 0) { return null; }
                data = await _context.M_Burden_Groups.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.SortOrder).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }
        #endregion M_Burden_Group

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("UpdateLuggageSortOrder")]
        public async Task<IActionResult> UpdateLuggageSortOrder()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_Katum> dataDto = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_Katum>>(abc);

                MasterDataModel model = new(_context);
                //resultVal = await model.UpdateLuggageListSortOrder(dataDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// Luggageマスタの登録更新処理
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("InsertUpdateLuggageMasterData")]
        public async Task<IActionResult> InsertUpdateLuggageMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_Luggage m_Luggage = System.Text.Json.JsonSerializer.Deserialize<Data.M_Luggage>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateLuggageMasterData(m_Luggage);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Luggage

        #region M_Luggage_Group
        /*****************************************************************************
          M_Luggage_Group
          *****************************************************************************/
        /// <summary>
        /// LuggageGroupListの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns>LuggageGroupList</returns>
        [HttpGet("LuggageGroupList")]
        public async Task<IActionResult> LuggageGroupList(int CompanyID)
        {
            IEnumerable<Data.M_Luggage_Group> data = null;

            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }
                data = await _context.M_Luggage_Groups.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.SortOrder).ToListAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// UpdateLuggageGroupSortOrder
        /// </summary>
        /// <returns>MsterDataCommonResultValDto</returns>
        [HttpPost("UpdateLuggageGroupSortOrder")]
        public async Task<IActionResult> UpdateLuggageGroupSortOrder()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                List<Data.M_Luggage_Group> dataDto = GetMultipartFormDataContentData<List<Data.M_Luggage_Group>>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.UpdateLuggageGroupListSortOrder(dataDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }

            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// InsertUpdateLuggageGroupMasterData
        /// </summary>
        /// <returns>MsterDataCommonResultValDto</returns>
        [HttpPost("InsertUpdateLuggageGroupMasterData")]
        public async Task<IActionResult> InsertUpdateLuggageGroupMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                Data.M_Luggage_Group m_Luggage_Group = GetMultipartFormDataContentData<Data.M_Luggage_Group>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateLuggageGroupMasterData(m_Luggage_Group);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }

            return new OkObjectResult(resultVal);
        }
        #endregion M_Luggage_Group

        #region V_Luggage
        /*****************************************************************************
          V_Luggage
          *****************************************************************************/
        /// <summary>
        /// LuggageViewListの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns>LuggageViewList</returns>
        [HttpGet("LuggageViewList")]
        public async Task<IActionResult> LuggageViewList(int CompanyID)
        {
            IEnumerable<Data.V_Luggage> data = null;
            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }
                data = await _context.V_Luggages.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.GroupSortOrder).ThenBy(m => m.SortOrder).ToListAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }
        #endregion V_Luggage

        #region M_Equipment
        /*****************************************************************************
          M_Equipment
          *****************************************************************************/
        /// <summary>
        /// EquipmentListの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns>EquipmentList</returns>
        [HttpGet("EquipmentList")]
        public async Task<IActionResult> EquipmentList(int CompanyID)
        {
            try
            {
                IEnumerable<Data.M_Equipment> data = null;
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }
                data = await _context.M_Equipments.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.SortOrder).ToListAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// UpdateEquipmentSortOrder
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns>MsterDataCommonResultValDto</returns>
        [HttpPost("UpdateEquipmentSortOrder")]
        public async Task<IActionResult> UpdateEquipmentSortOrder()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<Data.M_Katum> dataDto = System.Text.Json.JsonSerializer.Deserialize<List<Data.M_Katum>>(abc);

                MasterDataModel model = new(_context);
                //resultVal = await model.UpdateEquipmentListSortOrder(dataDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// InsertUpdateEquipmentMasterData
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns>MsterDataCommonResultValDto</returns>
        [HttpPost("InsertUpdateEquipmentMasterData")]
        public async Task<IActionResult> InsertUpdateEquipmentMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Data.M_Equipment m_Equipment = System.Text.Json.JsonSerializer.Deserialize<Data.M_Equipment>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateEquipmentMasterData(m_Equipment);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }
        #endregion M_Equipment

        #region M_Equipment_Group
        /*****************************************************************************
          M_Equipment_Group
          *****************************************************************************/
        /// <summary>
        /// EquipmentGroupListの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns>EquipmentGroupList</returns>
        [HttpGet("EquipmentGroupList")]
        public async Task<IActionResult> EquipmentGroupList(int CompanyID)
        {
            IEnumerable<Data.M_Equipment_Group> data = null;
            try
            {
                if (CompanyID == 0) { throw new Exception("パラメーターエラー：CompanyID"); }
                data = await _context.M_Equipment_Groups.Where(m => m.Company_ID == CompanyID).OrderBy(m => m.SortOrder).ToListAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// UpdateEquipmentGroupSortOrder
        /// </summary>
        /// <returns>MsterDataCommonResultValDto</returns>
        [HttpPost("UpdateEquipmentGroupSortOrder")]
        public async Task<IActionResult> UpdateEquipmentGroupSortOrder()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                List<Data.M_Equipment_Group> dataDto = GetMultipartFormDataContentData<List<Data.M_Equipment_Group>>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.UpdateEquipmentGroupListSortOrder(dataDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }

            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// InsertUpdateEquipmentGroupMasterData
        /// </summary>
        /// <returns>MsterDataCommonResultValDto</returns>
        [HttpPost("InsertUpdateEquipmentGroupMasterData")]
        public async Task<IActionResult> InsertUpdateEquipmentGroupMasterData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                Data.M_Equipment_Group m_Equipment_Group = GetMultipartFormDataContentData<Data.M_Equipment_Group>("name");

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateEquipmentGroupMasterData(m_Equipment_Group);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }

            return new OkObjectResult(resultVal);
        }
        #endregion M_Equipment_Group

        #region M_Report_Serch
        /*****************************************************************************
          M_Report_Serch
          *****************************************************************************/
        /// <summary>
        /// ReportSearchList
        /// </summary>
        /// <param name="reportNum"></param>
        /// <param name="kubunID"></param>
        /// <returnsReportSearchDataDto</returns>
        [HttpGet("ReportSearchData")]
        public async Task<Dto.ReportSearchDataDto> ReportSearchData(int reportNum = 0, int kubunID = 0)
        {
            try
            {
                Dto.ReportSearchDataDto data = null;
                if (reportNum == 0 || kubunID == 0) { return null; }
                data = await _context.M_Report_Serches
                    .Join(_context.M_Report_Serch_Kubuns,
                        report => report.Report_Serch_ID,
                        reportKubun => reportKubun.Report_Serch_ID,
                        (report, reportKubun) => new { ReportSearch = report, ReportSearchKubun = reportKubun })
                    .Where(m => m.ReportSearch.Report_Number == reportNum && m.ReportSearchKubun.Report_Serch_Kubun_ID == kubunID)
                    .Select(data => new Dto.ReportSearchDataDto() { ReportName = data.ReportSearch.Report_Name, DisplayTitle = data.ReportSearchKubun.Display_Title })
                    .FirstOrDefaultAsync();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        #endregion M_Report_Serch

        #region M_Report_Output_Item_Master
        /*****************************************************************************
          M_Report_Output_Item_Master
          *****************************************************************************/
        /// <summary>
        /// ReportOutputItemMasterList
        /// </summary>
        /// <param name="kubunID"></param>
        /// <returns>M_Report_Output_Item_Master</returns>
        [HttpGet("ReportOutputItemMasterList")]
        public async Task<IEnumerable<Data.M_Report_Output_Item_Master>> ReportOutputItemMasterList(int kubunID = 0)
        {
            try
            {
                IEnumerable<Data.M_Report_Output_Item_Master> data = null;
                if (kubunID == 0) { return null; }
                data = await _context.M_Report_Output_Item_Masters
                    .Where(m => m.Report_Serch_Kubun_ID == kubunID)
                    .OrderBy(o => o.Sort_Order)
                    .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        #endregion M_Report_Output_Item_Master

        #region M_Report_Output_Item
        /*****************************************************************************
          M_Report_Output_Item
          *****************************************************************************/
        /// <summary>
        /// ReportOutputItemList
        /// </summary>
        /// <param name="kubunID"></param>
        /// <param name="companyID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet("ReportOutputItemList")]
        public async Task<IEnumerable<Data.M_Report_Output_Item>> ReportOutputItemList(int kubunID, int companyID, int userID)
        {
            try
            {
                IEnumerable<Data.M_Report_Output_Item> data = null;
                if (kubunID == 0) { return null; }
                data = await _context.M_Report_Output_Items
                    .Where(m => m.Report_Serch_Kubun_ID == kubunID && m.Company_ID == companyID && m.User_ID == userID)
                    .OrderBy(o => o.Sort_Order)
                    .ToListAsync();

                if (data.Count() == 0)
                {
                    data = _context.M_Report_Output_Items
                    .Where(m => m.Report_Serch_Kubun_ID == kubunID && m.Company_ID == companyID && m.User_ID == 0)
                    .OrderBy(o => o.Sort_Order)
                    .ToList();
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// InsertReportOutputItems
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns>MsterDataCommonResultValDto</returns>
        [HttpPost("InsertReportOutputItems")]
        public async Task<IActionResult> InsertReportOutputItems()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                CreateReportOutputItemDto items = System.Text.Json.JsonSerializer.Deserialize<CreateReportOutputItemDto>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertReportOutputItems(items);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        #endregion M_Report_Output_Item

        #region M_Vender
        /*****************************************************************************
          M_Vender
          *****************************************************************************/
        /// <summary>
        /// M_Venderリストの返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="Cd"></param>
        /// <param name="Key"></param>
        /// <param name="Phone"></param>
        /// <param name="SelectRowCount"></param>
        /// <returns></returns>
        [HttpGet("VenderList")]
        public async Task<IActionResult> VenderList(int CompanyID, string Cd = null, string Key = null, string Phone = null, int SelectRowCount = 100)
        {
            try
            {
                if (CompanyID is 0 or < 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                IQueryable<Data.M_Vender> dataList = _context.M_Venders;

                if (Cd != null && Cd.Length > 0)
                {
                    dataList = dataList.Where(m => m.Vender_Code.Substring(0, Cd.Length) == Cd);
                }

                if (Key != null && Key.Length > 0)
                {
                    dataList = dataList.Where(m => (m.Vender_Name.Substring(0, Key.Length) == Key ||
                                            m.Vender_Name_Abbr.Substring(0, Key.Length) == Key ||
                                            m.Vender_Name_Kana.Substring(0, Key.Length) == Key));
                }

                if (Phone != null && Phone.Length > 0)
                {
                    dataList = dataList.Where(m => (m.Phone1.Contains(Phone) || m.Phone2.Contains(Phone) ||
                                                    m.Fax1.Contains(Phone) || m.Fax2.Contains(Phone)));
                }

                List<Data.M_Vender> result = await dataList.OrderBy(m => m.Vender_Code).Take(SelectRowCount).ToListAsync();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }

        /// <summary>
        /// M_Venderの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [HttpGet("VenderData")]
        public async Task<IActionResult> VenderData(int VenderID)
        {
            Data.M_Vender data = null;
            try
            {
                if (VenderID == 0 || VenderID < 0) { throw new Exception("パラメーターエラー：CompanyID"); }

                data = await _context.M_Venders.FirstOrDefaultAsync(m => m.Vender_ID == VenderID);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }


        /// <summary>
        /// M_Venderの追加更新
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpPost("InsertUpdateVenderData")]
        public async Task<IActionResult> InsertUpdateVenderData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                Dto.VenderModalDto dto = System.Text.Json.JsonSerializer.Deserialize<Dto.VenderModalDto>(abc);

                MasterDataModel model = new(_context);
                resultVal = await model.InsertUpdateVenderData(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);

        }

        /// <summary>
        /// M_Vender使用率TOPXXリスト
        /// XXは引数指定
        /// </summary>
        /// <param name="VenderID"></param>
        /// <param name="userId"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        [HttpGet("GetVenderListForUtilizationRate")]
        public async Task<IActionResult> GetVenderListForUtilizationRate(int CompanyID, int userId, int Top = 0)
        {
            string sql = "";
            sql += " SELECT";
            if (Top > 0) { sql += " TOP " + Top.ToString(); }
            sql += " C.* ";
            sql += " FROM";
            sql += " [M_Vender] C";
            sql += " INNER JOIN";
            sql += " (";
            sql += " SELECT ";
            sql += " D.[Vender_ID]";
            sql += " ,COUNT(*) AS AA";
            sql += " FROM ";
            sql += " [T_Expense_Payment] D";
            sql += " INNER JOIN [T_Expense] A ON D.[Expense_ID] = A.[Expense_ID]";
            sql += " WHERE";
            sql += " A.[Company_ID] = {1}";
            sql += " AND";
            sql += " (";
            sql += " D.[Insert_User] = {0}";
            sql += " OR";
            sql += " D.[Update_User] = {0}";
            sql += " )";
            sql += " GROUP BY";
            sql += " [Vender_ID]";
            sql += " )X ON";
            sql += " C.[Vender_ID] = X.[Vender_ID]";
            sql += " ORDER BY";
            sql += " X.AA DESC";

            try
            {
                List<M_Vender> result = await _context.M_Venders.FromSqlRaw(sql, userId, CompanyID).ToListAsync();
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                return CommonHelper.HandleError(e);
            }
        }

        /// <summary>
        /// M_Vender使用履歴TOPXXリスト
        /// XXは引数指定
        /// </summary>
        /// <param name="VenderID"></param>
        /// <param name="userId"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        [HttpGet("GetVenderListForRireki")]
        public async Task<IActionResult> GetVenderListForRireki(int CompanyID, int userId, int Top)
        {
            string sql = "";
            sql += " SELECT";
            if (Top > 0) { sql += " TOP " + Top.ToString(); }
            sql += " C.* ";
            sql += " FROM";
            sql += " [dbo].[M_Vender] C";
            sql += " INNER JOIN";
            sql += " (";
            sql += " ";
            sql += " SELECT";
            sql += " [Vender_ID]";
            sql += " ,MAX([Insert_Datetime]) AS [Sort_Datetime]";
            sql += " FROM";
            sql += " (";
            sql += " SELECT ";
            sql += " D.[Vender_ID]";
            sql += " ,D.[Insert_Datetime]";
            sql += " FROM ";
            sql += " [T_Expense_Payment] D";
            sql += " INNER JOIN [T_Expense] A ON D.[Expense_ID] = A.[Expense_ID]";
            sql += " WHERE";
            sql += " A.[Company_ID] = {1}";
            sql += " AND";
            sql += " D.[Insert_User] = {0}";
            sql += " )Z";
            sql += " GROUP BY";
            sql += " [Vender_ID]";
            sql += " ";
            sql += " )X ON";
            sql += " C.[Vender_ID] = X.[Vender_ID]";
            sql += " ORDER BY";
            sql += " X.[Sort_Datetime] DESC";

            try
            {
                List<M_Vender> result = await _context.M_Venders.FromSqlRaw(sql, userId, CompanyID).ToListAsync();
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                return CommonHelper.HandleError(e);
            }
        }

        #endregion M_Vender

        #region M_Area
        /*****************************************************************************
          M_Area
          *****************************************************************************/
        /// <summary>
        /// M_Areaの取得
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAreaList")]
        public async Task<List<Data.M_Area>> GetAreaList(int CompanyID)
        {
            try
            {
                List<Data.M_Area> list = null;
                list = await _context.M_Areas.Where(m => m.Company_ID == CompanyID).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }
        #endregion M_Area

        #region M_Area_Ken
        /*****************************************************************************
          M_Area_Ken
          *****************************************************************************/
        /// <summary>
        /// M_Area_Kenの取得
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAreaKenList")]
        public async Task<List<Data.M_Area_Ken>> GetAreaKenList(int CompanyID, int AreaID = 0)
        {
            try
            {
                IQueryable<Data.M_Area_Ken> list = null;
                list = _context.M_Area_Kens.Where(m => m.Company_ID == CompanyID);
                if (AreaID > 0) { list = list.Where(m => m.Area_ID == AreaID); }
                return await list.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }
        #endregion M_Area_Ken

        #region M_Report
        /*****************************************************************************
          M_Report
          *****************************************************************************/
        /// <summary>
        /// M_Report_Serch by Report_Number詳細取得のAPI
        /// </summary>
        /// <param name="ReportSerchNum">検索帳票番号</param>
        /// <returns>
        /// 200: M_Report_Serchの詳細
        /// </returns>
        [HttpGet("M_ReportSerch")]
        public async Task<Data.M_Report_Serch> M_ReportSerch(int ReportSerchNum)
        {
            try
            {
                Data.M_Report_Serch result = null;

                if (ReportSerchNum > 0)
                {
                    result = await _context.M_Report_Serches.FirstOrDefaultAsync(m => m.Report_Number == ReportSerchNum);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
            finally { }
        }

        /// <summary>
        /// Report_Serch_IDでM_Report_Serch_Kubun一覧取得API
        /// </summary>
        /// <param name="ReportSerchID">検索帳票ID</param>
        /// <returns>
        /// 200: M_Report_Serch_Kubunの一覧
        /// </returns>
        [HttpGet("M_ReportSerchKubun")]
        public async Task<IActionResult> M_ReportSerchKubun(int ReportSerchID)
        {
            try
            {
                IEnumerable<Data.M_Report_Serch_Kubun> list = null;

                if (ReportSerchID == 0)
                {
                    return null;
                }

                list = await _context.M_Report_Serch_Kubuns.Where(m => (m.Report_Serch_ID == ReportSerchID)).OrderBy(m => m.Sort_Order).ToListAsync();
                return new OkObjectResult(list);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        ///APIはReport_Serch_Kubun_IDでM_Report_Serch_Kubunテーブルから詳細情報を取得
        /// </summary>
        /// <param name="ReportSerchID">Report_Serch_ID</param>
        /// <returns>
        /// 200: detail M_Report_Serch_Kubun
        /// </returns>
        [HttpGet("M_ReportSerchKubunDetail")]
        public async Task<IActionResult> M_ReportSerchKubunDetail(int ReportSerchKubunID)
        {
            try
            {
                Data.M_Report_Serch_Kubun result = null;

                if (ReportSerchKubunID > 0)
                {
                    result = await _context.M_Report_Serch_Kubuns.Where(m => (m.Report_Serch_Kubun_ID == ReportSerchKubunID)).OrderBy(m => m.Sort_Order).FirstOrDefaultAsync();
                }
                return new OkObjectResult(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// Report_Serch_Kubun_IDで M_Report_Serch_Item一覧取得のAPI
        /// </summary>
        /// <param name="ReportSerchKubunID">検索帳票の区分ID</param>
        /// <returns>
        /// 200: M_Report_Serch_Itemの一覧
        /// </returns>
        [HttpGet("M_ReportSerchItem")]
        public async Task<IActionResult> M_ReportSerchItem(int ReportSerchKubunID)
        {
            try
            {
                IEnumerable<Data.M_Report_Serch_Item> list = null;

                if (ReportSerchKubunID == 0)
                {
                    return null;
                }

                list = await _context.M_Report_Serch_Items.Where(m => (m.Report_Serch_Kubun_ID == ReportSerchKubunID)).OrderBy(m => m.Row_Order).ThenBy(m => m.Sort_Order).ToListAsync();
                return new OkObjectResult(list);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// Report_Serch_Kubun_IDでM_Report_Detail_Param一覧取得のAPI
        /// </summary>
        /// <param name="ReportSerchKubunID">検索帳票の区分ID</param>
        /// <returns>
        /// 200: M_Report_Detail_Paramの一覧
        /// </returns>
        [HttpGet("M_ReportDetailParam")]
        public async Task<IActionResult> M_ReportDetailParam(int ReportSerchKubunID)
        {
            try
            {
                IEnumerable<Data.M_Report_Detail_Param> list = null;

                if (ReportSerchKubunID == 0)
                {
                    return null;
                }

                list = await _context.M_Report_Detail_Params.Where(m => (m.Report_Serch_Kubun_ID == ReportSerchKubunID)).ToListAsync();
                return new OkObjectResult(list);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }
        #endregion M_Report
    }
}
