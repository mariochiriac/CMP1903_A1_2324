using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * Create a Game object and call its methods.
             * Create a Testing object to verify the output and operation of the other classes.
             */

            Game game = new Game();
            SevensOut sevensOut = new SevensOut();
            ThreeOrMore threeOrMore = new ThreeOrMore();
            try
            {
                int user_choice = 0;
                Console.WriteLine("Select Game: \n1. Base Game\n2. Sevens Out\n3. Three or More");

                while (true)
                {
                    int.TryParse(Console.ReadLine(), out user_choice);
                        
                    if (user_choice <= 3 && user_choice >= 1) break;
                    else Console.WriteLine("Please select a valid game! (1 - 3)");
                }

                bool isAgainstComputer = false;
                int game_mode = 0;
                Console.WriteLine("Play Modes:\n(1) Player vs Player\n(2) Player vs Computer");

                while (true)
                {
                    int.TryParse(Console.ReadLine(), out game_mode);

                    if (game_mode == 1) 
                    {
                        isAgainstComputer = false; 
                        break; 
                    }
                    else if (game_mode == 2)
                    {
                        isAgainstComputer = true;
                        break;
                    }
                    else Console.WriteLine("Please select a valid game mode! (1 or 2)");
                }

                switch (user_choice)
                {
                    case 1:
                        game.isComputer = isAgainstComputer;
                        game.StartGame();
                        break;
                    case 2:
                        sevensOut.isComputer = isAgainstComputer;
                        sevensOut.StartGame();
                        break;
                    case 3:
                        threeOrMore.isComputer = isAgainstComputer;
                        threeOrMore.StartGame();
                        break;
                    default:
                        Console.WriteLine("Invalid game choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while running the program: " + ex.Message);
            }
        }
    }
}
