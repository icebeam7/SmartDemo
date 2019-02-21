using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using SmartDemo.Models;
using SmartDemo.Helpers;

using LinqToTwitter;

namespace SmartDemo.Services
{
    public static class TwitterService
    {
        private static async Task<IAuthorizer> TwitterAuthorize()
        {
            var auth = new ApplicationOnlyAuthorizer()
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = Constants.TwitterApiKey,
                    ConsumerSecret = Constants.TwitterApiSecret,
                },
            };

            await auth.AuthorizeAsync();
            return auth;
        }

        public static async Task<List<Tweet>> GetTweets(string dato)
        {
            var twitter = new TwitterContext(await TwitterAuthorize());
            var tweets = new List<Tweet>();

            var search = await (from t in twitter.Search
                                where t.Type == SearchType.Search &&
                                t.Query == dato
                                select t).SingleOrDefaultAsync();

            if (search != null && search.Statuses != null)
            {
                search.Statuses.ForEach(tweet =>
                    tweets.Add(new Tweet()
                    {
                        Message = tweet.Text,
                        Language = tweet.Lang
                    }));
            }

            return tweets;
        }
    }
}
