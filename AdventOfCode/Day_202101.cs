using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202101: BaseDay
    {
        private readonly List<int> _input;

        public Day_202101()
        {
            _input = File.ReadAllLines(InputFilePath).Select(l => int.Parse(l)).ToList();
        }

        public override string Solve_1()
        {
            int increases = 0;

            for (int i = 1; i < _input.Count; i++)
            {
                increases += _input[i] > _input[i - 1] ? 1 : 0;
            }

            return CountIncreases(_input).ToString();
        }

        public override string Solve_2()
        {
            List<int> result = new List<int>();
            
            for (int i = 0; i < _input.Count - 2; i++)
            {
                int amount = _input[i] + _input[i + 1] + _input[i + 2];
                result.Add(amount);
            }

            return CountIncreases(result).ToString();
        }

        private int CountIncreases(List<int> collection)
        {
            var count = 0;
            for (int i = 1; i < collection.Count; i++)
            {
                count += collection[i] > collection[i - 1] ? 1 : 0;
            }

            return count;
        }
    }
}