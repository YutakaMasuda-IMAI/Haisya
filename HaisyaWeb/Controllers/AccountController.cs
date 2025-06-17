using HaisyaWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace HaisyaWeb.Controllers
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
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ログイン画面のインデックス
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult Login(string returnUrl)
        {
            try
            {
                AccountModel model = new();
                model.ReturnUrl = returnUrl;

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                ErrorViewModel errorModel = new();
                errorModel.RequestId = "1";
                errorModel.Message = ex.Message;
                return View("Error", errorModel);
            }

        }

        /// <summary>
        /// ログアウト画面のインデックス
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult Logout(string returnUrl)
        {
            InitializeSession();
            Models.AccountModel model = new();
            return View(model);
        }

        


        /// <summary>
        /// ログイン画面のインデックス
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult SignIn()
        {
            try
            {
                Models.AccountModel model = new();
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                ErrorViewModel errorModel = new();
                errorModel.RequestId = "1";
                errorModel.Message = ex.Message;
                return View("Error", errorModel);
            }

        }

        public IActionResult Details(string id)
        {
            if (id == null) return NotFound();

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
        public IActionResult Create([Bind("LOGIN_ID,Password,USER_NAME,DEL_FLG")] Dto.V_LoginUser_Local m_User)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(m_User);
        }

        // GET: Login/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null) return NotFound();

            return View();
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("LOGIN_ID,Password,USER_NAME,DEL_FLG")] Dto.V_LoginUser_Local m_User)
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
        public IActionResult Delete(string id)
        {
            if (id == null) return NotFound();

            return View();
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
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
            if (!ModelState.IsValid) return View(model.LoginUrl, model);

            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.V_LoginUser_Local user = await api.GetV_LoginUserData(model.CompanyCode, model.LoginID);
                if (user == null)
                {
                    // ユーザーIDが間違っている場合ログインさせない。
                    model.Password = null;
                    model.ValidationMessage = "ログインIDが間違っています。";
                    return View(model.LoginUrl, model);
                }

                //
                ApplicationUser loginUser = new()
                {
                    LoginUserId = user.LoginUser_ID,
                    Id = user.LoginID,
                    UserId = user.User_ID,
                    UserName = user.User_Name,
                    Password = user.Password,
                    Role = user.Role,
                    CompanyName = user.Company_Name,
                };

                //
                SignInResult result = await _signInManager.PasswordSignInAsync(loginUser, model.Password, false, false);
                if (result.Succeeded)
                {
                    Console.Write("Success!");
                }
                else
                {
                    Console.WriteLine("Failed");
                    //パスワードが間違っている場合ログインさせない。
                    model.Password = null;
                    model.ValidationMessage = "パスワードが間違っています。";
                    return View("Login", model);
                }

                // ★以下ログイン処理
                // 名前、電子メール アドレス、年齢、Sales ロールのメンバーシップなど、id 情報の一部
                Claim[] claims = {
                        new Claim(ClaimTypes.NameIdentifier, loginUser.Id), // ユニークID
                        new Claim(ClaimTypes.Name, loginUser.UserName),
                        new Claim(ClaimTypes.Role, loginUser.Role.ToString()),
                        new Claim(ClaimTypes.UserData, loginUser.CompanyName),
                      };

                // 一意の ID 情報
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // ログイン
                await HttpContext.SignInAsync(
                                      CookieAuthenticationDefaults.AuthenticationScheme,
                                      new ClaimsPrincipal(claimsIdentity),
                                      new AuthenticationProperties
                                      {
                                          ////サーバーアクセスによる認証時間を更新する
                                          //AllowRefresh = true,
                                          ////認証の有効期間,認証cookieタイムアウト
                                          //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5),
                                          //cookieの有効期間を優先かsessionを優先か
                                          //true:cookieが有効であれば、ブラウザを閉じても再ログインが必要ない
                                          //false:ブラウザ閉じたら再ログインが必要（ブラウザを閉じてもログアウトしないかどうか）
                                          IsPersistent = false,
                                          //cookieの認証時間
                                          IssuedUtc = DateTime.UtcNow
                                      });

                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            catch (Exception x)
            {
                return Error(x);
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
            InitializeSession();
            await _signInManager.SignOutAsync();
            // クッキーを削除
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // ログアウト後はトップページへリダイレクト
            return LocalRedirect(Url.Content("~/"));

        }

        public IActionResult PasswordNew()
        {
            return View();
        }

        public new IActionResult Forbid()
        {

            return View();
        }

    }
}
