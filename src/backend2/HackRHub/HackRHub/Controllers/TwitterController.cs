using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using HackRHub.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HackRHub.Controllers
{
    public class TwitterController : ApiController
    {
        static string consumerKey = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerKey"];
        static string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerSecret"];

        [HttpGet]
        [Route("api/twitter/tweets")]
        public async Task<IHttpActionResult> GetTweets()
        {
            var header = consumerKey + ":" + consumerSecret;
            var headerBytes = Encoding.UTF8.GetBytes(header);
            var encodedHeader = Convert.ToBase64String(headerBytes);

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.twitter.com");

            var getOAuthTokenRequest = new HttpRequestMessage()
            {
                RequestUri = new Uri("oauth2/token", UriKind.Relative),
                Method = HttpMethod.Post,
                Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded")
            };
            getOAuthTokenRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(headerBytes));

            var getOAuthTokenResponse = await client.SendAsync(getOAuthTokenRequest);
            var oAuthTokenResponse = await getOAuthTokenResponse.Content.ReadAsAsync<OAuthTokenResponse>();

            var getTweetsRequest = new HttpRequestMessage()
            {
                RequestUri = new Uri("1.1/search/tweets.json?q=%23civicadigihack17&tweet_mode=extended", UriKind.Relative),
                Method = HttpMethod.Get
            };
            getTweetsRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", oAuthTokenResponse.AccessToken);

            var getTweetsResponse = await client.SendAsync(getTweetsRequest);
            var tweetsJson = await getTweetsResponse.Content.ReadAsStringAsync();

            dynamic obj = JsonConvert.DeserializeObject(tweetsJson);

            var tweets = new List<Tweet>();

            foreach (dynamic status in obj.statuses)
            {
                tweets.Add(new Tweet
                {
                    Text = status.full_text,
                    UserName = status.user.name,
                    UserScreenName = status.user.screen_name,
                    HasMedia = status?.entities?.media?.HasValues ?? false,
                    MediaUrls = ((JArray)status?.extended_entities?.media)?.Select(x => x["media_url"].ToString()).ToArray() ?? new string[0],
                    People = new Person[0]
                });
            }

            return Ok(tweets);
        }
    }
}