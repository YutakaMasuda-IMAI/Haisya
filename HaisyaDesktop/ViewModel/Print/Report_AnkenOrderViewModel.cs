using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace HaisyaDesktop.ViewModel.Print
{
    class Report_AnkenOrderViewModel : AnkenOrderViewModel
    {

        public string PrintDate { set; get; }

        public Report_AnkenOrderViewModel(AnkenOrderViewModel model)
        {

            //CopyProperty(this, model);
            /// <summary>宛先情報</summary>
            //AtesakiDisplay = new() { Value = model.AtesakiDisplay.Value };
            AtesakiDisplay = model.AtesakiDisplay;
            // <summary>送り主担当者</summary>
            SendNameDisplay = model.SendNameDisplay;

            // <summary>車種・台数</summary>
            SyasyuSyabanDaisuuDisplay = model.SyasyuSyabanDaisuuDisplay;
            // <summary>装備品</summary>
            EquipmentDisplay = model.EquipmentDisplay;
            // <summary>トラック積み荷</summary>
            TruckLoadDocument = model.TruckLoadDocument;
            TruckLoadDisplay = model.TruckLoadDisplay;

            // <summary>積み日時場所</summary>
            TsumiDocument = model.TsumiDocument;
            TsumiDisplay = model.TsumiDisplay;
            TsumiTitle1Display = model.TsumiTitle1Display;
            TsumiTitle2Display = model.TsumiTitle2Display;

            // <summary>卸し日時場所</summary>
            OroshiDocument = model.OroshiDocument;
            OroshiDisplay = model.OroshiDisplay;
            OroshiTitle1Display = model.OroshiTitle1Display;
            OroshiTitle2Display = model.OroshiTitle2Display;

            // <summary>特記事項</summary>
            SpecialNotesDocument = model.SpecialNotesDocument;
            SpecialNotesDisplay = model.SpecialNotesDisplay;
            SpecialNotesTitle1Display = model.SpecialNotesTitle1Display;
            SpecialNotesTitle2Display = model.SpecialNotesTitle2Display;



            PrintDate = DateTime.Now.ToString(@"yyyy\年M\月d\日");

            //TsumiDocument = new() { Value = CreateFlowDoc(TsumiDisplay.Value) };
            //OroshiDocument = new() { Value = CreateFlowDoc(OroshiDisplay.Value) };
            //SpecialNotesDocument = new() { Value = CreateFlowDoc(SpecialNotesDisplay.Value) };
            //TruckLoadDocument = new() { Value = CreateFlowDoc(TruckLoadDisplay.Value) };

            TsumiDocument = new() { Value = CreateFlowDocForFirstRowBold(TsumiDisplay.Value) };
            OroshiDocument = new() { Value = CreateFlowDocForFirstRowBold(OroshiDisplay.Value) };
            SpecialNotesDocument = new() { Value = GetFlowDocument(SpecialNotesDisplay.Value) };
            TruckLoadDocument = new() { Value = GetFlowDocument(TruckLoadDisplay.Value) };
        }

        //private static FlowDocument CreateFlowDoc(string innerText)
        //{
        //    var paragraph = new Paragraph();
        //    paragraph.Inlines.Add(new Run("FixText_"));
        //    paragraph.Inlines.Add(new Run("&#10;"));
        //    //paragraph.Inlines.Add(new Run(innerText) { Foreground = new SolidColorBrush(Colors.BlueViolet) });
        //    return new FlowDocument(paragraph);
        //}

        private static FlowDocument GetFlowDocument(string innerText)
        {
            var paragraph = new Paragraph();
            string[] slist = innerText.Split("\r\n");

            for (int i = 0; i < slist.Count(); i++)
            {
                paragraph.Inlines.Add(new Run(slist[i]));
                if (i < slist.Count() - 1)
                {
                    paragraph.Inlines.Add(new Run("\r\n"));
                }
            }
            return new FlowDocument(paragraph);
        }

        private static FlowDocument CreateFlowDocForFirstRowBold(string innerText)
        {
            string[] slist = innerText.Split("\r\n");

            var paragraph = new Paragraph();

            for (int i = 0; i < slist.Count(); i++)
            {
                
                if (i == 0)
                {
                    paragraph.Inlines.Add(new Run(slist[i]) { FontWeight = System.Windows.FontWeights.Bold });
                } else
                {
                    paragraph.Inlines.Add(new Run(slist[i]));
                }
                

                if (i < slist.Count() - 1)
                {
                    paragraph.Inlines.Add(new Run("\r\n"));
                }

            }

            return new FlowDocument(paragraph);

        }

    }
}
