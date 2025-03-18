using PartnerWeb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerWeb.Context
{
    public class AddressList
    {


        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<M_PostCode_Local> HokkaidoAddressItem { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<M_PostCode_Local> TohokuAddressItem { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<M_PostCode_Local> ChubuAddressItem { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<M_PostCode_Local> KantoAddressItem { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<M_PostCode_Local> KinkiAddressItem { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<M_PostCode_Local> ChugokuAddressItem { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<M_PostCode_Local> ShikokuAddressItem { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<M_PostCode_Local> KyusyuAddressItem { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<M_PostCode_Local> OkinawaAddressItem { set; get; }

    }

    
}
