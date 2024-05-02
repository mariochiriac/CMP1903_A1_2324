using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CMP1903_A1_2324
{
    internal class Testing
    {
        /*
         * This class should test the Game and the Die class.
         * Create a Game object, call the methods and compare their output to expected output.
         * Create a Die object and call its method.
         * Use debug.assert() to make the comparisons and tests.
         */

        //Method

        public void StartTest()
        {
            try
            {
                Console.WriteLine("=========== TEST VERSION ===========");
                Console.WriteLine("Which version would you like to test?\n(1) Game Class\n(2) Die Class\n(3) Sevens Out Game\n(4) Three or More Game");
                int user_choice = 0;

                while (true)
                {
                    int.TryParse(Console.ReadLine(), out user_choice);

                    if (user_choice == 1)
                    {
                        TestGame();
                        break;
                    }
                    else if (user_choice == 2)
                    {
                        TestDie();
                        break;
                    }
                    else if (user_choice == 3)
                    {
                        TestSevensOut();
                        break;
                    }
                    else if (user_choice == 4)
                    {
                        TestThreeOrMore();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid choice! ( 1 - 3 ).");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Encapsulated Method
        private void TestGame()
        {
            // Create new game
            Game game = new Game();
            game.StartGame(); // Call method

            int sum = 0;
            int min_sum = 3;
            int max_sum = 18;

            // A loop that goes through all of the dices rolled in the game
            foreach (int dice in game.DiceList) // Checks each dice rolled, stored in a list
            {

                Debug.Assert(dice <= 6 && dice >= 1, $"The dice rolled ({dice}) from the list of dices ({game.DiceList}) is not between 1 and 6"); // Compares each dice rolled, checking if it is equal or higher than 1, equal or less than 6
                sum = sum + dice;
            }

            // Checks if the total of the dice values are within the allowed limit
            Debug.Assert(sum >= min_sum && sum <= max_sum, "The sum of the dice values are not within the limit. (3 - 18).");
            // Outputs
            Console.WriteLine("===== The sum of dice rolls are as expected (OK) =====");
        }

        // Encapsulated Method
        private void TestDie()
        {
            // Created new Die
            Die dice_test = new Die();
            int rolled_dice = dice_test.Roll(); // Calls the method in Die

            Debug.Assert(rolled_dice <= 6 && rolled_dice >= 1); // Compares output received from Die, ensuring it is less or equal than 6, equal or higher than 1
            // Outputs
            Console.WriteLine("========= Die rolls are between 1 and 6 (OK) =========");
        }

        // Encapsulated Method
        private void TestSevensOut()
        {
            // Instantiate Sevens Out
            SevensOut sevensOut = new SevensOut();
            sevensOut.TestVersion = true;

            // Lists with dices, will use to compare if game correctly compares scores
            List<int> list_20 = new List<int>();
            List<int> list_10 = new List<int>();

            list_20.Add(20); // Adds score 20 to list
            list_10.Add(10); // Adds score 10 to list

            sevensOut.Stats.PlayerOneDices = list_20; // Sets Player 1 total dices as 20
            sevensOut.Stats.PlayerTwoDices = list_10; // Sets player 2 total dices as 10

            int player_1_dice_score = sevensOut.GetCurrentTotal(1);
            int player_2_dice_score = sevensOut.GetCurrentTotal(2);
            Console.WriteLine("====== [TEST VERSION] CALCULATING WINNER... ======");
            sevensOut.GetWinner(); // Gets winner, player 1 should have a higher score than player 2
            Debug.Assert(sevensOut.Gamestatus == false, "Game status has not been set to false once the winner has been checked!");

            int player_1_final = sevensOut.Stats.PlayerOne_Score; // Stores final score of player 1 
            int player_2_final = sevensOut.Stats.PlayerTwo_Score; // Stores final score of player 2

            // When comparing scores and total dices, player 1 should have a better score and should win the game, giving him a Player Score 1
            Debug.Assert(player_1_dice_score > player_2_dice_score, "Sevens Out: Player 1 (SCORE 20) does not a higher score than Player 2 (SCORE 10).");
            Debug.Assert(player_1_final > player_2_final, "Sevens Out: Player 1 should have won the game (SCORE 20), not Player 2 (SCORE 10).");

            Console.WriteLine("Sevens Out: Winner has been calculated successfully.");
            Console.WriteLine("Player 1 Dice Score (20) is higher than Player 2 Dice Score (10).");
            Console.WriteLine("Player 1 (Score 1) has won more times than Player 2 (Score 0).");

            Console.WriteLine("=========== Sevens Out Test Passed ===========");
        }

        private void TestThreeOrMore()
        {

            // Test Three Or More: Scores set and added correctly, recognize when total >= 20
            ThreeOrMore threeOrMore = new ThreeOrMore();

            // Total = 11, register score
            threeOrMore.Stats.AddScore(1, 11); // Player 1 add score 11
            Debug.Assert(threeOrMore.Stats.PlayerOne_Score == 11, "Three or More: Player score has not been registered correctly.");
            Console.WriteLine("Three or More: Player score has been registered successfully.");
            // Total = 18, continue
            threeOrMore.Stats.AddScore(2, 18); // Player 2 add score 18
            Debug.Assert(threeOrMore.isWinner == false, "Three or More: Game did not continue when total was less than 20.");
            Console.WriteLine("Three or More: Game has continued successfully.");
            // Total = 21, game should stop
            threeOrMore.Stats.PlayerOne_Score = 21;
            threeOrMore.CheckWinner();
            Debug.Assert(threeOrMore.isWinner = true, "Three or More: Winner has not been recorded successfully.");
            Console.WriteLine("Winner has been recorded successfully!");

            Console.WriteLine("=========== Three or More Test Passed ===========");
        }
    }
}
