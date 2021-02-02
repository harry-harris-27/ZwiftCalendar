using System;

namespace Zwift.Calendar.API.Models.Calendar
{
    public class World
    {
        public World(string name, string link)
        {
            this.Name = name;
            this.Link = link;
        }


        public string Name { get; }

        public string Link { get; }
    }
}
