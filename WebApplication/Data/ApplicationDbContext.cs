using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<B_Seikyu> B_Seikyus { get; set; }
        public virtual DbSet<B_Seikyu_Detail> B_Seikyu_Details { get; set; }
        public virtual DbSet<B_Uriage_Month> B_Uriage_Months { get; set; }
        public virtual DbSet<B_Uriage_Month_Detail> B_Uriage_Month_Details { get; set; }
        public virtual DbSet<M_Anken_Excharge> M_Anken_Excharges { get; set; }
        public virtual DbSet<M_Area> M_Areas { get; set; }
        public virtual DbSet<M_Area_Ken> M_Area_Kens { get; set; }
        public virtual DbSet<M_Burden> M_Burdens { get; set; }
        public virtual DbSet<M_Burden_Group> M_Burden_Groups { get; set; }
        public virtual DbSet<M_Code> M_Codes { get; set; }
        public virtual DbSet<M_Code_Datum> M_Code_Data { get; set; }
        public virtual DbSet<M_Company> M_Companies { get; set; }
        public virtual DbSet<M_CompanyBranch> M_CompanyBranches { get; set; }
        public virtual DbSet<M_CompanyDriver> M_CompanyDrivers { get; set; }
        public virtual DbSet<M_CompanyDriver_Syaryo> M_CompanyDriver_Syaryos { get; set; }
        public virtual DbSet<M_CompanyOrganization> M_CompanyOrganizations { get; set; }
        public virtual DbSet<M_CompanyUser> M_CompanyUsers { get; set; }
        public virtual DbSet<M_CompanyUser_Group> M_CompanyUser_Groups { get; set; }
        public virtual DbSet<M_CompanyUser_GroupUser> M_CompanyUser_GroupUsers { get; set; }
        public virtual DbSet<M_Customer> M_Customers { get; set; }
        public virtual DbSet<M_Customer_Branch> M_Customer_Branches { get; set; }
        public virtual DbSet<M_Customer_Driver> M_Customer_Drivers { get; set; }
        public virtual DbSet<M_Customer_Driver_Syaryo> M_Customer_Driver_Syaryos { get; set; }
        public virtual DbSet<M_Customer_ICSeikyuKubun> M_Customer_ICSeikyuKubuns { get; set; }
        public virtual DbSet<M_Customer_Shiharai_Calc> M_Customer_Shiharai_Calcs { get; set; }
        public virtual DbSet<M_Customer_Tantou> M_Customer_Tantous { get; set; }
        public virtual DbSet<M_Customer_TantouHaisyaGroup> M_Customer_TantouHaisyaGroups { get; set; }
        public virtual DbSet<M_Customer_TollSeikyuKubun> M_Customer_TollSeikyuKubuns { get; set; }
        public virtual DbSet<M_Customer_Uriage_Calc> M_Customer_Uriage_Calcs { get; set; }
        public virtual DbSet<M_Customer_bak20240718> M_Customer_bak20240718s { get; set; }
        public virtual DbSet<M_DefaultMoney> M_DefaultMoneys { get; set; }
        public virtual DbSet<M_DefaultMoney_WaitTimeForArea> M_DefaultMoney_WaitTimeForAreas { get; set; }
        public virtual DbSet<M_DefaultMoney_WaitTimeForCompany> M_DefaultMoney_WaitTimeForCompanies { get; set; }
        public virtual DbSet<M_Equipment> M_Equipments { get; set; }
        public virtual DbSet<M_Equipment_Group> M_Equipment_Groups { get; set; }
        public virtual DbSet<M_FuelCost> M_FuelCosts { get; set; }
        public virtual DbSet<M_Jiko_Item> M_Jiko_Items { get; set; }
        public virtual DbSet<M_Jiko_WorkFlow> M_Jiko_WorkFlows { get; set; }
        public virtual DbSet<M_Jiko_WorkFlow_Route> M_Jiko_WorkFlow_Routes { get; set; }
        public virtual DbSet<M_Katum> M_Kata { get; set; }
        public virtual DbSet<M_KojinUnsyu_Kubun> M_KojinUnsyu_Kubuns { get; set; }
        public virtual DbSet<M_KojinUnsyu_Route> M_KojinUnsyu_Routes { get; set; }
        public virtual DbSet<M_LoginUser> M_LoginUsers { get; set; }
        public virtual DbSet<M_LoginUser_Customer> M_LoginUser_Customers { get; set; }
        public virtual DbSet<M_LoginUser_Role> M_LoginUser_Roles { get; set; }
        public virtual DbSet<M_Luggage> M_Luggages { get; set; }
        public virtual DbSet<M_Luggage_Group> M_Luggage_Groups { get; set; }
        public virtual DbSet<M_PersonnelExpense> M_PersonnelExpenses { get; set; }
        public virtual DbSet<M_PostCode> M_PostCodes { get; set; }
        public virtual DbSet<M_PostCode_TEMP> M_PostCode_TEMPs { get; set; }
        public virtual DbSet<M_PublishGroup> M_PublishGroups { get; set; }
        public virtual DbSet<M_PublishGroup_Detail> M_PublishGroup_Details { get; set; }
        public virtual DbSet<M_Report_Detail_Param> M_Report_Detail_Params { get; set; }
        public virtual DbSet<M_Report_Output_Item> M_Report_Output_Items { get; set; }
        public virtual DbSet<M_Report_Output_Item_Master> M_Report_Output_Item_Masters { get; set; }
        public virtual DbSet<M_Report_Serch> M_Report_Serches { get; set; }
        public virtual DbSet<M_Report_Serch_Item> M_Report_Serch_Items { get; set; }
        public virtual DbSet<M_Report_Serch_Kubun> M_Report_Serch_Kubuns { get; set; }
        public virtual DbSet<M_Role> M_Roles { get; set; }
        public virtual DbSet<M_Senzoku> M_Senzokus { get; set; }
        public virtual DbSet<M_Senzoku_Driver> M_Senzoku_Drivers { get; set; }
        public virtual DbSet<M_Syaryo> M_Syaryos { get; set; }
        public virtual DbSet<M_SyaryoCost> M_SyaryoCosts { get; set; }
        public virtual DbSet<M_SyaryoManagement> M_SyaryoManagements { get; set; }
        public virtual DbSet<M_SyaryoSize> M_SyaryoSizes { get; set; }
        public virtual DbSet<M_SyasyuKubun> M_SyasyuKubuns { get; set; }
        public virtual DbSet<M_Unit> M_Units { get; set; }
        public virtual DbSet<M_Vender> M_Venders { get; set; }
        public virtual DbSet<M_Yosya> M_Yosyas { get; set; }
        public virtual DbSet<M_Yosya_Branch> M_Yosya_Branches { get; set; }
        public virtual DbSet<M_Yosya_Driver> M_Yosya_Drivers { get; set; }
        public virtual DbSet<M_Yosya_Driver_Syaryo> M_Yosya_Driver_Syaryos { get; set; }
        public virtual DbSet<M_Yosya_Shiharai_Calc> M_Yosya_Shiharai_Calcs { get; set; }
        public virtual DbSet<M_Yosya_Tantou> M_Yosya_Tantous { get; set; }
        public virtual DbSet<TEMP_CUSTOMER> TEMP_CUSTOMERs { get; set; }
        public virtual DbSet<TEMP_CUSTOMER2> TEMP_CUSTOMER2s { get; set; }
        public virtual DbSet<TEMP_Driver> TEMP_Drivers { get; set; }
        public virtual DbSet<T_Admin_Info> T_Admin_Infos { get; set; }
        public virtual DbSet<T_Anken> T_Ankens { get; set; }
        public virtual DbSet<T_Anken_Detail> T_Anken_Details { get; set; }
        public virtual DbSet<T_Anken_Display> T_Anken_Displays { get; set; }
        public virtual DbSet<T_Anken_Equipment> T_Anken_Equipments { get; set; }
        public virtual DbSet<T_Anken_Excharge> T_Anken_Excharges { get; set; }
        public virtual DbSet<T_Anken_Luggage> T_Anken_Luggages { get; set; }
        public virtual DbSet<T_Anken_No> T_Anken_Nos { get; set; }
        public virtual DbSet<T_Anken_OyaKokyaku> T_Anken_OyaKokyakus { get; set; }
        public virtual DbSet<T_Anken_Point> T_Anken_Points { get; set; }
        public virtual DbSet<T_Anken_Publish> T_Anken_Publishes { get; set; }
        public virtual DbSet<T_Anken_Remark> T_Anken_Remarks { get; set; }
        public virtual DbSet<T_Anken_Riyounso> T_Anken_Riyounsos { get; set; }
        public virtual DbSet<T_Anken_Riyounso_Point> T_Anken_Riyounso_Points { get; set; }
        public virtual DbSet<T_Anken_SyabanRenraku> T_Anken_SyabanRenrakus { get; set; }
        public virtual DbSet<T_BATCH_RESULT> T_BATCH_RESULTs { get; set; }
        public virtual DbSet<T_Check_Seikyu> T_Check_Seikyus { get; set; }
        public virtual DbSet<T_Check_Seikyu_Change> T_Check_Seikyu_Changes { get; set; }
        public virtual DbSet<T_Check_Seikyu_Detail> T_Check_Seikyu_Details { get; set; }
        public virtual DbSet<T_Check_Seikyu_Done> T_Check_Seikyu_Dones { get; set; }
        public virtual DbSet<T_Check_Shitabarai> T_Check_Shitabarais { get; set; }
        public virtual DbSet<T_Check_Shitabarai_Change> T_Check_Shitabarai_Changes { get; set; }
        public virtual DbSet<T_Check_Shitabarai_Detail> T_Check_Shitabarai_Details { get; set; }
        public virtual DbSet<T_Check_Shitabarai_Done> T_Check_Shitabarai_Dones { get; set; }
        public virtual DbSet<T_Commit_Kaikei> T_Commit_Kaikeis { get; set; }
        public virtual DbSet<T_Commit_Seikyu> T_Commit_Seikyus { get; set; }
        public virtual DbSet<T_Commit_Shitabarai> T_Commit_Shitabarais { get; set; }
        public virtual DbSet<T_Commit_Unsyu> T_Commit_Unsyus { get; set; }
        public virtual DbSet<T_Expense> T_Expenses { get; set; }
        public virtual DbSet<T_Expense_Item> T_Expense_Items { get; set; }
        public virtual DbSet<T_Expense_Payment> T_Expense_Payments { get; set; }
        public virtual DbSet<T_Haisya> T_Haisyas { get; set; }
        public virtual DbSet<T_Haisya_Around> T_Haisya_Arounds { get; set; }
        public virtual DbSet<T_Haisya_Batch> T_Haisya_Batches { get; set; }
        public virtual DbSet<T_Haisya_Detail> T_Haisya_Details { get; set; }
        public virtual DbSet<T_Haisya_Driver_Day_Remark> T_Haisya_Driver_Day_Remarks { get; set; }
        public virtual DbSet<T_Haisya_SyabanRenraku> T_Haisya_SyabanRenrakus { get; set; }
        public virtual DbSet<T_Haisya_SyabanRenraku_Detail> T_Haisya_SyabanRenraku_Details { get; set; }
        public virtual DbSet<T_Haisya_SyabanRenraku_Remark> T_Haisya_SyabanRenraku_Remarks { get; set; }
        public virtual DbSet<T_Haisya_Yosya> T_Haisya_Yosyas { get; set; }
        public virtual DbSet<T_Jiko> T_Jikos { get; set; }
        public virtual DbSet<T_Jiko_Detail> T_Jiko_Details { get; set; }
        public virtual DbSet<T_Jiko_Item> T_Jiko_Items { get; set; }
        public virtual DbSet<T_Jiko_No> T_Jiko_Nos { get; set; }
        public virtual DbSet<T_Jiko_Type> T_Jiko_Types { get; set; }
        public virtual DbSet<T_Jiko_WorkFlow_Route> T_Jiko_WorkFlow_Routes { get; set; }
        public virtual DbSet<T_Jiko_WorkFlow_Status> T_Jiko_WorkFlow_Statuses { get; set; }
        public virtual DbSet<T_Kintai> T_Kintais { get; set; }
        public virtual DbSet<T_Nippou> T_Nippous { get; set; }
        public virtual DbSet<T_Nippou_Approval> T_Nippou_Approvals { get; set; }
        public virtual DbSet<T_Nippou_Kaiso> T_Nippou_Kaisos { get; set; }
        public virtual DbSet<T_Nippou_Kaiso_Degitako> T_Nippou_Kaiso_Degitakos { get; set; }
        public virtual DbSet<T_Nippou_Stay> T_Nippou_Stays { get; set; }
        public virtual DbSet<T_Nippou_Stay_Degitako> T_Nippou_Stay_Degitakos { get; set; }
        public virtual DbSet<T_Nippou_Toll> T_Nippou_Tolls { get; set; }
        public virtual DbSet<T_Nippou_Toll_Other> T_Nippou_Toll_Others { get; set; }
        public virtual DbSet<T_Nyukin> T_Nyukins { get; set; }
        public virtual DbSet<T_Point> T_Points { get; set; }
        public virtual DbSet<T_Portal_Info> T_Portal_Infos { get; set; }
        public virtual DbSet<T_Print_Download> T_Print_Downloads { get; set; }
        public virtual DbSet<T_Print_Parameter> T_Print_Parameters { get; set; }
        public virtual DbSet<T_Print_Rireki> T_Print_Rirekis { get; set; }
        public virtual DbSet<T_Print_Seikyu> T_Print_Seikyus { get; set; }
        public virtual DbSet<T_Print_Seikyu_Detail> T_Print_Seikyu_Details { get; set; }
        public virtual DbSet<T_Print_Shitabarai> T_Print_Shitabarais { get; set; }
        public virtual DbSet<T_Print_Shitabarai_Detail> T_Print_Shitabarai_Details { get; set; }
        public virtual DbSet<T_Report_Layout> T_Report_Layouts { get; set; }
        public virtual DbSet<T_Seikyu> T_Seikyus { get; set; }
        public virtual DbSet<T_Seikyu_Detail> T_Seikyu_Details { get; set; }
        public virtual DbSet<T_Shitabarai> T_Shitabarais { get; set; }
        public virtual DbSet<T_Shitabarai_Detail> T_Shitabarai_Details { get; set; }
        public virtual DbSet<T_Uriage> T_Uriages { get; set; }
        public virtual DbSet<T_Uriage_Futan> T_Uriage_Futans { get; set; }
        public virtual DbSet<T_Uriage_Shitabarai> T_Uriage_Shitabarais { get; set; }
        public virtual DbSet<T_Uriage_Unchin> T_Uriage_Unchins { get; set; }
        public virtual DbSet<T_Uriage_Unsyu> T_Uriage_Unsyus { get; set; }
        public virtual DbSet<T_YosyaShiharai> T_YosyaShiharais { get; set; }
        public virtual DbSet<V_Anken_Detail> V_Anken_Details { get; set; }
        public virtual DbSet<V_CompanyDriver> V_CompanyDrivers { get; set; }
        public virtual DbSet<V_Customer_Syaryo> V_Customer_Syaryos { get; set; }
        public virtual DbSet<V_HaisyaDataList_old> V_HaisyaDataList_olds { get; set; }
        public virtual DbSet<V_LoginUser> V_LoginUsers { get; set; }
        public virtual DbSet<V_Luggage> V_Luggages { get; set; }
        public virtual DbSet<V_Print_Seikyu> V_Print_Seikyus { get; set; }
        public virtual DbSet<V_Senzoku> V_Senzokus { get; set; }
        public virtual DbSet<V_Senzoku_Driver> V_Senzoku_Drivers { get; set; }
        public virtual DbSet<V_Tokuisaki> V_Tokuisakis { get; set; }
        public virtual DbSet<V_TokuisakiForNotConnect> V_TokuisakiForNotConnects { get; set; }
        public virtual DbSet<V_TokuisakiForNotConnect_old> V_TokuisakiForNotConnect_olds { get; set; }
        public virtual DbSet<V_Tokuisaki_old> V_Tokuisaki_olds { get; set; }
        public virtual DbSet<V_Yosyasaki> V_Yosyasakis { get; set; }
        public virtual DbSet<V_YosyasakiForNotConnect> V_YosyasakiForNotConnects { get; set; }
        public virtual DbSet<___M_Tokuisaki_SeikyuTantou> ___M_Tokuisaki_SeikyuTantous { get; set; }
        public virtual DbSet<担当者一段階目> 担当者一段階目s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=IMAI-SRV-DB\\SQLEXPRESS;Database=Haisya;user id=sa;password=Imaiunso00;Connection Timeout=300;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<B_Seikyu>(entity =>
            {
                entity.HasKey(e => e.Bak_Seikyu_ID)
                    .HasName("PK_B_Seikyu_1");

                entity.Property(e => e.Print_Pattern).HasComment("M_Code:11");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<B_Seikyu_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Bak_Seikyu_ID, e.Data_Kubun, e.Data_Sort })
                    .HasName("PK_B_Seikyu_Detail_1");

                entity.Property(e => e.Data_Kubun).HasComment("1：ヘッダー、2：入金、３：案件明細");
            });

            modelBuilder.Entity<B_Uriage_Month>(entity =>
            {
                entity.HasKey(e => e.Bak_Uriage_Month_ID)
                    .HasName("PK_B_Uriage_Month_1");

                entity.Property(e => e.Print_Pattern).HasComment("M_Code:11");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<B_Uriage_Month_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Bak_Uriage_Month_ID, e.Data_Kubun, e.Data_Sort })
                    .HasName("PK_B_Uriage_Month_Detail_1");

                entity.Property(e => e.Data_Kubun).HasComment("1：ヘッダー、2：入金、３：案件明細");
            });

            modelBuilder.Entity<M_Anken_Excharge>(entity =>
            {
                entity.HasKey(e => e.Komoku_ID)
                    .HasName("PK_M_Anken_Excharge_1");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_Area>(entity =>
            {
                entity.HasKey(e => new { e.Area_ID, e.Company_ID });
            });

            modelBuilder.Entity<M_Burden>(entity =>
            {
                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_Burden_Group>(entity =>
            {
                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_Code>(entity =>
            {
                entity.Property(e => e.Code_ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<M_Code_Datum>(entity =>
            {
                entity.HasKey(e => new { e.Code_ID, e.Code_Data });
            });

            modelBuilder.Entity<M_CompanyBranch>(entity =>
            {
                entity.HasKey(e => new { e.Branch_ID, e.Company_ID })
                    .HasName("PK_M_ComopanyBranch");

                entity.Property(e => e.Branch_ID).ValueGeneratedOnAdd();

                entity.Property(e => e.Branch_Code).IsUnicode(false);

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_CompanyDriver>(entity =>
            {
                entity.HasKey(e => e.Driver_ID)
                    .HasName("PK_M_CompanyDriver_1");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_CompanyDriver_Syaryo>(entity =>
            {
                entity.HasKey(e => e.DriverSyaryo_ID)
                    .HasName("PK_[M_CompanyDriver_Syaryo");

                entity.Property(e => e.Start_Date).HasDefaultValueSql("('1900/01/01')");

                entity.Property(e => e.SyaryoManagement_ID1).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_CompanyOrganization>(entity =>
            {
                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_CompanyUser>(entity =>
            {
                entity.HasKey(e => e.User_ID)
                    .HasName("PK_M_CompanyUser_1");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
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

            modelBuilder.Entity<M_Customer>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Yosya_Flg).HasComment("0:荷主のみ、1:傭車先でもある");
            });

            modelBuilder.Entity<M_Customer_Branch>(entity =>
            {
                entity.HasKey(e => e.Customer_Branch_ID)
                    .HasName("PK_M_Customer_Branch_1");

                entity.Property(e => e.Collection_Day).HasComment("回収日");

                entity.Property(e => e.Collection_Sight).HasComment("回収サイト");

                entity.Property(e => e.Customer_Branch_Code).IsUnicode(false);

                entity.Property(e => e.Customer_Branch_Code_Trac_Tokuisaki).HasDefaultValueSql("((0))");

                entity.Property(e => e.Customer_Branch_Code_Trac_Yosyasaki).HasDefaultValueSql("((0))");

                entity.Property(e => e.Customer_Branch_Number)
                    .IsUnicode(false)
                    .HasComment("法人番号");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Receipt_Output_Flg)
                    .HasDefaultValueSql("((1))")
                    .HasComment("0:印刷する、1:印刷しない");

                entity.Property(e => e.SeikyuDate_Kubun).HasComment("0：配車日（積日）、1：卸日");

                entity.Property(e => e.Seikyu_Kubun).HasComment("0：案件単位、1：卸し単位");

                entity.Property(e => e.Shiharai_Day).HasComment("回収日");

                entity.Property(e => e.Shiharai_Sight).HasComment("回収サイト");

                entity.Property(e => e.Tax_Fraction_Kubun).HasComment("０：切り捨て、１：四捨五入、２：切り上げ");

                entity.Property(e => e.Tax_Fraction_Position).HasComment("消費税");

                entity.Property(e => e.Toll_Kubun).HasComment("1：荷主負担、2：会社負担、3：自己負担、4：手動設定");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_Customer_Driver>(entity =>
            {
                entity.HasKey(e => e.Customer_Driver_ID)
                    .HasName("PK_M_Customer_Driver_1");

                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");

                entity.Property(e => e.Yosya_Kubun).HasComment("0:傭車、１:専属庸車");
            });

            modelBuilder.Entity<M_Customer_Driver_Syaryo>(entity =>
            {
                entity.HasKey(e => e.Customer_DriverSyaryo_ID)
                    .HasName("PK_[M_Customer_Driver_Syaryo");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Start_Date).HasDefaultValueSql("('1900/01/01')");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_Customer_ICSeikyuKubun>(entity =>
            {
                entity.HasKey(e => new { e.Customer_Branch_ID, e.Sort });

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SeikyuKubun).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_Customer_Shiharai_Calc>(entity =>
            {
                entity.Property(e => e.Customer_Branch_ID).ValueGeneratedNever();

                entity.Property(e => e.ShiharaiTotal_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi1_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi1_Visible).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi2_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi2_Visible).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi3_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi3_Visible).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi4_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi4_Visible).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi5_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi5_Visible).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_Customer_Tantou>(entity =>
            {
                entity.HasKey(e => e.Tantou_ID)
                    .HasName("PK_M_Customer_Tantou_1");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Report_Output_Name_Flg).HasComment("0:印刷する、1:印刷しない");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_Customer_TantouHaisyaGroup>(entity =>
            {
                entity.HasKey(e => new { e.Customer_ID, e.Group_ID })
                    .HasName("PK_M_Customer_Tantou");
            });

            modelBuilder.Entity<M_Customer_TollSeikyuKubun>(entity =>
            {
                entity.HasKey(e => new { e.Customer_Branch_ID, e.Seikyu_Type, e.Sort });

                entity.Property(e => e.Seikyu_Type).HasComment("1:距離、2:住所、3:IC");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.SeikyuKubun).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_Customer_Uriage_Calc>(entity =>
            {
                entity.HasKey(e => e.Customer_Branch_ID)
                    .HasName("PK_M_Customer_Seikyu");

                entity.Property(e => e.Customer_Branch_ID).ValueGeneratedNever();

                entity.Property(e => e.SeikyuTotal_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi1_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi1_Visible).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi2_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi2_Visible).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi3_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi3_Visible).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi4_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi4_Visible).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi5_Calc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi5_Visible).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_Customer_bak20240718>(entity =>
            {
                entity.Property(e => e.Address1).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Address2).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Address3).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.AnkenRemarks).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Customer_Code).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Customer_Code_Oya).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Customer_ID).ValueGeneratedOnAdd();

                entity.Property(e => e.Customer_Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Customer_Name_Abbr).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Customer_Name_Kana).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Fax1).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Fax2).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Mail_Address1).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Mail_Address2).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Mail_Title).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Phone1).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Phone2).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.PostCode).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Seikuy_PostCode).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SeikyuRemarks).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Seikyu_Address).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Shiharai_Remarks).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<M_DefaultMoney>(entity =>
            {
                entity.HasKey(e => new { e.Area, e.SyasyuSize, e.From_Distance })
                    .HasName("PKM_DefaultMoney");

                entity.Property(e => e.AdditionAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Amount).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_DefaultMoney_WaitTimeForArea>(entity =>
            {
                entity.HasKey(e => new { e.SyasyuSize, e.Area })
                    .HasName("PK_M_DefaultMoney_WaitTimeForEria");
            });

            modelBuilder.Entity<M_DefaultMoney_WaitTimeForCompany>(entity =>
            {
                entity.HasKey(e => new { e.Company_ID, e.Area, e.SyasyuSize })
                    .HasName("PK_M_DefaultMoney_WaitTimeForCompany_1");
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

            modelBuilder.Entity<M_FuelCost>(entity =>
            {
                entity.HasKey(e => new { e.Company_ID, e.Branch_ID, e.FromDate })
                    .HasName("PK_M_FuelCost_1");
            });

            modelBuilder.Entity<M_Jiko_Item>(entity =>
            {
                entity.Property(e => e.Jiko_Items_ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<M_Jiko_WorkFlow_Route>(entity =>
            {
                entity.HasKey(e => new { e.Jiko_WorkFlow_Base_ID, e.Jiko_WorkFlow_Sort });
            });

            modelBuilder.Entity<M_KojinUnsyu_Kubun>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_KojinUnsyu_Route>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_LoginUser>(entity =>
            {
                entity.Property(e => e.LoginID).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_LoginUser_Customer>(entity =>
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

            modelBuilder.Entity<M_PersonnelExpense>(entity =>
            {
                entity.HasKey(e => new { e.Company_ID, e.Syasyu, e.Kata });

                entity.Property(e => e.BenefitsCosts).HasDefaultValueSql("((0))");

                entity.Property(e => e.IndirectCosts).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_PostCode>(entity =>
            {
                entity.Property(e => e.POSTAL_CODE).IsUnicode(false);

                entity.Property(e => e.POSTAL_CODE_OLD).IsUnicode(false);

                entity.Property(e => e.PUBLIC_SECTOR_CODE).IsUnicode(false);
            });

            modelBuilder.Entity<M_PostCode_TEMP>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedOnAdd();

                entity.Property(e => e.POSTAL_CODE).IsUnicode(false);

                entity.Property(e => e.POSTAL_CODE_OLD).IsUnicode(false);

                entity.Property(e => e.PUBLIC_SECTOR_CODE).IsUnicode(false);
            });

            modelBuilder.Entity<M_PublishGroup>(entity =>
            {
                entity.Property(e => e.Branch_ID).HasDefaultValueSql("((0))");

                entity.Property(e => e.Company_ID).HasDefaultValueSql("((0))");

                entity.Property(e => e.User_ID).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_PublishGroup_Detail>(entity =>
            {
                entity.Property(e => e.Branch_ID).HasDefaultValueSql("((0))");

                entity.Property(e => e.Comany_ID).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_Report_Detail_Param>(entity =>
            {
                entity.HasKey(e => new { e.Report_Serch_Kubun_ID, e.Sort_Order });
            });

            modelBuilder.Entity<M_Report_Output_Item>(entity =>
            {
                entity.HasKey(e => new { e.Report_Serch_Kubun_ID, e.Report_Output_Item_ID, e.Company_ID, e.User_ID });
            });

            modelBuilder.Entity<M_Role>(entity =>
            {
                entity.HasKey(e => new { e.Company_ID, e.Role, e.Controller, e.Action, e.Method })
                    .HasName("PK_M_Role_1");

                entity.Property(e => e.Method).HasDefaultValueSql("(N'ALL')");
            });

            modelBuilder.Entity<M_Senzoku>(entity =>
            {
                entity.Property(e => e.Calc_Kubun).HasComment("0:月額から計算、１:日額から計算");

                entity.Property(e => e.Seikyu_Kubun).HasComment("0:案件ごと、1:月額");
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

            modelBuilder.Entity<M_SyaryoCost>(entity =>
            {
                entity.HasKey(e => new { e.Syaryo_ID, e.From_Distance });

                entity.Property(e => e.AdditionAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Amount).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_SyaryoManagement>(entity =>
            {
                entity.HasKey(e => e.SyaryoManagement_ID)
                    .HasName("PK_M_車両管理台帳");

                entity.Property(e => e.Syaryo_ID).HasDefaultValueSql("((0))");

                entity.Property(e => e.Syaryo_Price).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_SyaryoSize>(entity =>
            {
                entity.HasKey(e => new { e.SIZE, e.Company_ID });

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_Unit>(entity =>
            {
                entity.Property(e => e.Del_Flg).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sort_Order).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<M_Vender>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<M_Yosya>(entity =>
            {
                entity.ToView("M_Yosya");

                entity.Property(e => e.Yosya_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<M_Yosya_Branch>(entity =>
            {
                entity.ToView("M_Yosya_Branch");

                entity.Property(e => e.Customer_Branch_Code).IsUnicode(false);

                entity.Property(e => e.Yosya_Branch_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<M_Yosya_Driver>(entity =>
            {
                entity.ToView("M_Yosya_Driver");

                entity.Property(e => e.Yosya_Driver_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<M_Yosya_Driver_Syaryo>(entity =>
            {
                entity.ToView("M_Yosya_Driver_Syaryo");

                entity.Property(e => e.Yosya_DriverSyaryo_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<M_Yosya_Shiharai_Calc>(entity =>
            {
                entity.ToView("M_Yosya_Shiharai_Calc");
            });

            modelBuilder.Entity<M_Yosya_Tantou>(entity =>
            {
                entity.ToView("M_Yosya_Tantou");

                entity.Property(e => e.Tantou_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TEMP_CUSTOMER>(entity =>
            {
                entity.Property(e => e.コード).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FAX番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.コード1).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.住所１).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.住所２).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.検索カナ).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.略称).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.社名).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.郵便番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.電話番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<TEMP_CUSTOMER2>(entity =>
            {
                entity.Property(e => e.BB).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CC).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FAX番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.コード1).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.住所１).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.住所２).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.検索カナ).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.略称).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.社名).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.郵便番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.電話番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<TEMP_Driver>(entity =>
            {
                entity.Property(e => e.WORKER_CD).ValueGeneratedNever();

                entity.Property(e => e.GYOUMU_START).HasDefaultValueSql("('1900/01/01')");

                entity.Property(e => e.OFFICE).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.STATUS).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WORKER_NAME).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<T_Admin_Info>(entity =>
            {
                entity.Property(e => e.Info_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Info_Kubun).HasComment("0:お知らせ、1:重要、2:緊急、3:未定");
            });

            modelBuilder.Entity<T_Anken>(entity =>
            {
                entity.Property(e => e.Anken_Kubun).HasComment("0:自動車運送,1:自動車運送(過去),2:利用運送,3利用運送(過去),4:専属");

                entity.Property(e => e.Anken_No).IsUnicode(false);

                entity.Property(e => e.Anken_Status).HasComment("0:確定,1:暫定(配車必要),2:暫定(配車不要)");
            });

            modelBuilder.Entity<T_Anken_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Anken_ID, e.Anken_Order });

                entity.Property(e => e.Daisuu).HasDefaultValueSql("((0))");

                entity.Property(e => e.HaisyaPlanKubun).HasComment("0:未定、1:自車、2:傭車、3:専属庸車、4:専属、5:自社専任");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NumberCommLimitKubun).HasDefaultValueSql("((0))");

                entity.Property(e => e.OroshiTaskTime).IsUnicode(false);

                entity.Property(e => e.Root_Ferry).IsUnicode(false);

                entity.Property(e => e.Root_Regulation).IsUnicode(false);

                entity.Property(e => e.Root_Twouturn).IsUnicode(false);

                entity.Property(e => e.RouteType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Route_BreakTime).HasDefaultValueSql("((0))");

                entity.Property(e => e.Route_RestTime).HasDefaultValueSql("((0))");

                entity.Property(e => e.Route_TotalDays).HasDefaultValueSql("((0))");

                entity.Property(e => e.SeikyuKubun).HasDefaultValueSql("((0))");

                entity.Property(e => e.TsumiTaskTime).IsUnicode(false);

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Anken_Display>(entity =>
            {
                entity.Property(e => e.End_Lat).IsUnicode(false);

                entity.Property(e => e.End_Lng).IsUnicode(false);

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Start_Lat).IsUnicode(false);

                entity.Property(e => e.Start_Lng).IsUnicode(false);

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Anken_Equipment>(entity =>
            {
                entity.HasKey(e => new { e.Anken_ID, e.Anken_Order, e.Equipment_ID });

                entity.Property(e => e.Equipment_Count).HasDefaultValueSql("((0))");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Anken_Excharge>(entity =>
            {
                entity.HasKey(e => new { e.Anken_ID, e.Anken_Order, e.Komoku_ID });
            });

            modelBuilder.Entity<T_Anken_Luggage>(entity =>
            {
                entity.HasKey(e => new { e.Anken_ID, e.Anken_Order, e.Luggage_ID });

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Luggage_Count).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Anken_No>(entity =>
            {
                entity.Property(e => e.NENDO).ValueGeneratedNever();
            });

            modelBuilder.Entity<T_Anken_OyaKokyaku>(entity =>
            {
                entity.HasKey(e => new { e.Anken_ID, e.Anken_Order, e.Kokyaku_Order })
                    .HasName("PK_T_Anken_OyaKokyaku_1");
            });

            modelBuilder.Entity<T_Anken_Point>(entity =>
            {
                entity.HasKey(e => new { e.Anken_ID, e.Anken_Order, e.Kubun, e.Point_Order });

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

            modelBuilder.Entity<T_Anken_Publish>(entity =>
            {
                entity.Property(e => e.Anken_ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<T_Anken_Remark>(entity =>
            {
                entity.HasKey(e => new { e.Anken_ID, e.Anken_Order })
                    .HasName("PK_T_Anken_Remarks");
            });

            modelBuilder.Entity<T_Anken_Riyounso>(entity =>
            {
                entity.HasKey(e => new { e.Anken_ID, e.Anken_Order });

                entity.Property(e => e.AdvancesPaid).HasDefaultValueSql("((0))");

                entity.Property(e => e.Daisuu).HasDefaultValueSql("((0))");

                entity.Property(e => e.EigyoID).HasDefaultValueSql("((0))");

                entity.Property(e => e.GrossAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.KokyakuId).HasDefaultValueSql("((0))");

                entity.Property(e => e.KokyakuTantouId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PaymentAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Root_Ferry).IsUnicode(false);

                entity.Property(e => e.Root_Regulation).IsUnicode(false);

                entity.Property(e => e.Root_Twouturn).IsUnicode(false);

                entity.Property(e => e.SeikyuKubun).HasDefaultValueSql("((0))");

                entity.Property(e => e.TantouID).HasDefaultValueSql("((0))");

                entity.Property(e => e.Toll).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.YosyaDriverID).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Anken_Riyounso_Point>(entity =>
            {
                entity.HasKey(e => new { e.Anken_ID, e.Anken_Order, e.Point_Order });

                entity.Property(e => e.From_Lat).IsUnicode(false);

                entity.Property(e => e.From_Lng).IsUnicode(false);

                entity.Property(e => e.From_Time).IsUnicode(false);

                entity.Property(e => e.To_Lat).IsUnicode(false);

                entity.Property(e => e.To_Lng).IsUnicode(false);

                entity.Property(e => e.To_Time).IsUnicode(false);
            });

            modelBuilder.Entity<T_Anken_SyabanRenraku>(entity =>
            {
                entity.Property(e => e.Anken_ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<T_BATCH_RESULT>(entity =>
            {
                entity.Property(e => e.EXIT_TIME).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Check_Seikyu>(entity =>
            {
                entity.Property(e => e.Check_Kubun).HasComment("1：WEB、2：帳票");

                entity.Property(e => e.Check_Status).HasComment("0：発行済み、1：確認中、2：確認済、3：未定、4：承認済");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Print_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<T_Check_Seikyu_Change>(entity =>
            {
                entity.HasKey(e => new { e.Check_Seikyu_ID, e.Uriage_Unchin_ID });

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Check_Seikyu_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Check_Seikyu_ID, e.Uriage_Unchin_ID });
            });

            modelBuilder.Entity<T_Check_Seikyu_Done>(entity =>
            {
                entity.Property(e => e.Check_Seikyu_ID).ValueGeneratedNever();

                entity.Property(e => e.Change_Flg)
                    .HasDefaultValueSql("((0))")
                    .HasComment("0：金額変更無し、1：金額変更あり");

                entity.Property(e => e.Check_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Check_Shitabarai>(entity =>
            {
                entity.Property(e => e.Check_Kubun).HasComment("1：WEB、2：帳票");

                entity.Property(e => e.Check_Status).HasComment("0：発行済み、1：確認中、2：確認済、3：未定、4：承認済");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Print_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<T_Check_Shitabarai_Change>(entity =>
            {
                entity.HasKey(e => new { e.Check_Shitabarai_ID, e.Uriage_Shiharai_ID });

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Check_Shitabarai_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Check_Shitabarai_ID, e.Uriage_Shiharai_ID });
            });

            modelBuilder.Entity<T_Check_Shitabarai_Done>(entity =>
            {
                entity.Property(e => e.Check_Shitabarai_ID).ValueGeneratedNever();

                entity.Property(e => e.Change_Flg)
                    .HasDefaultValueSql("((0))")
                    .HasComment("0：金額変更無し、1：金額変更あり");

                entity.Property(e => e.Check_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Commit_Kaikei>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Tax_Fraction_Kubun).HasComment("０：切り捨て、１：四捨五入、２：切り上げ");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:免税あり");
            });

            modelBuilder.Entity<T_Commit_Seikyu>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Tax_Fraction_Kubun).HasComment("０：切り捨て、１：四捨五入、２：切り上げ");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:免税あり");
            });

            modelBuilder.Entity<T_Commit_Shitabarai>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<T_Commit_Unsyu>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Expense>(entity =>
            {
                entity.Property(e => e.Expense_Kubun).HasComment("1:乗務員,2:車輌,3:事故");

                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");
            });

            modelBuilder.Entity<T_Expense_Item>(entity =>
            {
                entity.Property(e => e.Expense_Item_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<T_Expense_Payment>(entity =>
            {
                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.Shime_Kubun).HasComment("0:未,1:締め	");

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");
            });

            modelBuilder.Entity<T_Haisya>(entity =>
            {
                entity.HasComment("");

                entity.Property(e => e.Haisya_ID).HasComment("");

                entity.Property(e => e.AnkenDisplay_ID).HasComment("");

                entity.Property(e => e.Anken_ID).HasComment("");

                entity.Property(e => e.Company_ID).HasComment("");

                entity.Property(e => e.Day).HasComment("");

                entity.Property(e => e.DriverSyaryo_ID).HasComment("");

                entity.Property(e => e.Driver_ID).HasComment("");

                entity.Property(e => e.Haisya_Kubun).HasComment("1：自車、2：傭車、3：専属傭車、4：自車専属、5：自車専任");

                entity.Property(e => e.Haisya_Status).HasComment("1:暫定、0：確定");

                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.KUBUN).HasComment("");

                entity.Property(e => e.Remarks).HasComment("");

                entity.Property(e => e.Route_Midnight).HasComment("");

                entity.Property(e => e.Route_OverTime).HasComment("");

                entity.Property(e => e.Route_Teate).HasComment("");

                entity.Property(e => e.SyaryoManagement_ID).HasComment("");

                entity.Property(e => e.SyaryoManagement_ID1)
                    .HasDefaultValueSql("((0))")
                    .HasComment("");

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");
            });

            modelBuilder.Entity<T_Haisya_Around>(entity =>
            {
                entity.Property(e => e.FlgGenchiKakunin).HasDefaultValueSql("((0))");

                entity.Property(e => e.Lat).IsUnicode(false);

                entity.Property(e => e.Lng).IsUnicode(false);

                entity.Property(e => e.PointTime).IsUnicode(false);

                entity.Property(e => e.SEKubun).IsUnicode(false);

                entity.Property(e => e.TollDisplayHeight).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Haisya_Driver_Day_Remark>(entity =>
            {
                entity.HasKey(e => new { e.Driver_ID, e.Date });
            });

            modelBuilder.Entity<T_Haisya_SyabanRenraku>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Haisya_SyabanRenraku_Detail>(entity =>
            {
                entity.HasKey(e => new { e.SyabanRenraku_ID, e.Anken_ID });
            });

            modelBuilder.Entity<T_Haisya_SyabanRenraku_Remark>(entity =>
            {
                entity.Property(e => e.AnkenDisplay_ID).ValueGeneratedNever();

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Haisya_Yosya>(entity =>
            {
                entity.HasKey(e => new { e.Haisya_ID, e.Yosya_Sort });

                entity.Property(e => e.Yosya_Count)
                    .HasDefaultValueSql("((1))")
                    .HasComment("第何傭車数");

                entity.Property(e => e.Yosya_Shiharai_Kubun).HasComment("1:暫定,0:確定");
            });

            modelBuilder.Entity<T_Jiko>(entity =>
            {
                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.Jiko_Kubun).HasComment("M_Code:18");

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");
            });

            modelBuilder.Entity<T_Jiko_Detail>(entity =>
            {
                entity.Property(e => e.Jiko_ID).ValueGeneratedNever();

                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.Jiko_Weather_Kubun).HasComment("M_Code:14");

                entity.Property(e => e.Lat).IsUnicode(false);

                entity.Property(e => e.Lng).IsUnicode(false);

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");
            });

            modelBuilder.Entity<T_Jiko_Item>(entity =>
            {
                entity.HasKey(e => new { e.Jiko_ID, e.Jiko_Items_ID });
            });

            modelBuilder.Entity<T_Jiko_No>(entity =>
            {
                entity.Property(e => e.NENDO).ValueGeneratedNever();
            });

            modelBuilder.Entity<T_Jiko_Type>(entity =>
            {
                entity.HasKey(e => new { e.Jiko_ID, e.Jiko_Type_ID });

                entity.Property(e => e.Jiko_Type_ID).HasComment("M_Code:17");
            });

            modelBuilder.Entity<T_Jiko_WorkFlow_Status>(entity =>
            {
                entity.Property(e => e.Approval_Kubun).HasComment("0:処理中,1:起案/承認,2:差戻し,3:差戻し未処理");
            });

            modelBuilder.Entity<T_Kintai>(entity =>
            {
                entity.HasKey(e => new { e.Driver_ID, e.Day });
            });

            modelBuilder.Entity<T_Nippou>(entity =>
            {
                entity.Property(e => e.DegitakoLink_Result).HasComment("デジタコ連動結果：０：未連携、１：正常連携、２：一部連携");

                entity.Property(e => e.Distance).HasComment("区間距離");

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Nippou_Approval>(entity =>
            {
                entity.HasKey(e => e.Nippou_Approval_ID)
                    .HasName("PK_T_Nippou_approval");

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Nippou_Kaiso>(entity =>
            {
                entity.Property(e => e.Nippou_ID).ValueGeneratedNever();

                entity.Property(e => e.Distance).HasComment("区間距離");

                entity.Property(e => e.End_PointName).HasComment("終了場所名");

                entity.Property(e => e.End_ShikuName).HasComment("終了市町村名");

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Start_PointName).HasComment("開始場所名");

                entity.Property(e => e.Start_ShikuName).HasComment("開始市町村名");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Nippou_Stay>(entity =>
            {
                entity.Property(e => e.Nippou_ID).ValueGeneratedNever();

                entity.Property(e => e.End_PointName).HasComment("終了場所名");

                entity.Property(e => e.End_ShikuName).HasComment("終了市町村名");

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Interval_Time).HasComment("区間距離");

                entity.Property(e => e.Start_PointName).HasComment("開始場所名");

                entity.Property(e => e.Start_ShikuName).HasComment("開始市町村名");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Nippou_Toll>(entity =>
            {
                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Nippou_Toll_Other>(entity =>
            {
                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Update_User).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Nyukin>(entity =>
            {
                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.Process_Kubun).HasComment("0：入金、1：返金");

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");
            });

            modelBuilder.Entity<T_Point>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Insert_User).HasDefaultValueSql("((0))");

                entity.Property(e => e.Lat).IsUnicode(false);

                entity.Property(e => e.Lng).IsUnicode(false);
            });

            modelBuilder.Entity<T_Portal_Info>(entity =>
            {
                entity.Property(e => e.Display_Flg).HasComment("0:表示、1:非表示");

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

            modelBuilder.Entity<T_Print_Download>(entity =>
            {
                entity.Property(e => e.Download_Datetime).HasDefaultValueSql("((0))");

                entity.Property(e => e.Download_Web_Browser).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Print_Parameter>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Print_Kubun).HasComment("2：車番連絡、10：請求問合せ、11：下払問合せ　　M_Codeの10");

                entity.Property(e => e.Tokun).IsFixedLength(true);

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Print_Rireki>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Print_Kubun).HasComment("2：車番連絡、10：請求問合せ、11：下払問合せ  M_CODE:10");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Print_Seikyu>(entity =>
            {
                entity.HasKey(e => e.Print_Seikyu_ID)
                    .HasName("PK_T_Print_Seikyu_1");

                entity.Property(e => e.Print_Pattern).HasComment("M_Code:11");

                entity.Property(e => e.Tax_Fraction_Kubun).HasComment("０：切り捨て、１：四捨五入、２：切り上げ");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<T_Print_Seikyu_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Print_Seikyu_ID, e.Data_Kubun, e.Data_Sort })
                    .HasName("PK_T_Print_Seikyu_Detail_1");

                entity.Property(e => e.Data_Kubun).HasComment("");

                entity.Property(e => e.Nyukin_ID).HasDefaultValueSql("((0))");

                entity.Property(e => e.Uriage_Unchin_ID).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<T_Print_Shitabarai>(entity =>
            {
                entity.HasKey(e => e.Print_Shitabarai_ID)
                    .HasName("PK_T_Print_Shitabarai_1");

                entity.Property(e => e.UP_DATE).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<T_Print_Shitabarai_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Print_Shitabarai_ID, e.Data_Kubun, e.Data_Sort })
                    .HasName("PK_T_Print_Shitabarai_Detail_1");

                entity.Property(e => e.Data_Kubun).HasComment("1：ヘッダー、2：入金、３：案件明細");
            });

            modelBuilder.Entity<T_Report_Layout>(entity =>
            {
                entity.Property(e => e.Print_Kubun).HasComment("2：車番連絡、10：請求問合せ、11：下払問合せ  M_CODE:10");
            });

            modelBuilder.Entity<T_Seikyu>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NENDOMATSU_FLG).HasComment("0:課税、1:非課税");

                entity.Property(e => e.Print_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Print_Kubun).HasComment("請求方法、InquiryTypes");

                entity.Property(e => e.Print_Pattern).HasComment("帳票印刷の場合、印刷パターン");

                entity.Property(e => e.Seikyu_Kubun).HasComment("1：WEB、2：帳票");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税のみ、1:非課税と混在");
            });

            modelBuilder.Entity<T_Seikyu_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Seikyu_ID, e.Uriage_Unchin_ID });
            });

            modelBuilder.Entity<T_Shitabarai>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Print_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Print_Pattern).HasComment("帳票印刷の場合、印刷パターン");

                entity.Property(e => e.Shitabarai_Kubun).HasComment("1：WEB、2：帳票");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<T_Shitabarai_Detail>(entity =>
            {
                entity.HasKey(e => new { e.Shitabarai_ID, e.Uriage_Unchin_ID });
            });

            modelBuilder.Entity<T_Uriage>(entity =>
            {
                entity.Property(e => e.AnkenDisplay_ID).HasComment("");

                entity.Property(e => e.Company_ID).HasComment("");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Reg_Kubun).HasComment("0：案件、1：直接");

                entity.Property(e => e.Reg_Status).HasComment("1：暫定登録、2：確定登録、３：仮登録");

                entity.Property(e => e.Reg_Status_Shitabarai).HasComment("1：暫定登録、2：確定登録、３：仮登録");

                entity.Property(e => e.SeikyuDate_Kubun).HasComment("0：配車日（積日）、1：卸日");

                entity.Property(e => e.Seikyu_Kubun).HasComment("0：案件単位、1：卸し単位");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_Uriage_Futan>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.futan_Kubun).HasComment("高速代、フェリー等の区分");
            });

            modelBuilder.Entity<T_Uriage_Shitabarai>(entity =>
            {
                entity.HasKey(e => e.Uriage_Shiharai_ID)
                    .HasName("PK_T_Uriage_Shiharai");

                entity.Property(e => e.CalcPrice).HasComment("計算運賃");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Qty).HasComment("数量");

                entity.Property(e => e.ShiharaiPrice).HasComment("単価");

                entity.Property(e => e.Unit).HasComment("単位");

                entity.Property(e => e.UnitPrice).HasComment("単価");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Uriage_Kubun).HasComment("0：伝票、1：赤黒伝票");

                entity.Property(e => e.WarimashiPrice).HasComment("計算運賃");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<T_Uriage_Unchin>(entity =>
            {
                entity.Property(e => e.CalcPrice).HasComment("計算運賃");

                entity.Property(e => e.Default_Kubun).HasComment("0：追加、1：初期値");

                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Qty).HasComment("数量");

                entity.Property(e => e.Unit).HasComment("単位");

                entity.Property(e => e.UnitPrice).HasComment("単価");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Uriage_Kubun).HasComment("0：伝票、1：赤黒伝票");

                entity.Property(e => e.Zei_Kubun).HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<T_Uriage_Unsyu>(entity =>
            {
                entity.Property(e => e.Insert_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Unsyu_Kubun).HasComment("案件、空車回送、泊まり等");

                entity.Property(e => e.Update_Datetime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Uriage_Kubun).HasComment("0：伝票、1：赤黒伝票");
            });

            modelBuilder.Entity<T_YosyaShiharai>(entity =>
            {
                entity.Property(e => e.Insert_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Insert_User).HasComment("");

                entity.Property(e => e.Process_Kubun).HasComment("0：入金、1：返金");

                entity.Property(e => e.Update_Datetime)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.Update_User).HasComment("");
            });

            modelBuilder.Entity<V_Anken_Detail>(entity =>
            {
                entity.ToView("V_Anken_Detail");

                entity.Property(e => e.OroshiTaskTime).IsUnicode(false);

                entity.Property(e => e.Root_Ferry).IsUnicode(false);

                entity.Property(e => e.Root_Regulation).IsUnicode(false);

                entity.Property(e => e.Root_Twouturn).IsUnicode(false);

                entity.Property(e => e.TsumiTaskTime).IsUnicode(false);
            });

            modelBuilder.Entity<V_CompanyDriver>(entity =>
            {
                entity.ToView("V_CompanyDriver");
            });

            modelBuilder.Entity<V_Customer_Syaryo>(entity =>
            {
                entity.ToView("V_Customer_Syaryo");
            });

            modelBuilder.Entity<V_HaisyaDataList_old>(entity =>
            {
                entity.ToView("V_HaisyaDataList_old");

                entity.Property(e => e.AnkenStatusDisplay)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.AnkenStep)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Anken_No)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Customer_Name_Abbr).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Display1).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Display2).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Driver_Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Eigyo_Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EndAddressDisplay).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EndAddressDisplay2).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EndAddressDisplay3).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_Address).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_Address2).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_Address3).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_Address4).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_BuildingName).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_BuildingNameRead).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_BuildingZid).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_BuildingZid_Attr).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_Lat)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_Lng)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_PointName).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_Point_KoumokuTitle).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_Point_Type).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.End_Post_code).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Equipment).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.EquipmentDisplay).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FILL_SYABAN).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Kata).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.KokyakuCode).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.KokyakuName).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.KokyakuTantouName).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.KokyakuTantouPhone).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Luggage).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.LuggageDisplay).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.OroshiTaskTime)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Remarks).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Root_Ferry)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Root_Regulation)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Root_Twouturn)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.RouteID).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.RouteTypeDisplay).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Route_RestTimeDisplay).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Route_TotalTime).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SYABAN).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.StartAddressDisplay).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.StartAddressDisplay2).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.StartAddressDisplay3).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_Address).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_Address2).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_Address3).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_Address4).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_BuildingName).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_BuildingNameRead).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_BuildingZid).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_BuildingZid_Attr).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_Lat)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_Lng)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_PointName).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_Point_KoumokuTitle).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_Point_Type).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Start_Post_code).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SyabanRenraku_Remarks).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Syasyu).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SyasyuDaisuDisplay).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SyasyuDisplay).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SyasyuDisplay2).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SyasyuSize).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tantou_Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TsumiTaskTime)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Work_Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<V_LoginUser>(entity =>
            {
                entity.ToView("V_LoginUser");

                entity.Property(e => e.Branch_Code).IsUnicode(false);

                entity.Property(e => e.LoginID).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);
            });

            modelBuilder.Entity<V_Luggage>(entity =>
            {
                entity.ToView("V_Luggage");
            });

            modelBuilder.Entity<V_Senzoku>(entity =>
            {
                entity.ToView("V_Senzoku");

                entity.Property(e => e.Customer_Branch_Code).IsUnicode(false);
            });

            modelBuilder.Entity<V_Senzoku_Driver>(entity =>
            {
                entity.ToView("V_Senzoku_Driver");

                entity.Property(e => e.Customer_Branch_Code).IsUnicode(false);
            });

            modelBuilder.Entity<V_Tokuisaki>(entity =>
            {
                entity.ToView("V_Tokuisaki");
            });

            modelBuilder.Entity<V_TokuisakiForNotConnect>(entity =>
            {
                entity.ToView("V_TokuisakiForNotConnect");
            });

            modelBuilder.Entity<V_TokuisakiForNotConnect_old>(entity =>
            {
                entity.Property(e => e.FAX番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.コード).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.住所１).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.住所２).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.検索カナ).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.略称).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.社名).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.補助検索キー).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.郵便番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.電話番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<V_Tokuisaki_old>(entity =>
            {
                entity.Property(e => e.FAX番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.コード).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.住所１).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.住所２).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.検索カナ).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.略称).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.社名).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.補助検索キー).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.郵便番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.電話番号).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<V_Yosyasaki>(entity =>
            {
                entity.ToView("V_Yosyasaki");
            });

            modelBuilder.Entity<V_YosyasakiForNotConnect>(entity =>
            {
                entity.ToView("V_YosyasakiForNotConnect");
            });

            modelBuilder.Entity<___M_Tokuisaki_SeikyuTantou>(entity =>
            {
                entity.HasKey(e => e.コード)
                    .HasName("PK_M_Tokuisaki_SeikyuTantou");

                entity.Property(e => e.コード).ValueGeneratedNever();
            });

            modelBuilder.Entity<担当者一段階目>(entity =>
            {
                entity.Property(e => e.担当者).UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.略称).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
