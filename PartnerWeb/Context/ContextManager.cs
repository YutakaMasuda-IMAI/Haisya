using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using PartnerWeb.Dto;
using Microsoft.VisualBasic;


namespace PartnerWeb.Context
{

    public sealed class ContextManager
    {

        /// <summary>
        /// コンテクストマネージャ情報（シングルトン）
        /// </summary>
        /// <remarks></remarks>
        private static ContextManager _objInstance;

        /// <summary>
        /// アドレス情報
        /// </summary>
        /// <remarks></remarks>
        private AddressList _objAddress;



        /// <summary>
        /// プライベートコンストラクタ
        /// </summary>
        /// <remarks></remarks>
        private ContextManager()
        {
        }




        /// <summary>
        /// セッションIDクッキープロパティ
        /// </summary>
        /// <value>セッションIDクッキー</value>
        /// <returns>セッションIDクッキー</returns>
        /// <remarks></remarks>
        public Cookie SessionIdCookie { get; set; }

        /// <summary>
        /// インスタンスを取得する。
        /// </summary>
        /// <value>設定なし</value>
        /// <returns>シングルトンインスタンスを返却する。</returns>
        /// <remarks></remarks>
        public static ContextManager Instance
        {
            get
            {
                if (Information.IsNothing(_objInstance))
                    throw new InvalidOperationException("ContextManagerが初期化されていません。Initializeを呼び出して初期化処理を実行してください。");
                return _objInstance;
            }
        }

        /// <summary>
        /// 初期化メソッド
        /// </summary>
        /// <param name="strExePath"></param>
        public static void Initialize()
        {
            _objInstance = new ContextManager();
        }



        /// <summary>
        /// アドレス情報の設定
        /// </summary>
        /// <param name="objAddress">アドレス情報</param>
        /// <remarks>
        /// 設定は一度のみしかできない。
        /// </remarks>
        public void SetAddressOnce(AddressList objAddress)
        {

            // 'アドレス情報のインスタンスが存在する場合
            if (_objAddress != null)
                throw new InvalidOperationException("すでにアドレス情報は設定済です。");

            _objAddress = objAddress;
        }

        /// <summary>
        /// アドレス情報をクリアする。
        /// </summary>
        /// <remarks></remarks>
        public void ClearAddressInfo()
        {
            _objAddress = null;
        }


    }

}
