using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_202001 : BaseDay
    {
        private readonly List<int> _input;

        public Day_202001()
        {
            _input = File.ReadAllLines(InputFilePath).Select(int.Parse).ToList();
        }

        public override string Solve_1() 
        {
            for (int i = 0; i < _input.Count; i++)
            {
                var possibleMatches = _input;
                var item = possibleMatches[i];
                var balance = 2020 - item;
                possibleMatches.ToList().Remove(possibleMatches[i]);

                if (possibleMatches.Contains(balance))
                return (item * balance).ToString();
            }
            return "error";
        }

        public override string Solve_2()
        {
            for (int i = 0; i < _input.Count; i++)
            {
                var possibleSecondMatches = _input;
                var firstItem = possibleSecondMatches[i];
                var initialBalance = 2020 - firstItem;
                possibleSecondMatches.ToList().Remove(possibleSecondMatches[i]);

                for (int j = 0; j < possibleSecondMatches.Count; j++)
                {
                    var possibleFinalMatches = _input;
                    var finalItem = possibleFinalMatches[j];
                    var finalBalance = initialBalance - finalItem;
                    possibleFinalMatches.ToList().Remove(possibleFinalMatches[j]);

                    if (possibleFinalMatches.Contains(finalBalance))
                        return (firstItem * finalItem * finalBalance).ToString();
                }

            }

            return "error";
        }
    }
}
