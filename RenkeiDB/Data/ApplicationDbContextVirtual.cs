using Microsoft.EntityFrameworkCore;

namespace RenkeiDB.Data
{
    /// <summary>
    /// アプリケーションのデータベースコンテキスト
    /// </summary>
    public partial class ApplicationDbContext
    {
        /// <summary>
        /// モデルの部分的な構成を行います。
        /// </summary>
        /// <param name="modelBuilder">モデルビルダー</param>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_Portal_Info>(entity =>
            {
                entity.HasOne(e => e.Company)
                    .WithMany(c => c.Portal_Infos)
                    .HasForeignKey(p => p.Company_ID)
                    .HasPrincipalKey(c => c.Renkei_Company_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<T_Share_Syaryo>(entity =>
            {
                entity.HasOne(e => e.Share_Syaryo_Detail)
                    .WithOne(p => p.Share_Syaryo)
                    .HasForeignKey<T_Share_Syaryo_Detail>(d => new { d.Share_Syaryo_ID, d.Share_Syaryo_Order })
                    .HasPrincipalKey<T_Share_Syaryo>(p => new { p.Share_Syaryo_ID, p.Share_Syaryo_Latest_Order })
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Company)
                    .WithMany(c => c.Share_Syaryos)
                    .HasForeignKey(d => d.Company_ID)
                    .HasPrincipalKey(c => c.Renkei_Company_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.CompanyBranch)
                    .WithMany(cb => cb.Share_Syaryos)
                    .HasForeignKey(d => d.Branch_ID)
                    .HasPrincipalKey(cb => cb.Branch_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.CompanyUserGroup)
                    .WithMany(c => c.Share_Syaryos)
                    .HasForeignKey(d => d.Tantou_Group_ID)
                    .HasPrincipalKey(cb => cb.Group_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<T_Renkei_Anken_Luggage>(entity =>
            {
                entity.HasOne(e => e.Luggage).WithMany().HasForeignKey(e => e.Luggage_ID);
            });

            modelBuilder.Entity<M_Luggage>(entity =>
            {
                entity.HasOne(e => e.Luggage_Group).WithMany().HasForeignKey(e => e.Luggage_Group_ID);
            });

            modelBuilder.Entity<T_Renkei_Anken_Equipment>(entity =>
            {
                entity.HasOne(e => e.Equiptment).WithMany().HasForeignKey(e => e.Equipment_ID);
            });

            modelBuilder.Entity<M_Equipment>(entity =>
            {
                entity.HasOne(e => e.Equipment_Group).WithMany().HasForeignKey(e => e.Equipment_Group_ID);
            });
        }
    }
}
