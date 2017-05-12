namespace HackRHub.Models
{
    public class Tweet
    {
        public string Text { get; set; }

        public string UserName { get; set; }

        public string UserScreenName { get; set; }

        public bool HasMedia { get; set; }

        public string[] MediaUrls { get; set; }

        public Person[] People { get; set; }
    }
}