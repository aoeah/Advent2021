using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day4
    {
        public string Part1() 
        {
            var finalResult = string.Empty;

            var data = GetData();

            foreach(var num in data.CalledNumbers)
            {
                foreach(var board in data.Boards)
                {
                    for(var i = 0; i < 5; i++)
                    {
                        for(var j = 0; j < 5; j++)
                        {
                            if (board[i, j] == num) board[i, j] = -1;
                        }
                    }

                    if(Judge(board))
                    {
                        var resultVal = 0;
                        //calculate result
                        for (var i = 0; i < 5; i++)
                        {
                            for (var j = 0; j < 5; j++)
                            {
                                if (board[i, j] != -1) resultVal += board[i,j];
                            }
                        }

                        return (resultVal * num).ToString();
                    }
                }
            }

            return 0.ToString();
        }

        public bool Judge(int[,] board)
        {
            for(var i = 0; i < 5; i++)
            {
                if (board[i, 0] + board[i, 1] + board[i, 2] + board[i, 3] + board[i, 4] == -5) return true;
                if (board[0, i] + board[1, i] + board[2, i] + board[3, i] + board[4, i] == -5) return true;
            }

            return false;
        }

        public string Part2() 
        {
            var finalResult = string.Empty;

            var data = GetData();
            var winners = new List<int>();

            foreach (var num in data.CalledNumbers)
            {
                var boardId = 0;
                foreach (var board in data.Boards)
                {
                    boardId++;

                    if (winners.Contains(boardId)) continue;

                    for (var i = 0; i < 5; i++)
                    {
                        for (var j = 0; j < 5; j++)
                        {
                            if (board[i, j] == num) board[i, j] = -1;
                        }
                    }

                    if (Judge(board))
                    {

                        var resultVal = 0;
                        //calculate result
                        for (var i = 0; i < 5; i++)
                        {
                            for (var j = 0; j < 5; j++)
                            {
                                if (board[i, j] != -1) resultVal += board[i, j];
                            }
                        }

                        finalResult = (resultVal * num).ToString();
                        winners.Add(boardId);
                    }
                }
            }

            return finalResult;
        }

        private static Game GetData()
        {
            var lines = File.ReadAllLines("Day4\\day4.txt");
            var game = new Game();

            game.CalledNumbers = lines[0].Trim().Split(',').Select(t => Convert.ToInt32(t)).ToList();

            for(var i = 2; i < lines.Length; i = i + 6)
            { 
                var board = new int[5, 5];

                for(var j = 0; j < 5; j++)
                {
                    var values = lines[i+j].Trim().Split(' ').Where(t => !string.IsNullOrWhiteSpace(t)).Select(n => Convert.ToInt32(n)).ToList();

                    for(var k = 0; k < 5; k++)
                    {
                        board[j, k] = values[k];
                    }
                }

                game.Boards.Add(board);
            }

            return game;
        }

        private class Game
        {
            public List<int> CalledNumbers { get; set; }
            public List<int[,]> Boards { get; set; } = new List<int[,]>();
        }
    }
}
