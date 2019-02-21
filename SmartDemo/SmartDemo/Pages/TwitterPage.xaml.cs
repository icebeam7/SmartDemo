using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SmartDemo.Services;
using SmartDemo.Models;

namespace SmartDemo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TwitterPage : ContentPage
	{
		public TwitterPage ()
		{
			InitializeComponent ();
		}

        void Loading(bool mostrar)
        {
            indicator.IsEnabled = mostrar;
            indicator.IsRunning = mostrar;
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            var busqueda = SearchEntry.Text;

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                Loading(true);
                var tweets = await TwitterService.GetTweets(busqueda);
                TweetsListView.ItemsSource = tweets;
                Loading(false);
            }
            else
            {
                await DisplayAlert("Warning", "Please type something to search", "OK");
            }
        }

        private async void TweetsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var tweet = (Tweet)e.SelectedItem;
                await Navigation.PushAsync(new AnalyzeTweetPage(tweet));
            }
            catch (Exception ex)
            {
            }
        }

    }
}