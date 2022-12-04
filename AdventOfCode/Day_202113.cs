//using AoCHelper;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text.RegularExpressions;

//namespace AdventOfCode
//{
//    public class Day_202113 : BaseDay
//    {
//        private readonly string[] _input;
//        private readonly IEnumerable<Point> _dots;
//        private readonly IEnumerable<Directive> _instructions;

//        public Day_202113()
//        {
//            _input = File.ReadAllText(InputFilePath).Split("\r\n\r\n");
//            _dots = _input[0].Split("\r\n").Select(i => i.Split(",")).Select(s => new Point(int.Parse(s[0].ToString()), int.Parse(s[1].ToString())));
//            _instructions = _input[1].Split("\r\n").Select(i => i.Substring(11).Split("=").ToList()).Select(x => new Fold(x[0], x[1]));
//        }

//        public override string Solve_1()
//        {
//            var instruction = _instructions.First();
//            var maxX = _dots.Select(d => d.X).Max();
//            var maxY = _dots.Select(d => d.Y).Max();

//            if (instruction.Axis.Equals("y"))
//            {
//                for (int y = 0; instruction.Value + y < maxY; y++)
//                {

//                }
//            } else
//            {
//                for (int i = 0; i < length; i++)
//                {

//                }
//            }
//            // for each fold
//            // if fold is horizontal (y)
//            // then fold up -- for y = fold + 1; < y.length
//            //      move to fold - y if no dot
//            //      else drop
//            // then remove fold line

//            // if fold is vertical (x) 
//            // then fold left -- for x = fold + 1; < x.length 
//            //      move to fold - x if no dot
//            //      else drop
//            // then remove fold line
//            return "not implemented";
//        }

//        public override string Solve_2()
//        {
//            return "not implemented";
//        }
//    }

//    public class Directive
//    {
//        public Directive(string axis, string value)
//        {
//            Axis = axis;
//            Value = int.Parse(value);
//        }

//        public string Axis { get; set; }
//        public int Value { get; set; }
//    }
//}