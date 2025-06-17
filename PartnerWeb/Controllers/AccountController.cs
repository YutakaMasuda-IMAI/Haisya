using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PartnerWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace PartnerWeb.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {

        public AccountController(SignInManager<ApplicationUser> signInManager, IOptions<MapApiSettings> mapApiSetting)
        {
            _signInManager = signInManager;
            _mapApiSettiong = mapApiSetting.Value;
        }


        // GET: Login
        public async Task<IActionResult> Index()
        {
            //API.WebApp.MasterDataApi api = new(_mapApiSettiong);

            //int loginId = int.Parse(_signInManager.Context.User.Claims.ToList()[0].Value);

            //List<Dto.M_LoginUser_Local> list = await api.GetLoginUserList(1);


            return View();
        }


        /// <summary>
        /// ログイン画面のインデックス
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult Login(string returnUrl)
        {
            //// ログインユーザー取得
            //Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            //API.WebApp.MasterDataApi api = new(_mapApiSettiong);

            Models.AccountModel model = new();
            model.ReturnUrl = returnUrl;
            //model.LoginUserList = await api.GetLoginUserList(loguinUser.Company_ID);

            return View(model);
        }

        /// <summary>
        /// ログアウト画面のインデックス
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult Logout(string returnUrl)
        {
            Models.AccountModel model = new();
            return View(model);
        }


        // GET: Login/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            //var m_User = await _context.M_Users.FirstOrDefaultAsync(m => m.LOGIN_ID == id);
            //if (m_User == null) return NotFound();

            return View();
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LOGIN_ID,Password,USER_NAME,DEL_FLG")] Dto.V_LoginUser_Local m_User)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(m_User);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(m_User);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            //var m_User = await _context.M_Users.FindAsync(id);
            //if (m_User == null) return NotFound();

            //return View(m_User);
            return View();
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LOGIN_ID,Password,USER_NAME,DEL_FLG")] Dto.V_LoginUser_Local m_User)
        {
            if (id != m_User.LoginID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(m_User);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!M_UserExists(m_User.LoginID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(m_User);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            //var m_User = await _context.M_Users.FirstOrDefaultAsync(m => m.LOGIN_ID == id);
            //if (m_User == null) return NotFound();

            //return View(m_User);
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var m_User = await _context.M_Users.FindAsync(id);
            //_context.M_Users.Remove(m_User);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool M_UserExists(string id)
        {
            //return _context.M_Users.Any(e => e.LOGIN_ID == id);
            return false;
        }




        /// <summary>
        /// ログイン処理
        /// submitボタンがクリックされてpostリクエストを受け付けたときに動く
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginExecAsync(AccountModel model)
        {
            // 必須入力がないなどの場合ログインさせない。（ログインページに戻る）
            if (!ModelState.IsValid) return View("Login", model);

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            Dto.V_LoginUser_Local user = await api.GetLoginUserList(model.CompanyCode, model.LoginID);

            if (user == null)
            {
                // ユーザーID・パスワードが間違っている場合ログインさせない。
                model.Password = null;
                model.ValidationMessage = "ログインID or パスワードが間違っています。";
                return View("Login", model);
            }

            //
            ApplicationUser loginUser = new()
            {
                LoginUserId = user.LoginUser_ID,
                Id = user.LoginID,
                UserId = user.User_ID,
                UserName = user.User_Name,
                Password = user.Password,
                Role = user.Role
            };

            //
            SignInResult result = await _signInManager.PasswordSignInAsync(loginUser, user.Password, false, false);
            if (result.Succeeded)
            {
                Console.Write("Success!");
            }
            else
            {
                Console.WriteLine("Failed");
            }




            //// ★以下ログイン処理
            //// 名前、電子メール アドレス、年齢、Sales ロールのメンバーシップなど、id 情報の一部
            //Claim[] claims = {
            //            new Claim(ClaimTypes.NameIdentifier, m_User.LOGIN_ID), // ユニークID
            //            new Claim(ClaimTypes.Name, m_User.USER_NAME),   
            //            new Claim(ClaimTypes.Role, m_User.ROLE.ToString()),
            //          };

            //// 一意の ID 情報
            //var claimsIdentity = new ClaimsIdentity( claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //// ログイン
            //await HttpContext.SignInAsync(
            //                      CookieAuthenticationDefaults.AuthenticationScheme,
            //                      new ClaimsPrincipal(claimsIdentity),
            //                      new AuthenticationProperties
            //                      {
            //                        // Cookie をブラウザー セッション間で永続化するか？（ブラウザを閉じてもログアウトしないかどうか）
            //                        IsPersistent = true
            //                      });

            if (Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


        /// <summary>
        /// ログアウト処理
        /// submitボタンがクリックされてpostリクエストを受け付けたときに動く
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> LogoutExecAsync(AccountModel model, string returnUrl)
        {

            await _signInManager.SignOutAsync();
            // クッキーを削除
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // ログアウト後はトップページへリダイレクト
            return LocalRedirect(Url.Content("~/"));

        }









    }
}
