using System.Collections.Generic;

namespace UnderPressureGame.Models
{
    public class Player
    {
        string PlayerName { get; set; }
        public List<Item> Inventory { get; set; }

        public Player(string playerName)
        {
            PlayerName = playerName;
            Inventory = new List<Item>();
        }
    }
}