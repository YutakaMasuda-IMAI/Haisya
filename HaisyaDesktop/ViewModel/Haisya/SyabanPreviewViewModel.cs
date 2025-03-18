using HaisyaDesktop.ViewModel.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HaisyaDesktop.ViewModel.Haisya
{
    class SyabanPreviewViewModel:BaseViewModel
    {

        public ICommand OpenSubWindowCommandSendMail { get; private set; }

        public System.Windows.Window ThisView { get; set; }

        public SyabanPreviewViewModel()
        {


            OpenSubWindowCommandSendMail = CreateCommand(v =>
            {
                App app = App.Current as App;
                View.Tool.MailSend win = (View.Tool.MailSend)app.ShowModalView(new MailSendViewModel(), ThisView);
                if (win == null)
                {

                }
                else
                {

                }
            });


        }



    }
}
