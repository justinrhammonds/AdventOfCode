using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202002 : BaseDay
    {
        private readonly List<Password> _input;

        public Day_202002()
        {
            _input = File.ReadAllLines(InputFilePath).Select(str => new Password(str)).ToList();
        }

        public override string Solve_1()
        {
            return _input.Where(i => i.IsValid).Count().ToString();
        }

        public override string Solve_2()
        {
            return _input.Where(i => i.IsNewPolicyValid).Count().ToString();
        }
    }

    public class Password
    {
        public Password(string _value, char _character, int _one, int _two)
        {
            Value = _value;
            Policy = new Policy(_character, _one, _two);
            NewPolicy = new NewPolicy(_character, _one, _two);

            var matches = Value.Where(c => c == Policy.Character).Count();
            IsValid = matches <= Policy.MaxOccurrences && matches >= Policy.MinOccurrences;
            var one = Value[NewPolicy.CriteriaOne - 1];
            var two = Value[NewPolicy.CriteriaTwo - 1];
            IsNewPolicyValid = (Value[NewPolicy.CriteriaOne - 1] == Policy.Character || Value[NewPolicy.CriteriaTwo - 1] == Policy.Character) && !(Value[NewPolicy.CriteriaOne - 1] == Policy.Character && Value[NewPolicy.CriteriaTwo - 1] == Policy.Character);
        }

        public Password(string _input)
        {
            var policy = _input.Split(" ");
            var occurrences = policy[0].Split("-").Select(int.Parse).ToList();
            var character = policy[1][0];

            var result = new Password(policy[2], character, occurrences[0], occurrences[1]);

            Value = result.Value;
            Policy = result.Policy;
            IsValid = result.IsValid;
            NewPolicy = result.NewPolicy;
            IsNewPolicyValid = result.IsNewPolicyValid;
        }

        public string Value { get; set; }
        public Policy Policy { get; set; }
        public NewPolicy NewPolicy { get; set; }
        public bool IsValid { get; set; }
        public bool IsNewPolicyValid { get; set; }
    }

    public class Policy
    {
        public Policy(char _character, int _min, int _max)
        {
            Character = _character;
            MinOccurrences = _min;
            MaxOccurrences = _max;
        }

        public char Character { get; set; }
        public int MinOccurrences { get; set; }
        public int MaxOccurrences { get; set; }
    }

    public class NewPolicy
    {
        public NewPolicy(char _character, int _one, int _two)
        {
            Character = _character;
            CriteriaOne = _one;
            CriteriaTwo = _two;
        }

        public char Character { get; set; }
        public int CriteriaOne { get; set; }
        public int CriteriaTwo { get; set; }
    }
}