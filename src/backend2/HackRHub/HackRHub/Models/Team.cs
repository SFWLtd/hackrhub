using System.Collections.Generic;

namespace HackRHub.Models
{
    public class Team
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public IEnumerable<Person> People { get; set; }
    }
}