using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    class Game
    {
        class SevensOut
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
            }

            // Private method that rolls the dices
            private void RollDices(Die die_1, Die die_2)
            {
                int rolled_dice1 = die_1.Roll();
                int rolled_dice2 = die_1.Roll();

                int total_dices = rolled_dice1 + rolled_dice2;
                if (rolled_dice1 == rolled_dice2) {
                    stats.AddDice(_playerid, total_dices * 2);
                }
                else {
                    if (total_dices <= 7) {
                        Console.WriteLine($"Game Over! Total sum of dices: {total_dices}.");
                    }
                    else {
                        stats.AddDice(_playerid, total_dices);
                        Console.WriteLine($"Player {_playerid} has rolled {total_dices}");
                    }
                }
            }

        }

        class ThreeOrMore
        {

        }





        /*
         * The Game class should create three die objects, roll them, sum and report the total of the three dice rolls.
         *
         * EXTRA: For extra requirements (these aren't required though), the dice rolls could be managed so that the
         * rolls could be continous, and the totals and other statistics could be summarised for example.
         */

        //Properties

        // Creates 3 Die objects
        SevensOut sOut = new SevensOut();

        private Die dice = new Die();
        private Die dice_2 = new Die(); 
        private Die dice_3 = new Die();

        private List<int> _diceList = new List<int>(); // Creates a new encapsulated List

        private int sumOfDices = 0; // Creates an integer that holds the sum of the dices

        public List<int> DiceList
        {
            get { return _diceList; }
        }

        //Methods

        // An encapsulated method that takes multiple Die objects in its parameters
        private void RollDices(params Die[] dices)
        {
            // A loop that checks each object in the parameters
            foreach (Die dice in dices)
            {
                int result = dice.Roll(); // Rolls each 'Die' object
                Console.WriteLine("Diced Rolled: " + result); // Outputs the dice rolled
                _diceList.Add(result); // Adds rolled dice to list
            }
        }

        // An encapsulated method that gets the dice total
        private int GetDiceTotal()
        {
            // A loop that checks how many dice rolled have been added to a list
            for (int i = 0; i < _diceList.Count; i++)
            {
                sumOfDices += _diceList[i];
            }

            return sumOfDices;
        }

        // An encapsulated method that outputs the summary data of the game
        private void GetSummary()
        {
            // Variables
            int lowest = _diceList[0]; // Initialises the lowest number to first element of array
            int highest = _diceList[0]; // Initialises the highest number to first element of array
            int average = sumOfDices / _diceList.Count; // Gets the average number of the dices rolled

            // A loop that performs a search for the lowest and highest number
            for (int i = 0; i < _diceList.Count; i++) // Checks each index in the array
            {
                if (_diceList[i] < lowest) // Compares index in the array to the lowest number
                {
                    lowest = _diceList[i]; // Sets the lowest number
                }
                else if (_diceList[i] > highest) // Compares index in the array to the highest number
                {
                    highest = _diceList[i]; // Sets the highest number
                }
            }

            // Prints out a summary of the game
            Console.WriteLine("-------- DICE ROLL GAME : SUMMARY --------");
            Console.WriteLine("Average of the dice rolled (INT): " + average);
            Console.WriteLine("Highest dice rolled: " + highest);
            Console.WriteLine("Lowest dice rolled: " + lowest);
            Console.WriteLine("---------------  THE END   ---------------");

        }
        
        // Encapsulated method, will allow the user to play again in a continuous manner
        private void ContinueGame()
        {
            Console.WriteLine("Play Again? (Type Y to play again / Press any key to end game.)"); // Prompts the user for input
            Game new_game = new Game(); // Creates a new game object

            string user_input = Console.ReadLine(); // Takes input from user

            if (user_input.ToLower() == "y") // Checks if the user has entered the letter Y
            {
                new_game.StartGame(); // If condition is met, a new game will start
            }
            else return; // If any other input is entered, nothing will happen

        }

        // Main method, public, will be used to start the game
        public void StartGame()
        {
            sOut.Play();
            RollDices(dice, dice_2, dice_3);
            Console.WriteLine("Sum of Dice Rolled: " + GetDiceTotal());
            GetSummary();
            ContinueGame();
        }
    }
}
