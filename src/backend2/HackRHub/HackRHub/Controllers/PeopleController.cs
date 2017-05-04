using HackRHub.Models;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HackRHub.Controllers
{
    public class PeopleController : ApiController
    {
        static string dbKey = System.Configuration.ConfigurationManager.AppSettings["DocumentDbKey"];
        static string dbEndpoint = System.Configuration.ConfigurationManager.AppSettings["DocumentDbEndpoint"];

        [Route("api/people")]
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };
            var client = new DocumentClient(new Uri(dbEndpoint), dbKey);

            var query = client.CreateDocumentQuery<Person>(UriFactory.CreateDocumentCollectionUri("ToDoList", "Items"), 
                "SELECT * FROM  root r WHERE r.entityType = 'user'", 
                queryOptions);

            return query.ToList();
        }
    }
}
