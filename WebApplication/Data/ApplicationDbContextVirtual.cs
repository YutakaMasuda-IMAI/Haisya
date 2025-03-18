using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Data
{
    /// <summary>
    /// データベース接続およびテーブル間のリレーションシップを定義するための部分クラス
    /// </summary>
    public partial class ApplicationDbContext
    {
        /// <summary>
        /// テーブル間のリレーションシップを定義するメソッド
        /// </summary>
        /// <param name="modelBuilder">モデルビルダーのパラメータ</param>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<M_Report_Serch>(entity =>
            {
                entity.HasMany(d => d.Report_Serch_Kubun_List)
                    .WithOne(p => p.Report_Serch)
                    .HasForeignKey(d => d.Report_Serch_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<M_Report_Serch_Kubun>(entity =>
            {
                entity.HasMany(d => d.Report_Serch_Item_List)
                    .WithOne(p => p.Report_Serch_Kubun)
                    .HasForeignKey(d => d.Report_Serch_Kubun_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(d => d.Report_Detail_Param_List)
                    .WithOne(p => p.Report_Serch_Kubun)
                    .HasForeignKey(d => d.Report_Serch_Kubun_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<M_Report_Output_Item_Master>(entity =>
            {
                entity.HasMany(d => d.Report_Output_Item_List)
                    .WithOne(p => p.Report_Output_Item_Master)
                    .HasForeignKey(d => d.Report_Output_Item_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
