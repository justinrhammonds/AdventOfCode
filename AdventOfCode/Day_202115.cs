using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202115 : BaseDay
    {
        private readonly int[][] _input;

        public Day_202115()
        {
             _input = File.ReadAllLines(InputFilePath).Select(l => l.ToArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
        }

        public override string Solve_1()
        {
            Point prevLocation = new Point(0, 0);
            for (int y = 0; y < _input.Count(); y++)
            {
                for (int x = 0; x < _input[y].Count(); x++)
                {

                    // not prev x,y
                    // not x,y
                    // if x+1 <= _input[y].Count() --> x+1,y
                    // if x-1 >= 0 --> x-1,y
                    // if y+1 <= _input.Count() --> x,y+1
                    // if y-1 >= 0 --> x,y-1
                }
            }

            return "not implemented";
        }

        public override string Solve_2()
        {
            return "not implemented";
        }
    }
}