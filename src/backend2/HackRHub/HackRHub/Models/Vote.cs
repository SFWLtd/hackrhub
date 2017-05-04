using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HackRHub.Models
{
    public class Vote
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required]
        public string VoterTeamId { get; set; }

        [Required]
        public string VoterTeamName { get; set; }

        [Required]
        public string RecipientTeamId { get; set; }

        [Required]
        public string RecipientTeamName { get; set; }

        [Range(1, 10)]
        public int ConceptScore { get; set; }

        [Range(1, 10)]
        public int ImplementationScore { get; set; }

        [Range(1, 10)]
        public int TechnicalNoveltyScore { get; set; }

        [Range(1, 10)]
        public int PresentationScore { get; set; }
    }
}