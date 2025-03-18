using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static HaisyaWeb.Models.MapApiModel;

namespace HaisyaWeb.Dto
{
    public partial class T_Anken_Local : WebApplication.Data.T_Anken
    {
    }

    public partial class T_Anken_Detail_Local : WebApplication.Data.T_Anken_Detail
    {
    }

    public partial class T_Anken_Publish_Local : WebApplication.Data.T_Anken_Publish
    {
    }

    public partial class T_Anken_OyaKokyaku_Local : WebApplication.Data.T_Anken_OyaKokyaku
    {
    }

    public partial class T_Anken_Point_Local : WebApplication.Data.T_Anken_Point
    {
    }

    public partial class T_Anken_Excharge_Local : WebApplication.Data.T_Anken_Excharge
    {
    }

    public partial class T_Anken_Luggage_Local : WebApplication.Data.T_Anken_Luggage
    {
    }

    public partial class T_Anken_Equipment_Local : WebApplication.Data.T_Anken_Equipment
    {
    }

    public partial class T_Anken_Riyounso_Local : WebApplication.Data.T_Anken_Riyounso
    {
    }

    public partial class T_Anken_Riyounso_Point_Local : WebApplication.Data.T_Anken_Riyounso_Point
    {
    }

    public partial class T_Anken_Display_Local : WebApplication.Data.T_Anken_Display
    {
    }

    public partial class T_Point_Local : WebApplication.Data.T_Point
    {
    }

    public partial class T_Anken_SyabanRenraku_Local : WebApplication.Data.T_Anken_SyabanRenraku
    {
    }

    /// <summary>
    /// 案件データリスト
    /// </summary>
    public partial class V_AnkenDataList_Local : WebApplication.Data.V_AnkenDataList
    {

        public V_AnkenDataList_Local() { }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="list">コピー元の案件データリスト</param>
        public V_AnkenDataList_Local(Dto.V_AnkenDataList_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.V_AnkenDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }

        /// <summary> 配車データ </summary>
        public V_HaisyaDataList_Local HaisyaData { get; set; }
    }

    /// <summary>
    /// 得意先データリスト
    /// </summary>
    public class V_Tokuisaki_Local : WebApplication.Data.V_Tokuisaki
    {

        public V_Tokuisaki_Local() { }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="list">コピー元の得意先データリスト</param>
        public V_Tokuisaki_Local(Dto.V_Tokuisaki_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.V_AnkenDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }

    }

    public class V_TokuisakiForNotConnect_Local : WebApplication.Data.V_TokuisakiForNotConnect {}

    public class V_YosyasakiForNotConnect_Local : WebApplication.Data.V_YosyasakiForNotConnect { }

    /// <summary>
    /// T_Anken_Point登録用DTO
    /// </summary>
    public class PointDto_Local : WebApplication.Model.PointDto
    {
        public Map_Building_NameItem_Local BuildingNameItem { get; set; }

        /// <summary>ポイント日時</summary>
        public DateTime? PointDateTime { get; set; }
    }

    /// <summary>
    /// ルート検索リスト(画面表示用) 
    /// </summary>
    public class DriveRouteListDisplay_Local : WebApplication.Model.AnkenModel.DriveRouteListDisplay
    {
    }

    public class AnkenExchargeDto_Local : WebApplication.Model.AnkenModel.AnkenExchargeDto
    {
    }
}
