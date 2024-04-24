using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    public class Statistics
    {
        private int _Plays = 0;


        private int _PlayerOne_Highscore = 0;
        private int _PlayerTwo_Highscore = 0;

        private List<int> _PlayerOne = new List<int>();
        private List<int> _PlayerTwo = new List<int>();

        public void AddDice(int playerid, int dice)
        {
            switch (playerid) {
                case 1:
                    _PlayerOne.Add(dice);
                    _Plays++;
                    _PlayerOne_Highscore += dice;
                    Console.WriteLine($"Current Highscore: {_PlayerOne_Highscore}");
                    break;
                case 2:
                    _PlayerTwo.Add(dice);
                    _Plays++;
                    _PlayerTwo_Highscore += dice;
                    break;
                default:
                    Console.WriteLine("Wrong player turn!");
                    break;
            }
        }

    }
}
