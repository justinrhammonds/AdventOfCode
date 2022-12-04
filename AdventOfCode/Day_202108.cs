using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202108 : BaseDay
    {
        private readonly List<EncodedDisplay> _input;

        public Day_202108()
        {
            _input = File.ReadAllLines(InputFilePath).Select(l => l.Split(" | ").Select(li => li.Split(" ")).ToList()).Select(item => new EncodedDisplay(item[0],item[1])).ToList();
        }

        public override string Solve_1()
        {
            return CountTotalUniqueDigits(_input).ToString();
        }

        public override string Solve_2()
        {
            var results = _input.Select(i => int.Parse(new DecodedDisplay(i).DisplayCode));
            return _input.Select(i => int.Parse(new DecodedDisplay(i).DisplayCode)).Sum().ToString();
        }

        private int CountTotalUniqueDigits(List<EncodedDisplay> input)
        {
            return input.SelectMany(i => i.EncodedValue).Where(e => e.Length.Equals(2) || e.Length.Equals(3) || e.Length.Equals(4) || e.Length.Equals(7)).Count();
        }
    }

    public class EncodedDisplay
    {
        public EncodedDisplay(IEnumerable<string> patterns, IEnumerable<string> encoding)
        {
            EncodedPatterns = patterns;
            EncodedValue = encoding;
        }

        public IEnumerable<string> EncodedPatterns { get; set; }

        public IEnumerable<string> EncodedValue { get; set; }
    }

    public class DecodedDisplay
    {
        public DecodedDisplay(EncodedDisplay display)
        {
            SetupDigitTemplate();
            DecodeDigitPatterns(display.EncodedPatterns);

            DisplayCode = ComputeFourDigitCode(display.EncodedValue.ToList());
        }

        public List<Digit> Patterns { get; set; }
        public string DisplayCode { get; set; }

        private void SetupDigitTemplate()
        {
            Patterns = new List<Digit>();
            for (int i = 0; i < 10; i++)
            {
                Patterns.Add(new Digit(i));
            }
        }

        /// RULES -ish
        /// 0: 6 & not 6 or 9
        /// 1: 2 segments
        /// 2: 5 & missing a segment from 1
        /// 3: 5 & each found in 9
        /// 4: 4 segments
        /// 5: 5 & each found in 6
        /// 6: 6 && missing a segment from 1
        /// 7: 3 segments
        /// 8: 7 segments
        /// 9: 6 & excluding those found in 5, has a segment from 1
        private void DecodeDigitPatterns(IEnumerable<string> patterns)
        {
            Patterns[1].Pattern = patterns.FirstOrDefault(p => p.Length.Equals(2));
            Patterns[7].Pattern = patterns.FirstOrDefault(p => p.Length.Equals(3));
            Patterns[4].Pattern = patterns.FirstOrDefault(p => p.Length.Equals(4));
            Patterns[8].Pattern = patterns.FirstOrDefault(p => p.Length.Equals(7));
            Patterns[3].Pattern = patterns.FirstOrDefault(p => p.Length.Equals(5) && p.Except(Patterns[1].Pattern).Count().Equals(3));
            Patterns[6].Pattern = patterns.FirstOrDefault(p => p.Length.Equals(6) && p.Except(Patterns[7].Pattern).Count().Equals(4));
            Patterns[0].Pattern = patterns.FirstOrDefault(p => p.Length.Equals(6) && p.Except(Patterns[7].Pattern).Count().Equals(3) && p.Except(Patterns[3].Pattern).Count().Equals(2));
            Patterns[9].Pattern = patterns.FirstOrDefault(p => p.Length.Equals(6) && p.Except(Patterns[4].Pattern).Count().Equals(2) && p.Except(Patterns[3].Pattern).Count().Equals(1));
            Patterns[5].Pattern = patterns.FirstOrDefault(p => p.Length.Equals(5) && p.Except(Patterns[1].Pattern).Count().Equals(4) && p.Except(Patterns[9].Pattern).Count().Equals(0));
            Patterns[2].Pattern = patterns.FirstOrDefault(p => p.Length.Equals(5) && p.Except(Patterns[1].Pattern).Count().Equals(4) && p.Except(Patterns[9].Pattern).Count().Equals(1));

            foreach (var p in Patterns)
            {
                p.Pattern = SortString(p.Pattern);
            }
        }

        private string ComputeFourDigitCode(List<string> input)
        {
            string result = string.Empty;
            foreach (var i in input)
            {
                var sortedInput = SortString(i);
                result += Patterns.FirstOrDefault(p => p.Pattern.SequenceEqual(sortedInput)).Value.ToString();
            }
            return result;
        }

        private string SortString(string input)
        {
            var sorted = string.Empty;
            var arr = input.ToCharArray();
            Array.Sort(arr);
            foreach (var c in arr)
            {
                sorted += c;
            }

            return sorted;
        }
    }

    public class Digit
    {
        public Digit(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
        public string Pattern { get; set; }
    }
}