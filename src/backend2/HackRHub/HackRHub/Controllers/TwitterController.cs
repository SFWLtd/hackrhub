using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using HackRHub.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.ProjectOxford.Face;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HackRHub.Controllers
{
    public class TwitterController : ApiController
    {
        static string consumerKey = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerKey"];
        static string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerSecret"];

        static string apiKey = System.Configuration.ConfigurationManager.AppSettings["FaceApiKey"];
        static string personGroupId = "civicans";

        static string dbKey = System.Configuration.ConfigurationManager.AppSettings["DocumentDbKey"];
        static string dbEndpoint = System.Configuration.ConfigurationManager.AppSettings["DocumentDbEndpoint"];

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
                RequestUri = new Uri("1.1/search/tweets.json?q=%23civicadigihack17 AND -filter:retweets AND -filter:replies&tweet_mode=extended&count=100", UriKind.Relative),
                Method = HttpMethod.Get
            };
            getTweetsRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", oAuthTokenResponse.AccessToken);

            var getTweetsResponse = await client.SendAsync(getTweetsRequest);
            var tweetsJson = await getTweetsResponse.Content.ReadAsStringAsync();

            dynamic obj = JsonConvert.DeserializeObject(tweetsJson);

            var tweets = new List<Tweet>();

            var faceServiceClient = new FaceServiceClient(apiKey, "https://westeurope.api.cognitive.microsoft.com/face/v1.0");

            foreach (dynamic status in obj.statuses)
            {
                Tweet tweet = new Tweet
                {
                    Text = status.full_text,
                    UserName = status.user.name,
                    UserScreenName = status.user.screen_name,
                    UserProfileImageUrl = status.user.profile_image_url,
                    CreatedAt = DateTime.ParseExact(status.created_at.ToString(), "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
                    HasMedia = status?.entities?.media?.HasValues ?? false,
                    MediaUrls = ((JArray)status?.extended_entities?.media)?.Select(x => x["media_url"].ToString()).ToArray() ?? new string[0]
                };

                var peopleInTweet = new List<Person>();

                foreach (var url in tweet.MediaUrls)
                {
                    var image = await client.GetStreamAsync(url);
                    var people = await GetPeopleInPicture(faceServiceClient, image);
                    peopleInTweet.AddRange(people);
                }

                tweet.People = peopleInTweet.ToArray();

                tweets.Add(tweet);
            }

            return Ok(tweets);
        }

        private static async Task<IEnumerable<Person>> GetPeopleInPicture(FaceServiceClient faceServiceClient, Stream image)
        {
            var result = new List<Person>();

            var faces = await faceServiceClient.DetectAsync(image);
            var faceIds = faces.Select(face => face.FaceId).ToArray();

            if (faceIds.Any())
            {
                var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);

                var queryOptions = new FeedOptions { MaxItemCount = 1 };
                var client = new DocumentClient(new Uri(dbEndpoint), dbKey);

                foreach (var identifyResult in results)
                {
                    if (identifyResult.Candidates.Length != 0)
                    {
                        // Get top 1 among all candidates returned
                        var candidateId = identifyResult.Candidates[0].PersonId;
                        var person = client.CreateDocumentQuery<Person>(UriFactory.CreateDocumentCollectionUri("ToDoList", "Items"),
                            $"SELECT * FROM  root r WHERE r.entityType = 'user' AND r.personId = '{candidateId}'",
                            queryOptions).AsEnumerable().FirstOrDefault();

                        result.Add(person);
                    }
                }
            }

            return result;
        }
    }
}