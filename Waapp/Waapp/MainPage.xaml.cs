using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.Diagnostics;

namespace Waapp
{
    public partial class MainPage : ContentPage
    {
        public double tempe;
        public double tempefinally;
        public double windy;
        public double windyfinally;
        List<Weather> weathers = new List<Weather>();
        public MainPage()
        {
            InitializeComponent();
            this.Title = "Waapp";
            getWeatherBtn.Clicked += GetWeatherBtn_Clicked;
            weathers = App.Database.GetItemsAsync().Result;
            foreach (Weather w in weathers)
            {
                Debug.WriteLine("       ");
                Debug.WriteLine(w.Title);
                Debug.WriteLine("       ");
            }
            if(weathers.Count < 1)
            {
                Debug.WriteLine("       ");
                Debug.WriteLine("Prazdno");
                Debug.WriteLine("       ");
            }


            //Set the default binding to a default object for now
            this.BindingContext = new Weather();
        }


        private async void GetWeatherBtn_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(zipCodeEntry.Text))
            {
                Weather weather = await Core.GetWeather(zipCodeEntry.Text, zipCodeEntry2.Text);
                // try
                //{
                // await DisplayAlert("TEMPE", weather.Temperature, "OK");
                tempe = Convert.ToDouble(weather.Temperature);
                // }
                //catch
                //{
                //await DisplayAlert("TEMPE2", tempe.ToString(), "OK");
                tempefinally = (5.0 / 9.0) * (tempe - 32.0);
                tempefinally = (double)((int)(tempefinally * 10.0)) / 10.0;

                windy = Convert.ToDouble(weather.Wind);
                windyfinally = (windy * 1.609344);
                windyfinally = (double)((int)(windyfinally * 10.0)) / 10.0;

                // }
                if (weather == null)
                {
                    this.BindingContext = weather;
                    getWeatherBtn.Text = "Počasí";
                    await DisplayAlert("Jmeno", zipCodeEntry.Text + " " + zipCodeEntry2.Text, "OK");
                }
                else
                {
                    if (weather.Icon == "clear-day")
                    {
                        weatherr.Source = "sun.png";
                    }
                    else if (weather.Icon == "rain")
                    {
                        weatherr.Source = "rain.jpg";
                    }
                    else if (weather.Icon == "partly-cloudy-day")
                    {
                        weatherr.Source = "cloud_sun.png";
                    }

                    if (tempefinally >= 20.00)
                    {
                        tempLabel.TextColor = Color.Red ;
                    }
                    else if (tempefinally <= 10.00)
                    {
                        tempLabel.TextColor = Color.Blue;
                    }
                    else
                    {
                        tempLabel.TextColor = Color.Black;
                    }

                    getWeatherBtn.Text = "Hledat znova";
                    locationLabel.Text = weather.Title;
                    tempLabel.Text = tempefinally.ToString() + " °C";
                    windLabel.Text = windyfinally.ToString() + " km/h";
                    humidityLabel.Text = weather.Humidity + "% Vlhkost";
                    iconLabel.Text = weather.Icon;
                    timeLabel.Text = weather.Time;
                    sirkaLabel.Text = weather.Sirka;
                    delkaLabel.Text = weather.Delka;
                    speed.Source = "wind_speed.jpg";
                    last.Text = "Poslední update:";
                    hum.Source = "humidity.png";
                    // await DisplayAlert("Jmeno", weather.Title + " " + weather.Temperature + " " + weather.Wind + " " + weather.Humidity, "ok");
                    // await DisplayAlert("Jmeno", weather.Title + " " + weather.Temperature + " " + weather.Wind + " " + weather.Humidity, "ok");
                }
            } //TADY JSEM PŘESTAL PRACOVAT MINUEL
        }

        private void datab_Clicked(object sender, EventArgs e)
        {
            if (locationLabel.Text != "" || tempLabel.Text != "" || humidityLabel.Text != "" || iconLabel.Text != "" || timeLabel.Text != "")
            {
                weathers = App.Database.GetItemsAsync().Result;
                if(weathers.Count < 1)
                {
                    Weather v = new Weather();
                    v.Title = locationLabel.Text;
                    v.Temperature = tempLabel.Text;
                    v.Wind = windLabel.Text;
                    v.Humidity = humidityLabel.Text;
                    v.Icon = iconLabel.Text;
                    v.Time = timeLabel.Text;
                    v.Sirka = sirkaLabel.Text;
                    v.Delka = delkaLabel.Text;
                    App.Database.SaveItemAsync(v);
                } else
                {
                    foreach (Weather w in weathers)
                    {
                        if (locationLabel.Text == w.Title)
                        {
                            break;
                        }
                        else
                        {
                            Weather v = new Weather();
                            v.Title = locationLabel.Text;
                            v.Temperature = tempLabel.Text;
                            v.Wind = windLabel.Text;
                            v.Humidity = humidityLabel.Text;
                            v.Icon = iconLabel.Text;
                            v.Time = timeLabel.Text;
                            App.Database.SaveItemAsync(v);
                        }
                    }
                }
                
                
            }
            else
            {
                DisplayAlert("", "Jsou prázdné hodnoty, zkus vyhledat znovu.", "OK");
            }

        }

        private void datap_ClickedAsync(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Save());
        }
    }
}
