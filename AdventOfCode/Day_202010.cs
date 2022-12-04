using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202010 : BaseDay
    {
        private readonly IEnumerable<int> _input;

        public Day_202010()
        {
            _input = File.ReadAllLines(InputFilePath).Select(l => int.Parse(l)).OrderBy(x => x);
        }

        public override string Solve_1()
        {
            return new AdapterConfigurator(_input.ToList()).CalculateAdapterDifferentials().ToString();
        }

        public override string Solve_2()
        {
            return new AdapterConfigurator(_input.ToList()).CalculateTotalDistinctConfigurations().ToString();
        }

        public class AdapterConfigurator
        {
            public AdapterConfigurator(List<int> input)
            {
                Adapters = input;
            }

            public List<int> Adapters { get; set; }

            public int CalculateAdapterDifferentials()
            {
                
                for (int i = 0; i < Adapters.Count; i++)
                {
                    if (i == 0)
                    {
                        Adapters.Insert(0, Adapters[i]); // need to add in the adapter to source differential
                    }
                    else if (i + 1 == Adapters.Count)
                    {
                        Adapters[i] = 3;
                    }
                    else
                    {
                        Adapters[i] = Adapters[i + 1] - Adapters[i];
                    }
                };

                return Adapters.Where(j => j == 1).Count() * Adapters.Where(j => j == 3).Count();
            }

            public long CalculateTotalDistinctConfigurations()
            {
                return 0;
            }
        }
    }
}