using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;

namespace PartnerWeb.Models
{
    public class MapApiModel
    {

        public class GenericResult
        {
            public string ErrrMessage { get; set; } = null;

            public Latlon Latlon { get; set; }
        }

        public partial class Map_Building_Name_Local : WebApplication.Model.Map_Building_Name
        {
        }

        public partial class Map_Building_NameItem_Local : WebApplication.Model.Map_Building_NameItem
        {
        }

        public partial class MapAddress_Local
        {
            public string ErrrMessage { get; set; } = null;

            public int hit { get; set; }

            public List<MapAddressItem_Local> item { get; set; }
        }

        public partial class MapAddressItem_Local : WebApplication.Model.MapAddressItem
        {

            public string Building_name { get; set; }

        }

        public partial class DriveListItemEx_Local : WebApplication.Model.DriveListItemEx2
        {

        }


        public partial class DriveList_Local
        {
            public string ErrrMessage { get; set; } = null;

            public List<DriveListItem_Local> item { get; set; }

            public DriveListItem_Local route { get; set; }
        }

        public partial class DriveListItem_Local : WebApplication.Model.DriveListItem
        {

        }

        public partial class DriveListEx_Local
        {
            public string ErrrMessage { get; set; } = null;

            public List<DriveListItemEx_Local> item { get; set; }

            public DriveListItemEx_Local route { get; set; }
        }

        public partial class DriveListItemEx_Local : WebApplication.Model.DriveListItemEx2
        {

        }

        public partial class Latlon : WebApplication.Model.Latlon
        {

        }

    }



}
