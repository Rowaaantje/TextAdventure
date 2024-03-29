﻿using System;

namespace Zuul
{
	public class Game
	{
		private Player player;
		private Parser parser;

		public Game()
		{
			parser = new Parser();
			player = new Player();
			CreateRooms();
		}

		private void CreateRooms()
		{
			// create the rooms
			Room outside = new Room("outside the main entrance of the university");
			Room theatre = new Room("in a lecture theatre");
			Room pub = new Room("in the campus pub");
			Room lab = new Room("in a computing lab");
			Room office = new Room("in the computing admin office");

			Room winecellar = new Room("wine cellar under the pub so much wine");
			Room theatreloft = new Room("in the theatre loft very dusty");



			// initialise room exits
			outside.AddExit("east", theatre);
			outside.AddExit("south", lab);
			outside.AddExit("west", pub);

			theatre.AddExit("west", outside);
			theatre.AddExit("up", theatreloft);


			pub.AddExit("east", outside);
			pub.AddExit("down", winecellar);

			lab.AddExit("north", outside);
			lab.AddExit("east", office);

			office.AddExit("west", lab);

			theatreloft.AddExit("down", theatre);
			winecellar.AddExit("up", pub);



			player.CurrentRoom = outside;  // start game outside

			//Items
			lab.Chest.Put("medkit", new Item(40, "a huge medkit"));
			theatreloft.Chest.Put("rathead", new Item(100, "very squishy and sticky of blood"));

		}

		/**
		 *  Main play routine.  Loops until end of play.
		 */
		public void Play()
		{
			PrintWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the player wants to quit.
			bool finished = false;
			while (!finished)
			{

				//check if player is alive
				if (!player.IsAlive())
				{

					Console.WriteLine("\nyou died a horrible death\n");
					return;
					//continue;
				}


				Command command = parser.GetCommand();
				finished = ProcessCommand(command);
			}
			Console.WriteLine("Thank you for playing.");
		}

		/**
		 * Print out the opening message for the player.
		 */
		private void PrintWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
			Console.WriteLine(player.CurrentRoom.GetLongDescription());
		}

		/**
		 * Given a command, process (that is: execute) the command.
		 * If this command ends the game, true is returned, otherwise false is
		 * returned.
		 */
		private bool ProcessCommand(Command command)
		{
			bool wantToQuit = false;

			if (command.IsUnknown())
			{
				Console.WriteLine("I don't know what you mean...");

				return false;
			}





			string commandWord = command.GetCommandWord();
			switch (commandWord)
			{
				case "help":
					PrintHelp();
					break;
				case "go":
					GoRoom(command);
					break;
				case "look":
					Console.WriteLine(player.CurrentRoom.GetLongDescription());
					Console.WriteLine(player.CurrentRoom.GetChestString()); //shows Room pickable objects
					break;
				case "take": //Take a object
					Take(command);
					break;
				case "inventory": //shows Player inventory
					Console.WriteLine(player.PlayerInventory.Show());
					break;
				case "quit":
					wantToQuit = true;
					break;
			}

			return wantToQuit;
		}

		// implementations of user commands:

		/**
		 * Print out some help information.
		 * Here we print the mission and a list of the command words.
		 */
		private void PrintHelp()
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
			// let the parser print the commands
			parser.PrintValidCommands();
		}

		/**
		 * Try to go to one direction. If there is an exit, enter the new
		 * room, otherwise print an error message.
		 */
		private void GoRoom(Command command)
		{
			if (!command.HasSecondWord())
			{
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Go where?");
				return;
			}

			string direction = command.GetSecondWord();

			// Try to go to the next room.
			Room nextRoom = player.CurrentRoom.GetExit(direction);

			if (nextRoom == null)
			{
				Console.WriteLine("There is no door to " + direction + "!");
			}
			else
			{
				player.CurrentRoom = nextRoom;
				player.Damage(10);
				Console.WriteLine(player.CurrentRoom.GetLongDescription());
				Console.WriteLine("your current Health is " + player.Health);

				if (player.IsAlive())
				{
					Console.WriteLine("Your slowly bleeding to death");
				}
			}
		}
		private void Take(Command command)
		{
			if (!command.HasSecondWord())
			{
                Console.WriteLine(" Take what? ");
                return;
            }
			
            string itemName = command.GetSecondWord();
            player.TakeFromChest(itemName);
        }
			
		private void drop (Command command)
		{
			if(!command.HasSecondWord())
			{
				Console.WriteLine("drop what? ");
				return;
			}
			string itemName = command.GetSecondWord();
			player.DropToChest(itemName);
		}

	}
}
