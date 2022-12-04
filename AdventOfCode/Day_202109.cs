using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202109 : BaseDay
    {
        private readonly List<List<int>> _input;

        public Day_202109()
        {
            _input = File.ReadAllLines(InputFilePath).Select(s => s.ToCharArray().Select(s => int.Parse(s.ToString())).ToList()).ToList();
        }

        public override string Solve_1()
        {
            List<Location> locations = new List<Location>();
            for (int y = 0; y < _input.Count; y++)
            {
                for (int x = 0; x < _input[y].Count; x++)
                {
                    locations.Add(new Location(_input, _input[y][x], new Point(x, y)));
                }
            }

            return locations.Where(l => l.IsLowPoint).Select(l => l.Height).Sum().ToString();
        }

        public override string Solve_2()
        {
            return "not implemented";
        }
    }

    public class Location
    {
        public Location(List<List<int>> heightmap, int height, Point location)
        {
            Height = height;
            Risk = height + 1;
            Point = location;

            GetAdjacentPoints(heightmap);

            IsLowPoint = !AdjacentPoints.Select(p => heightmap[p.Y][p.X]).Any(v => v >= Height);
        }

        public int Height { get; set; }
        public int Risk { get; set; }
        public Point Point { get; set; }
        public List<Point> AdjacentPoints { get; set; }
        public bool IsLowPoint { get; set; }

        public void GetAdjacentPoints(List<List<int>> input)
        {
            AdjacentPoints = new List<Point>();

            //up
            if (Point.Y - 1 >= 0) 
                AdjacentPoints.Add(new Point(Point.X, Point.Y - 1));
            //down
            if (Point.Y + 1 < input.Count)
                AdjacentPoints.Add(new Point(Point.X, Point.Y + 1));
            //left
            if (Point.X - 1 >= 0)
                AdjacentPoints.Add(new Point(Point.X - 1, Point.Y));
            //right
            if (Point.X + 1 < input[Point.Y].Count)
                AdjacentPoints.Add(new Point(Point.X + 1, Point.Y));
        }
    }
}