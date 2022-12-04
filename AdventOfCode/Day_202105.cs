//using AoCHelper;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text.RegularExpressions;

//namespace AdventOfCode
//{
//    public class Day_202105 : BaseDay
//    {
//        private readonly IEnumerable<LineSegment> _inputOne;
//        private readonly IEnumerable<LineSegment> _inputTwo;

//        public Day_202105()
//        {
//            _inputOne = File.ReadAllLines(InputFilePath).Select(l => l.Split(" -> ").Select(p => p.Split(",")).Select(e => new Point(int.Parse(e[0]), int.Parse(e[1]))).ToList()).Select(i => new LineSegment(i[0], i[1]));
//            _inputTwo = File.ReadAllLines(InputFilePath).Select(l => l.Split(" -> ").Select(p => p.Split(",")).Select(e => new Point(int.Parse(e[0]), int.Parse(e[1]))).ToList()).Select(i => new LineSegment(i[0], i[1], includeDiagonals: true));
//        }

//        public override string Solve_1()
//        {
//            IEnumerable<Point> plots = _inputOne.SelectMany(i => i.Line);
//            var field = new Field(plots);
//            return field.DangerousPlots.Count().ToString();
//        }

//        public override string Solve_2()
//        {
//            IEnumerable<Point> plots = _inputTwo.SelectMany(i => i.Line);
//            var field = new Field(plots);
//            return field.DangerousPlots.Count().ToString();
//        }
//    }

//    public class LineSegment
//    {
//        public LineSegment(Point startingPoint, Point endingPoint, bool includeDiagonals = false)
//        {
//            FirstPoint = startingPoint;
//            SecondPoint = endingPoint;
//            Line = new List<Point>();

//            BuildLine(includeDiagonals);
//        }

//        public Point FirstPoint { get; set; }
//        public Point SecondPoint { get; set; }
//        public List<Point>  Line { get; set; }

//        private void BuildLine(bool includeDiagonals = false)
//        {
//            List<Point> OrderedPoints = new List<Point>() { FirstPoint, SecondPoint }.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

//            //0,9 -> 5,9 - h
//            //9,4 -> 3,4 - h
//            //0,9 -> 2,9 - h
//            //3,4 -> 1,4 - h
//            if (IsHorizontal())
//            {
//                for (int i = OrderedPoints[0].X; i <= OrderedPoints[1].X; i++)
//                {
//                    Line.Add(new Point(i, OrderedPoints[0].Y));
//                }
//            }

//            //2,2 -> 2,1 - v
//            //7,0 -> 7,4 - v
//            else if (IsVertical())
//            {
//                for (int i = OrderedPoints[0].Y; i <= OrderedPoints[1].Y; i++)
//                {
//                    Line.Add(new Point(OrderedPoints[0].X, i));
//                }
//            }
//            else if (includeDiagonals && IsDiagonal())
//            {
//                //8,0 -> 0,8 - d
//                //6,4 -> 2,0 - d
//                //0,0 -> 8,8 - d
//                //5,5 -> 8,2 - d
//                if (OrderedPoints[0].X == OrderedPoints[0].Y && OrderedPoints[1].X == OrderedPoints[1].Y)
//                {
//                    for (int i = OrderedPoints[0].X; i <= OrderedPoints[1].X; i++)
//                    {
//                        Line.Add(new Point(i, i));
//                    }
//                }

//                //8,0 -> 0,8 - d
//                //6,4 -> 2,0 - d
//                //0,0 -> 8,8 - d
//                //5,5 -> 8,2 - d
//                else if (OrderedPoints[0].X + OrderedPoints[0].Y == OrderedPoints[1].X + OrderedPoints[1].Y)
//                {
//                    for (int i = 0; i <= OrderedPoints[1].X - OrderedPoints[0].X; i++)
//                    {
//                        Line.Add(new Point(OrderedPoints[0].X + i, OrderedPoints[0].Y - i));
//                    }
//                }

//                //8,0 -> 0,8 - d
//                //6,4 -> 2,0 - d
//                //0,0 -> 8,8 - d
//                //5,5 -> 8,2 - d
//                else if (OrderedPoints[0].X + OrderedPoints[1].X == OrderedPoints[0].Y + OrderedPoints[1].Y)
//                {
//                    for (int i = OrderedPoints[0].X; i <= OrderedPoints[1].X - OrderedPoints[0].X; i++)
//                    {
//                        Line.Add(new Point(OrderedPoints[0].X + i, OrderedPoints[0].Y - i));
//                    }
//                }

//                //8,0 -> 0,8 - d
//                //6,4 -> 2,0 - d
//                //0,0 -> 8,8 - d
//                //5,5 -> 8,2 - d
//                else if (OrderedPoints[0].X - OrderedPoints[1].X == OrderedPoints[0].Y - OrderedPoints[1].Y)
//                {
//                    for (int i = 0; i <= OrderedPoints[1].X; i++)
//                    {
//                        Line.Add(new Point(OrderedPoints[0].X + i, OrderedPoints[0].Y + i));
//                    }
//                }
//            }
//        }

//        public bool IsHorizontal()
//        {
//            return FirstPoint.Y == SecondPoint.Y;
//        }

//        public bool IsVertical()
//        {
//            return FirstPoint.X == SecondPoint.X;
//        }

//        public bool IsDiagonal()
//        {
//            return (FirstPoint.X + SecondPoint.X == FirstPoint.Y + SecondPoint.Y) || (FirstPoint.X - SecondPoint.X == FirstPoint.Y - SecondPoint.Y);
//        }
//    }

//    public class Field
//    {
//        public Field(IEnumerable<Point> plots)
//        {
//            AffectedPlots = new List<Point>();
//            DangerousPlots = new List<Point>();

//            ProcessPlots(plots);
//        }

//        //0,9 -> 5,9 - h
//        //8,0 -> 0,8 - d
//        //9,4 -> 3,4 - h
//        //2,2 -> 2,1 - v
//        //7,0 -> 7,4 - v
//        //6,4 -> 2,0 - d
//        //0,9 -> 2,9 - h
//        //3,4 -> 1,4 - h
//        //0,0 -> 8,8 - d
//        //5,5 -> 8,2 - d
//        public List<Point> AffectedPlots { get; set; }
//        public List<Point> DangerousPlots { get; set; }

//        private void ProcessPlots(IEnumerable<Point> plots)
//        {
//            foreach (Point plot in plots)
//            {
//                // if not in affected plots, add
//                if (!AffectedPlots.Contains(plot))
//                {
//                    AffectedPlots.Add(plot);
//                }

//                // else if not in dangerous plots, add
//                else if (!DangerousPlots.Contains(plot))
//                {
//                    DangerousPlots.Add(plot);
//                }
//            }

//        }
//    }
//}