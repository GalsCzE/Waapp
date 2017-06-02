using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Waapp
{
    public class Weather
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Unique]
        public string Title { get; set; }
        public string Temperature { get; set; }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public string Icon { get; set; }
        public string Time { get; set; }
        public string Sirka { get; set; }
        public string Delka { get; set; }

        public Weather()
        {
            //Because labels bind to these values, set them to an empty string to
            //ensure that the label appears on all platforms by default.
            this.Title = " ";
            this.Temperature = " ";
            this.Wind = " ";
            this.Humidity = " ";
            this.Icon = " ";
            this.Time = " ";
            this.Sirka = " ";
            this.Delka = " ";

        }

        public override string ToString()
        {
            return string.Format(Title);
        }
    }

}
