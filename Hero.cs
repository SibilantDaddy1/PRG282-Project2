using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperheroesApp
{
    public class Hero
    {
        public string HeroID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Superpower { get; set; }
        public int ExamScore { get; set; }
        public string Rank { get; set; }
        public string ThreatLevel { get; set; }

        public Hero(string id, string name, int age, string superpower, int examScore)
        {
            HeroID = id;
            Name = name;
            Age = age;
            Superpower = superpower;
            ExamScore = examScore;
            Rank = CalculateRank(examScore);
            ThreatLevel = CalculateThreat(Rank);
        }

        private string CalculateRank(int score)
        {
            if (score >= 81) return "S-Rank";
            if (score >= 61) return "A-Rank";
            if (score >= 41) return "B-Rank";
            return "C-Rank";
        }

        private string CalculateThreat(string rank)
        {
            switch (rank)
            {
                case "S-Rank": return "Finals Week";
                case "A-Rank": return "Midterm Madness";
                case "B-Rank": return "Group Project Gone Wrong";
                default: return "Pop Quiz";
            }
        }

        public override string ToString()
        {
            return $"{HeroID},{Name},{Age},{Superpower},{ExamScore},{Rank},{ThreatLevel}";
        }
    }
}
