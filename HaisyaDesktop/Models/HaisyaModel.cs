using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.Models
{
    class HaisyaModel: BaseModel
    {

        public HaisyaModel()
        {

        }

    }

    public class Schedule
    {
        public string DriverID { get; set; }

        public string DriverName { get; set; }

    }

    public class SyabanRenraku
    {
        public int SelectKubun { get; set; }

        public string Status { get; set; }

        public int KokyakuId { get; set; }
        public string KokyakuAbbrName { get; set; }
        public string KokyakuName { get; set; }

        public int KokyakuTantouID { get; set; }
        public string KokyakuTantouName { get; set; }

        public string KokyakuPhoneNumber { get; set; }


        // 車番連絡期日
        public int NumberCommLimitKubunMin { get; set; }
        public DateTime NumberCommLimitDateMin { get; set; }
        public string NumberCommLimitTimeMin { get; set; }
        public string NumberCommLimitDateDisplay { get; set; }

        public int AnkenCount { get; set; }

        public int AnkenCommitCount { get; set; }

        public int AnkenTempCount { get; set; }
        public int AnkenTempCountFlg { get; set; }

        public int AnkenNonCount { get; set; }
        public int AnkenNonCountFlg { get; set; }

        public string RowColor { get; set; }
    }


    public class DriverList
    {
        
        public string Before_PointName { get; set; }

        public string Before_Address2 { get; set; }

        public string Before_Address3 { get; set; }

        public string Before_Address4 { get; set; }

        public string Before_Lng { get; set; }

        public string Before_Lat { get; set; }

        public string Syaryo_Sayban { get; set; }

        public string Syaryo_Sasyu { get; set; }

        public string Syaryo_Kata { get; set; }

        public string Syaryo_Sayban_Trailer { get; set; }

        public string Syaryo_Sasyu_Trailer { get; set; }

        public string Syaryo_Kata_Trailer { get; set; }

        public int Tantou_ID { get; set; }

    }


    public class AnkenDataListDto : Dto.V_AnkenDataList_Local
    {

        public string Driver_EMPLOYEE_NNMBER { get; set; }

        public string Driver_Name { get; set; }


    }

}
