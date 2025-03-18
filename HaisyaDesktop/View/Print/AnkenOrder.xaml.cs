using HaisyaDesktop.ViewModel.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HaisyaDesktop.View.Print
{
    /// <summary>
    /// AnkenOrder.xaml の相互作用ロジック
    /// </summary>
    public partial class AnkenOrder : Window
    {
        public AnkenOrder()
        {
            InitializeComponent();

            //AnkenOrderViewModel vm = new();
            //DataContext = vm;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }


        private AnkenOrderViewModel VModel => (AnkenOrderViewModel)DataContext;

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            //TextRange range;
            //範囲を指定した文字列の取得 
            // トラック積み荷
            FlowDocument nimotsu = this.Nimotsu.Document;
            VModel.TruckLoadDisplay.Value = new TextRange(nimotsu.ContentStart, nimotsu.ContentEnd).Text;
            // 積み日時場所
            FlowDocument tsumi = this.Tsumi.Document;
            VModel.TsumiDisplay.Value = new TextRange(tsumi.ContentStart, tsumi.ContentEnd).Text;
            // 卸し日時場所
            FlowDocument oroshi = this.Oroshi.Document;
            VModel.OroshiDisplay.Value = new TextRange(oroshi.ContentStart, oroshi.ContentEnd).Text;
            // 特記事項
            FlowDocument specialNotes = this.SpecialNotes.Document;
            VModel.SpecialNotesDisplay.Value = new TextRange(specialNotes.ContentStart, specialNotes.ContentEnd).Text;

            // 印刷ダイアログを表示します。
            PrintDialog printDialog = new();
            bool? result = printDialog.ShowDialog();

            // 印刷ボタン以外が押下された場合、処理を終了します。
            if (!result.HasValue || !result.Value) return;

            // 印刷データを生成します。            
            Report_AnkenOrderViewModel vm = new(VModel);
            View.Print.Report_AnkenOrder userControl = new();
            userControl.DataContext = vm;

            Canvas canvas = new();
            Canvas.SetTop(userControl, 80);
            Canvas.SetLeft(userControl, 60);
            Canvas.SetRight(userControl, 60);
            Canvas.SetBottom(userControl, 50);
            canvas.Children.Add(userControl);

            FixedPage page = new();
            page.Children.Add(canvas);

            // 印刷します。
            PrintQueue queue = printDialog.PrintQueue;

            PrintTicket ticket = queue.DefaultPrintTicket;
            ticket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);      //A4
            ticket.PageOrientation = PageOrientation.Portrait;


            System.Windows.Xps.XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(queue);
            writer.Write(page);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
