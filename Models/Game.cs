using System;
using System.Collections.Generic;

namespace UnderPressureGame.Models
{
    public class Game
    {
        bool playing = false;
        Room _currentRoom;
        Player _currentPlayer;
        Dictionary<string, Room> GameRooms = new Dictionary<string, Room>();

        //Setup and Starts the Game loop
        public void StartGame()
        {
            Setup();
            while (playing)
            {
                Console.WriteLine(_currentRoom.Name);
                GetUserInput();
            }
        }

        //Initializes the game, creates rooms, their exits, and add items to rooms
        void Setup()
        {
            playing = true;

            Room oceanTop = new Room("Middle Of The Ocean", "You are a brave explorer hired by the eccentric Millionare Mr. E. You are dropped into the middle of the ocean in diving gear. To the north and east there is only ocean. To the west there appears to be a floating green plant.Below you there is only blurry looks into the deep");
            Room oceanDown = new Room("Down In The Ocean", @"In the ocean there are fishes and plants swimming all around you.
            To your east there is a dark cave opening ");
            Room cave = new Room("Mysterious Cave", "Hey Hey ");
            Room oceanBottom = new Room("Ocean Floor", "What Is Up");

            GameRooms.Add("oceanTop", oceanTop);
            GameRooms.Add("oceanDown", oceanDown);
            GameRooms.Add("cave", cave);
            GameRooms.Add("oceanBottom", oceanBottom);

            oceanTop.Exits.Add("south", oceanDown);
            oceanDown.Exits.Add("north", oceanTop);
            oceanDown.Exits.Add("south", oceanBottom);
            oceanDown.Exits.Add("east", cave);
            cave.Exits.Add("west", oceanDown);
            oceanBottom.Exits.Add("north", oceanDown);

            Item algea = new Item("Algea", "Green and Slimey", false);
            Item food = new Item("Food", "Pieces of Fish Meat", false);
            Item knife = new Item("Knife", "A Dagger with a moldy handle and a sharp looking blade.", false);
            Item key = new Item("Key", "A rusty looking key.", false);

            oceanTop.Items.Add(algea);
            oceanDown.Items.Add(food);
            cave.Items.Add(knife);
            cave.Items.Add(key);

            _currentRoom = oceanTop;
            Console.Write("Hello clever Explorer! What is your name? ");
            string player = System.Console.ReadLine();
            _currentPlayer = new Player(player);
            Console.WriteLine(_currentRoom.Name);
            Console.WriteLine(_currentRoom.Description);
        }

        // //Resets Game
        // void Reset();

        //Gets the user input and calls the appropriate command
        void GetUserInput()
        {
            Console.WriteLine("What Will You Do Next?");
            string input = Console.ReadLine();
            input = input.ToLower();
            switch (input)
            {
                case "go north":
                    North();
                    break;
                case "go south":
                    South();
                    break;
                case "go west":
                    West();
                    break;
                case "go east":
                    East();
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
            Console.WriteLine("Go [north | south | west | east]");
            Console.WriteLine("Look");
            Console.WriteLine("Quit");
            Console.WriteLine("Inventory");
            Console.WriteLine("Take [item]");
            Console.WriteLine("Use [item]");
        }

        //Print the list of items in the players inventory to the console
        void Inventory()
        {
            Console.WriteLine("You Have:");
            foreach (var item in _currentPlayer.Inventory)
            {
                Console.WriteLine(item.Name);
            }
        }

        //Display the CurrentRoom Description, Exits, and Items
        void Look()
        {
            Console.WriteLine(_currentRoom.Name);
            Console.WriteLine(_currentRoom.Description);
        }
        void North()
        {
            if (_currentRoom.Name == "Middle Of The Ocean")
            {
                Console.WriteLine("More Ocean");
            }
            else if (_currentRoom.Name == "Down In The Ocean")
            {
                _currentRoom = GameRooms["oceanTop"];
            }
            else if (_currentRoom.Name == "Mysterious Cave")
            {
                Console.WriteLine("In the darkness of the cave there appears to be a dagger stuck in the walls.");
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
                // if (_currentPlayer.Inventory.algea.Taken == true)
                // {
                //     _currentRoom = GameRooms["oceanBottom"];
                // }
                // else
                // {
                //     Console.WriteLine("The Door is Locked. Do you have a key?");
                // }
            }
            else if (_currentRoom.Name == "Mysterious Cave")
            {
                Console.WriteLine("Hey");
            }
            else if (_currentRoom.Name == "Ocean Floor")
            {
                Console.WriteLine("HEy Hey");
            }
        }
        void West()
        {
            if (_currentRoom.Name == "Middle Of The Ocean")
            {
                Console.WriteLine("Algea");
            }
            else if (_currentRoom.Name == "Down In The Ocean")
            {
                Console.WriteLine("Food");
            }
            else if (_currentRoom.Name == "Mysterious Cave")
            {
                _currentRoom = GameRooms["oceanDown"];
            }
            else if (_currentRoom.Name == "Ocean Floor")
            {
                Console.WriteLine("Monster");
            }
        }
        void East()
        {
            if (_currentRoom.Name == "Middle Of The Ocean")
            {
                Console.WriteLine("More Ocean");
            }
            else if (_currentRoom.Name == "Down In The Ocean")
            {
                _currentRoom = GameRooms["cave"];
            }
            else if (_currentRoom.Name == "Mysterious Cave")
            {
                Console.WriteLine("key");
            }
            else if (_currentRoom.Name == "Ocean Floor")
            {
                Console.WriteLine("Monster");
            }
        }
    }
}