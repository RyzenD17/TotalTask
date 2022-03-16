using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace TotalTask.Forms
{
    /// <summary>
    /// Логика взаимодействия для NewRegistrationPage.xaml
    /// </summary>
    public partial class NewRegistrationPage : Page
    {
        private Service _newreg;
        ClientService clientserv = new ClientService();
        bool flag=false;
        public NewRegistrationPage(Service NewReg)
        {
            InitializeComponent();
            _newreg = NewReg;
            TBServiceName.Text = _newreg.Title;
            string fio = "";
            List<Client> C = Classes.BaseClass.Base.Client.ToList();

            for(int i =0;i<C.Count;i++)
            {
                fio = C[i].LastName + " " + C[i].FirstName + " " + C[i].Patronymic;
                CBClients.Items.Add(fio);
            }
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index =  _newreg.ID;
            List<Service> DT = Classes.BaseClass.Base.Service.Where(x => x.ID == index).ToList();
            int minutes = 0;
            foreach (Service s in DT)
            {
                minutes = s.DurationInSeconds / 60;
            }
            tb.Text = "Продолжительность курса - " + minutes + " минут";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.MainFrame.Navigate(new AdminPage());
        }

        private void AddReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int gtime = 0;
                int clientid = CBClients.SelectedIndex + 1;
                int serviceid = _newreg.ID;
               
                Regex r = new Regex("^[0-1]{1}[0-9]{1}:[0-5]{1}[0-9]{1}$");
                if (r.IsMatch(TBxStartTime.Text) == false) { }
                else { gtime += 1; }
                r = new Regex("^[2]{1}[0-3]{1}:[0-5]{1}[0-9]{1}$");
                if (r.IsMatch(TBxStartTime.Text) == false) { }
                else { gtime += 1; }
                if (gtime==0)
                {
                    MessageBox.Show("Некорректное время", "Запись");
                }
                if (gtime>=1)
                {
                    DateTime dt = Convert.ToDateTime(DPDate.SelectedDate);
                    dt = dt.Add(TimeSpan.Parse(TBxStartTime.Text));
                    if (dt < DateTime.Now)
                    {
                        MessageBox.Show("Запись не добавлена!", "Запись");
                    }
                    else
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        clientserv.ClientID = clientid; clientserv.ServiceID = serviceid; clientserv.StartTime = dt;
                        Classes.BaseClass.Base.ClientService.Add(clientserv);
                        Classes.BaseClass.Base.SaveChanges();
                        MessageBox.Show("Данные записаны!", "Запись");
                        Classes.FrameClass.MainFrame.Navigate(new AdminPage());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Запись не добавлена!", "Запись");
            }
        }
    }
}
