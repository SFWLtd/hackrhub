using System.Collections.Generic;

namespace HackRHub.Models
{
    public class Team
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Person> People { get; set; }
    }
}