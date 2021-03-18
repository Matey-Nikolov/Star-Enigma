namespace Star_Enigma_Regex
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main()
        {
            Dictionary<string, string> allPlanets = new Dictionary<string, string>();
            List<string> attackPlanets = new List<string>();
            List<string> destroyPlanets = new List<string>();

            int planetNumber = int.Parse(Console.ReadLine());

            for (int i = 0; i < planetNumber; i++)          //for each planet Do
            {
                string currentPlanet = Console.ReadLine();

                string pattern = @"[starSTAR]";

                MatchCollection codeMatch = Regex.Matches(currentPlanet, pattern); // Match ALL letters
                string decodedPlanet = "";
                
                foreach (char letter in currentPlanet)
                    decodedPlanet += (char)(letter - codeMatch.Count);  //decode

                string planetPatter = @"@(?<planet>[A-Za-z]+)[\w\d#$%+-]*:[\d]+!(?<action>A|D)![\w\d#$%+-]*->(\d+)";

                Match PlanetMatch = Regex.Match(decodedPlanet, planetPatter);       // Match Planet and Attack/Destroy
                if (!allPlanets.ContainsKey(PlanetMatch.Groups["planet"].Value))
                {
                    allPlanets.Add(PlanetMatch.Groups["planet"].Value, PlanetMatch.Groups["action"].Value);
                }
            }

            foreach (var item in allPlanets.OrderBy(x => x.Key))        //Sort and separate to A and D
            {
                if (item.Value == "A")
                    attackPlanets.Add(item.Key);
                else if (item.Value == "D")
                    destroyPlanets.Add(item.Key);
            }

            Console.WriteLine($"Attacked planets: {attackPlanets.Count}");
            if (attackPlanets.Count > 0)
                foreach (var planet in attackPlanets)
                    Console.WriteLine($"-> {planet}");

            Console.WriteLine($"Destroyed planets: {destroyPlanets.Count}");
            if (destroyPlanets.Count > 0)
                foreach (var planet in destroyPlanets)
                    Console.WriteLine($"-> {planet}");
        }
    }
}
