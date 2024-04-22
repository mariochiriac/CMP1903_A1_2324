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

        private int[] _PlayerOne { get; set; } = { };
        private int[] _PlayerTwo { get; set; } = { };



        public void AddDice(int playerid, int dice)
        {
            switch (playerid) {
                case 1:
                    _PlayerOne.Append(dice);
                    _Plays++;
                    _PlayerOne_Highscore += dice;
                    break;
                case 2:
                    _PlayerTwo.Append(dice);
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
