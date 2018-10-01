using System.Collections.Generic;

namespace UnderPressureGame.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Taken { get; set; }

        public Item(string name, string description, bool taken)
        {
            Name = name;
            Description = description;
            Taken = taken;
        }
    }
}