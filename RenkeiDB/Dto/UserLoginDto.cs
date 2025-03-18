namespace RenkeiDB.Dto
{
    /// <summary>
    /// ユーザーログイン情報を表すDTO
    /// </summary>
    public class UserLoginDto
    {
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
    }
}
