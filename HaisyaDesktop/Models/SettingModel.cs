using HaisyaDesktop.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.Models
{
    class SettingModel: CommonModel
    {


        public class SyaryoSizeListDto
        {
            public List<M_SyaryoSize_Local> M_SyaryoSizeList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class SyaryoListDto
        {
            public List<M_Syaryo_Local> M_SyaryoList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }

    }
}
