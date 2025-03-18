using System;
using System.Collections.Generic;
using WebApplication.Data;

#nullable disable

namespace WebApplication.Model
{
    public class PaymentInputDataListModel : PaymentInputDataListModel<T_Nyukin> { }

    /// <summary>
    /// 入金入力モデル
    /// </summary>
    public class PaymentInputDataListModel<Nyukin_DataType>
        where Nyukin_DataType : T_Nyukin
    {
        /// <summary>
        /// 請求入金履歴
        /// </summary>
        public List<BillingPaymentHistory> BillingPaymentHistoryList { get; set; }

        /// <summary>
        /// 請求印刷
        /// </summary>
        public T_Print_Seikyu PrintSeikyu { get; set; }

        /// <summary>
        /// 入金一覧
        /// </summary>
        public List<Nyukin_DataType> PaymentNyukinList { get; set; }

        /// <summary>
        /// 返金一覧
        /// </summary>
        public List<Nyukin_DataType> RepaymentNyukinList { get; set; }

    }

    /// <summary>
    /// 請求入金履歴クラス
    /// </summary>
    public class BillingPaymentHistory
    {
        /// <summary>
        /// 請求印刷
        /// </summary>
        public T_Print_Seikyu PrintSeikyu { get; set; }

        /// <summary>
        /// 入金日
        /// </summary>
        public DateTime Process_Date { get; set; }

        /// <summary>
        /// 預金合計額
        /// </summary>
        public decimal Deposit_Total { get; set; }
    }

    public class PostPaymentInputDataModel : PostPaymentInputDataModel<T_Nyukin> { }

    /// <summary>
    /// 入金入力後モデル
    /// </summary>
    public class PostPaymentInputDataModel<Nyukin_DataType>
        where Nyukin_DataType : T_Nyukin
    {
        /// <summary>
        /// 入金一覧
        /// </summary>
        public List<Nyukin_DataType> PaymentNyukinList { get; set; }

        /// <summary>
        /// 返金一覧
        /// </summary>
        public List<Nyukin_DataType> RepaymentNyukinList { get; set; }

        /// <summary>
        /// ユーザID
        /// </summary>
        public int User_ID { get; set; }

        /// <summary>
        /// 請求ID
        /// </summary>
        public int Seikyu_ID { get; set; }
    }
}
