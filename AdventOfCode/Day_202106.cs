using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day_202106 : BaseDay
    {
        private readonly List<int> _input;

        public Day_202106()
        {
            _input = File.ReadAllText(InputFilePath).Split(",").Select(f => int.Parse(f)).ToList();
        }

        public override string Solve_1()
        {
            //return LifecycleSchoolOfFishForDays(80, _input).ToString();
            return MoreEfficientlyLifecycleFish(80, _input).ToString();
        }

        public override string Solve_2()
        {
            return "blah";
        }

        private int LifecycleSchoolOfFishForDays(int days, List<int> input)
        {
            List<Fish> schoolOfFish = input.Select(f => new Fish(0, days, f)).ToList();
            for (int day = 1; day <= days; day++)
            {
                int countSpawnerFish = schoolOfFish.FindAll(f => f.SpawningDays.Any(d => d.Equals(day))).Count;

                for (int i = 0; i < countSpawnerFish; i++)
                {
                    schoolOfFish.Add(new Fish(day, days, 8));
                }

                //List<Fish> newFish = new List<Fish>();
                //fish = fish.Select(f =>
                //{
                //    f -= 1;
                //    if (f < 0)
                //    {
                //        f = 6;
                //        newFish.Add(8);
                //    }

                //    return f;
                //}).AsParallel().ToList();

                //fish.AddRange(newFish);                
            }

            return schoolOfFish.Count;
        }

        private int MoreEfficientlyLifecycleFish(int days, List<int> input)
        {
            List<SchoolOfFish> schools = CreateDaySchools(9);
            for (int i = 0; i < input.Count; i++)
            {
                schools.Find(s => s.Day.Equals(input[i])).AddNewFish(1);
            }

            for (int i = 1; i <= days; i++)
            {
                int newFish = schools.Where(s => s.Day.Equals(0)).Count();
                schools.ForEach(school =>
                {
                    if (school.Day.Equals(0))
                        school.UpdateDay(6);
                    else
                        school.UpdateDay(school.Day - 1);
                });

                var newSchool = new SchoolOfFish(8);
                for (int j = 0; j < newFish; j++)
                {
                    newSchool.School.Add(new LanternFish());
                }
                schools.Add(newSchool);
            }

            return schools.SelectMany(f => f.School).Count();
        }

        private List<SchoolOfFish> CreateDaySchools(int days)
        {
            List<SchoolOfFish> DaySchools = new List<SchoolOfFish>();
            for (int i = 0; i < days; i++)
            {
                DaySchools.Add(new SchoolOfFish(i));
            }

            return DaySchools;
        }
    }

    public class Fish
    {
        public Fish(int birthday, int lifespan, int age = 8)
        {
            Birthday = birthday + 1;
            Age = age + Birthday;
            Lifespan = lifespan;
            SpawningDays = DetermineSpawningDays();
        }

        public int Birthday { get; set; }
        public int Age { get; set; }
        public int Lifespan { get; set; }
        public IEnumerable<int> SpawningDays { get; set; }

        private IEnumerable<int> DetermineSpawningDays()
        {
            List<int> days = new List<int>();
            for (int i = Age; i <= Lifespan; i += 7) days.Add(i);

            return days;
        }
    }

    public class SchoolOfFish
    {
        public SchoolOfFish(int day)
        {
            Day = day;
            School = new List<LanternFish>();
        }

        public int Day { get; set; }
        public List<LanternFish> School { get; set; }

        public void AddNewFish(int fishCount)
        {
            for (int i = 0; i < fishCount; i++)
            {
                School.Add(new LanternFish());
            }
        }

        public void UpdateDay(int day)
        {
            Day = day;
        }
    }

    public class LanternFish
    {
        public LanternFish()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}