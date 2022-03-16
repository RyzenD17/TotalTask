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

namespace TotalTask.Forms
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            LVServices.ItemsSource = Classes.BaseClass.Base.Service.ToList();
        }
        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<Service> DT = Classes.BaseClass.Base.Service.Where(x => x.ID == index).ToList();
            int minutes = 0;
            foreach (Service s in DT)
            {
                minutes = s.DurationInSeconds / 60;
            }
            tb.Text = " за " + minutes + " минут";
        }

        private void TextBlock_Loaded_1(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<Service> DT = Classes.BaseClass.Base.Service.Where(x => x.ID == index).ToList();
            int costr = 0; double discount = 0;
            foreach (Service c in DT)
            {
                discount = Convert.ToDouble(c.Discount);
            }
            foreach (Service c in DT)
            {
                costr = Convert.ToInt32(c.Cost);
            }
            if (discount == 0)
            {
                tb.Text = costr + " рублей";
            }
            else
            {
                double costd = costr;
                costd = costr - costd * discount;
                tb.Text = costd + " рублей";
            }


        }

        private void TextBlock_Loaded_2(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<Service> DT = Classes.BaseClass.Base.Service.Where(x => x.ID == index).ToList();
            double discount = 0;
            foreach (Service c in DT)
            {
                discount = Convert.ToDouble(c.Discount);
            }
            if (discount == 0)
            {
                tb.Text = "";
            }
            else
            {
                tb.Text = "* скидка " + discount * 100 + " %";
            }
        }

        private void TextBlock_Loaded_3(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<Service> DT = Classes.BaseClass.Base.Service.Where(x => x.ID == index).ToList();
            int costr = 0; double discount = 0;
            foreach (Service c in DT)
            {
                discount = Convert.ToDouble(c.Discount);
            }
            foreach (Service c in DT)
            {
                costr = Convert.ToInt32(c.Cost);
            }
            if (discount != 0)
            {
                tb.Text = costr + " рублей ";
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.MainFrame.Navigate(new Forms.CreateOrUpdate());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            int id = Convert.ToInt32(B.Uid);
            Service ServiceUpdate = Classes.BaseClass.Base.Service.FirstOrDefault(x => x.ID == id);
            Classes.FrameClass.MainFrame.Navigate(new Forms.CreateOrUpdate(ServiceUpdate));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            int id = Convert.ToInt32(B.Uid);
            Service ServiceDelete = Classes.BaseClass.Base.Service.FirstOrDefault(x => x.ID == id);
            Classes.BaseClass.Base.Service.Remove(ServiceDelete);
            Classes.BaseClass.Base.SaveChanges();
            Classes.FrameClass.MainFrame.Navigate(new AdminPage());
            MessageBox.Show("Запись удалена!", "Удаление");
        }

        private void NewApp_Click(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            int id = Convert.ToInt32(B.Uid);
            Service NewReg = Classes.BaseClass.Base.Service.FirstOrDefault(x => x.ID == id);
            Classes.FrameClass.MainFrame.Navigate(new NewRegistrationPage(NewReg));
        }

        private void btnShowEntries_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.MainFrame.Navigate(new UpcomingEntries());
        }
    }
}
