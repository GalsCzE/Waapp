using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waapp
{
    public class Core2
    {
        public static string tempe;
        public static double tempnumber;
        public static string t;
        public static string w;
        public static string h;
        private static double tempe2;
        private static double tempefinally2;
        private static double windyfinally2;
        private static double windy2;

        public static async Task<Weather> GetWeather(string zipCodeEntry, string zipCodeEntry2)
        {
            //Sign up for a free API key at http://openweathermap.org/appid
            /* string key = "54b076df3448cc8f33ea4d7305761f54";
             string queryString = "http://api.openweathermap.org/data/2.5/weather?zip="
                 + zipCode + "&appid=" + key;*/
            string key = "05d457135af22713aeb4675fa0e889ff";
            string queryString = "https://api.darksky.net/forecast/" + key + "/" + zipCodeEntry + "," + zipCodeEntry2;

            var results = await DataService.getDataFromService(queryString).ConfigureAwait(false);


            if (results["currently"] != null)
            {
                t = (string)results["currently"]["temperature"];
                w = (string)results["currently"]["windSpeed"];
                h = (string)results["currently"]["humidity"];

                tempe2 = Convert.ToDouble(t);
                tempefinally2 = (5.0 / 9.0) * (tempe2 - 32.0);
                tempefinally2 = (double)((int)(tempefinally2 * 10.0)) / 10.0;

                windy2 = Convert.ToDouble(w);
                windyfinally2 = (windy2 * 1.609344);
                windyfinally2 = (double)((int)(windyfinally2 * 10.0)) / 10.0;
                /*tempe = (string)results["currently"]["temperature"];
                tempnumber = Int32.Parse(tempe) - 17;*/

                Weather weather2 = new Weather()
                {
                    Title = (string)results["timezone"],
                    Temperature = tempefinally2.ToString(),
                    Wind = windy2.ToString(),
                    Humidity = h + "% Vlhkost",
                    Icon = (string)results["currently"]["icon"],
                    Sirka = (string)results["latitude"],
                    Delka = (string)results["longitude"],
                };
                //DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                //DateTime nowtime = time.AddSeconds((double)results["currently"]["time"]);
                weather2.Time = DateTime.Now.ToString("HH:mm:ss");
                return weather2;
            }
            else
            {
                return null;
            }
        }
    }
}
