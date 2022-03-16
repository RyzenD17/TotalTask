using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TotalTask
{
    public partial class Service

    {
        public SolidColorBrush DiscountColor
        {
            get
            {
                Classes.BaseClass.Base.Service.Where(x => x.ID == ID);
                if (Discount > 0)
                {
                    return Brushes.LightGreen;
                }
                else
                {
                    return Brushes.White;
                }
           

            }
        }

        public byte[] Image
        {
            get
            {
                var path = MainImagePath;
                if(File.Exists(path))
                {
                    return File.ReadAllBytes(path);
                }
                return null ;
            }
        }


    }
}
