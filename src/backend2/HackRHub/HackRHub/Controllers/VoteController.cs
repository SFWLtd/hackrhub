using HackRHub.Models;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HackRHub.Controllers
{
    public class VoteController : ApiController
    {
        static string dbKey = System.Configuration.ConfigurationManager.AppSettings["DocumentDbKey"];
        static string dbEndpoint = System.Configuration.ConfigurationManager.AppSettings["DocumentDbEndpoint"];

        [HttpPost]
        [Route("api/vote")]
        public async Task<HttpResponseMessage> Vote(Vote model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var client = new DocumentClient(new Uri(dbEndpoint), dbKey);

            var response = await client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri("ToDoList", "Votes"), model);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/votes")]
        public IEnumerable<Vote> Get()
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };
            var client = new DocumentClient(new Uri(dbEndpoint), dbKey);

            var query = client.CreateDocumentQuery<Vote>(UriFactory.CreateDocumentCollectionUri("ToDoList", "Votes"),
                queryOptions);

            return query.ToList();
        }

        [HttpGet]
        [Route("api/teamvotes/{teamId}")]
        public IEnumerable<Vote> Get(string teamId)
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };
            var client = new DocumentClient(new Uri(dbEndpoint), dbKey);

            var query = client.CreateDocumentQuery<Vote>(UriFactory.CreateDocumentCollectionUri("ToDoList", "Votes"),
                queryOptions)
                .Where(v => v.VoterTeamId == teamId);

            return query.ToList();
        }
    }
}
