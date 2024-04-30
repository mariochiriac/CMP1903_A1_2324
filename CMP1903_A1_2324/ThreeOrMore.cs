using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Media;
using System.Text;
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
            Console.WriteLine("================= THREE OR MORE =================");
            PlayGame();
        }

        private void PlayGame()
        {
            while (!isWinner)
            {
                RollDices(dieList);
                CheckWinner();
            }
            // get final summary
        }

        public void RollDices(List<Die> dieList)
        {
            rolledDices.Clear();

            // Prompt the user to roll
            Console.WriteLine();
            Console.WriteLine($"[Player {_PlayerID}] Press Enter to roll the dice.");
            Console.ReadLine(); // Wait for user input to roll the dice

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

            switch (maxDuplicates)
            {
                case 2:
                    Console.WriteLine($"[Player {_PlayerID}] 2 of a kind, try again");
                    while (true)
                    {
                        int user_input = 0;
                        Console.WriteLine($"[Player {_PlayerID}] You have only managed to roll a 2-of-a-kind.\n(1) Roll the rest of the 3 dices || (2) Roll all dices again");
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
                    break;
                case 3:
                    Console.WriteLine($"[Player {_PlayerID}] 3 of a kind");
                    break;
                case 4:
                    Console.WriteLine($"[Player {_PlayerID}] 4 of a kind");
                    break;
                case 5:
                    Console.WriteLine($"[Player {_PlayerID}] 5 of a kind");
                    break;
                default:
                    Console.WriteLine("No Duplicates");
                    break;
            }
            SwapTurn();




            // Deciding based on the maximum count
            /*
            if (maxDuplicates < 2)
            {
                Console.WriteLine($"[Player {_PlayerID}] No duplicates");
            }
            else if (maxDuplicates == 2)
            {
                Console.WriteLine($"[Player {_PlayerID}] 2 of a kind, try again");
                while (true)
                {
                    int user_input = 0;
                    Console.WriteLine($"[Player {_PlayerID}] You have only managed to roll a 2-of-a-kind.\n(1) Roll the rest of the 3 dices || (2) Roll all dices again");
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
            else if (maxDuplicates == 3)
            {
                Console.WriteLine($"[Player {_PlayerID}] 3 of a kind");
            }
            else if (maxDuplicates == 4)
            {
                Console.WriteLine($"[Player {_PlayerID}] 4 of a kind");
            }
            else if (maxDuplicates == 5)
            {
                Console.WriteLine($"[Player {_PlayerID}] 5 of a kind");
            }

            // Swap turn after the current player's turn
            SwapTurn();
            */
        }

        public void SwapTurn()
        {
            if (_PlayerID == 1) _PlayerID = 2;
            else _PlayerID = 1;
            
        }

        public void CheckWinner()
        {
            switch (_PlayerID)
            {
                case 1:
                    if (stats.PlayerOne_Score >= 20) isWinner = true;
                    break;
                case 2:
                    if (stats.PlayerTwo_Score >= 20) isWinner = true;
                    break;
                default:
                    Console.WriteLine("Wrong player selected!");
                    break;
            }
        }
    }
}
