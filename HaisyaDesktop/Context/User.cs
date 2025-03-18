using HaisyaDesktop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.Context
{
    public class User : DynamicDictionary
    {

        /// <summary>
        /// ログインID
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string LoginId { get; set; }

        /// <summary>
        /// ユーザID
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int UserId { get; set; } = 0;


        /// <summary>
        /// ユーザ名
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string UserName { get; set; }


        /// <summary>
        /// ユーザ車種（初期表示）
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Syasyu { get; set; }

        /// <summary>
        /// ユーザ型（初期表示）
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Kata { get; set; }


        /// <summary>
        /// ユーザ車種サイズ（初期表示）
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SyasyuSize { get; set; }

        /// <summary>
        /// ユーザ車種表示（初期表示）
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SyasyuDisplay { get; set; }

        /// <summary>
        /// ユーザ車型表示（初期表示）
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string KataDisplay { get; set; }


        /// <summary>
        /// 会社ID
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CompanyID { get; set; }

        /// <summary>
        /// 営業所・支店ID
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int BranchID { get; set; }

        /// <summary>
        /// 配車担当ID
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int HaisyaTantouID { get; set; }

    }
}
