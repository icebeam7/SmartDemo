using SmartDemo.Models;
using SmartDemo.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartDemo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnalyzeTweetPage : ContentPage
	{
        Tweet tweet;

        public AnalyzeTweetPage(Tweet tweet)
        {
            InitializeComponent();
            this.tweet = tweet;
        }

        void Loading(bool show)
        {
            indicator.IsEnabled = show;
            indicator.IsRunning = show;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Loading(true);
            var tweetAnalytics = await TextAnalyticsService.AnalyzeTweet(tweet);
            Loading(false);
            BindingContext = tweetAnalytics;
        }
    }
}