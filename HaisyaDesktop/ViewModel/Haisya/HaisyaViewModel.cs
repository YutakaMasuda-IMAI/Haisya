using HaisyaDesktop.Behavior;
using HaisyaDesktop.Models;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static HaisyaDesktop.Models.AnkenModel;
using static HaisyaDesktop.Models.MapApiModel;

namespace HaisyaDesktop.ViewModel.Haisya
{
    class HaisyaViewModel: BaseViewModel
    {

        public ObservableCollection<AnkenInfo> AnkenInfoList { get; set; }

        public ObservableCollection<Schedule> ScheduleList { get; set; }

        public ObservableCollection<AnkenInfo> WorkingTimes { get; }

        public ReactiveProperty<string> MapsApiForJSUrl { get; set; }

        private DropAcceptDescription _description;
        public DropAcceptDescription Description
        {
            get { return this._description; }
            set
            {
                if (this._description == value)
                {
                    return;
                }
                this._description = value;

                this.RaisePropertyChanged(nameof(Description));
            }
        }

        public HaisyaViewModel()
        {

            AnkenInfoList = new();
            ScheduleList = new();

            string mapUrl = PublicObjects.GetMapsAPIHosts();
            MapsApiForJSUrl = new() { Value = mapUrl + "DesktopApp/Index?" + PublicObjects.GetWebViewFlgForString() };

            Temp();

            Description = new();
            Description.DragOver += Description_DragOver;
            Description.DragDrop += Description_DragDrop;

        }

        private void Description_DragDrop(System.Windows.DragEventArgs args)
        {
            if (args.Data.GetDataPresent(typeof(Schedule)))
            {
                var data = args.Data.GetData(typeof(Schedule)) as Schedule;
                var fe = args.OriginalSource as FrameworkElement;

                if (fe != null)
                {
                    if (fe.DataContext is AnkenInfo)
                    {
                        // リスト本体へドロップ
                    }
                    else if (fe.DataContext is Schedule)
                    {
                        // リスト項目へドロップ
                    }
                }
            }
        }

        private void Description_DragOver(System.Windows.DragEventArgs args)
        {
            if (args.AllowedEffects.HasFlag(DragDropEffects.Copy))
            {
                var fe = args.OriginalSource as FrameworkElement;
                Schedule aa = (Schedule)fe.DataContext;

                if (fe.DataContext is Schedule)
                {
                    return;
                }
            }
            args.Effects = DragDropEffects.None;
        }

        private void Temp()
        {
            AnkenInfoList.Add(new AnkenInfo
            {
                AnkenID = 1,
                KokyakuId = 1,
                KokyakuName = "〇〇工業",
                SyasyuDisplay = "8tW",
                TsumiFacilityName = "〇〇倉庫",
                TsumiAddress = "○○県○○市○○町",
                TsumiPoint = new Latlon() { lat = "35.5", lng = "140.0" },
                Daisuu = 1,
                TsumiDate = DateTime.Parse("2022/06/14 15:25"),
                OroshiDate = DateTime.Parse("2022/06/15 15:25"),
                Products = "鋼材"
            }); ;

            AnkenInfoList.Add(new AnkenInfo
            {
                AnkenID = 2,
                KokyakuId = 5,
                KokyakuName = "〇〇運輸",
                SyasyuDisplay = "8tW",
                TsumiFacilityName = "〇〇工業○○支店",
                TsumiAddress = "○○県○○市○○町",
                TsumiPoint = new Latlon() { lat = "35.3", lng = "139.7" },
                Daisuu = 2,
                TsumiDate = DateTime.Parse("2022/06/14 13:35"),
                OroshiDate = DateTime.Parse("2022/06/15 13:35"),
                Products = "コイル"
            });


            for (int i = 1; i < 20; i++)
            {
                ScheduleList.Add(new Schedule
                {
                    DriverID = i.ToString(),
                    DriverName = "山田　太郎" + i.ToString(),
                });
            }


        }


        
    }






}
