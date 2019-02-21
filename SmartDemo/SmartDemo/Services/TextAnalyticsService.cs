using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using SmartDemo.Models;
using SmartDemo.Helpers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SmartDemo.Services
{
    public static class TextAnalyticsService
    {
        private static readonly HttpClient client = CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Constants.TextAnalyticsEndpoint);
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Constants.TextAnalyticsApiKey);
            return httpClient;
        }

        public static async Task<TweetAnalytics> AnalyzeTweet(Tweet tweet)
        {
            try
            {
                var docs = PrepareDocuments(tweet);

                var tweetAnalytics = new TweetAnalytics()
                {
                    Message = tweet.Message
                };

                var jsonSentiment = await SendRequest(docs, "sentiment");
                var sentiment = JObject.Parse(jsonSentiment);
                tweetAnalytics.Sentiment = double.Parse((string)sentiment["documents"][0]["score"]);

                var jsonLanguage = await SendRequest(docs, "languages");
                var language = JObject.Parse(jsonLanguage);
                tweetAnalytics.Language = (string)language["documents"][0]["detectedLanguages"][0]["name"];

                var jsonKeywords = await SendRequest(docs, "keyPhrases");
                var keyWords = JObject.Parse(jsonKeywords);

                tweetAnalytics.Keywords = (keyWords["documents"].HasValues)
                    ? string.Join(",", keyWords["documents"][0]["keyPhrases"])
                    : "N/A";

                return tweetAnalytics;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static async Task<string> SendRequest(string body, string servicio)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(bytes))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(servicio, content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        private static string PrepareDocuments(Tweet tweet)
        {
            Document doc = new Document()
            {
                Id = "1",
                Language = tweet.Language,
                Text = tweet.Message
            };

            var docs = new List<Document>() { doc };
            var wrapper = new { documents = docs };
            return JsonConvert.SerializeObject(wrapper);
        }
    }
}
