using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202111 : BaseDay
    { 
        private readonly List<List<int>> _input;
        private readonly List<Octopus> _octopuses;
        public static int FlashCount = 0;

        public Day_202111()
        {
            _octopuses = new List<Octopus>();
            _input = File.ReadAllLines(InputFilePath).Select(l => l.ToCharArray().Select(i => int.Parse(i.ToString())).ToList()).ToList();

            for (int y = 0; y < _input.Count(); y++)
            {
                for (int x = 0; x < _input[y].Count(); x++)
                {
                    _octopuses.Add(new Octopus(_input[y][x], new Point(x, y)));
                }
            }

        }

        public override string Solve_1()
        {
            for (int i = 0; i < 100; i++)
            {
                DoStep();
            }

            return "not implemented";
        }

        public override string Solve_2()
        {
            return "not implemented";
        }

        private void DoStep()
        {
            _octopuses.ForEach(o => o.IncrementEnergy());
            while (_octopuses.Any(o => o.Energy > 9 && !o.HasFlashed))
            {
                var flashers = _octopuses.Where(o => o.Energy > 9 && !o.HasFlashed).ToList();
                var neighbors = flashers.SelectMany(f => f.AdjacentLocations).ToList();

                flashers.ForEach(f => Flash(f));
                neighbors.Select(n => _octopuses.Find(octopus => octopus.Location == n)).ToList().ForEach(n => n.IncrementEnergy());
            }

            _octopuses.ForEach(o => o.ResetEnergy());
        }

        private void Flash(Octopus octopus)
        {
            FlashCount += 1;
            octopus.HasFlashed = true;
        }
    }

    public class Octopus
    {
        public Octopus(int energy, Point location)
        {
            Energy = energy;
            Location = location;
            HasFlashed = false;
            AdjacentLocations = new List<Point>();

            DetermineAdjacentLocations();
        }

        public Point Location { get; set; }
        public int Energy { get; set; }
        public bool HasFlashed { get; set; }
        public List<Point> AdjacentLocations { get; set; }

        private void DetermineAdjacentLocations()
        {
            for (int y = Location.Y - 1; y <= Location.Y + 1; y++)
            {
                for (int x = Location.X - 1; x <= Location.X + 1; x++)
                {
                    if (y >= 0 && y < 10 && x >= 0 && x < 10)
                    {
                        AdjacentLocations.Add(new Point(x, y));
                    }

                }
            }
            AdjacentLocations.Remove(AdjacentLocations.Find(l => l.Y == Location.Y && l.X == Location.X));
        }

        public void IncrementEnergy()
        {
            Energy += 1;
        }

        public void ResetEnergy()
        {
            HasFlashed = false;
            if (Energy > 9) Energy = 0;
        }
    }   
}