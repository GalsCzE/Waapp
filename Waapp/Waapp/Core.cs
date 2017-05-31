using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waapp
{
    public class Core
    {
        public static string tempe;
        public static double tempnumber;
        public static async Task<Weather> GetWeather(string zipCodeEntry, string zipCodeEntry2)
        {
            //Sign up for a free API key at http://openweathermap.org/appid
            /* string key = "54b076df3448cc8f33ea4d7305761f54";
             string queryString = "http://api.openweathermap.org/data/2.5/weather?zip="
                 + zipCode + "&appid=" + key;*/
            string key = "05d457135af22713aeb4675fa0e889ff";
            string queryString = "https://api.darksky.net/forecast/"+ key+ "/" + zipCodeEntry + "," + zipCodeEntry2;

            var results = await DataService.getDataFromService(queryString).ConfigureAwait(false);
            if (results["currently"] != null)
            {
                /*tempe = (string)results["currently"]["temperature"];
                tempnumber = Int32.Parse(tempe) - 17;*/

                Weather weather = new Weather()
                {
                    Title = (string)results["timezone"],
                    Temperature = (string)results["currently"]["temperature"],
                    Wind = (string)results["currently"]["windSpeed"],
                    Humidity = (string)results["currently"]["humidity"],
                    Icon = (string)results["currently"]["icon"],
                    Sirka = (string)results["latitude"],
                    Delka = (string)results["longitude"],
                };
                //DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                //DateTime nowtime = time.AddSeconds((double)results["currently"]["time"]);
                weather.Time = DateTime.Now.ToString("HH:mm:ss");
                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}
