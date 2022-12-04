using AoCHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_202004 : BaseDay
    {
        private readonly IEnumerable<Passport> _input;

        public Day_202004()
        {
            _input = File.ReadAllText(InputFilePath).Split("\r\n\r\n").Select(item => item.Replace("\r\n", " "))
                            .Select(unit =>
                            {
                                var passport = new Passport();
                                var credentials = unit.Split(" ").Select(c => c.Split(":")).ToArray();

                                for (int i = 0; i < credentials.Length; i++)
                                {
                                    passport.Credentials.Add(credentials[i][0], credentials[i][1]);
                                }

                                return passport;
                            });
        }

        public override string Solve_1()
        {
            return _input.Where(i => i.HasAllCredentials()).Count().ToString();
        }

        public override string Solve_2()
        {
            return _input.Where(i => i.HasAllValidCredentials()).Count().ToString();
        }

        public class Passport
        {
            public Passport()
            {
                Credentials = new Dictionary<string, string>();
            }

            public Dictionary<string, string> Credentials { get; set; }

            public bool HasAllCredentials()
            {
                return  Credentials.ContainsKey(Credential.byr.ToString()) &&
                        Credentials.ContainsKey(Credential.iyr.ToString()) &&
                        Credentials.ContainsKey(Credential.eyr.ToString()) &&
                        Credentials.ContainsKey(Credential.hgt.ToString()) &&
                        Credentials.ContainsKey(Credential.hcl.ToString()) &&
                        Credentials.ContainsKey(Credential.ecl.ToString()) &&
                        Credentials.ContainsKey(Credential.pid.ToString());
            }

            public bool HasAllValidCredentials()
            {
                return  HasAllCredentials() &&
                        HasValidBirthYear(Credentials[Credential.byr.ToString()]) &&
                        HasValidIssueYear(Credentials[Credential.iyr.ToString()]) &&
                        HasValidExpirationYear(Credentials[Credential.eyr.ToString()]) &&
                        HasValidHeight(Credentials[Credential.hgt.ToString()]) &&
                        HasValidHairColor(Credentials[Credential.hcl.ToString()]) &&
                        HasValidEyeColor(Credentials[Credential.ecl.ToString()]) &&
                        HasValidPassportIdLength(Credentials[Credential.pid.ToString()]);
            }

            public bool HasValidBirthYear(string birthYear)
            {
                int year = int.Parse(birthYear);
                return birthYear.Length == 4 && (year >= 1920) && (year <= 2002);
            }

            public bool HasValidIssueYear(string issueYear)
            {
                int year = int.Parse(issueYear);
                return issueYear.Length == 4 && (year >= 2010) && (year <= 2020);
            }

            public bool HasValidExpirationYear(string expirationYear)
            {
                int year = int.Parse(expirationYear);
                return expirationYear.Length == 4 && (year >= 2020) && (year <= 2030);
            }

            public bool HasValidHeight(string height)
            {
                if (height.EndsWith("cm"))
                {
                    int heightValue = 0;
                    int.TryParse(Regex.Replace(height, @"\D", ""), out heightValue);

                    return heightValue >= 150 && heightValue <= 193;

                }

                if (height.EndsWith("in"))
                {
                    int heightValue = 0;
                    int.TryParse(Regex.Replace(height, @"\D", ""), out heightValue);

                    return heightValue >= 59 && heightValue <= 76;
                }

                return false;
            }

            public bool HasValidHairColor(string hairColor)
            {
                return hairColor.StartsWith("#") && hairColor.Substring(1).Length == 6 && Regex.IsMatch(hairColor.Substring(1), @"[0-9,a-f]{6}");
            }

            public bool HasValidEyeColor(string eyeColor)
            {
                return eyeColor.Equals(EyeColor.amb.ToString()) ||
                        eyeColor.Equals(EyeColor.blu.ToString()) ||
                        eyeColor.Equals(EyeColor.brn.ToString()) ||
                        eyeColor.Equals(EyeColor.gry.ToString()) ||
                        eyeColor.Equals(EyeColor.grn.ToString()) ||
                        eyeColor.Equals(EyeColor.hzl.ToString()) ||
                        eyeColor.Equals(EyeColor.oth.ToString());

            }

            public bool HasValidPassportIdLength(string ppId)
            {
                return ppId.Length == 9;
            }
        }

        public enum Credential
        {
            byr, iyr, eyr, hgt, hcl, ecl, pid, cid
        }

        public enum EyeColor
        {
            amb, blu, brn, gry, grn, hzl, oth
        }
    }
}