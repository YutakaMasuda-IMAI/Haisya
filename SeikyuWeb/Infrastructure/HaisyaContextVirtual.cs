using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Models;

#nullable disable

namespace SeikyuWeb.Infrastructure
{
    public partial class HaisyaContext
    {
        /// <summary>
        /// テーブル間のリレーションの定義
        /// </summary>
        /// <param name="modelBuilder">モデルビルダー</param>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MCompanyUserGroupUser>(entity =>
            {
                entity.HasOne(e => e.CompanyUserGroup)
                    .WithMany(g => g.CompanyUserGroupUsers)
                    .HasForeignKey(gu => gu.GroupId);

                entity.HasOne(e => e.CompanyUser)
                    .WithMany(u => u.CompanyUserGroupUsers)
                    .HasForeignKey(gu => gu.UserId);
            });

            modelBuilder.Entity<MCustomerBranch>(entity =>
            {
                entity.HasOne(e => e.ShiharaiTantou)
                    .WithMany(g => g.ShiharaiTantouCustomerBranchs)
                    .IsRequired(false)
                    .HasForeignKey(s => s.ShiharaiTantouId)
                    .HasPrincipalKey(g => g.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.SeikuTantou)
                    .WithMany(g => g.SeikuTantouCustomerBranchs)
                    .IsRequired(false)
                    .HasForeignKey(s => s.SeikyuTantouId)
                    .HasPrincipalKey(g => g.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.CustomerUriageCalc)
                    .WithOne(uc => uc.CustomerBranch)
                    .IsRequired(false)
                    .HasForeignKey<MCustomerBranch>(s => s.CustomerBranchId)
                    .HasPrincipalKey<MCustomerUriageCalc>(g => g.CustomerBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TCheckShitabarai>(entity =>
            {
                entity.HasOne(e => e.CustomerBranch)
                    .WithMany(b => b.CheckShitabarais)
                    .HasForeignKey(s => s.YosyaBranchId)
                    .HasPrincipalKey(b => b.CustomerBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TCheckShitabaraiChange>(entity =>
            {
                entity.HasOne(e => e.CheckShitabaraiDetail)
                    .WithOne(c => c.CheckShitabaraiChange)
                    .IsRequired(false)
                    .HasForeignKey<TCheckShitabaraiChange>(d => new { d.UriageShiharaiId, d.CheckShitabaraiId })
                    .HasPrincipalKey<TCheckShitabaraiDetail>(c => new { c.UriageShiharaiId, c.CheckShitabaraiId })
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TCheckShitabaraiDetail>(entity =>
            {
                entity.HasOne(e => e.UriageShitabarai)
                    .WithOne(u => u.CheckShitabaraiDetail)
                    .HasForeignKey<TCheckShitabaraiDetail>(c => c.UriageShiharaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TCheckShitabaraiDone>(entity =>
            {
                entity.HasOne(e => e.CustomerTantou)
                    .WithMany(d => d.CheckShitabaraiDones)
                    .HasForeignKey(t => t.CheckUser)
                    .HasPrincipalKey(d => d.TantouId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MReportSerch>(entity =>
            {
                entity.HasMany(d => d.Report_Serch_Kubun_List)
                    .WithOne(p => p.Report_Serch)
                    .HasForeignKey(d => d.ReportSerchId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MReportSerchKubun>(entity =>
            {
                entity.HasMany(d => d.Report_Serch_Item_List)
                    .WithOne(p => p.Report_Serch_Kubun)
                    .HasForeignKey(d => d.ReportSerchKubunId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(d => d.Report_Detail_Param_List)
                    .WithOne(p => p.Report_Serch_Kubun)
                    .HasForeignKey(d => d.ReportSerchKubunId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(d => d.Report_Output_Item_List)
                    .WithOne(p => p.Report_Serch_Kubun)
                    .HasForeignKey(d => d.ReportSerchKubunId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MReportOutputItemMaster>(entity =>
            {
                entity.HasMany(d => d.Report_Output_Item_List)
                    .WithOne(p => p.ReportOutputItemMaster)
                    .HasForeignKey(d => d.ReportOutputItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
