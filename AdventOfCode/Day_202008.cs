using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202008 : BaseDay
    {
        private readonly IEnumerable<Instruction> _input;

        public Day_202008()
        {
            _input = File.ReadAllLines(InputFilePath).Select(str => str.Split(" ")).Select(line => new Instruction(line[0],line[1]));
        }

        public override string Solve_1()
        {
            var accumulator = new Accumulator(_input);
            accumulator.Execute();

            return accumulator.Total.ToString();
        }

        public override string Solve_2()
        {
            List<Instruction> alterableInput = _input.ToList();
            for (int i = 0; i < _input.Count(); i++)
            {
                if (alterableInput[i].Directive.Equals("acc")) continue;

                if (alterableInput[i].Directive.Equals("jmp"))
                {
                    alterableInput[i].Directive = "nop";
                } 
                else if (alterableInput[i].Directive.Equals("nop"))
                {
                    alterableInput[i].Directive = "jmp";
                }

                var accumulator = new Accumulator(alterableInput, shouldTerminate: true);
                accumulator.Execute();

                if (accumulator.Terminated)
                {
                    return accumulator.Total.ToString();
                }

                alterableInput = _input.ToList(); // reset
            }

            return "did not solve";
        }
    }

    public class Instruction
    {
        public Instruction(string directive, string increment)
        {
            Directive = directive;
            Increment = int.Parse(increment);
        }

        public string Directive { get; set; }
        public int Increment { get; set; }
    }

    public class Accumulator
    {
        public Accumulator(IEnumerable<Instruction> instructions, bool shouldTerminate = false)
        {
            ExecuteHistory = new List<int>();
            Instructions = instructions;
            ShouldTerminate = shouldTerminate;
        }

        public int Total { get; set; }
        public int CurrentIndex { get; set; }
        public List<int> ExecuteHistory { get; set; }
        public IEnumerable<Instruction> Instructions { get; set; }
        public bool ShouldTerminate { get; set; }
        public bool Terminated { get; set; }


        public void Execute()
        {
            if (ShouldTerminate && CurrentIndex == Instructions.Count())
            {
                Terminated = true;
                return;
            }
            if (ExecuteHistory.Any(i => i == CurrentIndex)) return;

            ExecuteHistory.Add(CurrentIndex);
            var currentInstruction = Instructions.ToList()[CurrentIndex];
            switch (currentInstruction.Directive)
            {
                case "acc":
                    Accumulate(currentInstruction.Increment);
                    break;
                case "jmp":
                    UpdateIndex(currentInstruction.Increment);
                    break;
                case "nop":
                    NoOperation();
                    break;
                default:
                    break;
            }

            Execute();
        }

        public void Accumulate(int number)
        {
            Total += number;
            UpdateIndex(1);
        }

        public void NoOperation()
        {
            UpdateIndex(1);
        }

        public void UpdateIndex(int distance)
        {
            var newIndex = CurrentIndex + distance;
            if (ShouldTerminate)
            {
                if (newIndex == Instructions.Count())
                {
                    CurrentIndex = newIndex;
                    return;
                }
            }

            if (newIndex >= Instructions.Count())
            {
                CurrentIndex = newIndex - Instructions.Count();
            }
            else
            {
                CurrentIndex = newIndex;
            }

        }
    }
}