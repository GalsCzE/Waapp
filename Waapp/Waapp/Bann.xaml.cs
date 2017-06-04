using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Waapp
{

    public partial class Bann : ContentPage
    {
        Weather weather;
        List<Weather> ws = new List<Weather>();
        Weather weather2;
        string jop;
        string nope;
        private double tempefinally;
        private double tempe;
        private double windy;
        private double windyfinally;
        private double tempe3;

        public Bann(Weather w)
        {
            InitializeComponent();

            weather = w;
            if (w.Icon == "clear-day")
            {
                weatherr2.Source = ImageSource.FromFile("sun.png");
            }
            else if (w.Icon == "rain")
            {
                weatherr2.Source = ImageSource.FromFile("rain.jpg");
            }
            else if (w.Icon == "partly-cloudy-day")
            {
                weatherr2.Source = ImageSource.FromFile("cloud_sun.png");
            }
            else if (weather.Icon == "cloudy")
            {
                weatherr2.Source = ImageSource.FromFile("cloud.png");
            }

            tempe3 = Convert.ToDouble(w.Temperature);
            /*tempefinally2 = (5.0 / 9.0) * (tempe2 - 32.0);
            tempefinally2 = (double)((int)(tempefinally2 * 10.0)) / 10.0;*/

            /*windy2 = Convert.ToDouble(w.Wind);
            windyfinally2 = (windy * 1.609344);
            windyfinally2 = (double)((int)(windyfinally2 * 10.0)) / 10.0;*/

            if (tempe3 >= 26.0)
             {
                 stupne.TextColor = Color.Red;
             }
             else if (tempe3 <= 9.0)
             {
                 stupne.TextColor = Color.Blue;
             }
             else
             {
                 stupne.TextColor = Color.Black;
             }

            titel.Text = w.Title;
            stupne.Text = w.Temperature + " °C";//tempefinally2.ToString();
            wind.Text = w.Wind + " km/h"; //windyfinally.ToString();
            humidity.Text = w.Humidity;
            time.Text = w.Time;
            Title = w.Title;
            speed2.Source = ImageSource.FromFile("wind_speed.jpg");
            hum2.Source = ImageSource.FromFile("humidity.png");
            last2.Text = "Poslední update:";
            
        }

        private void delete_Clicked(object sender, EventArgs e)
        {
            Weather l = weather;
            App.Database.DeleteItemAsync(l);
            Navigation.PushAsync(new Save());
        }

       private async void update_ClickedAsync()
        {
           // Debug.WriteLine(weather.Sirka + " " + weather.Delka);
            ws = App.Database.GetCategories(weather.Sirka, weather.Delka).Result;
            foreach (Weather wet in ws)
            {
                jop = wet.Sirka;
                nope = wet.Delka;
               //await DisplayAlert("", jop + nope, "OK");
            }
            Weather weather2 = await Core2.GetWeather(jop, nope);
            weather2.ID = weather.ID;
            await App.Database.SaveItemAsync(weather2);

               /* titel.Text = weather2.Title;
                wind.Text = windyfinally.ToString() + " km/h";
                stupne.Text = tempefinally.ToString() + " °C";
                humidity.Text = weather2.Humidity + "% Vlhkost";
                time.Text = weather2.Time;*/
                // await DisplayAlert("Jmeno", weather.Title + " " + weather.Temperature + " " + weather.Wind + " " + weather.Humidity, "ok");
        }

        private void update_Clicked(object sender, EventArgs e)
        {
            update_ClickedAsync();
            //update_ClickedAsync();
            DisplayAlert("", "Během několikati sekund se počasí aktualizuje.", "OK");
            Navigation.PushAsync(new MainPage());
            /*Debug.WriteLine("");
            Debug.WriteLine("Za tímhle se to vypínalo bez důvodu OPRAVIT!!!");
            Debug.WriteLine("");*/
            //Za tímhle se to vypínalo bez důvodu OPRAVIT!!!

        }

        private void back2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Save());
        }
    }

}
