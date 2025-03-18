using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 連携案件ポイントリポジトリ
    /// </summary>
    public class RenkeiAnkenPointRepository : RepositoryBaseAsync<T_Renkei_Anken_Point, ApplicationDbContext>, IRenkeiAnkenPointRepository
    {
        public RenkeiAnkenPointRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 案件詳細取得
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <param name="anken_order">案件注文</param>
        /// <returns>案件ポイントリスト</returns>
        public async Task<IList<T_Renkei_Anken_Point>> GetPoints(int anken_id, int anken_order)
        {
            IQueryable<T_Renkei_Anken_Point> builder =
                from trap in DbContext.Set<T_Renkei_Anken_Point>()
                where
                    trap.Renkei_Anken_ID == anken_id && trap.Renkei_Anken_Order == anken_order
                select trap;

            return await builder.ToListAsync();
        }

        /// <summary>
        /// 最新の案件ポイントリストを取得
        /// </summary>
        /// <returns>案件ポイントリスト</returns>
        public async Task<IList<T_Renkei_Anken_Point>> GetPointsMostRecents()
        {
            string sql = "";
            sql += " SELECT";
            sql += " MIN([Renkei_Anken_ID]) AS [Renkei_Anken_ID]";
            sql += " ,MIN([Renkei_Anken_Order]) AS [Renkei_Anken_Order]";
            sql += " ,MIN([Kubun]) AS [Kubun]";
            sql += " ,COUNT(*) AS [Point_Order]";
            sql += " ,Null AS [SEKubun]";
            sql += " ,[Address]";
            sql += " ,[Address_Code]";
            sql += " ,Null AS [Address_Level]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,Null AS [BuildingNameRead]";
            sql += " ,Null AS [Point_KoumokuTitle]";
            sql += " ,Null AS [Point_Type]";
            sql += " ,Null AS [PointName]";
            sql += " ,Null AS [PointDate]";
            sql += " ,Null AS [PointTime]";
            sql += " ,Null AS [PointTimeKubun]";
            sql += " ,Null AS [PointStatusKubun]";
            sql += " ,Null AS [FlgGenchiKakunin]";
            sql += " ,Null AS [TollDisplay]";
            sql += " ,Null AS [TollDisplayHeight]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += " ,[Address4]";

            sql += " ,Null AS [RoadType]";
            sql += " ,MAX([Insert_Datetime]) AS [Insert_Datetime]";
            sql += " ,Null AS [Insert_User]";
            sql += " ,Null AS [Update_Datetime]";
            sql += " ,Null AS [Update_User]";

            sql += " FROM";
            sql += " (";
            sql += " SELECT ";
            sql += " [Renkei_Anken_ID]";
            sql += " ,[Renkei_Anken_Order]";
            sql += " ,[Kubun]";
            sql += " ,[Point_Order]";
            sql += " ,[SEKubun]";
            sql += " ,[Address]";
            sql += " ,[Address_Code]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += ",[Address4]";
            sql += ",[Insert_Datetime]";
            sql += " FROM [T_Renkei_Anken_Point] P";
            sql += " WHERE";
            sql += " [Post_code] IS NOT NULL";

            sql += " UNION ALL";

            sql += " SELECT ";
            sql += " [Renkei_Anken_ID]";
            sql += " ,[Renkei_Anken_Order]";
            sql += " ,[Kubun]";
            sql += " ,[Point_Order]";
            sql += " ,[SEKubun]";
            sql += " ,[Address]";
            sql += " ,[Address_Code]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += " ,[Address4]";
            sql += " ,[Update_Datetime] AS [Insert_Datetime]";
            sql += " FROM [T_Renkei_Anken_Point] P";
            sql += " WHERE";
            sql += " [Post_code] IS NOT NULL";

            sql += " )X";
            sql += " GROUP BY";
            sql += " [Address]";
            sql += " ,[Address_Code]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += " ,[Address4]";

            sql += " ORDER BY";
            sql += " [Insert_Datetime] DESC";
            return await DbContext.T_Renkei_Anken_Points.FromSqlRaw(sql).ToListAsync();
        }
        /// <summary>
        /// 住所履歴の使用率リストを取得
        /// </summary>
        /// <returns>案件ポイントリスト</returns>
        public async Task<IList<T_Renkei_Anken_Point>> GetPointsUsageRate()
        {
            string sql = "";
            sql += " SELECT";
            sql += " MIN([Renkei_Anken_ID]) AS [Renkei_Anken_ID]";
            sql += " ,MIN([Renkei_Anken_Order]) AS [Renkei_Anken_Order]";
            sql += " ,MIN([Kubun]) AS [Kubun]";
            sql += " ,COUNT(*) AS [Point_Order]";
            sql += " ,Null AS [SEKubun]";
            sql += " ,[Address]";
            sql += " ,[Address_Code]";
            sql += " ,Null AS [Address_Level]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,Null AS [BuildingNameRead]";
            sql += " ,Null AS [Point_KoumokuTitle]";
            sql += " ,Null AS [Point_Type]";
            sql += " ,Null AS [PointName]";
            sql += " ,Null AS [PointDate]";
            sql += " ,Null AS [PointTime]";
            sql += " ,Null AS [PointTimeKubun]";
            sql += " ,Null AS [PointStatusKubun]";
            sql += " ,Null AS [FlgGenchiKakunin]";
            sql += " ,Null AS [TollDisplay]";
            sql += " ,Null AS [TollDisplayHeight]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += " ,[Address4]";

            sql += " ,Null AS [RoadType]";
            sql += " ,Null AS [Insert_Datetime]";
            sql += " ,Null AS [Insert_User]";
            sql += " ,Null AS [Update_Datetime]";
            sql += " ,Null AS [Update_User]";

            sql += " FROM [T_Renkei_Anken_Point] P";
            sql += " WHERE";
            sql += " [Post_code] IS NOT NULL";

            sql += " GROUP BY";
            sql += " [Address]";
            sql += " ,[Address_Code]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += " ,[Address4]";
            sql += " ORDER BY";
            sql += " COUNT(*) DESC";
            return await DbContext.T_Renkei_Anken_Points.FromSqlRaw(sql).ToListAsync();
        }

        /// <summary>
        /// 案件詳細取得
        /// </summary>
        /// <param name="address2">住所2</param>
        /// <returns>案件ポイントリスト</returns>
        public async Task<IEnumerable<T_Renkei_Anken_Point>> GetPointsAsync(string address2)
        {
            if (string.IsNullOrEmpty(address2))
            {
                return await GetAllPointsAsync();
            }
            else
            {
                return await FindByCondition(x => EF.Functions.Like(x.Address2, $"%{address2}%")).ToListAsync();
            }
        }

        /// <summary>
        /// 全てのポイント取得
        /// </summary>
        /// <returns>案件ポイントリスト</returns>
        private async Task<IEnumerable<T_Renkei_Anken_Point>> GetAllPointsAsync()
        {
            return await FindAll().ToListAsync();
        }
    }
}
