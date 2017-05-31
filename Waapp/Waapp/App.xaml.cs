using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Waapp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private static database _database;
        public static database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new database(DependencyService.Get<IFileHelper>().GetLocalFilePath("WeatherWaapp.db3"));
                }
                return _database;
            }
        }
    }
}
