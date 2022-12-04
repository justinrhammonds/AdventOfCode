using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202009 : BaseDay
    {
        private readonly IEnumerable<long> _input;

        public Day_202009()
        {
            _input = File.ReadAllLines(InputFilePath).Select(l => long.Parse(l));
        }

        public override string Solve_1()
        {
            return new Decipher(25, _input).Run().ToString();
        }

        public override string Solve_2()
        {
            var decipher = new Decipher(25, _input);
            var invalid = decipher.Run();
            var set = _input.ToList();


            return decipher.CalculateWeakness(invalid, set).ToString();
        }

        public class Decipher
        {
            public Decipher(int preamble, IEnumerable<long> input)
            {
                Preamble = preamble;
                Input = input;
            }

            public int Preamble { get; set; }
            public IEnumerable<long> Input { get; set; }

            public long Run()
            {
                for (int i = 0; (i + Preamble) < Input.Count(); i++)
                {
                    var num = Input.ToList()[i + Preamble];
                    var set = Input.ToList().GetRange(i, Preamble).Distinct();

                    if (!IsValid(num, set)) return num;
                }

                return -1;
            }

            public long CalculateWeakness(long invalid, List<long> set)
            {
                for (int i = 0; i < set.Count; i++)
                {
                    var total = set[i];
                    for (int j = 1; (i + j) < set.Count; j++)
                    {
                        // iteratively add the next items to total until total >= invalid
                        total += set[i + j];

                        // if total == invalid, then return range of indeces 'i - (i + j)'
                        if (total == invalid)
                        {
                            var workingSet = set.GetRange(i, j);
                            return workingSet.Min() + workingSet.Max();
                        }

                        // if total > invalid
                        if (total > invalid)
                        {
                            continue;
                        }
                    }
                }

                return -1;
            }

            private bool IsValid(long value, IEnumerable<long> set)
            {
                for (int i = 0; i < set.Count(); i++)
                {
                    var workingSet = set.ToList();
                    var workingNum = workingSet[i];
                    workingSet.Remove(workingNum);

                    var workingSumSet = workingSet.Select(ws => ws + workingNum);

                    if (workingSumSet.Any(ws => ws == value)) return true;
                }

                return false;
            }
        }
    }
}