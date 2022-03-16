using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TotalTask.Forms
{
    /// <summary>
    /// Логика взаимодействия для UpcomingEntries.xaml
    /// </summary>
    public partial class UpcomingEntries : Page
    {
        DispatcherTimer disTimer = new DispatcherTimer();
        public bool flagColor;
        public UpcomingEntries()
        {
            InitializeComponent();
            DateTime dt = DateTime.Today;
            DateTime dt_2 = DateTime.Today.AddDays(2);
            List<ClientService> CS = Classes.BaseClass.Base.ClientService.Where(x => x.StartTime > dt && x.StartTime<dt_2).OrderBy(x=>x.StartTime).ToList();
            LVClientService.ItemsSource = CS;
            disTimer.Interval = new TimeSpan(0, 0, 30);
            disTimer.Tick += new EventHandler(DisTimer_Tick);
            disTimer.Start();
        }

        private void DisTimer_Tick(object sender,EventArgs e)
        {
            LVClientService.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.MainFrame.Navigate(new AdminPage());
        }

        private void TBService_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            Service TitleServ = Classes.BaseClass.Base.Service.FirstOrDefault(x => x.ID == index);
            tb.Text = TitleServ.Title;
        }

        private void TBClient_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            Client client = Classes.BaseClass.Base.Client.FirstOrDefault(x => x.ID == index);
            tb.Text = client.LastName + " " + client.FirstName + " " + client.Patronymic;
        }

        private void TBEmail_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            Client client = Classes.BaseClass.Base.Client.FirstOrDefault(x => x.ID == index);
            tb.Text = "Электронная почта: "+client.Email;
        }

        private void TBPhone_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            Client client = Classes.BaseClass.Base.Client.FirstOrDefault(x => x.ID == index);
            tb.Text = "Телефон: " + client.Phone;
        }

        private void TBTimeLeft_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            ClientService CS = Classes.BaseClass.Base.ClientService.FirstOrDefault(x => x.ID == index);
            DateTime dt = CS.StartTime;
            DateTime dtT = DateTime.Now;
            int hoursleft = dtT.Hour - dt.Hour; 
            int minuteleft = dtT.Minute - dt.Minute;
            int dayleft = dtT.Day - dt.Day;
            hoursleft = hoursleft + dayleft * 24;

            if (minuteleft <= 0&&hoursleft>=0)
            {
                minuteleft += 60;
            }
            tb.Text = "С начала прошло : " + hoursleft + " час(a/ов) " + minuteleft + " минут(ы)";

            if(hoursleft<0)
            {
                if(minuteleft<0)
                {
                    minuteleft *= -1;
                }
                else
                {
                    minuteleft = 60 - minuteleft;
                    hoursleft = hoursleft + 1;
                }
                hoursleft *= -1;     
                tb.Text = "До начала осталось : " + hoursleft + " час(а/ов) " + minuteleft + " минут(ы)";
                if (hoursleft == 0 && minuteleft <= 59)
                {
                    flagColor = true;
                    tb.Foreground = Brushes.Red;
                }
            }
            
        }
    }
}
