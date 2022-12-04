using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202104 : BaseDay
    {
        private readonly IEnumerable<IEnumerable<IEnumerable<string>>> _boards;
        private readonly IEnumerable<string> _numberSet;

        public Day_202104()
        {
            _numberSet = File.ReadAllLines(InputFilePath)[0].Split(",");
            _boards = File.ReadAllText(InputFilePath).Split("\r\n\r\n").Where(i => !Regex.IsMatch(i, @",")).Select(board => board.Split("\r\n").Select(row => row.Replace("  ", " ").TrimStart().Split(" ")));
        }

        public override string Solve_1()
        {
            List<List<List<string>>> boards = _boards.Select(b => b.Select(r => r.ToList()).ToList()).ToList();
            List<string> numberSet = _numberSet.ToList();

            for (int n = 0; n < _numberSet.Count(); n++)
            {
                for (int b = 0; b < _boards.Count(); b++)
                {
                    for (int r = 0; r < boards[b].ToList().Count; r++)
                    {
                        if (boards[b][r].Any(i => i.Equals(numberSet[n])))
                        {
                            var indexOfMatch = boards[b][r].FindIndex(num => num.Equals(numberSet[n]));
                            boards[b][r][indexOfMatch] = "x";

                            var row = boards[b][r];
                            var column = boards[b].Select(r => r[indexOfMatch]).ToList();
                            if (IsComplete(row) || IsComplete(column))
                            {
                                return CalculateScore(boards[b], numberSet[n]).ToString();
                            }
                        }
                    }
                }
            }

            return "didn't solve";
        }

        public override string Solve_2()
        {
            List<List<List<string>>> boards = _boards.Select(b => b.Select(r => r.ToList()).ToList()).ToList();
            List<string> numberSet = _numberSet.ToList();

            for (int n = 0; n < numberSet.Count(); n++)
            {
                if (numberSet[n] == "13") {
                    Console.WriteLine("ping");
                }
                if (boards.Count.Equals(1))
                {
                    var finalBoard = boards[0];
                    for (int r = 0; r < finalBoard.ToList().Count; r++)
                    {
                        if (finalBoard[r].Any(i => i.Equals(numberSet[n])))
                        {
                            var indexOfMatch = finalBoard[r].FindIndex(num => num.Equals(numberSet[n]));
                            finalBoard[r][indexOfMatch] = "x";

                            var row = finalBoard[r];
                            var column = finalBoard.Select(r => r[indexOfMatch]).ToList();
                            if (!IsComplete(row) && !IsComplete(column))
                            {
                                break;
                            }
                            return CalculateScore(finalBoard, numberSet[n]).ToString();
                        }
                    }
                }

                for (int b = 0; b < boards.Count(); b++)
                {
                    ///// need to account for setting 'x's on the boards after a board/row/num wins
                    for (int r = 0; r < boards[b].ToList().Count; r++)
                    {
                        if (boards[b][r].Any(i => i.Equals(numberSet[n])))
                        {
                            var indexOfMatch = boards[b][r].FindIndex(num => num.Equals(numberSet[n]));
                            boards[b][r][indexOfMatch] = "x";

                            var row = boards[b][r];
                            var column = boards[b].Select(r => r[indexOfMatch]).ToList();
                            if (IsComplete(row) || IsComplete(column))
                            {
                                boards.RemoveAt(b);
                                b -= 1;
                                break;
                            }
                        }
                    }
                }

            }

            return "didn't solve";
        }

        private bool IsComplete(List<string> set)
        {
            return !set.Any(n => !n.Equals("x"));
        }

        private int CalculateScore(List<List<string>> board, string winningNumber)
        {
            return board.SelectMany(i => i).Where(s => !s.Equals("x")).Select(n => int.Parse(n)).Sum() * (int.Parse(winningNumber));
        }
    }
}