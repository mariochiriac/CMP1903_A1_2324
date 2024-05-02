using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    class SevensOut : Game, IGame
    {
        // Objects
        private Die die_1;
        private Die die_2;
        private Statistics stats = new Statistics();

        // Protected Variables
        protected int _playerid = 1;
        protected bool gameStatus = true;

        // Public Variables
        public int PlayerID { get { return _playerid; } set { _playerid = value; } }
        public bool Gamestatus { get { return gameStatus; } set { gameStatus = value; } }

        public int TotalDices = 0;
        public bool TestVersion = false;

        public Statistics Stats { get { return this.stats; } set { this.stats = value; } }

        // Generates new dices each time SevensOut is instantiated
        public SevensOut()
        {
            die_1 = new Die();
            die_2 = new Die();
        }

        // Main method that will be called
        public override void StartGame()
        {
            try
            {
                // Lets user know which game mode they selected
                Console.WriteLine("================= SEVENS OUT =================");
                if (isComputer == true) Console.WriteLine("========== GAME MODE: PLAYING AGAINST COMPUTER ==========");
                else Console.WriteLine("========== GAME MODE: PLAYER 1 VS PLAYER 2 ==========");
                Console.WriteLine("======= GAME RULES =======\nROLL YOUR DICE\nIF 7 IS ROLLED, YOUR TURN IS OVER!\nPLAYER WITH THE MOST POINTS BEFORE A 7 IS ROLLED WINS!");
                Console.WriteLine("===========================");
                PlayGame(); // Plays game
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            
        }

        // Private method that rolls the dices
        public void RollDices(Die die_1, Die die_2)
        {

            // Roll the 2 die objects and store in variable
            int rolled_dice1 = die_1.Roll();
            int rolled_dice2 = die_2.Roll();

            // Sum of 2 rolled dices
            TotalDices = rolled_dice1 + rolled_dice2;
            if (rolled_dice1 == rolled_dice2) // Checks if it's a double
            {
                stats.AddDice(_playerid, TotalDices * 2); // Adds total of dices * 2 if it's a double to player
                Console.WriteLine($"A double has been rolled. {TotalDices * 2} has been added to Player {_playerid} total.");
                Console.WriteLine($"Player {_playerid} current total: {GetCurrentTotal(_playerid)}");
            }
            else // if it's not a double
            {
                // Checks if its a 7 or less
                if (TotalDices <= 7)
                {
                    Console.WriteLine($"Turn Over! Player {_playerid} has rolled less than seven! (Rolled {TotalDices})"); // Terminates player's turn
                    Console.WriteLine($"==== Player {_playerid} ended his streak with the total: {GetCurrentTotal(_playerid)} ====");
                    // Checks if it's player 1 turn
                    if (_playerid == 1)
                    {
                        // If it's player 1's turn and it's less than 7
                        // Switches turn to player 2
                        _playerid = 2;
                        Console.WriteLine($"It is now Player {_playerid} turn!");
                    }
                    else GetWinner(); // If player 2's turn, it will end the game and get winner
                }
                else // If it's not a 7 or less
                {
                    stats.AddDice(_playerid, TotalDices); // Adds the total of dices rolled to player
                    Console.WriteLine($"Player {_playerid} has rolled {TotalDices}");
                    Console.WriteLine($"Player {_playerid} current total: {GetCurrentTotal(_playerid)}");
                }
            }
            PlayGame(); // Will restart dice rolls
        }

        // Main Method for playing the game
        private void PlayGame()
        {
            while (gameStatus) // Checks if game is over
            {
                // Check if game mode is against player or computer
                    // If player 1 turn, or player 2 is not robot, ask to roll dice
                if (_playerid == 1 || !isComputer && _playerid == 2) 
                {
                    Console.WriteLine($"[Player {_playerid}] Type (Y) to roll the dice."); // Prompts user to roll the dice
                    string user_input = Console.ReadLine();

                    // Checks if user has entered "Y" or "y"
                    if (user_input.ToLower() == "y")
                    {
                        // If user rolls dice, the roll dices method is called
                        RollDices(die_1, die_2);
                        break;
                    }
                    // If user has not entered "y" or "Y"
                    else
                    {
                        // If player 1 does not roll dice, swaps turns instead of ending the game
                        if (_playerid == 1)
                        {
                            Console.WriteLine($"Player {_playerid} has not rolled his dice. It is Player {_playerid + 1} turn!");
                            _playerid = 2;
                        }
                        else
                        {
                            Console.WriteLine($"Player {_playerid} has not rolled the dice. Game Over!");
                            GetWinner(); // Ends game if user decides to not roll dice and gets the winner for current round
                            break;
                        }   
                    }
                }
                else // If robot turn, it will roll straight away
                {
                    Console.WriteLine();
                    Console.WriteLine("Computer is rolling...");
                    Thread.Sleep(1000); // Wait 1 second
                    RollDices(die_1, die_2);
                    break;
                }
                
            }
        }

        // Method that will prompt the user to play again
        private void PlayAgain()
        {
            while (true)
            {
                Console.WriteLine("== Type (Y) to restart Sevens Out! ==");
                string user_input = Console.ReadLine();

                // Checks if user has entered "y"
                if (user_input.ToLower() == "y")
                {
                    gameStatus = true; // Sets game status as true, meaning it can continue
                    ResetGame(); // Resets statistics
                    PlayGame(); // Starts the game
                    break;
                }
                else
                {
                    // Ends game
                    Console.WriteLine("You have chosen not to continue playing Sevens Out.");
                    Console.WriteLine("Thanks for playing! GAME ENDED.");
                    break;
                }
            }
        }

        // Resets statistic so user can play again
        // Encapsulated method
        private void ResetGame()
        {
            this._playerid = 1; // Switches back to player 1
            this.gameStatus = true;
            stats.ResetDices();
        }

        // Method responsible for comparisons and determining the winner
        public void GetWinner()
        {
            // Variables
            int player1_total = GetCurrentTotal(1);
            int player2_total = GetCurrentTotal(2);

            gameStatus = false; // Ends game so loops can stop

            Console.WriteLine($"GAME ENDED!\nPlayer 1 Total: {player1_total}\nPlayer 2 Total: {player2_total}");

            if (player1_total > player2_total)
            {
                stats.PlayerOne_Score++; // Accesses statistics class score
                Console.WriteLine($"Player 1 has won!\nCurrent Score:\nPlayer 1: {stats.PlayerOne_Score}\nPlayer 2: {stats.PlayerTwo_Score}");
            }
            else if (player1_total < player2_total)
            {
                stats.PlayerTwo_Score++; // Accesses statistics class score
                Console.WriteLine($"Player 2 has won!\nCurrent Score:\nPlayer 1: {stats.PlayerOne_Score}\nPlayer 2: {stats.PlayerTwo_Score}");
            }
            else
            {
                stats.PlayerOne_Score++;
                stats.PlayerTwo_Score++;
                Console.WriteLine($"The game is equal! Both players received a point.\nCurrent Score:\nPlayer 1: {stats.PlayerOne_Score}\nPlayer 2: {stats.PlayerTwo_Score}");
            }
            PlayAgain();
        }

        // Gets total of current dices rolled
        public int GetCurrentTotal(int playerid)
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
