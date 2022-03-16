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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        List<Service> ServiceStart = Classes.BaseClass.Base.Service.ToList();
        public UserPage()
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
            foreach(Service s in DT)
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
            if (discount==0)
            {
                tb.Text = costr+" рублей";
            }
            else
            { 
                double costd = costr;
                costd = costr-costd * discount;
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
            if (discount==0)
            {
                tb.Text = "";
            }
            else
            {
                tb.Text = "* скидка "+discount*100 + " %";
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
        List<Service> ServiceFilter;
        private void Filters()

        {
            ServiceFilter = ServiceStart;
            if (!string.IsNullOrWhiteSpace(TBFilterService.Text))
            {
                ServiceFilter = ServiceFilter.Where(x => x.Title.ToLower().Contains(TBFilterService.Text.ToLower())).ToList();
            }
            LVServices.ItemsSource = ServiceFilter;
        }
        private void TBFilterService_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filters();
        }
    }
}
