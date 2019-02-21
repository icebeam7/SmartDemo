using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media.Abstractions;
using SmartDemo.Services;
using SmartDemo.Helpers;
using System.Linq;

namespace SmartDemo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnalyzePicturePage : ContentPage
	{
		public AnalyzePicturePage ()
		{
			InitializeComponent ();
		}

        MediaFile foto;

        void Loading(bool mostrar)
        {
            indicator.IsEnabled = mostrar;
            indicator.IsRunning = mostrar;
        }

        private async void AnalyzeButton_Clicked(object sender, EventArgs e)
        {
            if (foto != null)
            {
                try
                {
                    Loading(true);

                    // Fase 2 - Vision
                    var descripcion = await VisionService.DescribePicture(foto);
                    DescriptionLabel.Text = descripcion.Description.Captions.First().Text;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Excepción: " + ex.Message, "OK");
                }
                finally
                {
                    Loading(false);
                }
            }
            else
                await DisplayAlert("Error", "Debes tomar la fotografía", "OK");
        }

        private async void SelectButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Loading(true);

                foto = await ImageService.TakePicture();
                if (foto != null)
                    image.Source = ImageSource.FromStream(foto.GetStream);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Excepción: " + ex.Message, "OK");
            }
            finally
            {
                Loading(false);
            }
        }
    }
}