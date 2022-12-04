using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202006 : BaseDay
    {
        private readonly string[] _input;

        public Day_202006()
        {
            _input = File.ReadAllText(InputFilePath).Split("\r\n\r\n");
        }

        public override string Solve_1()
        {
            return _input.Select(s => s.Replace("\r\n", "")).Select(i => i.Distinct().Count()).Sum().ToString();
        }

        public override string Solve_2()
        {
            var ins = _input.Select(group => group.Split("\r\n"))
                            .Select(group => new { 
                                                    Members = group.Length, 
                                                    Answers = CountCommonAnswers(group.SelectMany(g => g), group.Length) 
                                                 })
                                                .Select(g => g.Answers)
                                                .Sum()
                                                .ToString();
            return ins;
        }

        private int CountCommonAnswers(IEnumerable<char> answers, int groupSize)
        {
            return answers.GroupBy(a => a).Where(a => a.Count() == groupSize).Count();
        }
    }
}