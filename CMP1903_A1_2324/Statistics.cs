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
        private int _Plays;
        private int _PlayerOne_Score;
        private int _PlayerTwo_Score;
        private List<int> _PlayerOneDices;
        private List<int> _PlayerTwoDices;

        // Public Objects
        public int Plays {  get { return _Plays; } set {  _Plays = value; } }
        public int PlayerOne_Score { get { return _PlayerOne_Score; } set { _PlayerOne_Score = value;} }
        public int PlayerTwo_Score { get { return _PlayerTwo_Score; } set { _PlayerTwo_Score = value;} }
        public List<int> PlayerOneDices { get { return _PlayerOneDices; } set { _PlayerOneDices = value; } }
        public List <int> PlayerTwoDices { get { return _PlayerTwoDices; } set { _PlayerTwoDices = value; } }

        public Statistics()
        {
            // Resets statistics every time class is instantiated
            _Plays = 0;
            _PlayerOne_Score = 0;
            _PlayerTwo_Score = 0;
            _PlayerOneDices = new List<int>();
            _PlayerTwoDices = new List<int>();
        }

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

        public void AddScore(int playerid, int score)
        {
            switch (playerid)
            {
                case 1:
                    _PlayerOne_Score += score;
                    _Plays++;
                    break;
                case 2:
                    _PlayerTwo_Score += score;
                    _Plays++;
                    break;
                default:
                    Console.WriteLine("Wrong player turn!");
                    break;
            }
        }

        public int GetScore(int playerid)
        {
            switch (playerid)
            {
                case 1:
                    return _PlayerOne_Score;
                case 2:
                    return _PlayerTwo_Score;
                default:
                    Console.WriteLine("Wrong player!");
                    return 0;
            }
        }

        public void ResetDices()
        {
            _PlayerOneDices.Clear();
            _PlayerTwoDices.Clear();
        }
    }
}
