using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data
{
    /// <summary>
    /// アプリケーションのデータベースコンテキストを表します。
    /// </summary>
    public partial class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// SP_T_Ankens テーブルを表します。
        /// </summary>
        public virtual DbSet<SP_ResultForInt> SP_T_Ankens { get; set; }

        /// <summary>
        /// SP_T_Anken_Nos テーブルを表します。
        /// </summary>
        public virtual DbSet<SP_ResultForString> SP_T_Anken_Nos { get; set; }

        /// <summary>
        /// V_AnkenDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_AnkenDataList> V_AnkenDataLists { get; set; }

        /// <summary>
        /// V_HaisyaDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_HaisyaDataList> V_HaisyaDataLists { get; set; }

        /// <summary>
        /// V_SeikyuCheckDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_SeikyuCheckDataList> V_SeikyuCheckDataLists { get; set; }

        /// <summary>
        /// V_ShitabaraiCheckDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_ShitabaraiCheckDataList> V_ShitabaraiCheckDataLists { get; set; }

        /// <summary>
        /// V_UriageDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_UriageDataList> V_UriageDataLists { get; set; }

        /// <summary>
        /// V_ExpenseDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_ExpenseDataList> V_ExpenseDataLists { get; set; }

        /// <summary>
        /// V_SeikyuDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_SeikyuDataList> V_SeikyuDataLists { get; set; }

        /// <summary>
        /// V_SeikyuZumiDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_SeikyuZumiDataList> V_SeikyuZumiDataLists { get; set; }

        /// <summary>
        /// V_SyabanRenrakuDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_SyabanRenrakuDataList> V_SyabanRenrakuDataLists { get; set; }

        /// <summary>
        /// Proc_PrintSeikyus テーブルを表します。
        /// </summary>
        public virtual DbSet<T_Print_Seikyu> Proc_PrintSeikyus { get; set; }

        /// <summary>
        /// V_InvoiceDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_InvoiceDataList> V_InvoiceDataLists { get; set; }

        /// <summary>
        /// V_InvoiceCheckDataLists テーブルを表します。
        /// </summary>
        public virtual DbSet<V_InvoiceCheckDataList> V_InvoiceCheckDataLists { get; set; }
    }

    /// <summary>
    /// ストアドプロシージャの結果を表します（整数）。
    /// </summary>
    [Keyless]
    public class SP_ResultForInt
    {
        /// <summary>
        /// 結果を取得または設定します。
        /// </summary>
        public int? Result { get; set; }
    }

    /// <summary>
    /// ストアドプロシージャの結果を表します（文字列）。
    /// </summary>
    [Keyless]
    public class SP_ResultForString
    {
        /// <summary>
        /// 結果を取得または設定します。
        /// </summary>
        public string Result { get; set; }
    }
}
