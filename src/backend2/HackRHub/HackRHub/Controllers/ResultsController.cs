using HackRHub.Models;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HackRHub.Controllers
{
    public class ResultsController : ApiController
    {
        static string dbKey = System.Configuration.ConfigurationManager.AppSettings["DocumentDbKey"];
        static string dbEndpoint = System.Configuration.ConfigurationManager.AppSettings["DocumentDbEndpoint"];

        [HttpGet]
        [Route("api/results")]
        public IEnumerable<Result> Get()
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };
            var client = new DocumentClient(new Uri(dbEndpoint), dbKey);

            var votes = client.CreateDocumentQuery<Vote>(UriFactory.CreateDocumentCollectionUri("ToDoList", "Votes"),
                queryOptions).ToList();

            var teams = client.CreateDocumentQuery<Team>(UriFactory.CreateDocumentCollectionUri("ToDoList", "Items"),
                "SELECT * FROM root r WHERE r.entityType = 'team'",
                queryOptions).ToList();

            var results = new List<Result>();

            foreach (var team in teams)
            {
                var teamVotes = votes.Where(v => v.RecipientTeamId == team.Id);
                var total = teamVotes.Count();

                var result = new Result
                {
                    Team = team.Name,
                    ConceptScore = Math.Round((double)teamVotes.Sum(v => v.ConceptScore) / total, 2, MidpointRounding.AwayFromZero),
                    ImplementationScore = Math.Round((double)teamVotes.Sum(v => v.ImplementationScore) / total, 2, MidpointRounding.AwayFromZero),
                    PresentationScore = Math.Round((double)teamVotes.Sum(v => v.PresentationScore) / total, 2, MidpointRounding.AwayFromZero),
                    TechnicalNoveltyScore = Math.Round((double)teamVotes.Sum(v => v.TechnicalNoveltyScore) / total, 2, MidpointRounding.AwayFromZero)
                };

                result.TotalScore = result.ConceptScore + result.ImplementationScore + result.PresentationScore + result.TechnicalNoveltyScore;

                results.Add(result);
            }

            return results.OrderByDescending(r => r.TotalScore);
        }
    }
}
