using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day21
    {
        static int score1 = 0;
        static int score2 = 0;

        static int player1 = 0;
        static int player2 = 0;

        static int numRolls = 0;
        public string Part1()
        {
            var positions = GetData().ToList();
            player1 = positions[0];
            player2 = positions[1];

            var firstPlayer = true;

            while(!CheckGame())
            {
                if (firstPlayer)
                {
                    UpdatePlayer1(Roll());
                    UpdatePlayer1(Roll());
                    UpdatePlayer1(Roll());

                    score1 += player1;
                }
                else
                {
                    UpdatePlayer2(Roll());
                    UpdatePlayer2(Roll());
                    UpdatePlayer2(Roll());

                    score2 += player2;
                }

                firstPlayer = !firstPlayer;
            }

            return (Math.Min(score1, score2) * numRolls).ToString();
        }

        public void UpdatePlayer1(int roll)
        {
            player1 = (player1 + roll) % 10; 
            if (player1 == 0) player1 = 10;
            
        }

        public void UpdatePlayer2(int roll)
        {
            player2 = (player2 + roll) % 10;
            if (player2 == 0) player2 = 10;
        }

        public bool CheckGame()
        {
            if (score1 >= 1000 || score2 >= 1000) return true;

            return false;
        }

        int dd = 0;
        public int Roll()
        {
            numRolls++;
            dd = (dd + 1) % 100;
            if (dd == 0) dd = 100;

            return dd;
        }

        public string Part2() 
        {
            return string.Empty;
        }

        private static IEnumerable<int> GetData()
        {
            return File.ReadAllLines("Day21\\day21.txt").Select(t => int.Parse(t.Split(':')[1].Trim()));

            
        }

    }
}
