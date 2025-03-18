using RenkeiDB.Dto.PdfDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// PDFサービスインターフェース
    /// </summary>
    public interface IPdfService
    {
        /// <summary>
        /// 車番通知を印刷します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>車番グループDTOの列挙</returns>
        Task<IEnumerable<ContactCarNumberGroupDto>> PrintSyabanNotifyAsync(int companyId, int branchId);
    }
}
