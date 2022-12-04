using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202003 : BaseDay
    {
        private readonly char[][] _input;

        public Day_202003()
        {
            _input = File.ReadAllLines(InputFilePath).Select(str => str.ToCharArray()).ToArray();
        }

        public override string Solve_1()
        {
            var xAxisLength = _input.First().Count();
            var yAxisLength = _input.Count();
            var slope = new Point(3, 1);
            var currentLocation = new Point(0,0);
            var treeCount = 0;


            for (int i = 0; i < yAxisLength; i = currentLocation.Y)
            {
                if (_input[currentLocation.Y][currentLocation.X % xAxisLength].ToString() == "#")
                {
                    treeCount++;
                }

                currentLocation.Offset(slope);
            }

            return treeCount.ToString();
        }

        public override string Solve_2()
        {
            List<Point> slopes = new List<Point>()
            {
                new Point(1,1),
                new Point(3,1),
                new Point(5,1),
                new Point(7,1),
                new Point(1,2)
            };

            long treeCountMultiplied = 0;
            List<int> treeCounts = slopes.Select(slope => TreeFinder.CountTreesInFieldByGivenSlope(_input, slope)).ToList();

            for (int i = 0; i + 1 < treeCounts.Count; i++)
            {
                if (i == 0)
                {
                    treeCountMultiplied = treeCounts[i] * treeCounts[i + 1];
                }
                else
                {
                    treeCountMultiplied *= treeCounts[i + 1];
                }

            }

            return treeCountMultiplied.ToString();
        }
    }

    public static class TreeFinder
    {
        public static int CountTreesInFieldByGivenSlope(char[][] field, Point slope)
        {
            var xAxisLength = field.First().Count();
            var yAxisLength = field.Count();
            var currentLocation = new Point(0, 0);
            var treeCount = 0;


            for (int i = 0; i < yAxisLength; i = currentLocation.Y)
            {
                if (field[currentLocation.Y][currentLocation.X % xAxisLength].ToString() == "#")
                {
                    treeCount++;
                }

                currentLocation.Offset(slope);
            }

            return treeCount;
        }
    }
}