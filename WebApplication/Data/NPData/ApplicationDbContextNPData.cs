using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication.Data.NPData
{
    public partial class ApplicationDbContextNPData : DbContext
    {
        public ApplicationDbContextNPData()
        {
        }

        public ApplicationDbContextNPData(DbContextOptions<ApplicationDbContextNPData> options)
            : base(options)
        {
        }

        public virtual DbSet<KUDGIVT> KUDGIVTs { get; set; }
        public virtual DbSet<KUDGIVT_ANYTIME> KUDGIVT_ANYTIMEs { get; set; }
        public virtual DbSet<KUDGIVT_ANYTIME_DEL> KUDGIVT_ANYTIME_DELs { get; set; }
        public virtual DbSet<KUDGIVT_ANYTIME_FORUPDATE> KUDGIVT_ANYTIME_FORUPDATEs { get; set; }
        public virtual DbSet<KUDGSIR> KUDGSIRs { get; set; }
        public virtual DbSet<KUDGSIR_ANYTIME> KUDGSIR_ANYTIMEs { get; set; }
        public virtual DbSet<KUDGURI> KUDGURIs { get; set; }
        public virtual DbSet<T_KUDGIVT> T_KUDGIVTs { get; set; }
        public virtual DbSet<T_KUDGIVT_1> T_KUDGIVT_1s { get; set; }
        public virtual DbSet<T_KUDGIVT_20241028> T_KUDGIVT_20241028s { get; set; }
        public virtual DbSet<T_KUDGIVT_DEL> T_KUDGIVT_DELs { get; set; }
        public virtual DbSet<T_KUDGIVT_TARGET> T_KUDGIVT_TARGETs { get; set; }
        public virtual DbSet<T_KUDGSIR> T_KUDGSIRs { get; set; }
        public virtual DbSet<T_KUDGURI> T_KUDGURIs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=IMAI-SRV-DB\\SQLEXPRESS;Database=NPDataRenkei;user id=sa;password=Imaiunso00;Connection Timeout=300;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<T_KUDGIVT>(entity =>
            {
                entity.Property(e => e.取り込み日).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_KUDGIVT_1>(entity =>
            {
                entity.Property(e => e.取り込み日).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_KUDGIVT_20241028>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<T_KUDGIVT_DEL>(entity =>
            {
                entity.Property(e => e.取り込み日).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_KUDGIVT_TARGET>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<T_KUDGSIR>(entity =>
            {
                entity.Property(e => e.取り込み日).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<T_KUDGURI>(entity =>
            {
                entity.Property(e => e.取り込み日).HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
