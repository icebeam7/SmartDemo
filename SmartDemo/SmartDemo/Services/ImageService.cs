using System;
using System.Threading.Tasks;

using Plugin.Media;
using Plugin.Media.Abstractions;

namespace SmartDemo.Services
{
    public static class ImageService
    {
        public static async Task<MediaFile> TakePicture()
        {
            MediaFile photo = null;

            try
            {
                await CrossMedia.Current.Initialize();

                photo = await CrossMedia.Current.PickPhotoAsync();

                /*if (CrossMedia.Current.IsCameraAvailable || CrossMedia.Current.IsTakePhotoSupported)
                {
                    foto = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = "Pictures",
                        Name = "emotion.jpg"
                    });
                }*/
            }
            catch (Exception ex)
            {

            }

            return photo;
        }
    }
}
