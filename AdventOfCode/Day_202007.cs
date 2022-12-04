using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202007 : BaseDay
    {
        private readonly List<Bag> _input;

        public Day_202007()
        {
            _input = File.ReadAllText(InputFilePath)
                            .Replace("no other", "0")
                            .Replace(" bags", "")
                            .Replace(" bag", "")
                            .Replace(" contain","")
                            .Replace(".","")
                            .Replace(",","")
                            .Replace(" ","")
                            .Split("\r\n").Select(l => new Bag(l)).ToList();
        }

        public override string Solve_1()
        {
            int total = 0;
            _input.ForEach(bag =>
            {
                if (CanCarryShinyGold(bag.Type))
                {
                    total++;
                }
            });

            return total.ToString();
        }

        public override string Solve_2()
        {
            return CountNestedBagContents("shinygold").ToString();
        }

        public bool CanCarryShinyGold(string bagType)
        {
            if (bagType.Equals("shinygold")) return false; // can't carry itself

            var bag = _input.Find(b => b.Type == bagType);

            if(bag.Contents.Any(b => b.Key.Equals("shinygold"))) 
            {
                return true;
            }

            for (int i = 0; i < bag.Contents.Count; i++)
            {
                var type = bag.Contents.ToList()[i].Key;
                if (CanCarryShinyGold(type))
                {
                    return true;
                }
            }

            return false;
        }

        public int CountNestedBagContents(string bagType)
        {
            var bag = _input.Find(b => b.Type.Equals(bagType));
            var totalNestedBags = bag.Contents.Values.Sum();

            foreach (var item in bag.Contents)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    var nested = CountNestedBagContents(_input.Find(b => b.Type.Equals(item.Key)).Type);
                    totalNestedBags = nested == 0 ? totalNestedBags++ : totalNestedBags + nested;
                }
            }

            return totalNestedBags;
        }
    }

    public class Bag
    {
        public Bag(string raw, bool mock)
        {
            Type = "raw";
            Contents = new Dictionary<string, int>() { };
            RawContents = raw;
        }

        public Bag(string raw)
        {
            var digits = Regex.Matches(raw, @"\d").ToList();
            var bagTypes = Regex.Split(raw, @"\d").ToList();

            Type = bagTypes[0];
            Contents = new Dictionary<string, int>() { };
            RawContents = raw;

            bagTypes.Remove(bagTypes[0]);

            if (int.Parse(digits[0].Value) == 0)
            {
                return;
            }

            for (int i = 0; i < digits.Count; i++)
            {
                Contents.Add(bagTypes[i], int.Parse(digits[i].Value));
            }
        }

        public Bag(string type, string content)
        {
            Type = type;
            Contents = new Dictionary<string, int>() { };
            RawContents = content;
        }

        public Bag(string name, IEnumerable<KeyValuePair<string, int>> contents)
        {
            Type = name;
            Contents = new Dictionary<string, int>();

            foreach (var c in contents)
            {
                Contents.Add(c.Key, c.Value);
            }
        }

        public string Type { get; set; }
        public Dictionary<string, int> Contents { get; set; }
        public string RawContents { get; set; }
    }
}