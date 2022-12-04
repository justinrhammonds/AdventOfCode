using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202103 : BaseDay
    {
        private readonly IEnumerable<string> _input;

        public Day_202103()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            var gammaRate = string.Empty;
            var epsilonRate = string.Empty;

            for (int i = 0; i < _input.First().Length; i++)
            {
                var resultSet = _input.Select(input => input[i]);
                var resultCounts = new[] { resultSet.Where(r => r.ToString().Equals("0")).Count(), resultSet.Where(r => r.ToString().Equals("1")).Count() };

                gammaRate += resultCounts[0] > resultCounts[1] ? "0" : "1";
                epsilonRate += resultCounts[0] < resultCounts[1] ? "0" : "1";
            }

            var gammaRateInt = Convert.ToInt32(gammaRate, 2);
            var epsilonRateInt = Convert.ToInt32(epsilonRate, 2);
            return (gammaRateInt * epsilonRateInt).ToString();
        }

        public override string Solve_2()
        {
            List<string> lifeSupportInputs = _input.ToList();
            List<string> oxygenGeneratorInputs = _input.ToList();

            for (int i = 0; i < _input.First().Length; i++)
            {
                var resultSet = lifeSupportInputs.Select(input => input[i]);
                var resultCounts = new[] { resultSet.Where(r => r.ToString().Equals("0")).Count(), resultSet.Where(r => r.ToString().Equals("1")).Count() };

                var lifeSupportFilterCriteria = resultCounts[0] > resultCounts[1] ? "0" : "1";
                lifeSupportInputs = lifeSupportInputs.Where(input => input[i].ToString().Equals(lifeSupportFilterCriteria)).ToList();

                if (lifeSupportInputs.Count == 1) break;
            }

            for (int i = 0; i < _input.First().Length; i++)
            {
                var resultSet = oxygenGeneratorInputs.Select(input => input[i]);
                var resultCounts = new[] { resultSet.Where(r => r.ToString().Equals("0")).Count(), resultSet.Where(r => r.ToString().Equals("1")).Count() };

                var oxygenGeneratorFitlerCriteria = resultCounts[0] <= resultCounts[1] ? "0" : "1";
                oxygenGeneratorInputs = oxygenGeneratorInputs.Where(input => input[i].ToString().Equals(oxygenGeneratorFitlerCriteria)).ToList();

                if (oxygenGeneratorInputs.Count == 1) break;
            }
            var lfRating = Convert.ToInt32(lifeSupportInputs.First(), 2);
            var ogRating = Convert.ToInt32(oxygenGeneratorInputs.First(), 2);

            return (Convert.ToInt32(lifeSupportInputs.First(), 2) * Convert.ToInt32(oxygenGeneratorInputs.First(), 2)).ToString();
        }
    }
}