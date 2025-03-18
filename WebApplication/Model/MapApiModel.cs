using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApplication.Model
{
    /// <summary>
    /// 製品クラス
    /// </summary>
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        /// <summary>
        /// 製品情報を表示します。
        /// </summary>
        /// <param name="product">製品情報</param>
        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}\tPrice: " + $"{product.Price}\tCategory: {product.Category}");
        }

        /// <summary>
        /// 製品を作成します。
        /// </summary>
        /// <param name="product">製品情報</param>
        /// <returns>作成された製品のURI</returns>
        static async Task<Uri> CreateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/products", product);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        /// <summary>
        /// 製品情報を取得します。
        /// </summary>
        /// <param name="path">製品のパス</param>
        /// <returns>製品情報</returns>
        static async Task<Product> GetProductAsync(string path)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                //product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        /// <summary>
        /// 非同期で実行します。
        /// </summary>
        /// <returns></returns>
        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Product product = new Product
                {
                    Name = "Gizmo",
                    Price = 100,
                    Category = "Widgets"
                };

                var url = await CreateProductAsync(product);
                Console.WriteLine($"Created at {url}");

                // Get the product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Update the product
                //Console.WriteLine("Updating price...");
                //product.Price = 80;
                //await UpdateProductAsync(product);

                // Get the updated product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                //// Delete the product
                //var statusCode = await DeleteProductAsync(product.Id);
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 汎用結果クラス
    /// </summary>
    public class GenericResult
    {
        public string ErrrMessage { get; set; } = null;

        public Latlon Latlon { get; set; }
    }

    /// <summary>
    /// ドライブリストクラス
    /// </summary>
    public class DriveList
    {
        public string ErrrMessage { get; set; } = null;

        public List<DriveListItem> item { get; set; }

        public DriveListItem route { get; set; }
    }

    /// <summary>
    /// ドライブリストアイテムクラス
    /// </summary>
    public class DriveListItem
    {

        //[JsonProperty("routeID")]
        [Display(Name = "ルートID")]
        public string routeID { get; set; }

        //[JsonProperty("type")]
        [Display(Name = "ルート区分")]
        public string routeType { get; set; }

        //[JsonProperty("distance")]
        public double distance { get; set; }

        //[JsonProperty("toll")]
        public double toll { get; set; }

        //[JsonProperty("invalidFee")]
        public bool invalidFee { get; set; }

        //[JsonProperty("time")]
        public int time { get; set; }

        //[JsonProperty("line")]
        public List<Latlon> line { get; set; }

        public List<Link> link { get; set; }

        public List<DetailedTime> detailedTime { get; set; }

        public bool passage { get; set; }

        //[JsonProperty("vicsTimeStamp")]
        public string vicsTimeStamp { get; set; }

        public string mPointsOrder { get; set; }


    }

    /// <summary>
    /// 緯度経度クラス
    /// </summary>
    public class Latlon
    {
        [JsonProperty("lat")]
        public string lat { get; set; }

        [JsonProperty("lng")]
        public string lng { get; set; }
    }

    /// <summary>
    /// 詳細時間クラス
    /// </summary>
    public class DetailedTime
    {
        public bool time { get; set; }

        public bool linkOffset { get; set; }

        public bool linkLength { get; set; }
    }

    /// <summary>
    /// ポイントフラグクラス
    /// </summary>
    public class Pointflg
    {
        public bool ic { get; set; }

        public bool jct { get; set; }

        public bool sa { get; set; }

        public bool pa { get; set; }
    }

    /// <summary>
    /// 画像URLクラス
    /// </summary>
    public class Imageurl
    {
        public string url { get; set; }

        public string type { get; set; }
    }

    /// <summary>
    /// 施設名クラス
    /// </summary>
    public class FacilityName
    {
        public string type { get; set; }

        public string name { get; set; }
    }

    /// <summary>
    /// 施設情報クラス
    /// </summary>
    public class FacilityInfo
    {
        public string name { get; set; }

        public List<FacilityInfoType> types { get; set; }

    }

    /// <summary>
    /// 施設情報タイプクラス
    /// </summary>
    public class FacilityInfoType
    {
        public string type { get; set; }
    }

    /// <summary>
    /// 規制クラス
    /// </summary>
    public class Regulation
    {
        public double height { get; set; }

        public double width { get; set; }

        public double weight { get; set; }

        public double load { get; set; }
    }

    /// <summary>
    /// ガイダンスクラス
    /// </summary>
    public class Guidance
    {

        public string guidancecode { get; set; }

        public string pointName { get; set; }

        public string routeName { get; set; }

        public string directionName { get; set; }

        public Pointflg pointflg { get; set; }

        public List<Imageurl> imageurl { get; set; }

    }

    /// <summary>
    /// リンククラス
    /// </summary>
    public class Link
    {

        public string roadType { get; set; }

        public bool tollFlag { get; set; }

        public double toll { get; set; }

        public double distance { get; set; }

        public List<Latlon> line { get; set; }

        public Guidance guidance { get; set; }

        public string linkID { get; set; }

        public List<FacilityName> facilityName { get; set; }

        public List<FacilityInfo> facilityInfo { get; set; }

        public Regulation regulation { get; set; }

        public double passagelink { get; set; }

    }

    /// <summary>
    /// 地図住所クラス
    /// </summary>
    public class MapAddress
    {
        public string ErrrMessage { get; set; } = null;

        public int hit { get; set; }

        public List<MapAddressItem> item { get; set; }
    }

    /// <summary>
    /// 地図住所アイテムクラス
    /// </summary>
    public class MapAddressItem
    {

        public string address_code { get; set; }

        public string address { get; set; }

        public string address_read { get; set; }

        public string address_read_through { get; set; }

        public string address_code1 { get; set; }

        public string address1 { get; set; }

        public string address_read1 { get; set; }

        public string address_read_through1 { get; set; }

        public string address_code2 { get; set; }

        public string address2 { get; set; }

        public string address_read2 { get; set; }

        public string address_read_through2 { get; set; }

        public string address_code3 { get; set; }

        public string address3 { get; set; }

        public string address_read3 { get; set; }

        public string address_read_through3 { get; set; }

        public string address_code4 { get; set; }

        public string address4 { get; set; }

        public string address_read4 { get; set; }

        public string address_read_through4 { get; set; }

        public string address_code5 { get; set; }

        public string address5 { get; set; }

        public string address_read5 { get; set; }

        public string address_read_through5 { get; set; }

        public string address_detail_code1 { get; set; }

        public string address_detail1 { get; set; }

        public string address_detail_read1 { get; set; }

        public string address_detail_read_through1 { get; set; }

        public string address_detail_code2 { get; set; }

        public string address_detail2 { get; set; }

        public string address_detail_read2 { get; set; }

        public string address_detail_read_through2 { get; set; }

        public string post_code { get; set; }

        public string address_level { get; set; }

        public string address_name_type { get; set; }

        public int exclude_level_flag { get; set; }

        public int child_level_flag { get; set; }

        public string entrance_point { get; set; }

        public Latlon position { get; set; }

        public double distance { get; set; }

        public List<Map_Building_NameItem> BuildingNameItemList { get; set; }

        public Map_Building_NameItem BuildingNameItem { get; set; }

    }

    /// <summary>
    /// 地図建物名クラス
    /// </summary>
    public class Map_Building_Name
    {
        public string ErrrMessage { get; set; } = null;

        public List<Map_Building_NameItem> item { get; set; }

        public int hit { get; set; }
    }

    /// <summary>
    /// 地図建物名アイテムクラス
    /// </summary>
    public class Map_Building_NameItem
    {

        public string zid { get; set; }

        public string zid_attr { get; set; }

        public string id { get; set; }

        public string id_attr { get; set; }

        public string building_name { get; set; }

        public string name { get; set; }

        public string name_read { get; set; }

        public string name_read_through { get; set; }

        public string post_code { get; set; }

        public string address_code { get; set; }

        public string address_level { get; set; }

        public string address { get; set; }

        public int building_type { get; set; }

        public int building_top_floor_num { get; set; }

        public int building_bottom_floor_num { get; set; }

        public Latlon position { get; set; }

        public string address2 { get; set; }

        public string address3 { get; set; }

        public string address4 { get; set; }

    }

    /// <summary>
    /// ゼンリン地図APIクラス
    /// </summary>
    public class ZenrinMapAPI
    {

        private readonly MapApiSettings _mapApiSettiong;

        public ZenrinMapAPI(MapApiSettings mapApiSettiong)
        {
            _mapApiSettiong = mapApiSettiong;
        }



        /// <summary>
        /// 自動車ルート候補一覧取得
        /// 緯度経度から自動車の経路候補を取得します。
        /// </summary>
        /// <param name="from">出発地点</param>
        /// <param name="to">到着地点</param>
        /// <param name="waypoint">経由地点</param>
        /// <param name="searchparam">検索挙動変更</param>
        /// <param name="height">車高</param>
        /// <param name="width">車幅</param>
        /// <param name="weight">車重</param>
        /// <param name="departuretime">出発時刻指定</param>
        /// <param name="regulationtype">詳細車種</param>
        /// <param name="tolltype">料金車種</param>
        /// <param name="smartic">スマートIC利用指定</param>
        /// <param name="regulation">規制考慮</param>
        /// <param name="twouturn">2段階Uターン回避指定</param>
        /// <param name="ferry">フェリー考慮指定</param>
        /// <param name="datum">測地系</param>
        /// <returns></returns>
        public async  Task<DriveList> GetDriveSerchList(string from, string to,
                                                        string waypoint = null,
                                                        int searchparam = 1,
                                                        double height = 0, double width = 0, double weight = 0,
                                                        string fromtype = "",
                                                        string totype = "",
                                                        string waypointtype = "",
                                                        string departuretime = null,string regulationtype = "121100", string tolltype = "large", 
                                                        string smartic = "true", string timerestriction = "season,time",string twouturn = "false", string ferry = "false",
                                                        string datum = "JGD")
        {

            string url = _mapApiSettiong.WebUri.WebAPI + "/api/zips/general/route_mbn/drive_list?";
            url += string.Format("from={0}&to={1}", from, to);
            if (waypoint != null) { url += string.Format("&waypoint={0}", waypoint); }
            if (height > 0) { url += string.Format("&height={0}", height); }
            if (width > 0) { url += string.Format("&width={0}", width); }
            if (weight > 0) { url += string.Format("&weight={0}", weight); }
            if (departuretime != null) { url += string.Format("&departure_time={0}", departuretime); }
            if (fromtype != null && fromtype != "") { url += string.Format("&from_type={0}", fromtype); }
            if (totype != null && totype != "") { url += string.Format("&to_type={0}", totype); }
            if (waypointtype != null && waypointtype != "") { url += string.Format("&waypoint_type={0}", waypointtype); }
            url += string.Format("&searchparam={0}", searchparam);
            url += string.Format("&regulation_type={0}", regulationtype);
            url += string.Format("&toll_type={0}", tolltype);
            url += string.Format("&smartic={0}", smartic);
            url += string.Format("&time_restriction={0}", timerestriction);
            url += string.Format("&two_uturn={0}", twouturn);
            url += string.Format("&ferry={0}", ferry);
            url += string.Format("&datum={0}", datum);

            string aid = "";
            string responseBody = "";
            string errorMessage = null;
            DriveList driveList = new();

            using (System.Net.Http.HttpClient client = new())
                try
                {
                    if ("server999".Equals(_mapApiSettiong.EnvKubun))
                    {
                        url += string.Format("&zis_authtype=special&zis_authkey={0}", _mapApiSettiong.AuthAid.AuthCode);
                    }else
                    {
                        ValueTuple<string, string, dynamic> loginVal = await LoginAsync();
                        aid = loginVal.Item1;
                        string kid = loginVal.Item2;
                        dynamic func = loginVal.Item3;

                        if (aid == null) { throw new Exception("Loginエラー:" + kid); }

                        string lmtinf = Getlmtinf(func, "0010", "0008");

                        url += string.Format("&zis_authtype=aid&zis_aid={0}&zis_lmtinf={1}&zis_zips_authkey={2}", aid, lmtinf, kid);
                    }
                    
                    

                    using System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                    
                    //生のレスポンス全体を文字列で取得
                    responseBody = await response.Content.ReadAsStringAsync();

                    //レスポンスの文字列をJSONとして解析されたJObjectとして取得。
                    errorMessage = responseBody;
                    dynamic oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    errorMessage = null;

                    if (oResponse.status == "OK")
                    {
                        driveList.item = new();

                        dynamic funcList = oResponse.result.item;

                        foreach (dynamic element in funcList)
                        {

                            DriveListItem target = new()
                            {
                                routeID = element.route.route_id,
                                routeType = element.header.search_type,
                                distance = element.route.distance,
                                toll = element.route.toll,
                                invalidFee = element.route.toll_result,
                                time = element.route.time,
                                line = new(),
                            };

                            
                            var lineCoordinates = element.route.line.coordinates;

                            foreach (dynamic d in lineCoordinates)
                            {
                                Newtonsoft.Json.Linq.JArray jArray = (Newtonsoft.Json.Linq.JArray)d;

                                var lng = jArray[0];
                                var lat = jArray[1];

                                Latlon line = new()
                                {
                                    lat = lat.ToString(),
                                    lng = lng.ToString(),
                                };

                                target.line.Add(line);
                            }

                            //target.routeID = element.routeID;

                            driveList.item.Add(target);
                        }

                        Console.WriteLine(driveList);

                    } else
                    {
                        throw new Exception("検索が見つかりませんでした。");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(responseBody);
                    if (errorMessage == null)
                    {
                        errorMessage = e.Message;
                    }
                    driveList.ErrrMessage = errorMessage;
                    return driveList;
                }
                finally
                {
                    await LogoutAsync(aid);
                }

            return driveList;

        }

        /// <summary>
        /// 自動車ルート検索
        /// 自動車の経路を返却します。
        /// 機能２．自動車ルート候補詳細取得
        /// 自動車ルート候補(route3/drive_list)で出力されたルートID(routeID)を元に該当する自動車の経路を返却します。
        /// </summary>
        /// <param name="routeID">ルートID</param>
        /// <param name="datum">出力座標の測地系を指定</param>
        /// <param name="llunit">緯度経度形式</param>
        /// <returns></returns>
        public async Task<DriveList> GetDriveDetail(string routeID,
                                                string datum = "JGD",
                                                string llunit = "dec")
        {

            string url = _mapApiSettiong.WebUri.WebAPI + "/api/zips/general/route_mbn/guide?";
            url += string.Format("route_id={0}", routeID);
            url += string.Format("&datum={0}", datum);
            url += string.Format("&llunit={0}", llunit);

            string aid = "";
            string responseBody = "";
            string errorMessage = null;
            DriveList driveList = new();

            using (System.Net.Http.HttpClient client = new())
                try
                {
                    if ("server999".Equals(_mapApiSettiong.EnvKubun))
                    {
                        url += string.Format("&zis_authtype=special&zis_authkey={0}", _mapApiSettiong.AuthAid.AuthCode);
                    }
                    else
                    {
                        ValueTuple<string, string, dynamic> loginVal = await LoginAsync();
                        aid = loginVal.Item1;
                        string kid = loginVal.Item2;
                        dynamic func = loginVal.Item3;

                        if (aid == null) { throw new Exception("Loginエラー:" + kid); }

                        string lmtinf = Getlmtinf(func, "0010", "0010");

                        url += string.Format("&zis_authtype=aid&zis_aid={0}&zis_lmtinf={1}&zis_zips_authkey={2}", aid, lmtinf, kid);
                    }
                    

                    using System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);

                    //生のレスポンス全体を文字列で取得
                    responseBody = await response.Content.ReadAsStringAsync();

                    //レスポンスの文字列をJSONとして解析されたJObjectとして取得。
                    errorMessage = responseBody;
                    dynamic oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    errorMessage = null;

                    if (oResponse.status == "OK")
                    {

                        foreach(dynamic item in oResponse.result.item)
                        {

                            dynamic routeLink = item.route;

                            DriveListItem target = new()
                            {
                                routeID = routeID,
                                toll = routeLink.toll,
                                invalidFee = routeLink.toll_result,
                                distance = routeLink.distance,
                                time = routeLink.time,
                                link = new(),
                                detailedTime = new(),
                                line = new(),
                                passage = routeLink.passage,
                                //mPointsOrder = item.mPointsOrder,
                                routeType  = item.header.search_type,
                                vicsTimeStamp = item.header.vics_time_stamp,
                            };

                            foreach (dynamic l in routeLink.link)
                            {
                                Link link = new()
                                {
                                    roadType = l.road_type,
                                    tollFlag = l.toll_flag,
                                    toll = l.toll,
                                    distance = l.distance,
                                    line = new(),
                                    guidance = new(),
                                    linkID = l.link_id,
                                    facilityName = new(),
                                    facilityInfo = new(),
                                    regulation = new(),
                                    passagelink = l.passage_link,
                                };


                                dynamic lineCoordinates = l.line.coordinates;

                                foreach (dynamic d in lineCoordinates)
                                {
                                    Newtonsoft.Json.Linq.JArray jArray = (Newtonsoft.Json.Linq.JArray)d;

                                    var lng = jArray[0];
                                    var lat = jArray[1];

                                    Latlon latlon = new()
                                    {
                                        lat = lat.ToString(),
                                        lng = lng.ToString(),
                                    };


                                    link.line.Add(latlon);

                                }

                                if (l.guidance != null)
                                {
                                    dynamic d = l.guidance;

                                    Guidance guidance = new()
                                    {
                                        guidancecode = d.direction_code,
                                        pointName = d.point_name,
                                        routeName = d.road_name,
                                        directionName = d.direction_name,
                                        pointflg = new(),
                                        imageurl = new(),
                                    };

                                    if (l.point_flag != null)
                                    {
                                        Pointflg pointflg = new()
                                        {
                                            ic = d.point_flag.ic,
                                            jct = d.point_flag.jct,
                                            sa = d.point_flag.sa,
                                            pa = d.point_flag.pa,
                                        };
                                        guidance.pointflg = pointflg;
                                    }

                                    link.guidance = guidance;
                                };

                                foreach (dynamic d in l.facility_info)
                                {
                                    FacilityInfo facilityInfo = new()
                                    {
                                        name = d.name,
                                        types = new(),
                                    };

                                    foreach (dynamic t in d.type)
                                    {
                                        FacilityInfoType type = new()
                                        {
                                            type = t,
                                        };

                                        facilityInfo.types.Add(type);
                                    }

                                    link.facilityInfo.Add(facilityInfo);
                                }

                                if (l.regulation != null)
                                {
                                    foreach (dynamic d in l.regulation)
                                    {
                                        Regulation regulationEx = new();
                                        if (d.Name == "height" && d.Value != null)
                                        {
                                            regulationEx.height = (double)d.Value;
                                        }
                                        if (d.Name == "width" && d.Value != null)
                                        {
                                            regulationEx.width = (double)d.Value;
                                        }
                                        if (d.Name == "weight" && d.Value != null)
                                        {
                                            regulationEx.weight = (double)d.Value;
                                        }
                                        if (d.Name == "load" && d.Value != null)
                                        {
                                            regulationEx.load = (double)d.Value;
                                        }

                                        link.regulation = regulationEx;
                                    }
                                }

                                // LINK情報を追加
                                target.link.Add(link);

                            }

                            foreach (dynamic m in item.route.detailed_time)
                            {
                                DetailedTime detailedTime = new()
                                {
                                    time = m.time,
                                    linkOffset = m.link_offset,
                                    linkLength = m.link_length,
                                };
                                target.detailedTime.Add(detailedTime);
                            };

                            driveList.route = target;

                            Console.WriteLine(driveList);



                            break;
                        };







                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(responseBody);
                    if (errorMessage == null)
                    {
                        errorMessage = e.Message;
                    }
                    driveList.ErrrMessage = errorMessage;
                    return driveList;
                }
                finally
                {
                    await LogoutAsync(aid);
                }

            return driveList;

        }


        /// <summary>
        /// 住所検索
        /// </summary>
        /// <param name="word"></param>
        /// <param name="word_match_type"></param>
        /// <param name="address_code"></param>
        /// <param name="code_match_type"></param>
        /// <param name="proximity"></param>
        /// <param name="position"></param>
        /// <param name="address_level"></param>
        /// <param name="sort"></param>
        /// <param name="limit"></param>
        /// <param name="datum"></param>
        /// <returns></returns>
        public async Task<MapAddress> GetMapAddress(string word = null,
                                                int word_match_type = 3,
                                                string address_code = null,
                                                int code_match_type = 1,
                                                string proximity = null,
                                                string position = null,
                                                string address_level = "TOD, SHK, OAZ, AZC, GIK, TBN",
                                                string sort = "address_sort",
                                                string limit = "0,50",
                                                string datum = "JGD"
                                                )
        {

            string url = _mapApiSettiong.WebUri.WebAPI + "/api/zips/general/address?";


            string param = "";
            if (word != null) { param += string.Format("&word={0}", word); }
            if (address_code != null) { param += string.Format("&address_code={0}", address_code); }
            if (proximity != null) { param += string.Format("&proximity={0}", proximity); }
            if (position != null) { param += string.Format("&position={0}", position); }

            param += string.Format("&word_match_type={0}", word_match_type);
            param += string.Format("&code_match_type={0}", code_match_type);

            param += string.Format("&address_level={0}", address_level);
            param += string.Format("&sort={0}", sort);
            param += string.Format("&limit={0}", limit);
            param += string.Format("&datum={0}", datum);

            param = param[1..];
            url += param;

            string aid = "";
            string responseBody = "";
            string errorMessage = null;
            MapAddress Result = new();
            Result.item = new();

            using (System.Net.Http.HttpClient client = new())
                try
                {
                    if ("server999".Equals(_mapApiSettiong.EnvKubun))
                    {
                        url += string.Format("&zis_authtype=special&zis_authkey={0}", _mapApiSettiong.AuthAid.AuthCode);
                    }
                    else
                    {
                        ValueTuple<string, string, dynamic> loginVal = await LoginAsync();
                        aid = loginVal.Item1;
                        string kid = loginVal.Item2;
                        dynamic func = loginVal.Item3;

                        if (aid == null) { throw new Exception("Loginエラー:" + kid); }

                        string lmtinf = Getlmtinf(func, "0002", "0001");

                        url += string.Format("&zis_authtype=aid&zis_aid={0}&zis_lmtinf={1}&zis_zips_authkey={2}", aid, lmtinf, kid);
                    } 

                    using System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);

                    //生のレスポンス全体を文字列で取得
                    responseBody = await response.Content.ReadAsStringAsync();

                    //レスポンスの文字列をJSONとして解析されたJObjectとして取得。
                    errorMessage = responseBody;
                    dynamic oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    errorMessage = null;

                    if (oResponse.status == "OK")
                    {
                        Result.hit = oResponse.result.info.hit;

                        if (Result.hit == 0)
                        {
                            Result.ErrrMessage = "住所の検索結果取得エラー";
                            return Result;
                        }

                        dynamic route = oResponse.result.item;

                        foreach (dynamic d in route)
                        {

                            if (Result.hit == 1 || (Result.hit > 1 && d.post_code != null)) { 


                            MapAddressItem target = new()
                            {
                                address_code = d.address_code,
                                address = d.address,
                                address_read = d.address_read,
                                address_read_through = d.address_read_through,
                                address_code1 = d.address_code1,
                                address1 = d.address1,
                                address_read1 = d.address_read1,
                                address_read_through1 = d.address_read_through1,
                                address_code2 = d.address_code2,
                                address2 = d.address2,
                                address_read2 = d.address_read2,
                                address_read_through2 = d.address_read_through2,
                                address_code3 = d.address_code3,
                                address3 = d.address3,
                                address_read3 = d.address_read3,
                                address_read_through3 = d.address_read_through3,
                                address_code4 = d.address_code4,
                                address4 = d.address4,
                                address_read4 = d.address_read4,
                                address_read_through4 = d.address_read_through4,
                                address_code5 = d.address_code5,
                                address5 = d.address5,
                                address_read5 = d.address_read5,
                                address_read_through5 = d.address_read_through5,
                                address_detail_code1 = d.address_detail_code1,
                                address_detail1 = d.address_detail1,
                                address_detail_read1 = d.address_detail_read1,
                                address_detail_read_through1 = d.address_detail_read_through1,
                                address_detail_code2 = d.address_detail_code2,
                                address_detail2 = d.address_detail2,
                                address_detail_read2 = d.address_detail_read2,
                                address_detail_read_through2 = d.address_detail_read_through2,
                                post_code = d.post_code,
                                address_level = d.address_level,
                                address_name_type = d.address_name_type,
                                exclude_level_flag = d.exclude_level_flag,
                                child_level_flag = d.child_level_flag,
                                //entrance_point = d.entrance_point,
                                //position = d.position,
                                //distance = d.distance,
                            };

                            if (d.position != null)
                            {
                                dynamic gg = d.position;
                                for (int i = 0; i < gg.Count; i += 2)
                                {
                                    Latlon latlon = new()
                                    {
                                        lat = gg[i + 1],
                                        lng = gg[i],
                                    };

                                    target.position = new();
                                    target.position = latlon;
                                }
                            }

                            if (d.distance != null)
                            {
                                target.distance = d.distance;
                            }

                            Result.item.Add(target);
                            }
                        }


                        List<MapAddressItem> tbnList = Result.item.Where(m => m.address_level == "TBN").ToList();

                        if (tbnList.Count > 0)
                        {
                            foreach (MapAddressItem target in tbnList)
                            {
                                Map_Building_Name builName = await GetBuilding_Name(target.address_code, null);
                                if (builName != null && builName.ErrrMessage == null)
                                {
                                    target.BuildingNameItemList = builName.item;
                                }
                            }
                        }



                        Console.WriteLine(Result);

                    } else
                    {
                        Result.ErrrMessage = oResponse.message;

                        return Result;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(responseBody);
                    if (errorMessage == null)
                    {
                        errorMessage = e.Message;
                    }
                    Result.ErrrMessage = errorMessage;

                    return Result;
                }
                finally
                {
                    await LogoutAsync(aid);
                }

            return Result;

        }

        /// <summary>
        /// 座標変換
        /// 日本測地系⇒世界測地系の相互変換、
        /// </summary>
        /// <param name="latlon"></param>
        /// <returns></returns>
        public async Task<GenericResult> GetConvert_CrsForJpnToWorld(Latlon latlon)
        {
            string url = _mapApiSettiong.WebUri.WebAPI + "/api/zips/general/convert_crs?";
            url += string.Format("positions={0},{1}", latlon.lng, latlon.lat);
            url += string.Format("&in_crs={0}", "EPSG:4301");
            url += string.Format("&out_crs={0}", "EPSG:4612");

            string aid = "";
            string responseBody = "";
            string errorMessage = null;
            GenericResult result = new();

            using (System.Net.Http.HttpClient client = new())
                try
                {
                    if ("server999".Equals(_mapApiSettiong.EnvKubun))
                    {
                        url += string.Format("&zis_authtype=special&zis_authkey={0}", _mapApiSettiong.AuthAid.AuthCode);
                    }
                    else
                    {
                        ValueTuple<string, string, dynamic> loginVal = await LoginAsync();
                        aid = loginVal.Item1;
                        string kid = loginVal.Item2;
                        dynamic func = loginVal.Item3;

                        if (aid == null) { throw new Exception("Loginエラー:" + kid); }

                        string lmtinf = Getlmtinf(func, "0009", "0001");

                        url += string.Format("&zis_authtype=aid&zis_aid={0}&zis_lmtinf={1}&zis_zips_authkey={2}", aid, lmtinf, kid);
                    }



                    using System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);

                    //生のレスポンス全体を文字列で取得
                    responseBody = await response.Content.ReadAsStringAsync();

                    //レスポンスの文字列をJSONとして解析されたJObjectとして取得。
                    errorMessage = responseBody;
                    dynamic oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    errorMessage = null;

                    var aa = oResponse.status;

                    dynamic gg = oResponse.result.item;

                    foreach (dynamic tt in gg)
                    {
                        for (int i = 0; i < tt.Count; i += 2)
                        {
                            result.Latlon = new()
                            {
                                lat = tt[i + 1],
                                lng = tt[i],
                            };
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(responseBody);
                    if (errorMessage == null)
                    {
                        errorMessage = e.Message;
                    }
                    result.ErrrMessage = errorMessage;
                    return result;
                }
                finally
                {
                    await LogoutAsync(aid);
                }

            return result;

        }


        /// <summary>
        /// 建物・テナント名称検索
        /// </summary>
        /// <param name="address_code"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task<Map_Building_Name> GetBuilding_Name(string address_code, string word)
        {
            string url = _mapApiSettiong.WebUri.WebAPI + "/api/service/building_name?";
            url += string.Format("building_type={0}", "3");
            if (address_code != null) { url += string.Format("&address_code_list={0}", address_code); }
            if (word != null) { url += string.Format("&word_match_type=3&word={0}", word); }

            string aid = "";
            string responseBody = "";
            string errorMessage = null;
            Map_Building_Name result = new();
            result.item = new();

            using (System.Net.Http.HttpClient client = new())
                try
                {
                    if ("server999".Equals(_mapApiSettiong.EnvKubun))
                    {
                        url += string.Format("&zis_authtype=special&zis_authkey={0}", _mapApiSettiong.AuthAid.AuthCode);
                    }
                    else
                    {
                        ValueTuple<string, string, dynamic> loginVal = await LoginAsync();
                        aid = loginVal.Item1;
                        string kid = loginVal.Item2;
                        dynamic func = loginVal.Item3;

                        if (aid == null) { throw new Exception("Loginエラー:" + kid); }

                        string lmtinf = Getlmtinf(func, "0002", "0020");

                        url += string.Format("&zis_authtype=aid&zis_aid={0}&zis_lmtinf={1}&zis_zips_authkey={2}", aid, lmtinf, kid);
                    }



                    using System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);

                    //生のレスポンス全体を文字列で取得
                    responseBody = await response.Content.ReadAsStringAsync();

                    //レスポンスの文字列をJSONとして解析されたJObjectとして取得。
                    errorMessage = responseBody;
                    dynamic oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    errorMessage = null;

                    if (oResponse.status == "OK")
                    {
                        result.hit = oResponse.result.info.hit;

                        if (result.hit == 0)
                        {
                            result.ErrrMessage = "住所の検索結果取得エラー";
                            return result;
                        }

                        dynamic route = oResponse.result.item;

                        foreach (dynamic d in route)
                        {
                            Map_Building_NameItem target = new()
                            {
                                zid = d.zid,
                                zid_attr = d.zid_attr,
                                id = d.id,
                                id_attr = d.id_attr,
                                building_name = d.building_name,
                                name = d.name,
                                name_read = d.name_read,
                                name_read_through = d.name_read_through,
                                post_code = d.post_code,
                                address_code = d.address_code,
                                address_level = "TBN",
                                address = d.address,
                                building_type = d.building_type,
                                building_top_floor_num = d.building_top_floor_num,
                                building_bottom_floor_num = d.building_bottom_floor_num,
                            };

                            string str = target.address;
                            Match matche = Regex.Match(str, "[都道府県]");
                            string todoufuken = matche.Value;

                            target.address2 = target.address.Substring(0, target.address.IndexOf(todoufuken) + 1);

                            if (d.position != null)
                            {
                                dynamic gg = d.position;
                                for (int i = 0; i < gg.Count; i += 2)
                                {
                                    Latlon latlon = new()
                                    {
                                        lat = gg[i + 1],
                                        lng = gg[i],
                                    };

                                    target.position = new();
                                    target.position = latlon;
                                }
                            }

                            result.item.Add(target);
                        }

                        Console.WriteLine(result);

                    }
                    else
                    {
                        result.ErrrMessage = oResponse.message;

                        return result;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(responseBody);
                    if (errorMessage == null)
                    {
                        errorMessage = e.Message;
                    }
                    result.ErrrMessage = errorMessage;
                    return result;
                }
                finally
                {
                    await LogoutAsync(aid);
                }

            return result;

        }



        /************************************************************************
         ************************** 共通処理*************************************
         ************************************************************************/

        /// <summary>
        /// ゼンリン地図ログイン処理
        /// </summary>
        /// <returns></returns>
        private async Task<ValueTuple<string, string, dynamic>> LoginAsync()
        {
            ValueTuple<string, string, dynamic> result = new();

            string url = string.Format(_mapApiSettiong.WebUri.WebAPI + "/api/auth/login?user_id={0}&password={1}&service_id={2}&device_flag=1", _mapApiSettiong.AuthAid.Uid, _mapApiSettiong.AuthAid.Pwd, _mapApiSettiong.AuthAid.Sid);

            using System.Net.Http.HttpClient client = new();
            System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);

            //生のレスポンス全体を文字列で取得
            var responseBody = await response.Content.ReadAsStringAsync();

            //レスポンスの文字列をJSONとして解析されたJObjectとして取得。
            dynamic oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

            if (oResponse.status.code == "10100000")
            {
                // 成功
                //レスポンスに含まれる各値を取得。
                string cid = oResponse.result.cid;
                string aid = oResponse.result.aid;
                string kid = oResponse.result.kid;
                var funcList = oResponse.result.items.func;

                result.Item1 = aid;
                result.Item2 = kid;
                result.Item3 = funcList;

                return result;

            }
            else
            {
                // エラー
                Console.WriteLine(oResponse.status.text);

                result.Item1 = null;
                result.Item2 = oResponse.status.text;
                result.Item3 = null;

                return result;
            }

        }

        /// <summary>
        /// ゼンリン地図ログアウト処理
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        private  async Task LogoutAsync(string aid)
        {

            string url = string.Format(_mapApiSettiong.WebUri.WebAPI + "/api/auth/logout?aid={0}", aid);

            using System.Net.Http.HttpClient client = new();
            using System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);

            //生のレスポンス全体を文字列で取得
            string responseBody = await response.Content.ReadAsStringAsync();

            //レスポンスの文字列をJSONとして解析されたJObjectとして取得。
            dynamic oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

            if (oResponse.status.code == "10300000")
            {
                // 成功
                Console.WriteLine(oResponse.status.text);
            }
            else
            {
                // エラー
                Console.WriteLine(oResponse.status.text);
            }
        }

        /// <summary>
        /// ゼンリン地図ログイン処理の為の機能コードを返却する
        /// ログインAPI(login)にて取得した「機能コード(id)」「機能サブコード(subid)」をキーにして「機能情報(funcInfo)」を取得します。
        /// </summary>
        /// <param name="func"></param>
        /// <param name="func_id"></param>
        /// <param name="func_subid"></param>
        /// <returns></returns>
        private static string Getlmtinf(dynamic func, string func_id, string func_subid)
        {
            foreach (var element in func)
            {
                if (element.id == func_id && element.subid == func_subid)
                {
                    return element.areaCode + "," + element.funcInfo;
                }
            }
            return null;
        }
    }




}
