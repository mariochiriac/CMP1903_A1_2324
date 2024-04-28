using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    class SevensOut : IGame
    {
        // Objects
        private Die die_1;
        private Die die_2;
        private Statistics stats = new Statistics();
        private int _playerid = 1;
        private bool gameStatus = true;

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
            PlayAgain();
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
                Console.WriteLine($"A double has been rolled. {total_dices * 2} has been added to Player {_playerid} total.");
                Console.WriteLine($"Player {_playerid} current total: {GetCurrentTotal(_playerid)}");
            }
            else
            {
                if (total_dices <= 7)
                {
                    Console.WriteLine($"Game Over! Player {_playerid} has rolled: {total_dices}.");
                    Console.WriteLine($"==== Player {_playerid} ended his streak with the total: {GetCurrentTotal(_playerid)} ====");
                    if (_playerid == 1)
                    {
                        _playerid = 2;
                        Console.WriteLine($"It is now Player {_playerid} turn!");
                    }
                    else GetWinner();
                }
                else
                {
                    stats.AddDice(_playerid, total_dices);
                    Console.WriteLine($"Player {_playerid} has rolled {total_dices}");
                    Console.WriteLine($"Player {_playerid} current total: {GetCurrentTotal(_playerid)}");
                }
            }
        }

        private void PlayAgain()
        {
            Console.WriteLine("Type (Y) to roll the dice.");
            string user_input = Console.ReadLine();

            while (gameStatus)
            {
                if (user_input.ToLower() == "y")
                {
                    Play();
                    break;
                }
                else Console.WriteLine("Game Over!"); break;
            }
        }


        private void GetWinner()
        {
            // Variables
            int player1_total = GetCurrentTotal(1);
            int player2_total = GetCurrentTotal(2);

            gameStatus = false;

            Console.WriteLine($"GAME ENDED!\nPlayer 1 Total: {player1_total}\nPlayer 2 Total: {player2_total}");

            if (player1_total > player2_total)
            {
                stats.PlayerOne_Score++;
                Console.WriteLine($"Player 1 has won!\nCurrent Score:\nPlayer 1: {stats.PlayerOne_Score}\nPlayer 2: {stats.PlayerTwo_Score}");
            }
            else if (player1_total < player2_total)
            {
                stats.PlayerTwo_Score++;
                Console.WriteLine($"Player 2 has won!\nCurrent Score:\nPlayer 1: {stats.PlayerOne_Score}\nPlayer 2: {stats.PlayerTwo_Score}");
            }
            else
            {
                stats.PlayerOne_Score++;
                stats.PlayerTwo_Score++;
                Console.WriteLine($"The game is equal! Both players received a point.\nCurrent Score:\nPlayer 1: {stats.PlayerOne_Score}\nPlayer 2: {stats.PlayerTwo_Score}");
            }
        }

        private int GetCurrentTotal(int playerid)
        {
            int total = 0;
            List<int> dice_list = new List<int>();

            switch (playerid)
            {
                case 1: 
                    dice_list = stats.PlayerOneDices; 
                    break;
                case 2:
                    dice_list = stats.PlayerTwoDices;
                    break;
                default:
                    Console.WriteLine("Player does not exist!");
                    break;
            }

            foreach (int dice in dice_list)
            {
                total += dice;
            }

            return total;
        }

    }
}
