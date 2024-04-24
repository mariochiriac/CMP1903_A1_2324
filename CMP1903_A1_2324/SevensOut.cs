using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    public class SevensOut : IGame
    {
        // Objects
        private Die die_1;
        private Die die_2;
        private Statistics stats = new Statistics();
        private int _playerid = 1;

        // Generates new dices each time SevensOut is instantiated
        public SevensOut()
        {
            die_1 = new Die();
            die_2 = new Die();
        }

        // Main method that will be called
        public void Play()
        {
            RollDices(die_1, die_2);

            Console.WriteLine("Would you like to play again? (Y / N)");
            string user_input = Console.ReadLine();

            while (true)
            {
                if (user_input.ToLower() == "y") 
                {
                    Play();
                    break;
                }
                else Console.WriteLine("Game Over!"); break;
            }
        }

        // Private method that rolls the dices
        private void RollDices(Die die_1, Die die_2)
        {
            int rolled_dice1 = die_1.Roll();
            int rolled_dice2 = die_2.Roll();

            int total_dices = rolled_dice1 + rolled_dice2;
            if (rolled_dice1 == rolled_dice2)
            {
                stats.AddDice(_playerid, total_dices * 2);
            }
            else
            {
                if (total_dices <= 7)
                {
                    Console.WriteLine($"Game Over! Total sum of dices: {total_dices}.");
                }
                else
                {
                    stats.AddDice(_playerid, total_dices);
                    Console.WriteLine($"Player {_playerid} has rolled {total_dices}");
                }
            }
        }

    }
}
