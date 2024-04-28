using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    public class Statistics
    {
        // Private Objects
        private int _Plays = 0;
        private int _PlayerOne_Score = 0;
        private int _PlayerTwo_Score = 0;
        private List<int> _PlayerOneDices = new List<int>();
        private List<int> _PlayerTwoDices = new List<int>();

        // Public Objects
        public int Plays {  get { return _Plays; } set {  _Plays = value; } }
        public int PlayerOne_Score { get { return _PlayerOne_Score; } set { _PlayerOne_Score = value;} }
        public int PlayerTwo_Score { get { return _PlayerTwo_Score; } set { _PlayerTwo_Score = value;} }
        public List<int> PlayerOneDices { get { return _PlayerOneDices; } set { _PlayerOneDices = value; } }
        public List <int> PlayerTwoDices { get { return _PlayerTwoDices; } set { _PlayerTwoDices = value; } }

        public void AddDice(int playerid, int dice)
        {
            switch (playerid) {
                case 1:
                    _PlayerOneDices.Add(dice);
                    _Plays++;
                    break;
                case 2:
                    _PlayerTwoDices.Add(dice);
                    _Plays++;
                    break;
                default:
                    Console.WriteLine("Wrong player turn!");
                    break;
            }
        }

    }
}
