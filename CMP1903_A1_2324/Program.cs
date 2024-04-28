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
            try
            {
                int user_choice = 0;
                Console.WriteLine("Select Game: \n1. Base Game\n2. Sevens Out\n3. To be implemented");

                while (true)
                {
                    int.TryParse(Console.ReadLine(), out user_choice);
                        
                    if (user_choice <= 2 && user_choice >= 1) break;
                    else Console.WriteLine("Please select a valid game! (1 - 2)");
                }

                switch (user_choice)
                {
                    case 1:
                        game.StartGame();
                        break;
                    case 2:
                        game.StartSevensOut();
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
