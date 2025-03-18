using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HaisyaWeb.Dto
{
    /// <summary>
    /// 請求チェックデータリストローカルクラス
    /// </summary>
    public partial class V_SeikyuCheckDataList_Local : WebApplication.Data.V_SeikyuCheckDataList
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public V_SeikyuCheckDataList_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public V_SeikyuCheckDataList_Local(Dto.V_SeikyuCheckDataList_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.V_SeikyuCheckDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// T_Print_Seikyuの取得
    /// </summary>
    public partial class T_Print_Seikyu_Local : WebApplication.Data.T_Print_Seikyu
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public T_Print_Seikyu_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public T_Print_Seikyu_Local(Dto.T_Print_Seikyu_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.T_Print_Seikyu_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// バッチ登録モデル
    /// </summary>
    public partial class BatchRegistrationModel : WebApplication.Model.BatchRegistrationModel
    {

    }

    /// <summary>
    /// モーダル承認モデル
    /// </summary>
    public partial class ModalApprovalModel : WebApplication.Model.ModalApprovalModel
    {

    }

    /// <summary>
    /// 承認ステータスモデル
    /// </summary>
    public partial class ApprovalStatusModel : WebApplication.Model.ApprovalStatusModel
    {

    }

    /// <summary>
    /// T_Print_Seikyu_Detailの取得
    /// </summary>
    public partial class T_Print_Seikyu_Detail_Local : WebApplication.Data.T_Print_Seikyu_Detail
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public T_Print_Seikyu_Detail_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public T_Print_Seikyu_Detail_Local(Dto.T_Print_Seikyu_Detail_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.T_Print_Seikyu_Detail_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }
    
    /// <summary>
    /// T_Nyukinの取得
    /// </summary>
    public partial class T_Nyukin_Local : WebApplication.Data.T_Nyukin
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public T_Nyukin_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public T_Nyukin_Local(Dto.T_Nyukin_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.T_Nyukin_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// T_Check_Seikyu_Changeの取得
    /// </summary>
    public partial class T_Check_Seikyu_Change_Local : WebApplication.Data.T_Check_Seikyu_Change
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public T_Check_Seikyu_Change_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public T_Check_Seikyu_Change_Local(Dto.T_Check_Seikyu_Change_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.T_Nyukin_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// T_Check_Seikyu_Doneの取得
    /// </summary>
    public partial class T_Check_Seikyu_Done_Local : WebApplication.Data.T_Check_Seikyu_Done
    {

    }

    /// <summary>
    /// T_Uriage_Unchinの取得
    /// </summary>
    public partial class T_Uriage_Unchin_Local : WebApplication.Data.T_Uriage_Unchin
    {
        /// <summary>
        /// 赤伝黒伝伝票判定
        /// </summary>
        public bool IsCreditSlip { get; set; } = false;

        /// <summary>
        /// 赤伝黒伝伝票締日
        /// </summary>
        public DateTime Shime_Datetime { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public T_Uriage_Unchin_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public T_Uriage_Unchin_Local(Dto.T_Uriage_Unchin_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.T_Uriage_Unchin_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// T_Uriageの取得
    /// </summary>
    public partial class T_Uriage_Local : WebApplication.Data.T_Uriage
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public T_Uriage_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public T_Uriage_Local(Dto.T_Uriage_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.T_Uriage_Unchin_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// T_Check_Seikyuの取得
    /// </summary>
    public partial class T_Check_Seikyu_Local : WebApplication.Data.T_Check_Seikyu
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public T_Check_Seikyu_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public T_Check_Seikyu_Local(Dto.T_Check_Seikyu_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.T_Uriage_Unchin_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// T_Check_Seikyu_Detailの取得
    /// </summary>
    public partial class T_Check_Seikyu_Detail_Local : WebApplication.Data.T_Check_Seikyu_Detail
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public T_Check_Seikyu_Detail_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public T_Check_Seikyu_Detail_Local(Dto.T_Check_Seikyu_Detail_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.T_Uriage_Unchin_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// 請求データリストローカルクラス
    /// </summary>
    public partial class V_SeikyuDataList_Local : WebApplication.Data.V_SeikyuDataList
    {
        private V_SeikyuDataList_Local list;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public V_SeikyuDataList_Local(V_SeikyuDataList_Local list) => this.list = list;
    }

    /// <summary>
    /// 請求データリストローカルクラス2
    /// </summary>
    public partial class V_SeikyuDataList_Local2 : WebApplication.Data.V_SeikyuDataList
    {
    }

    /// <summary>
    /// 請求済データリストローカルクラス
    /// </summary>
    public partial class V_SeikyuZumiDataList_Local : WebApplication.Data.V_SeikyuZumiDataList { }

    /// <summary>
    /// 請求済データリストローカルクラスの拡張メソッド
    /// </summary>
    public static class V_SeikyuZumiDataList_Local_Extensions
    {
        /// <summary>
        /// 問い合わせステータスラベルを取得
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>ステータスラベル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Inquiry_status_label(this V_SeikyuZumiDataList_Local item)
            => item.Inquiry_Status == 1 ? "WEB" : item.Inquiry_Status == 2 ? "帳票" : "";

        /// <summary>
        /// 登録ステータスラベルを取得
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>ステータスラベル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Reg_status_label(this V_SeikyuZumiDataList_Local item)
            => item.Reg_Status == 0 ? "可" : item.Reg_Status == 1 ? "不可" : "";

        public static string Seikyu_month_display(this V_SeikyuZumiDataList_Local item)
        {
            DateTime sm = item.Seikyu_Month;
            DateTime dt = sm.AddDays(-sm.Day + item.Shime_Day);
            dt = dt.Month == sm.Month ? dt : sm.AddDays(-sm.Day + 1).AddMonths(1).AddDays(-1);
            return $"{dt:MM}/{dt:dd}";
        }
    }
}
