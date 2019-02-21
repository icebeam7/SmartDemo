using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartDemo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

        private async void AnalyzeTweets_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TwitterPage());
        }

        private async void AnalyzePicture_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AnalyzePicturePage());
        }
    }
}