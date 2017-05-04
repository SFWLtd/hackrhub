using HackRHub.Models;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HackRHub.Controllers
{
    public class TeamController : ApiController
    {
        static string dbKey = System.Configuration.ConfigurationManager.AppSettings["DocumentDbKey"];
        static string dbEndpoint = System.Configuration.ConfigurationManager.AppSettings["DocumentDbEndpoint"];

        [Route("api/teams")]
        [HttpGet]
        public IEnumerable<Team> Get()
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };
            var client = new DocumentClient(new Uri(dbEndpoint), dbKey);

            var query = client.CreateDocumentQuery<Team>(UriFactory.CreateDocumentCollectionUri("ToDoList", "Items"),
                "SELECT * FROM root r WHERE r.entityType = 'team'",
                queryOptions);

            return query.ToList();
        }
    }
}
