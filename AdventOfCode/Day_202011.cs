using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202011 : BaseDay
    {
        private readonly List<Seat> _input;

        public Day_202011()
        {
            var input = File.ReadAllLines(InputFilePath).Select(l => l.Split("")).ToArray();
            _input = ConvertToSeatingChart(input);
        }

        public override string Solve_1()
        {
            List<bool> results = new List<bool>();
            do
            {
                results = UpdateSeatingChart();
            } while (!results.Any(r => r.Equals(true)));

            return _input.Where(s => s.Status.Equals("#")).ToString();
        }

        public override string Solve_2()
        {
            return "not implemented";
        }

        private List<Seat> ConvertToSeatingChart(string[][] input)
        {
            var seatingChart = new List<Seat>();
            for (int y = 0; y < input.Length; y++)
            {
                var row = input[y];
                for (int x = 0; x < input[y].Count(); x++)
                {
                    seatingChart.Add(new Seat(new Point(x, y), input[y][x]));
                }
            }

            seatingChart = seatingChart.Where(s => s.Status.Equals(".")).ToList();
            seatingChart.ForEach(s => s.AdjacentSeats = s.FindAdjacentSeats(seatingChart, s.Location));

            return seatingChart;
        }

        private List<bool> UpdateSeatingChart()
        {
            List<bool> results = new List<bool>();
            for (int i = 0; i < _input.Count; i++)
            {
                results.Add(_input[i].UpdateStatus());
            }

            return results;
        }
    }

    public class Seat
    {
        public Seat(Point location, string status)
        {
            Id = (Location.Y * 10) + Location.X;
            Location = location;
            Status = status;
        }

        public int Id { get; set; }
        public Point Location { get; set; }
        public string Status { get; set; }
        public IEnumerable<Seat> AdjacentSeats { get; set; }

        public Seat GetSeat(IEnumerable<Seat> seatingChart, int id)
        {
            return seatingChart.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Seat> FindAdjacentSeats(IEnumerable<Seat> seatingChart, Point seat)
        {
            return seatingChart
                        .Where(s => !s.Status.Equals("."))
                        .Where(s => s.Location.Y == seat.Y - 1 || s.Location.Y == seat.Y || s.Location.Y == seat.Y + 1)
                        .Where(s => s.Location.X == seat.X - 1 || s.Location.X == seat.X || s.Location.X == seat.X + 1);
        }

        public bool UpdateStatus()
        {
            if(Status.Equals("L") && !AdjacentSeats.Any(s => s.Status.Equals("#")))
            {
                Status = "#";
                return true;
            }
            if(Status.Equals("#") && AdjacentSeats.Where(s => s.Status.Equals("#")).Count().Equals(4)) 
            {
                Status = "L";
                return true;
            }

            return false;

        }
    }

}