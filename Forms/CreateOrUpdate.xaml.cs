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
    /// Логика взаимодействия для CreateOrUpdate.xaml
    /// </summary>
    public partial class CreateOrUpdate : Page
    {
        bool flag;
        Service serv = new Service();

        public CreateOrUpdate()
        {
            InitializeComponent();
            flag = true;

        }
        public CreateOrUpdate(Service ServiceUpdate)
        {
            InitializeComponent();
            serv = ServiceUpdate;
            TBTitle.Text = serv.Title;
            TBCost.Text = Convert.ToString(serv.Cost);
            TBDuration.Text = Convert.ToString(serv.DurationInSeconds);
            TBComment.Text = serv.Description;
            TBDiscount.Text = Convert.ToString(serv.Discount);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.MainFrame.Navigate(new Forms.AdminPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TBTitle.Text;
                double cost = Convert.ToDouble(TBCost.Text);
                int duration = Convert.ToInt32(TBDuration.Text);
                string comment = TBComment.Text;
                double discount = Convert.ToDouble(TBDiscount.Text);
                string image = null;
                serv.Title = title; 
                serv.Cost = Convert.ToDecimal(cost); 
                serv.DurationInSeconds = Convert.ToInt32(duration); 
                serv.Description = comment; 
                serv.Discount = Convert.ToDouble(discount); 
                serv.MainImagePath = image;
                if(flag==true)
                {
                    MessageBox.Show("Данные записаны!" +serv.ID, "Запись");
                    Classes.BaseClass.Base.Service.Add(serv);
                }

                Classes.BaseClass.Base.SaveChanges();
                MessageBox.Show("Данные записаны!", "Запись");
                Classes.FrameClass.MainFrame.Navigate(new AdminPage());
            }
            catch
            {

                MessageBox.Show("Запись не добавлена!", "Запись");
            }
        }
    }
}
