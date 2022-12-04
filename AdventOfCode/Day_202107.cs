using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202107 : BaseDay
    {
        private readonly IEnumerable<int> _input;

        public Day_202107()
        {
            _input = File.ReadAllText(InputFilePath).Split(",").Select(n => int.Parse(n));
        }

        public override string Solve_1()
        {
            return _input.Select(input => CalculateRequiredFuel(input)).Min().ToString();
        }

        public override string Solve_2()
        {
            return _input.Select(input =>
            {
                return _input.Select(i => CalculateRequiredFuel(input)).Sum();
            }).Min().ToString();
        }

        private int CalculateRequiredFuel(int position, bool increasesExponentially = false)
        {
            if (increasesExponentially)
            {

            }

            return _input.Select(i => Math.Abs(i - position)).Sum();
        }
    }
}