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
            // Add 5 Die Objects to List
            dieList.Add(new Die());
            dieList.Add(new Die());
            dieList.Add(new Die());
            dieList.Add(new Die());
            dieList.Add(new Die());
            // Add 3 Die Objects to Temporary List
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
                PlayGame(); // Plays game
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        // Plays game
        private void PlayGame()
        {
            // Check if players meet requirements to win game
            while (!isWinner)
            {
                RollDices(dieList);
                CheckWinner(); // After rolling, check if requirements met for winning
            }
            GetSummary(); // Outputs final scores

        }

        public void RollDices(List<Die> dieList)
        {
            rolledDices.Clear(); // Clears list for new rolled dice values

            // Prompt the user to roll if not a robot
            if (_PlayerID == 1 || !isComputer && _PlayerID == 2)
            {
                Console.WriteLine();
                Console.WriteLine($"[Player {_PlayerID}] Press Enter to roll the dice.");
                Console.ReadLine(); // Wait for user input to roll the dice
            }
            // If robot turn, it will roll without asking for input
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

        // Method to output dices rolled
        private void OutputDices(List<int> list)
        {
            Console.WriteLine($"=== [PLAYER {_PlayerID}] ROLLED DICES ===");
            foreach (var die in list)
            {
                Console.WriteLine(die);
            }
        }

        // Method to check if user rolled duplicates
            // 2 duplicates - 2 of a kind
            // 3 duplicates - 3 of a kind
            // 4 duplicates - 4 of a kind
            // 5 duplicates - 5 of a kind
        private void FindDuplicates(List<int> list)
        {
            // LINQ Query to check and count duplicates
            var counts = list.GroupBy(x => x)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Count());

            // Finding the maximum count of duplicates
            int maxDuplicates = counts.DefaultIfEmpty(0).Max();
            bool swapTurnAfterDuplicate = true;

            // Different actions depending on number of duplicates
            switch (maxDuplicates)
            {
                case 2:
                    Console.WriteLine($"[Player {_PlayerID}] 2 of a kind, try again");
                    while (true)
                    {
                        // Check if it's robot turn
                        if (isComputer && _PlayerID == 2)
                        {
                            // Generate random value
                            Random random = new Random();
                            int random_choice = random.Next(1, 3);

                            // If 2 of a kind, reroll depending on random value
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
                        // Checks if it's player's turn
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

    }

        // Swap player turn
        public void SwapTurn()
        {
            if (_PlayerID == 1) _PlayerID = 2;
            else _PlayerID = 1;
            
        }

        // Check if requirement met for winning game
        public void CheckWinner()
        {
            if (stats.PlayerOne_Score >= 20 || stats.PlayerTwo_Score >= 20) isWinner = true;
        }

        // Overrides method of parent class
        // Outputs summary
        public override void GetSummary()
        {
            int winner = _PlayerID;
            if (isWinner) Console.WriteLine($"Player {winner} HAS WON THE GAME!");
            Console.WriteLine($"Player 1 points: {stats.PlayerOne_Score}");
            Console.WriteLine($"Player 2 points: {stats.PlayerTwo_Score}");
        }
    }
}
