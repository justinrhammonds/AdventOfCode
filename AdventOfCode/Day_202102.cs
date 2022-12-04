using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202102 : BaseDay
    {
        private readonly IEnumerable<Instructions> _input;

        public Day_202102()
        {
            _input = File.ReadAllLines(InputFilePath).Select(l => l.Split(" ")).Select(s => new Instructions(s[0], s[1]));
        }

        public override string Solve_1()
        {
            var position = new PositionCalculator();
            _input.ToList().ForEach(i => position.Move(i.Direction, i.Distance));

            return (position.X * position.Y).ToString();
        }

        public override string Solve_2()
        {
            var position = new PositionCalculator();
            _input.ToList().ForEach(i => position.Move(i.Direction, i.Distance, isAiming: true));

            return (position.X * position.Y).ToString();
        }
    }

    public class PositionCalculator
    {
        public PositionCalculator()
        {
            X = 0;
            Y = 0;
            Aim = 0;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Aim { get; set; }


        public void Move(string direction, int distance, bool isAiming = false)
        {
            if (isAiming) 
            {
                AdjustAim(direction, distance);
            }
            else
            {
                if (direction.Equals("up"))
                {
                    Y -= distance;
                }

                if (direction.Equals("down"))
                {
                    Y += distance;
                }

                if (direction.Equals("forward"))
                {
                    X += distance;
                }
            }
        }

        public void AdjustAim(string direction, int distance)
        {
            if (direction.Equals("up"))
            {
                Aim -= distance;
            }

            if (direction.Equals("down"))
            {
                Aim += distance;
            }

            if (direction.Equals("forward"))
            {
                X += distance;
                Y += Aim * distance;
            }
        }
    }

    public class Instructions
    {
        public Instructions(string direction, string distance)
        {
            Direction = direction;
            Distance = int.Parse(distance);
        }

        public string Direction { get; set; }
        public int Distance { get; set; }
    }

}