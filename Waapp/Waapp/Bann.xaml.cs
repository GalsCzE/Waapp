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

        public Bann(Weather w)
        {
            InitializeComponent();

            weather = w;

            /*tempe2 = Convert.ToDouble(w.Temperature);
            tempefinally2 = (5.0 / 9.0) * (tempe2 - 32.0);
            tempefinally2 = (double)((int)(tempefinally2 * 10.0)) / 10.0;

            windy2 = Convert.ToDouble(w.Wind);
            windyfinally2 = (windy2 * 1.609344);
            windyfinally2 = (double)((int)(windyfinally2 * 10.0)) / 10.0;*/

            titel.Text = w.Title;
            temp.Text = w.Temperature; //tempefinally.ToString();
            stupne.Text = w.Temperature;
            wind.Text = w.Wind; //windyfinally.ToString();
            humidity.Text = w.Humidity;
            icon.Text = w.Icon;
            time.Text = w.Time;
            
        }

        private void delete_Clicked(object sender, EventArgs e)
        {
            Weather l = weather;
            App.Database.DeleteItemAsync(l);
            Navigation.PushAsync(new Save());
        }

       private async void update_ClickedAsync()
        {
            Debug.WriteLine("            ");
            Debug.WriteLine("     AKTUALIZACE       ");
            Debug.WriteLine("            ");
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

            tempe = Convert.ToDouble(weather2.Temperature);
            // }
            //catch
            //{
            //await DisplayAlert("TEMPE2", tempe.ToString(), "OK");
            tempefinally = (5.0 / 9.0) * (tempe - 32.0);
            tempefinally = (double)((int)(tempefinally * 10.0)) / 10.0;

            windy = Convert.ToDouble(weather2.Wind);
            windyfinally = (windy * 1.609344);
            windyfinally = (double)((int)(windyfinally * 10.0)) / 10.0;

                titel.Text = weather2.Title;
                wind.Text = windyfinally.ToString() + " km/h";
                temp.Text = tempefinally.ToString() + " °C";
                stupne.Text = tempefinally.ToString() + " °C";
                humidity.Text = weather2.Humidity + "% Vlhkost";
                time.Text = weather2.Time;
                icon.Text = weather2.Icon;
                // await DisplayAlert("Jmeno", weather.Title + " " + weather.Temperature + " " + weather.Wind + " " + weather.Humidity, "ok");
                // await DisplayAlert("Jmeno", weather.Title + " " + weather.Temperature + " " + weather.Wind + " " + weather.Humidity, "ok");
        }

        private void update_Clicked(object sender, EventArgs e)
        {
            //update_ClickedAsync();
            update_ClickedAsync();
            Navigation.PushAsync(new Save());
        }

        private void back2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Save());
        }
    }

}
