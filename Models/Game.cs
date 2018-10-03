using System;
using System.Collections.Generic;

namespace UnderPressureGame.Models
{
    public class Game
    {
        bool playing = false;
        Room _currentRoom;
        Player _currentPlayer;
        Target _target;
        Dictionary<string, Room> GameRooms = new Dictionary<string, Room>();

        //Setup and Starts the Game loop
        public void StartGame()
        {
            Setup();
            while (playing)
            {
                Console.WriteLine(_currentRoom.Name + "\n");
                Console.WriteLine(_currentRoom.Description + "\n");
                GetUserInput();
            }
        }

        //Initializes the game, creates rooms, their exits, and add items to rooms
        void Setup()
        {
            playing = true;

            Room oceanTop = new Room("Middle Of The Ocean", "You are a brave explorer hired by the eccentric Millionare Mr. E. You are dropped into the middle of the ocean in diving gear. To the north and east and west there is only ocean. Below you there is only blurry depths of the ocean. \n");
            Room oceanDown = new Room("Down In The Ocean", "In the ocean there are fishes and plants swimming all around you. To your east there is a dark cave opening. To your west there is dead fish. To the south there is a hatch which is locked \n");
            Room cave = new Room("Mysterious Cave", "The cave is dark there is a glinting to your north and east. To the south it is dark and to the west is the opening. \n ");
            Room oceanBottom = new Room("Ocean Floor", "It's darker here. To the south there is a shadowy figure swimming around. To your west and east there is nothing but darkness. \n");
            Room pit = new Room("Dark Ocean Floor", "There is only the bunyip here");

            GameRooms.Add("oceanTop", oceanTop);
            GameRooms.Add("oceanDown", oceanDown);
            GameRooms.Add("cave", cave);
            GameRooms.Add("oceanBottom", oceanBottom);
            GameRooms.Add("pit", pit);

            oceanTop.Exits.Add("south", oceanDown);
            oceanDown.Exits.Add("north", oceanTop);
            oceanDown.Exits.Add("south", oceanBottom);
            oceanDown.Exits.Add("east", cave);
            cave.Exits.Add("west", oceanDown);
            oceanBottom.Exits.Add("north", oceanDown);

            Item food = new Item("Dead Fish", "Pieces of Fish Meat \n", false);
            Item knife = new Item("Dagger", "A Dagger with a moldy handle and a sharp looking blade. \n", false);
            Item key = new Item("Key", "A rusty looking key. \n", false);
            Item prize = new Item("PRIZE", "The cause of all your Suffering \n", false);

            oceanDown.Items.Add(food);
            cave.Items.Add(knife);
            cave.Items.Add(key);
            pit.Items.Add(prize);

            _currentRoom = oceanTop;
            Console.Write("Hello clever Explorer! What is your name? ");
            string player = System.Console.ReadLine();
            _currentPlayer = new Player(player);
            _target = new Target("Bunyip", false);
        }

        //Gets the user input and calls the appropriate command
        void GetUserInput()
        {
            Console.WriteLine($"What Will You Do Next?");
            string input = Console.ReadLine();
            input = input.ToLower();
            switch (input)
            {
                case "go":
                    Go();
                    break;
                case "use key":
                    UseKey();
                    break;
                case "use dagger":
                    Fight();
                    break;
                case "use dead fish":
                    UseFood();
                    break;
                case "take key":
                    TakeKey();
                    break;
                case "take dagger":
                    TakeDagger();
                    break;
                case "take dead fish":
                    TakeFood();
                    break;
                case "quit":
                    Quit();
                    break;
                case "help":
                    Help();
                    break;
                case "inventory":
                    Inventory();
                    break;
                case "look":
                    Look();
                    break;
                case "take prize":
                    TakePrize();
                    break;
                default:
                    Console.WriteLine("That's not a good idea");
                    break;
            }
        }

        //Stops the application
        void Quit()
        {
            playing = false;
        }

        //Should display a list of commands to the console
        void Help()
        {
            Console.WriteLine("Go");
            Console.WriteLine("Look");
            Console.WriteLine("Quit");
            Console.WriteLine("Inventory");
            Console.WriteLine("Take [item]");
            Console.WriteLine("Use [item]");
        }

        //Print the list of items in the players inventory to the console
        void Inventory()
        {
            Console.WriteLine("You Have: \n");
            foreach (var item in _currentPlayer.Inventory)
            {
                Console.WriteLine(item.Name);
            }
        }

        //Display the CurrentRoom Description, Exits, and Items
        void Look()
        {
            Console.WriteLine(_currentRoom.Name + "\n");
            Console.WriteLine(_currentRoom.Description + "\n");
        }
        //Directions

        void Go()
        {
            Console.Write("What Direction Do You Go? ");
            string direction = Console.ReadLine();
            direction = direction.ToLower();
            switch (direction)
            {
                case "north":
                    North();
                    break;
                case "south":
                    South();
                    break;
                case "west":
                    West();
                    break;
                case "east":
                    East();
                    break;
                default:
                    Console.WriteLine("That isn't a direction you can go.");
                    break;
            }
        }
        void North()
        {
            if (_currentRoom.Name == "Middle Of The Ocean")
            {
                Console.WriteLine("More Ocean \n");
            }
            else if (_currentRoom.Name == "Down In The Ocean")
            {
                _currentRoom = GameRooms["oceanTop"];
            }
            else if (_currentRoom.Name == "Mysterious Cave")
            {
                Console.WriteLine("In the darkness of the cave there appears to be a dagger stuck in the walls. \n");
            }
            else if (_currentRoom.Name == "Ocean Floor")
            {
                _currentRoom = GameRooms["oceanDown"];
            }
        }
        void South()
        {
            if (_currentRoom.Name == "Middle Of The Ocean")
            {
                _currentRoom = GameRooms["oceanDown"];
            }
            else if (_currentRoom.Name == "Down In The Ocean")
            {
                Item key = _currentPlayer.Inventory.Find(i => i.Name == "Key");
                if (key != null && key.Taken == true)
                {
                    _currentRoom = GameRooms["oceanBottom"];
                }
                else
                {
                    Console.WriteLine("The Door is Locked. Do you have a key?");
                }
            }
            else if (_currentRoom.Name == "Mysterious Cave")
            {
                Console.WriteLine("There is nothing here but slimy fish and plants");
            }
            else if (_currentRoom.Name == "Ocean Floor")
            {
                _target.Active = true;
                Console.WriteLine(@"
                            /|               |\                              
                           / | ___-------___ | \                             
                          /  \/ ^ /\   /\ ^ \/  \                            
                         |   (  O-. \ / .-O  )   |                           
                      /-\/   ^-----^-V-^-----^   \/-\                        
                    /-      (~ 0O0 ~) (~ 000 ~)     -\                       
                   <        (~ OOO ~) (~ 000 ~)       >                      
                    \-      (____---===---____)     -/                       
                     \-   /\ \ \|         |/ / /\  -/                        
                     -/\-/  \ \ V         V / /  \-/\-                       
                       v    \ \           / /    v                          
                             \ \ A     A / /                                
                               \_\^-----^/_/                                 
                                \_/\___/\_/                                  
                                  \_____/");
                Console.WriteLine("The Dreaded Bunyip was hiding in the depths. You Must Conquer Him!!!");
                _currentRoom = GameRooms["pit"];
            }
        }
        void West()
        {
            if (_currentRoom.Name == "Middle Of The Ocean")
            {
                Console.WriteLine("More Ocean \n");
            }
            else if (_currentRoom.Name == "Down In The Ocean")
            {
                Console.WriteLine("The dead fish reeks here \n");
            }
            else if (_currentRoom.Name == "Mysterious Cave")
            {
                _currentRoom = GameRooms["oceanDown"];
            }
            else if (_currentRoom.Name == "Ocean Floor")
            {
                Console.WriteLine("There is nothing here but the darkness \n");
            }
        }
        void East()
        {
            if (_currentRoom.Name == "Middle Of The Ocean")
            {
                Console.WriteLine("More Ocean \n");
            }
            else if (_currentRoom.Name == "Down In The Ocean")
            {
                _currentRoom = GameRooms["cave"];
            }
            else if (_currentRoom.Name == "Mysterious Cave")
            {
                Console.WriteLine("Here there appears to be a key glinting in all the plants \n");
            }
            else if (_currentRoom.Name == "Ocean Floor")
            {
                Console.WriteLine("There is nothing here but darkness \n");
            }
        }
        //Taking Items
        void TakeKey()
        {
            if (_currentRoom.Name != "Mysterious Cave")
            {
                Console.WriteLine("There's No Key Here. \n");
            }
            else
            {
                Item key = _currentRoom.Items.Find(i => i.Name == "Key");
                _currentPlayer.Inventory.Add(key);
                _currentRoom.Items.Remove(key);
                _currentRoom.Description = "The cave is dark there is a glinting to your north. To the south it is dark and to the west is the opening. \n ";
                Console.WriteLine($"You Now Have {key.Name} in Your Inventory \n");
            }
        }
        void TakeDagger()
        {
            if (_currentRoom.Name != "Mysterious Cave")
            {
                Console.WriteLine("There's No Dagger Here. \n");
            }
            else
            {
                Item dagger = _currentRoom.Items.Find(i => i.Name == "Dagger");
                _currentPlayer.Inventory.Add(dagger);
                _currentRoom.Items.Remove(dagger);
                _currentRoom.Description = "The cave is dark there is a glinting to your east. To the south it is dark and to the west is the opening. \n ";
                Console.WriteLine($"You now Have {dagger.Name} in Your Inventory \n");
            }
        }
        void TakeFood()
        {
            if (_currentRoom.Name != "Down In The Ocean")
            {
                Console.WriteLine("There's No Dead Fish Here. \n");
            }
            else
            {
                Item food = _currentRoom.Items.Find(i => i.Name == "Dead Fish");
                _currentPlayer.Inventory.Add(food);
                _currentRoom.Items.Remove(food);
                _currentRoom.Description = "In the ocean there are fishes and plants swimming all around you. To your east there is a dark cave opening. To the south there is a hatch which is locked \n";
                Console.WriteLine($"You now Have {food.Name} in Your Inventory \n");
            }
        }
        void TakePrize()
        {
            if (_currentRoom.Name != "Dark Ocean Floor")
            {
                Console.WriteLine("There's No Prize Here. Don't Get Ahead Of yourself.");
            }
            else
            {
                Item prize = _currentRoom.Items.Find(i => i.Name == "PRIZE");
                _currentPlayer.Inventory.Add(prize);
                _currentRoom.Items.Remove(prize);
                Console.WriteLine($"You now Have {prize.Name} in Your Inventory");
                Won();
            }
        }
        //Use Items
        void UseKey()
        {
            Item key = _currentPlayer.Inventory.Find(i => i.Name == "Key");
            if (_currentRoom.Name == "Down In The Ocean" && _currentPlayer.Inventory.Contains(key))
            {
                key.Taken = true;
                Console.WriteLine("You Used The Key. The Hatch is Unlocked \n");
                _currentRoom.Description = "In the ocean there are fishes and plants swimming all around you. To your east there is a dark cave opening. To your west there is dead fish. To the south there is a hatch which is now unlocked \n";
            }
            else
            {
                Console.WriteLine("Where Would You Even Use The Key? \n");
            }
        }
        void Fight()
        {
            if (_currentRoom.Name != "Bottom Of The Ocean" && _target.Active != true)
            {
                Console.WriteLine("Why Would You Use the Dagger? \n");
            }
            else
            {
                Console.WriteLine($"The Bunyip is at {_target.Health} health. \n");
                _target.Health -= 10;
                _currentPlayer.Health -= 50;
            }
            isDead();
        }
        void UseFood()
        {
            if (_currentRoom.Name != "Bottom Of The Ocean" && _target.Active != true)
            {
                Console.WriteLine("Why Would You Use the dead Fish? \n");
            }
            else
            {
                Console.WriteLine("You Feed the dead fish to the Bunyip. It swims away with its food. Where the bunyip had been there is the prize you have been looking for. \n");
                _currentRoom.Description = "There is no more bunyip here";
            }
            Won();
        }
        void isDead()
        {
            if (_currentPlayer.Health <= 0)
            {
                Console.Clear();
                Console.WriteLine(" \n You DIE because how in the world did you think you would defeat a monster with a dagger? \n");
                playing = false;
            }
        }
        void Won()
        {
            Item prize = _currentPlayer.Inventory.Find(i => i.Name == "PRIZE");
            if (prize != null)
            {
                Console.WriteLine("You Won!! Now It is time to give the prize to Mr. E. \n");
                playing = false;
            }
        }
    }
}