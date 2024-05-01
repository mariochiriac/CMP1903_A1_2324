using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class ThreeOrMore : Game, IGame
    {
        // Objects
        Statistics stats = new Statistics();
        public List<Die> dieList = new List<Die>();
        public List<int> rolledDices = new List<int>();
        public List<int> duplicateDices = new List<int>();
        public List<Die> tempDieList = new List<Die>();


        // Variables
        public int _PlayerID = 1;
        public bool isWinner = false;

        public ThreeOrMore()
        {
            dieList.Add(new Die());
            dieList.Add(new Die());
            dieList.Add(new Die());
            dieList.Add(new Die());
            dieList.Add(new Die());
            tempDieList.Add(new Die());
            tempDieList.Add(new Die());
            tempDieList.Add(new Die());
        }
        public override void StartGame()
        {
            try
            {
                Console.WriteLine("================= THREE OR MORE =================");
                if (isComputer == true) Console.WriteLine("========== GAME MODE: PLAYING AGAINST COMPUTER ==========");
                else Console.WriteLine("========== GAME MODE: PLAYER 1 VS PLAYER 2 ==========");
                PlayGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void PlayGame()
        {
            while (!isWinner)
            {
                RollDices(dieList);
                CheckWinner();
            }
            GetSummary();

        }

        public void RollDices(List<Die> dieList)
        {
            rolledDices.Clear();

            // Prompt the user to roll
            if (_PlayerID == 1 || !isComputer && _PlayerID == 2)
            {
                Console.WriteLine();
                Console.WriteLine($"[Player {_PlayerID}] Press Enter to roll the dice.");
                Console.ReadLine(); // Wait for user input to roll the dice
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Computer is rolling...");
                Thread.Sleep(1500);
            }
            // If no one met the points to win the game, continue
            if (!isWinner) // Check if there is a winner
            {
                foreach (var die in dieList) // Checks all die objects
                {
                    rolledDices.Add(die.Roll()); // Adds each rolled die to a new list
                }
                OutputDices(rolledDices); // Outputs the rolled dices
                FindDuplicates(rolledDices); // Finds how many duplicate dices have been found
            }
            else
            {
                Console.WriteLine($"Player {_PlayerID} has won!"); // If there is a winner, it is declared
            }
        }

        private void OutputDices(List<int> list)
        {
            Console.WriteLine($"=== [PLAYER {_PlayerID}] ROLLED DICES ===");
            foreach (var die in list)
            {
                Console.WriteLine(die);
            }
        }

        private void FindDuplicates(List<int> list)
        {
            // Grouping by number and counting duplicates
            var counts = list.GroupBy(x => x)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Count());

            // Finding the maximum count of duplicates
            int maxDuplicates = counts.DefaultIfEmpty(0).Max();
            bool swapTurnAfterDuplicate = true;

            switch (maxDuplicates)
            {
                case 2:
                    Console.WriteLine($"[Player {_PlayerID}] 2 of a kind, try again");
                    while (true)
                    {
                        if (isComputer && _PlayerID == 2)
                        {
                            Random random = new Random();
                            int random_choice = random.Next(1, 3);

                            if (random_choice == 1)
                            {
                                Console.WriteLine("Computer has rolled the remaining 3 dices.");
                                RollDices(tempDieList);
                                break;
                            }
                            else if (random_choice == 2)
                            {
                                Console.WriteLine("Computer has rolled rerolled all the dices.");
                                RollDices(dieList);
                                break;
                            }
                        }
                        else
                        {
                            int user_input = 0;
                            Console.WriteLine($"[Player {_PlayerID}] You have only managed to roll a 2-of-a-kind.\n(1) Roll the remaining 3 dices || (2) Roll all dices again");
                            int.TryParse(Console.ReadLine(), out user_input);

                            if (user_input == 1)
                            {
                                RollDices(tempDieList);
                                break;
                            }
                            else if (user_input == 2)
                            {
                                RollDices(dieList);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Please enter a valid option! (1 - 2)");
                            }
                        }
                    }
                    swapTurnAfterDuplicate = false; // No need to swap turn after this case
                    break;
                case 3:
                    stats.AddScore(_PlayerID, 3);
                    Console.WriteLine($"[Player {_PlayerID}] Rolled 3 of a kind.\n+ 3 points earned.\n[Player {_PlayerID}] Current Points: {stats.GetScore(_PlayerID)}");
                    break;
                case 4:
                    stats.AddScore(_PlayerID, 6);
                    Console.WriteLine($"[Player {_PlayerID}] Rolled 4 of a kind.\n+ 6 points earned.\n[Player {_PlayerID}] Current Points: {stats.GetScore(_PlayerID)}");
                    break;
                case 5:
                    stats.AddScore(_PlayerID, 12);
                    Console.WriteLine($"[Player {_PlayerID}] Rolled 5 of a kind.\n+ 12 points earned.\n[Player {_PlayerID}] Current Points: {stats.GetScore(_PlayerID)}");
                    break;
                default:
                    Console.WriteLine($"No Duplicates\n[Player {_PlayerID}] Current Points: {stats.GetScore(_PlayerID)}");
                    break;
            }

            // Swap turn after the current player's turn, if needed
            if (swapTurnAfterDuplicate)
            {
                SwapTurn();
            }

        // Deciding based on the maximum count
    }

        public void SwapTurn()
        {
            if (_PlayerID == 1) _PlayerID = 2;
            else _PlayerID = 1;
            
        }

        public void CheckWinner()
        {
            if (stats.PlayerOne_Score >= 20 || stats.PlayerTwo_Score >= 20) isWinner = true;
        }

        public override void GetSummary()
        {
            int winner = _PlayerID;
            if (isWinner) Console.WriteLine($"Player {winner} HAS WON THE GAME!");
            Console.WriteLine($"Player 1 points: {stats.PlayerOne_Score}");
            Console.WriteLine($"Player 2 points: {stats.PlayerTwo_Score}");
        }
    }
}
