using StemV5.Pages.View.Tabs.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StemV5.Pages.Control
{
 
    public class Calendar 
    {
        // Feilds
        //
        public int month, year;

        //Methods
        //
        public string GetDataOfDays(FlowLayoutPanel FLP_dayscontainer)
        {
            string clblDate = "";
                String monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                clblDate = monthName + "  " + year;
                //get frist day of month ...
                DateTime startofthemonth = new DateTime(year, month, 1);
                //get count days of month 
                int days = DateTime.DaysInMonth(year, month);

                //conver startofthemonth to integer .
                int daysoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

                // create blank user control 
                for (int i = 1; i < daysoftheweek; i++)
                {
                    UserControlBlank ucblank = new UserControlBlank();
                    FLP_dayscontainer.Controls.Add(ucblank);
                }
                //create user control for days
                for (int i = 1; i <= days; i++)
                {
                    UserControlDay ucDay = new UserControlDay();
                    ucDay.days(i);
                    FLP_dayscontainer.Controls.Add(ucDay);
                }
            return clblDate;
        }


    }
}
