using RenkeiDB.Data;

namespace RenkeiDB.Dto
{
    /// <summary>
    /// 会社ユーザーグループ情報を表すDTO
    /// </summary>
    public class CompanyUserGroupDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public int id { get; set; }
        public int groupKubun { get; set; }
        public string groupName { get; set; }
        public string displayName { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        public static CompanyUserGroupDto FromEntity(M_CompanyUser_Group entity) => entity == null ? null : new()
        {
            id = entity.Group_ID,
            groupKubun = entity.Group_Kubun,
            groupName = entity.Group_Name,
            displayName = entity.Display_Name
        };
    }
}
