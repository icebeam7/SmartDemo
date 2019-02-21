using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using SmartDemo.Helpers;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace SmartDemo.Services
{
    public static class VisionService
    {
        public async static Task<ImageAnalysis> DescribePicture(MediaFile photo)
        {
            ImageAnalysis description = null;

            try
            {
                if (photo != null)
                {
                    using (var stream = photo.GetStream())
                    {
                        var credentials = new ApiKeyServiceClientCredentials(Constants.VisionApiKey);
                        var client = new System.Net.Http.DelegatingHandler[] { };

                        var visionClient = new ComputerVisionClient(credentials, client);
                        visionClient.Endpoint = Constants.VisionEndpoint;

                        var features = new VisualFeatureTypes[] { VisualFeatureTypes.Tags, VisualFeatureTypes.Faces, VisualFeatureTypes.Categories, VisualFeatureTypes.Description, VisualFeatureTypes.Color };
                        description = await visionClient.AnalyzeImageInStreamAsync(stream, features);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return description;
        }
    }
}
