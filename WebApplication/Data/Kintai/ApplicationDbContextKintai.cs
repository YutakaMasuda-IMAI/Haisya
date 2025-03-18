using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication.Data.Kintai
{
    public partial class ApplicationDbContextKintai : DbContext
    {
        public ApplicationDbContextKintai()
        {
        }

        public ApplicationDbContextKintai(DbContextOptions<ApplicationDbContextKintai> options)
            : base(options)
        {
        }

        public virtual DbSet<ALC_TEMP> ALC_TEMPs { get; set; }
        public virtual DbSet<KUDGIVT_ANYTIME> KUDGIVT_ANYTIMEs { get; set; }
        public virtual DbSet<M_Allowance> M_Allowances { get; set; }
        public virtual DbSet<M_Allowance_UnitPrice> M_Allowance_UnitPrices { get; set; }
        public virtual DbSet<M_BATCH> M_BATCHes { get; set; }
        public virtual DbSet<M_Day> M_Days { get; set; }
        public virtual DbSet<M_Driver> M_Drivers { get; set; }
        public virtual DbSet<M_Driver_UnitPrice> M_Driver_UnitPrices { get; set; }
        public virtual DbSet<M_Holiday> M_Holidays { get; set; }
        public virtual DbSet<M_HolidayWork> M_HolidayWorks { get; set; }
        public virtual DbSet<M_LEAVE> M_LEAVEs { get; set; }
        public virtual DbSet<M_LEAVE_REASON> M_LEAVE_REASONs { get; set; }
        public virtual DbSet<M_Leave_Setting> M_Leave_Settings { get; set; }
        public virtual DbSet<M_Leave_Setting_Table> M_Leave_Setting_Tables { get; set; }
        public virtual DbSet<M_Role> M_Roles { get; set; }
        public virtual DbSet<M_Salary_Syakaku> M_Salary_Syakakus { get; set; }
        public virtual DbSet<M_Setting> M_Settings { get; set; }
        public virtual DbSet<M_User> M_Users { get; set; }
        public virtual DbSet<M_User_Role> M_User_Roles { get; set; }
        public virtual DbSet<M_乗務外内容> M_乗務外内容s { get; set; }
        public virtual DbSet<Q_F_事業所名> Q_F_事業所名s { get; set; }
        public virtual DbSet<T_ALC_CHECK> T_ALC_CHECKs { get; set; }
        public virtual DbSet<T_ALC_CHECK_IMPORT> T_ALC_CHECK_IMPORTs { get; set; }
        public virtual DbSet<T_BATCH_RESULT> T_BATCH_RESULTs { get; set; }
        public virtual DbSet<T_Driver_Salary> T_Driver_Salaries { get; set; }
        public virtual DbSet<T_KINTAI_BASE> T_KINTAI_BASEs { get; set; }
        public virtual DbSet<T_KINTAI_BASE2> T_KINTAI_BASE2s { get; set; }
        public virtual DbSet<T_KINTAI_COMMIT> T_KINTAI_COMMITs { get; set; }
        public virtual DbSet<T_KINTAI_COMMIT_CREW> T_KINTAI_COMMIT_CREWs { get; set; }
        public virtual DbSet<T_KINTAI_COMMIT_NON_CREW> T_KINTAI_COMMIT_NON_CREWs { get; set; }
        public virtual DbSet<T_KINTAI_COMMIT_NON_CREW_BK> T_KINTAI_COMMIT_NON_CREW_BKs { get; set; }
        public virtual DbSet<T_KUDGIVT> T_KUDGIVTs { get; set; }
        public virtual DbSet<T_KUDGIVT_DEL> T_KUDGIVT_DELs { get; set; }
        public virtual DbSet<T_KUDGIVT_TARGET> T_KUDGIVT_TARGETs { get; set; }
        public virtual DbSet<T_KUDGSIR> T_KUDGSIRs { get; set; }
        public virtual DbSet<T_KUDGURI> T_KUDGURIs { get; set; }
        public virtual DbSet<T_Kakutei> T_Kakuteis { get; set; }
        public virtual DbSet<T_LOCK> T_LOCKs { get; set; }
        public virtual DbSet<T_Leave> T_Leaves { get; set; }
        public virtual DbSet<T_Leave_Auto_Grant_Rireki> T_Leave_Auto_Grant_Rirekis { get; set; }
        public virtual DbSet<T_Leave_Give> T_Leave_Gives { get; set; }
        public virtual DbSet<T_Leave_Summary> T_Leave_Summaries { get; set; }
        public virtual DbSet<T_Leave_Summary_Detail> T_Leave_Summary_Details { get; set; }
        public virtual DbSet<T_Leave_Summary_Detail2> T_Leave_Summary_Detail2s { get; set; }
        public virtual DbSet<T_安全報奨> T_安全報奨s { get; set; }
        public virtual DbSet<V_BATCH_RESULT_FOR_PORTAL> V_BATCH_RESULT_FOR_PORTALs { get; set; }
        public virtual DbSet<V_DRIVER> V_DRIVERs { get; set; }
        public virtual DbSet<V_DigiTachoLinkAlert> V_DigiTachoLinkAlerts { get; set; }
        public virtual DbSet<V_Driver2> V_Driver2s { get; set; }
        public virtual DbSet<V_Haisya> V_Haisyas { get; set; }
        public virtual DbSet<V_Leave_Auto_Grant_Rireki> V_Leave_Auto_Grant_Rirekis { get; set; }
        public virtual DbSet<V_OFFICE> V_OFFICEs { get; set; }
        public virtual DbSet<V_勤怠1時間重複エラー> V_勤怠1時間重複エラーs { get; set; }
        public virtual DbSet<V_差分チェック> V_差分チェックs { get; set; }
        public virtual DbSet<V_差分チェック2> V_差分チェック2s { get; set; }
        public virtual DbSet<V_月次勤怠一覧> V_月次勤怠一覧s { get; set; }
        public virtual DbSet<V_燃料無駄候補リスト> V_燃料無駄候補リストs { get; set; }
        public virtual DbSet<V_計算エラー> V_計算エラーs { get; set; }
        public virtual DbSet<V_重複デジタコデータ> V_重複デジタコデータs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=IMAI-SRV-DB\\SQLEXPRESS;Database=DriverKintai;user id=sa;password=Imaiunso00;Connection Timeout=300;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<ALC_TEMP>(entity =>
            {
                entity.Property(e => e.ALC).IsUnicode(false);

                entity.Property(e => e.CODE).IsUnicode(false);

                entity.Property(e => e.DATETIME).IsUnicode(false);

                entity.Property(e => e.DUMMY1).IsUnicode(false);

                entity.Property(e => e.DUMMY2).IsUnicode(false);

                entity.Property(e => e.DUMMY3).IsUnicode(false);

                entity.Property(e => e.DUMMY4).IsUnicode(false);

                entity.Property(e => e.DUMMY5).IsUnicode(false);

                entity.Property(e => e.FUMEI1).IsUnicode(false);

                entity.Property(e => e.ID).ValueGeneratedOnAdd();

                entity.Property(e => e.NAME).IsUnicode(false);

                entity.Property(e => e.STATUS).IsUnicode(false);
            });

            modelBuilder.Entity<KUDGIVT_ANYTIME>(entity =>
            {
                entity.ToView("KUDGIVT_ANYTIME");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<M_Allowance>(entity =>
            {
                entity.Property(e => e.Allowance_ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<M_Allowance_UnitPrice>(entity =>
            {
                entity.HasKey(e => new { e.Allowance_ID, e.Start_Month })
                    .HasName("PK_M_Allowance_UnitPrice_1");
            });

            modelBuilder.Entity<M_Driver>(entity =>
            {
                entity.Property(e => e.WORKER_CD).ValueGeneratedNever();

                entity.Property(e => e.GYOUMU_START).HasDefaultValueSql("('1900/01/01')");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_Driver_UnitPrice>(entity =>
            {
                entity.HasKey(e => new { e.WORKER_CD, e.Start_Month });
            });

            modelBuilder.Entity<M_Holiday>(entity =>
            {
                entity.Property(e => e.WeekDay).IsFixedLength(true);
            });

            modelBuilder.Entity<M_HolidayWork>(entity =>
            {
                entity.Property(e => e.HolidayWork_CD).ValueGeneratedNever();
            });

            modelBuilder.Entity<M_LEAVE>(entity =>
            {
                entity.HasKey(e => new { e.LEAVE_CD, e.Company_ID });
            });

            modelBuilder.Entity<M_LEAVE_REASON>(entity =>
            {
                entity.HasKey(e => new { e.LEAVE_CD, e.REASON_CD, e.Company_ID })
                    .HasName("PK_M_LEAVE_REASON_1");
            });

            modelBuilder.Entity<M_Leave_Setting>(entity =>
            {
                entity.Property(e => e.Leave_Setting_CD).ValueGeneratedNever();
            });

            modelBuilder.Entity<M_Leave_Setting_Table>(entity =>
            {
                entity.HasKey(e => new { e.Leave_Setting_CD, e.Leave_Setting_Month });
            });

            modelBuilder.Entity<M_Role>(entity =>
            {
                entity.HasKey(e => new { e.Company_ID, e.Role, e.Controller, e.Action, e.Method })
                    .HasName("PK_M_Role_1");

                entity.Property(e => e.Method).HasDefaultValueSql("(N'ALL')");
            });

            modelBuilder.Entity<M_Salary_Syakaku>(entity =>
            {
                entity.Property(e => e.Syakaku_ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<M_Setting>(entity =>
            {
                entity.Property(e => e.Setting_CD).ValueGeneratedNever();
            });

            modelBuilder.Entity<M_User>(entity =>
            {
                entity.Property(e => e.Password).IsUnicode(false);
            });

            modelBuilder.Entity<M_User_Role>(entity =>
            {
                entity.Property(e => e.Role).ValueGeneratedNever();

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Q_F_事業所名>(entity =>
            {
                entity.ToView("Q_F_事業所名");
            });

            modelBuilder.Entity<T_ALC_CHECK_IMPORT>(entity =>
            {
                entity.Property(e => e.CODE)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<T_BATCH_RESULT>(entity =>
            {
                entity.Property(e => e.EXIT_TIME).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Driver_Salary>(entity =>
            {
                entity.HasKey(e => new { e.WORKER_CD, e.Month });

                entity.Property(e => e.定額残業).HasDefaultValueSql("((0))");

                entity.Property(e => e.定額深夜).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_KINTAI_COMMIT>(entity =>
            {
                entity.HasKey(e => new { e.乗務員CD, e.勤怠日 })
                    .HasName("PK_T_KINTAI_COMMIT_1");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();

                entity.Property(e => e.更新区分).HasDefaultValueSql("((0))");

                entity.Property(e => e.更新日).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.確定区分).HasComment("1:締め,2:仮");
            });

            modelBuilder.Entity<T_KINTAI_COMMIT_CREW>(entity =>
            {
                entity.HasKey(e => new { e.乗務員CD, e.勤怠日 })
                    .HasName("PK_T_KINTAI_COMMIT_CREW_1");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<T_KINTAI_COMMIT_NON_CREW>(entity =>
            {
                entity.HasKey(e => new { e.乗務員CD, e.勤怠日 })
                    .HasName("PK_T_KINTAI_COMMIT_NON_CREW_1");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<T_KUDGIVT>(entity =>
            {
                entity.ToView("T_KUDGIVT");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<T_KUDGIVT_DEL>(entity =>
            {
                entity.ToView("T_KUDGIVT_DEL");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<T_KUDGIVT_TARGET>(entity =>
            {
                entity.ToView("T_KUDGIVT_TARGET");
            });

            modelBuilder.Entity<T_KUDGSIR>(entity =>
            {
                entity.ToView("T_KUDGSIR");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<T_KUDGURI>(entity =>
            {
                entity.ToView("T_KUDGURI");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<T_Kakutei>(entity =>
            {
                entity.Property(e => e.Kakutei_flg).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_LOCK>(entity =>
            {
                entity.Property(e => e.BATCH_LOCK).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Leave>(entity =>
            {
                entity.Property(e => e.Days).HasDefaultValueSql("((0.00))");
            });

            modelBuilder.Entity<T_Leave_Auto_Grant_Rireki>(entity =>
            {
                entity.Property(e => e.ADD_DAYS).HasDefaultValueSql("((0))");

                entity.Property(e => e.ADD_TARGET_MONTH).HasDefaultValueSql("((0))");

                entity.Property(e => e.ATTENDANCE_DAYS).HasDefaultValueSql("((0))");

                entity.Property(e => e.ATTENDANCE_RATE).HasDefaultValueSql("((0))");

                entity.Property(e => e.LEAVE_DAYS).HasDefaultValueSql("((0))");

                entity.Property(e => e.LEAVE_RATE).HasDefaultValueSql("((0))");

                entity.Property(e => e.MON).HasDefaultValueSql("((0))");

                entity.Property(e => e.NEN).HasDefaultValueSql("((0))");

                entity.Property(e => e.NON_ATTENDANCE_DAYS).HasDefaultValueSql("((0))");

                entity.Property(e => e.NON_ATTENDANCE_RATE).HasDefaultValueSql("((0))");

                entity.Property(e => e.NON_LEAVE_DAYS).HasDefaultValueSql("((0))");

                entity.Property(e => e.TOTALWORK_DAYS).HasDefaultValueSql("((0))");

                entity.Property(e => e.TSUKI).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Leave_Summary>(entity =>
            {
                entity.Property(e => e.乗務員CD).ValueGeneratedNever();
            });

            modelBuilder.Entity<T_Leave_Summary_Detail>(entity =>
            {
                entity.HasKey(e => new { e.乗務員CD, e.LEAVE_CD });
            });

            modelBuilder.Entity<T_Leave_Summary_Detail2>(entity =>
            {
                entity.HasKey(e => new { e.乗務員CD, e.LEAVE_CD, e.Leave_Give_ID })
                    .HasName("PK_T_Leave_Summary_Detail2_1");
            });

            modelBuilder.Entity<V_BATCH_RESULT_FOR_PORTAL>(entity =>
            {
                entity.ToView("V_BATCH_RESULT_FOR_PORTAL");
            });

            modelBuilder.Entity<V_DRIVER>(entity =>
            {
                entity.ToView("V_DRIVER");
            });

            modelBuilder.Entity<V_DigiTachoLinkAlert>(entity =>
            {
                entity.ToView("V_DigiTachoLinkAlert");
            });

            modelBuilder.Entity<V_Driver2>(entity =>
            {
                entity.ToView("V_Driver2");
            });

            modelBuilder.Entity<V_Haisya>(entity =>
            {
                entity.ToView("V_Haisya");
            });

            modelBuilder.Entity<V_Leave_Auto_Grant_Rireki>(entity =>
            {
                entity.ToView("V_Leave_Auto_Grant_Rireki");
            });

            modelBuilder.Entity<V_OFFICE>(entity =>
            {
                entity.ToView("V_OFFICE");
            });

            modelBuilder.Entity<V_勤怠1時間重複エラー>(entity =>
            {
                entity.ToView("V_勤怠1時間重複エラー");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<V_差分チェック>(entity =>
            {
                entity.ToView("V_差分チェック");
            });

            modelBuilder.Entity<V_差分チェック2>(entity =>
            {
                entity.ToView("V_差分チェック2");
            });

            modelBuilder.Entity<V_月次勤怠一覧>(entity =>
            {
                entity.ToView("V_月次勤怠一覧");
            });

            modelBuilder.Entity<V_燃料無駄候補リスト>(entity =>
            {
                entity.ToView("V_燃料無駄候補リスト");
            });

            modelBuilder.Entity<V_計算エラー>(entity =>
            {
                entity.ToView("V_計算エラー");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<V_重複デジタコデータ>(entity =>
            {
                entity.ToView("V_重複デジタコデータ");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
