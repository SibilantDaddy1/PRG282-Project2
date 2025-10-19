using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperheroesApp
{
    public static class FileHandler
    {
        private static string filePath = "superheroes.txt";

        public static void SaveHero(Hero hero)
        {
            File.AppendAllText(filePath, hero.ToString() + Environment.NewLine);
        }

        public static List<Hero> LoadHeroes()
        {
            List<Hero> heroes = new List<Hero>();
            if (!File.Exists(filePath)) return heroes;

            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] data = line.Split(',');
                if (data.Length >= 7)
                {
                    Hero hero = new Hero(
                        data[0],
                        data[1],
                        int.Parse(data[2]),
                        data[3],
                        int.Parse(data[4])
                    );
                    heroes.Add(hero);
                }
            }
            return heroes;
        }

        public static void OverwriteHeroes(List<Hero> heroes)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var hero in heroes)
                {
                    writer.WriteLine(hero.ToString());
                }
            }
        }
    }
}
