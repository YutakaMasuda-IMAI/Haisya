using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携データベースコンテキスト
    /// </summary>
    public partial class RenkeiContext : DbContext
    {
        public RenkeiContext()
        {
        }

        public RenkeiContext(DbContextOptions<RenkeiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<M_Area> M_Areas { get; set; }
        public virtual DbSet<M_Area_Ken> M_Area_Kens { get; set; }
        public virtual DbSet<M_Code> M_Codes { get; set; }
        public virtual DbSet<M_Code_Datum> M_Code_Data { get; set; }
        public virtual DbSet<M_Company> M_Companies { get; set; }
        public virtual DbSet<M_CompanyBranch> M_CompanyBranches { get; set; }
        public virtual DbSet<M_CompanyUser> M_CompanyUsers { get; set; }
        public virtual DbSet<M_CompanyUser_Group> M_CompanyUser_Groups { get; set; }
        public virtual DbSet<M_CompanyUser_GroupUser> M_CompanyUser_GroupUsers { get; set; }
        public virtual DbSet<M_Equipment> M_Equipments { get; set; }
        public virtual DbSet<M_Equipment_Group> M_Equipment_Groups { get; set; }
        public virtual DbSet<M_Katum> M_Kata { get; set; }
        public virtual DbSet<M_LoginUser> M_LoginUsers { get; set; }
        public virtual DbSet<M_LoginUser_Role> M_LoginUser_Roles { get; set; }
        public virtual DbSet<M_Luggage> M_Luggages { get; set; }
        public virtual DbSet<M_Luggage_Group> M_Luggage_Groups { get; set; }
        public virtual DbSet<M_PostCode> M_PostCodes { get; set; }
        public virtual DbSet<M_Role> M_Roles { get; set; }
        public virtual DbSet<M_Syaryo> M_Syaryos { get; set; }
        public virtual DbSet<T_BATCH_RESULT> T_BATCH_RESULTs { get; set; }
        public virtual DbSet<T_Portal_Info> T_Portal_Infos { get; set; }
        public virtual DbSet<T_Renkei_Anken> T_Renkei_Ankens { get; set; }
        public virtual DbSet<T_Renkei_Anken_Check> T_Renkei_Anken_Checks { get; set; }
        public virtual DbSet<T_Renkei_Anken_Detail> T_Renkei_Anken_Details { get; set; }
        public virtual DbSet<T_Renkei_Anken_Equipment> T_Renkei_Anken_Equipments { get; set; }
        public virtual DbSet<T_Renkei_Anken_Excharge> T_Renkei_Anken_Excharges { get; set; }
        public virtual DbSet<T_Renkei_Anken_Luggage> T_Renkei_Anken_Luggages { get; set; }
        public virtual DbSet<T_Renkei_Anken_No> T_Renkei_Anken_Nos { get; set; }
        public virtual DbSet<T_Renkei_Anken_Point> T_Renkei_Anken_Points { get; set; }
        public virtual DbSet<T_Renkei_Anken_Secure> T_Renkei_Anken_Secures { get; set; }
        public virtual DbSet<T_Renkei_Anken_Secure_Check> T_Renkei_Anken_Secure_Checks { get; set; }
        public virtual DbSet<T_Renkei_Anken_Secure_Print> T_Renkei_Anken_Secure_Prints { get; set; }
        public virtual DbSet<T_Share_Luggage> T_Share_Luggages { get; set; }
        public virtual DbSet<T_Share_Luggage_Detail> T_Share_Luggage_Details { get; set; }
        public virtual DbSet<T_Share_Luggage_Notify_Setting> T_Share_Luggage_Notify_Settings { get; set; }
        public virtual DbSet<T_Share_Luggage_Secure> T_Share_Luggage_Secures { get; set; }
        public virtual DbSet<T_Share_No> T_Share_Nos { get; set; }
        public virtual DbSet<T_Share_Syaryo> T_Share_Syaryos { get; set; }
        public virtual DbSet<T_Share_Syaryo_Detail> T_Share_Syaryo_Details { get; set; }
        public virtual DbSet<T_Share_Syaryo_Notify_Setting> T_Share_Syaryo_Notify_Settings { get; set; }
        public virtual DbSet<T_Share_Syaryo_Secure> T_Share_Syaryo_Secures { get; set; }

        /// <summary>
        /// データベースコンテキストの構成を行います。
        /// </summary>
        /// <param name="optionsBuilder">オプションビルダー</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.*/
                optionsBuilder.UseSqlServer("Server=.\\CHILDDB16;Initial Catalog=Renkei;Persist Security Info=False;User ID=sa;Password=_ChildDB2345;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            }
        }

        /// <summary>
        /// モデルの構成を行います。
        /// </summary>
        /// <param name="modelBuilder">モデルビルダー</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<M_Area>(entity =>
            {
                entity.HasKey(e => new { e.Area_ID, e.Company_ID });
            });

            modelBuilder.Entity<M_Code>(entity =>
            {
                entity.Property(e => e.Code_ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<M_Code_Datum>(entity =>
            {
                entity.HasKey(e => new { e.Code_ID, e.Code_Data });
            });

            modelBuilder.Entity<M_Company>(entity =>
            {
                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.Owner_Flg).HasComment("0:業者、1:荷主");

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");
            });

            modelBuilder.Entity<M_CompanyBranch>(entity =>
            {
                entity.HasKey(e => new { e.Branch_ID, e.Company_ID })
                    .HasName("PK_M_ComopanyBranch");

                entity.Property(e => e.Branch_ID).ValueGeneratedOnAdd();

                entity.Property(e => e.Branch_Code).IsUnicode(false);

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_CompanyUser>(entity =>
            {
                entity.HasKey(e => e.User_ID)
                    .HasName("PK_M_CompanyUser_1");

                entity.Property(e => e.Haisya_Send_Kubun).HasComment("1:案件単位で通知、0:全案件で通知");

                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");
            });

            modelBuilder.Entity<M_CompanyUser_Group>(entity =>
            {
                entity.Property(e => e.Group_Kubun).HasDefaultValueSql("((1))");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_CompanyUser_GroupUser>(entity =>
            {
                entity.HasKey(e => new { e.Group_ID, e.User_ID })
                    .HasName("PK_M_CompanyUser_Group_User");
            });

            modelBuilder.Entity<M_Equipment>(entity =>
            {
                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_Equipment_Group>(entity =>
            {
                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_LoginUser>(entity =>
            {
                entity.Property(e => e.LoginID).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_LoginUser_Role>(entity =>
            {
                entity.HasKey(e => new { e.Company_ID, e.Role });

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_Luggage>(entity =>
            {
                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_Luggage_Group>(entity =>
            {
                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_PostCode>(entity =>
            {
                entity.Property(e => e.POSTAL_CODE).IsUnicode(false);

                entity.Property(e => e.POSTAL_CODE_OLD).IsUnicode(false);

                entity.Property(e => e.PUBLIC_SECTOR_CODE).IsUnicode(false);
            });

            modelBuilder.Entity<M_Role>(entity =>
            {
                entity.HasKey(e => new { e.Company_ID, e.Role, e.Controller, e.Action, e.Method })
                    .HasName("PK_M_Role_1");

                entity.Property(e => e.Method).HasDefaultValueSql("(N'ALL')");
            });

            modelBuilder.Entity<M_Syaryo>(entity =>
            {
                entity.Property(e => e.AVG_FUEL_COSTS).HasDefaultValueSql("((0))");

                entity.Property(e => e.CAR_GROSS_WEIGHT).HasDefaultValueSql("((0))");

                entity.Property(e => e.CAR_WEIGHT).HasDefaultValueSql("((0))");

                entity.Property(e => e.HEIGHT).HasDefaultValueSql("((0))");

                entity.Property(e => e.LONG).HasDefaultValueSql("((0))");

                entity.Property(e => e.MAX_LOAD_CAPA).HasDefaultValueSql("((0))");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WIDTH).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_BATCH_RESULT>(entity =>
            {
                entity.Property(e => e.EXIT_TIME).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Portal_Info>(entity =>
            {
                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.Portal_Kubun).HasComment("1：配車WEB、2：請求WEB、3：連携WEB");

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");
            });

            modelBuilder.Entity<T_Renkei_Anken>(entity =>
            {
                entity.Property(e => e.Renkei_Anken_Kubun).HasComment("0:依頼案件,1:受注案件");

                entity.Property(e => e.Renkei_Anken_No).IsUnicode(false);

                entity.Property(e => e.Renkei_Anken_Status).HasComment("0:確定,1:暫定,3:取消,7:変更依頼");
            });

            modelBuilder.Entity<T_Renkei_Anken_Check>(entity =>
            {
                entity.Property(e => e.Renkei_Anken_ID).ValueGeneratedNever();

                entity.Property(e => e.Check_Status).HasComment("0:未確認,1:確認済");
            });

            modelBuilder.Entity<T_Renkei_Anken_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Renkei_Anken_ID, e.Renkei_Anken_Order });

                entity.Property(e => e.OroshiTaskTime).IsUnicode(false);

                entity.Property(e => e.Root_Ferry).IsUnicode(false);

                entity.Property(e => e.Root_Regulation).IsUnicode(false);

                entity.Property(e => e.Root_Twouturn).IsUnicode(false);

                entity.Property(e => e.Route_BreakTime).HasDefaultValueSql("((0))");

                entity.Property(e => e.Route_RestTime).HasDefaultValueSql("((0))");

                entity.Property(e => e.Route_TotalDays).HasDefaultValueSql("((0))");

                entity.Property(e => e.TsumiTaskTime).IsUnicode(false);
            });

            modelBuilder.Entity<T_Renkei_Anken_Equipment>(entity =>
            {
                entity.HasKey(e => new { e.Renkei_Anken_ID, e.Renkei_Anken_Order, e.Equipment_ID });

                entity.Property(e => e.Equipment_Count).HasDefaultValueSql("((0))");

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Renkei_Anken_Excharge>(entity =>
            {
                entity.HasKey(e => new { e.Renkei_Anken_ID, e.Renkei_Anken_Order, e.Komoku_ID });
            });

            modelBuilder.Entity<T_Renkei_Anken_Luggage>(entity =>
            {
                entity.HasKey(e => new { e.Renkei_Anken_ID, e.Renkei_Anken_Order, e.Luggage_ID });

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Luggage_Count).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Renkei_Anken_No>(entity =>
            {
                entity.Property(e => e.NENDO).ValueGeneratedNever();
            });

            modelBuilder.Entity<T_Renkei_Anken_Point>(entity =>
            {
                entity.HasKey(e => new { e.Renkei_Anken_ID, e.Renkei_Anken_Order, e.Kubun, e.Point_Order });

                entity.Property(e => e.FlgGenchiKakunin).HasDefaultValueSql("((0))");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Lat).IsUnicode(false);

                entity.Property(e => e.Lng).IsUnicode(false);

                entity.Property(e => e.PointTime).IsUnicode(false);

                entity.Property(e => e.SEKubun).IsUnicode(false);

                entity.Property(e => e.TollDisplayHeight).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Renkei_Anken_Secure>(entity =>
            {
                entity.Property(e => e.Del_Kubun).HasComment("1:乗務員変更、");

                entity.Property(e => e.Driver_ID).HasComment("HaisyaDBのID");

                entity.Property(e => e.Renkei_Anken_Secure_Status).HasComment("0:確定配車,1:暫定,3:取消,7:変更依頼");
            });

            modelBuilder.Entity<T_Renkei_Anken_Secure_Check>(entity =>
            {
                entity.Property(e => e.Renkei_Anken_Secure_ID).ValueGeneratedNever();

                entity.Property(e => e.Check_Status).HasComment("0:未確認,1:確認済");
            });

            modelBuilder.Entity<T_Renkei_Anken_Secure_Print>(entity =>
            {
                entity.Property(e => e.Renkei_Anken_Secure_ID).ValueGeneratedNever();

                entity.Property(e => e.Print_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Print_Kubun).HasComment("1:車番連絡");
            });

            modelBuilder.Entity<T_Share_Luggage>(entity =>
            {
                entity.Property(e => e.Share_Luggage_No).IsUnicode(false);

                entity.Property(e => e.Share_Luggage_Status).HasComment("0:公開中,1:確保,2:取消");
            });

            modelBuilder.Entity<T_Share_Luggage_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Share_Luggage_ID, e.Share_Luggage_Order });

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Kokyaku_Public_Flg).HasComment("0:公開、1:非公開");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Share_Luggage_Notify_Setting>(entity =>
            {
                entity.HasKey(e => e.Share_Luggage_Notify_Setting_ID)
                    .HasName("PK_T_Share_Luggage_Notify_Setting_1");
            });

            modelBuilder.Entity<T_Share_Luggage_Secure>(entity =>
            {
                entity.HasKey(e => e.Share_Luggage_Secure_ID)
                    .HasName("PK_T_Share_Luggage_Secure_1");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Share_No>(entity =>
            {
                entity.Property(e => e.NENDO).ValueGeneratedNever();
            });

            modelBuilder.Entity<T_Share_Syaryo>(entity =>
            {
                entity.Property(e => e.Share_Syaryo_No).IsUnicode(false);

                entity.Property(e => e.Share_Syaryo_Status).HasComment("0:公開中,1:確保,2:取消");
            });

            modelBuilder.Entity<T_Share_Syaryo_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Share_Syaryo_ID, e.Share_Syaryo_Order });

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Share_Syaryo_Notify_Setting>(entity =>
            {
                entity.HasKey(e => e.Share_Syaryo_Notify_Setting_ID)
                    .HasName("PK_T_Share_Syaryo_Notify_Setting_1");
            });

            modelBuilder.Entity<T_Share_Syaryo_Secure>(entity =>
            {
                entity.HasKey(e => e.Share_Syaryo_Secure_ID)
                    .HasName("PK_T_Share_Syaryo_Secure_1");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
