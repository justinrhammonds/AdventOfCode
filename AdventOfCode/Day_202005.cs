using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202005 : BaseDay
    {
        private readonly IEnumerable<Seat> _input;

        public Day_202005()
        {
            _input = File.ReadAllLines(InputFilePath).Select(str => new Seat(str));
        }

        public override string Solve_1()
        {
            return _input.OrderByDescending(i => i.Id).First().Id.ToString();
        }

        public override string Solve_2()
        {
            // TODO: This one doesn't work completely, though I could assume the answer upon inspecting the collection of remaining seats. Should refactor at some point...
            var remainingSeats = Plane.SeatMap.SelectMany(s => s.Select(seat => new Seat(seat.Y, seat.X)).ToList()).ToList();
            remainingSeats = remainingSeats.Where(s => s.Id != 1 && s.Id != -1).ToList();
            
            foreach (var i in _input)
            {
                var seat = remainingSeats.Find(s => s.Id == i.Id);
                remainingSeats.Remove(seat);
            }

            // remove missing seats
            if (remainingSeats.Count > 1) throw new Exception("Too many remaining seats");

            return remainingSeats.First().Id.ToString();
        }

        public static class Plane
        {
            public const int Rows = 128;
            public const int Columns = 8;
            public static IEnumerable<IEnumerable<Point>> SeatMap = BuildSeatMap();

            private static IEnumerable<IEnumerable<Point>> BuildSeatMap()
            {
                var map = new List<List<Point>>();
                for (int i = 0; i < Rows; i++)
                {
                    var row = new List<Point>();
                    for (int j = 0; j < Columns; j++)
                    {
                        var seat = new Point(j, i);
                        row.Add(seat);
                    }
                    map.Add(row);
                }
                return map;
            }
        }

        public class Seat
        {
            public Seat(int rowId, int columnId)
            {
                RowId = rowId;
                ColumnId = columnId;
                Id = CalculateSeatId(rowId, columnId);
            }

            public Seat(string binary)
            {
                Binary = binary;
                RowId = CalculateRowId();
                ColumnId = CalculateColumnId(RowId);
                Id = CalculateSeatId(RowId, ColumnId);
            }

            public int Id { get; set; }
            public string Binary { get; set; }
            public int RowId { get; set; }
            public int ColumnId { get; set; }

            private int CalculateSeatId(int rowId, int colId)
            {
                return (rowId * 8) + colId;
            }

            private int CalculateRowId()
            {
                var f = Convert.ToChar("f");
                var b = Convert.ToChar("b");
                var rows = new Span<IEnumerable<Point>>(Plane.SeatMap.ToArray(), 0, Plane.Rows);
                foreach (char c in Binary.Substring(0,7).ToLower())
                {
                    if (c.Equals(f))
                    {
                        rows = rows.Slice(0, rows.Length / 2);
                    }
                    else if (c.Equals(b))
                    {
                        rows = rows.Slice(rows.Length / 2);
                    }
                    else
                    {
                        throw new Exception("Bad binary code: row");
                    }
                }

                if (rows.Length > 1) throw new Exception("More than one row remain");

                return rows[0].Take(1).First().Y; // get "Y" for row value;
            }

            private int CalculateColumnId(int rowId)
            {
                var l = Convert.ToChar("l");
                var r = Convert.ToChar("r");
                var row = Plane.SeatMap.First(r => r.First().Y == rowId);
                var cols = new Span<Point>(row.ToArray(), 0, Plane.Columns);
                foreach (char c in Binary.Substring(7).ToLower())
                {
                    if (c.Equals(l))
                    {
                        cols = cols.Slice(0, cols.Length / 2);
                    }
                    else if (c.Equals(r))
                    {
                        cols = cols.Slice(cols.Length / 2);
                    }
                    else
                    {
                        throw new Exception("Bad binary code: col");
                    }
                }

                if (cols.Length > 1) throw new Exception("More than one cols remain");

                return cols[0].X; // get "X" for row value;
            }
        }
    }
}