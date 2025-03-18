//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using System.Security;
//using System.Text;
//using System.Threading.Tasks;
//using HaisyaWeb.Dto;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualBasic;
//using Newtonsoft.Json;

//namespace HaisyaWeb.Context
//{

//    public sealed class ContextManager : Controller
//    {

//        /// <summary>
//        /// コンテクストマネージャ情報（シングルトン）
//        /// </summary>
//        /// <remarks></remarks>
//        private static ContextManager _objInstance;

//        ///// <summary>
//        ///// アドレス情報
//        ///// </summary>
//        ///// <remarks></remarks>
//        //private AddressList _objAddress;

//        ///// <summary>
//        ///// 権限ロール情報
//        ///// </summary>
//        //private List<Dto.M_Role_Local> _objRole;

//        /// <summary>
//        /// プライベートコンストラクタ
//        /// </summary>
//        /// <remarks></remarks>
//        private ContextManager()
//        {
//            HttpContext.Session.SetObject("objAddress", new AddressList());
//            HttpContext.Session.SetObject("objRole", new List<Dto.M_Role_Local>());
//        }


//        /// <summary>
//        /// ユーザ情報
//        /// </summary>
//        /// <value>設定なし</value>
//        /// <returns>ユーザ情報</returns>
//        /// <remarks></remarks>
//        public List<Dto.M_Role_Local> M_RoleList
//        {
//            get
//            {
//                try
//                {
//                    return HttpContext.Session.GetObject<List<Dto.M_Role_Local>>("objRole");
//                } catch (Exception ex)
//                {
//                    return null;
//                }
//            }
//            //get => HttpContext.Session.GetObject<List<Dto.M_Role_Local>>("objRole");
//        }

//        /// <summary>
//        /// セッションIDクッキープロパティ
//        /// </summary>
//        /// <value>セッションIDクッキー</value>
//        /// <returns>セッションIDクッキー</returns>
//        /// <remarks></remarks>
//        public Cookie SessionIdCookie { get; set; }

//        /// <summary>
//        /// インスタンスを取得する。
//        /// </summary>
//        /// <value>設定なし</value>
//        /// <returns>シングルトンインスタンスを返却する。</returns>
//        /// <remarks></remarks>
//        public static ContextManager Instance
//        {
//            get
//            {
//                if (Information.IsNothing(_objInstance))
//                    throw new InvalidOperationException("ContextManagerが初期化されていません。Initializeを呼び出して初期化処理を実行してください。");
//                return _objInstance;
//            }
//        }

//        /// <summary>
//        /// 初期化メソッド
//        /// </summary>
//        /// <param name="strExePath"></param>
//        public static void Initialize()
//        {
//            _objInstance = new ContextManager();
            
//        }



//        /// <summary>
//        /// アドレス情報の設定
//        /// </summary>
//        /// <param name="objAddress">アドレス情報</param>
//        /// <remarks>
//        /// 設定は一度のみしかできない。
//        /// </remarks>
//        public void SetAddressOnce(AddressList objAddress)
//        {
//            // 'アドレス情報のインスタンスが存在する場合
//            if (HttpContext.Session.GetObject<AddressList>("objAddress") == default)
//                throw new InvalidOperationException("すでにアドレス情報は設定済です。");

//            HttpContext.Session.SetObject("objAddress", objAddress);
//        }

//        /// <summary>
//        /// アドレス情報をクリアする。
//        /// </summary>
//        /// <remarks></remarks>
//        public void ClearAddressInfo()
//        {
//            HttpContext.Session.SetObject("objAddress", new AddressList());
//        }

//        /// <summary>
//        /// 権限ロール情報の設定
//        /// </summary>
//        /// <param name="objRole">権限ロール情報</param>
//        /// <remarks>
//        /// 設定は一度のみしかできない。
//        /// </remarks>
//        public void SetRoleOnce(List<Dto.M_Role_Local> objRole)
//        {
//            // '権限ロール情報のインスタンスが存在する場合
//            if (HttpContext.Session.GetObject<AddressList>("objRole") == null)
//                throw new InvalidOperationException("すでに権限ロール情報は設定済です。");

//            HttpContext.Session.SetObject("objRole", objRole);
//        }

//        /// <summary>
//        /// 権限ロール情報をクリアする。
//        /// </summary>
//        /// <remarks></remarks>
//        public void ClearRoleInfo()
//        {
//            HttpContext.Session.SetObject("objRole", new List<Dto.M_Role_Local>());
//        }


//    }

//    // セッションにオブジェクトを設定・取得する拡張メソッドを用意する
//    public static class SessionExtensions
//    {
//        // セッションにオブジェクトを書き込む
//        public static void SetObject<TObject>(this ISession session, string key, TObject obj)
//        {
//            var json = JsonConvert.SerializeObject(obj);
//            session.SetString(key, json);
//        }

//        // セッションからオブジェクトを読み込む
//        public static TObject GetObject<TObject>(this ISession session, string key)
//        {
//            var json = session.GetString(key);
//            return string.IsNullOrEmpty(json)
//                ? default(TObject)
//                : JsonConvert.DeserializeObject<TObject>(json);
//        }
//    }

//}
