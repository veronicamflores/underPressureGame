using System.Collections.Generic;

namespace UnderPressureGame.Models
{
    public class Target
    {
        string TargetName { get; set; }
        public List<Item> Inventory { get; set; }
        public int Health { get; set; }
        public bool Active { get; set; }
        public Target(string targetName, bool active)
        {
            TargetName = targetName;
            Health = 1000;
            Active = active;
            Inventory = new List<Item>();
        }
    }
}