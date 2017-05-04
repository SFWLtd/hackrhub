namespace HackRHub.Models
{
    public class Result
    {
        public string Team { get; set; }

        public double ConceptScore { get; set; }

        public double ImplementationScore { get; set; }

        public double TechnicalNoveltyScore { get; set; }

        public double PresentationScore { get; set; }

        public double TotalScore { get; internal set; }
    }
}