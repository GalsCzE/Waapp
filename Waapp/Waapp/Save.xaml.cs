using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Waapp
{
    public partial class Save : ContentPage
    {
        List<Weather> we = new List<Weather>();
        public Save()
        {
            InitializeComponent();
            we = App.Database.GetItemsAsync().Result;
            data.ItemsSource = we;


        }

        private void data_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new Bann(e.SelectedItem as Weather));
        }

        private void back_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }


       
}
