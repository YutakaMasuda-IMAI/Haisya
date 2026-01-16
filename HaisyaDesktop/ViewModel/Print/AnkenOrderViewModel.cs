using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace HaisyaDesktop.ViewModel.Print
{




    class AnkenOrderViewModel : BaseViewModel
    {

        /// <summary>宛先情報</summary>
        public ReactiveProperty<string> AtesakiDisplay { get; set; }
        // <summary>送り主担当者</summary>
        public ReactiveProperty<string> SendNameDisplay { get; set; }

        // <summary>車種・台数</summary>
        public ReactiveProperty<string> SyasyuSyabanDaisuuDisplay { get; set; }
        // <summary>装備品</summary>
        public ReactiveProperty<string> EquipmentDisplay { get; set; }
        // <summary>トラック積み荷</summary>
        public ReactiveProperty<FlowDocument> TruckLoadDocument { get; set; }
        public ReactiveProperty<string> TruckLoadDisplay { get; set; }

        // <summary>積み日時場所</summary>
        public ReactiveProperty<FlowDocument> TsumiDocument { get; set; }
        public ReactiveProperty<string> TsumiDisplay { get; set; }
        public ReactiveProperty<string> TsumiTitle1Display { get; set; }
        public ReactiveProperty<string> TsumiTitle2Display { get; set; }

        // <summary>卸し日時場所</summary>
        public ReactiveProperty<FlowDocument> OroshiDocument { get; set; }
        public ReactiveProperty<string> OroshiDisplay { get; set; }
        public ReactiveProperty<string> OroshiTitle1Display { get; set; }
        public ReactiveProperty<string> OroshiTitle2Display { get; set; }

        // <summary>特記事項</summary>
        public ReactiveProperty<FlowDocument> SpecialNotesDocument { get; set; }
        public ReactiveProperty<string> SpecialNotesDisplay { get; set; }
        public ReactiveProperty<string> SpecialNotesTitle1Display { get; set; }
        public ReactiveProperty<string> SpecialNotesTitle2Display { get; set; }

        public Models.AnkenModel.AnkenDataModelDto Dto { get; set; }

        public AnkenOrderViewModel()
        {
  
        }


        public AnkenOrderViewModel(int AnkenId)
        {
            
            AtesakiDisplay = new() { Value = "" };
            SendNameDisplay = new() { Value = "" };
            SyasyuSyabanDaisuuDisplay = new() { Value = "" };
            EquipmentDisplay = new() { Value = "" };

            TruckLoadDocument = new();
            TruckLoadDisplay = new() { Value = "" };
            //TruckLoadDocument = CreateFlowDoc("FlowDocument in VM");

            TsumiDocument = new();
            TsumiDisplay = new() { Value = "" };
            TsumiTitle1Display = new() { Value = "引取日時" };
            TsumiTitle2Display = new() { Value = "場所" };
            //TsumiDocument = CreateFlowDoc("FlowDocument in VM");
            //TsumiTitle1Display = "引取日時";
            //TsumiTitle2Display = "場所";

            OroshiDocument = new();
            OroshiDisplay = new() { Value = "" };
            OroshiTitle1Display = new() { Value = "納品日時" };
            OroshiTitle2Display = new() { Value = "場所" };
            //OroshiDocument = CreateFlowDoc("FlowDocument in VM");
            //OroshiTitle1Display = "納品日時";
            //OroshiTitle2Display = "場所";

            SpecialNotesDocument = new();
            SpecialNotesDisplay = new() { Value = "" };
            SpecialNotesTitle1Display = new() { Value = "特記事項" };
            SpecialNotesTitle2Display = new() { Value = "" };
            //SpecialNotesDocument = CreateFlowDoc("FlowDocument in VM");
            //SpecialNotesTitle1Display = "特記事項";
            //SpecialNotesTitle2Display = "";

            GetAnkenData(AnkenId);

        }

        private async void GetAnkenData(int paramAnkenId)
        {

            Dto = new()
            {
                T_Anken = new()
                ,T_Anken_Detail = new()
                ,T_Anken_PointList = new()
            };

            try
            {

                API.WebApp.AnkenDataApi api = new();
                Dto = await api.GetAnkenData(paramAnkenId);

                // <summary>車種・台数</summary>
                SyasyuSyabanDaisuuDisplay.Value = Dto.T_Anken_Detail.SyasyuDisplay + "　×　" + Dto.T_Anken_Detail.Daisuu + "台";
                Dto.M_CompanyUser_Local user = Context.ContextManager.Instance.companyUserList.FirstOrDefault(m => m.User_ID == Dto.T_Anken_Detail.TantouID);
                SendNameDisplay.Value = user.Last_Name + "　" + user.First_Name;


                Dto.T_Anken_Point_Local point;

                // <summary>積み日時場所</summary>
                point = Dto.T_Anken_PointList.Where(m => m.Kubun == 1).ToList()[0];
                TsumiDocument.Value = new FlowDocument(GetPointParagraph(point));

                // <summary>卸し日時場所</summary>
                point = Dto.T_Anken_PointList.Where(m => m.Kubun == 9).ToList()[Dto.T_Anken_PointList.Where(m => m.Kubun == 9).ToList().Count - 1];
                OroshiDocument.Value = new FlowDocument(GetPointParagraph(point, Dto.T_Anken_Detail.CheckOroshiSpace));

            }
            catch (Exception ex)
            {
                throw;
            } finally
            {

            }

        } 


        private Paragraph GetPointParagraph(Dto.T_Anken_Point_Local point, bool oroshiCheckFlg = false)
        {
            string sVal = "";
            Paragraph paragraph = new Paragraph();

            if (point.PointTime == "00:00")
            {
                sVal = DateTime.Parse(point.PointDate?.ToString("yyyy/MM/dd") + " 　" + point.PointTime).ToString("yyyy年MM月dd日 dddd");
            }
            else
            {
                DateTime dt = DateTime.Parse(point.PointDate?.ToString("yyyy/MM/dd") + " 　" + point.PointTime);
                if (dt.ToString("mm") == "00")
                {
                    sVal = dt.ToString("yyyy年MM月dd日   dddd   HH時");
                }
                else
                {
                    sVal = dt.ToString("yyyy年MM月dd日   dddd   HH時mm分");
                }
                if (point.PointTimeKubun != null)
                {
                    sVal += PublicObjects.PointComboBoxItemsTimeKubun().FirstOrDefault(m => m.Id == point.PointTimeKubun).Name;
                }
            }
            paragraph.Inlines.Add(new Run(sVal) { FontWeight = System.Windows.FontWeights.Bold });
            paragraph.Inlines.Add(new Run("\r\n"));

            sVal = "";

            if (point.BuildingName != null)
            {
                sVal += point.BuildingName + "   ";
            }
            if (point.Post_code != null)
            {
                sVal += "〒" + point.Post_code + " ";
            }
            if (point.Address != null)
            {
                sVal += point.Address;
            }
            paragraph.Inlines.Add(new Run(sVal));

            if (oroshiCheckFlg == true)
            {
                sVal = "※詳細は積地でご確認ください。";
                paragraph.Inlines.Add(new Run("\r\n"));
                paragraph.Inlines.Add(new Run(sVal));
            }


            //paragraph.Inlines.Add(new Run(sVal));
            return paragraph;
        }


        private static FlowDocument CreateFlowDoc(string innerText)
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run("FixText_"));
            paragraph.Inlines.Add(new Run("\r\n"));
            paragraph.Inlines.Add(new Run(innerText) { Foreground = new SolidColorBrush(Colors.BlueViolet) });
            return new FlowDocument(paragraph);
        }



    }
}
