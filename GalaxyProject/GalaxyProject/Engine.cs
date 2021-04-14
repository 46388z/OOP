using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GalaxyProject.Enums;
using GalaxyProject.Models;
using GalaxyProject.Utils;

namespace GalaxyProject
{
    public class Engine
    {
        private List<Galaxy> galaxies;

        public Engine()
        {
            this.galaxies = new List<Galaxy>();
        }
        
        public void Run()
        {
            while (true)
            {
                string command = Console.ReadLine();
                if (command == Constants.TerminateCommand)
                {
                    break;
                }

                this.InterpretCommand(command);
            }
        }

        private void InterpretCommand(string aCommand)
        {
            string[] tokens = Regex.Split(aCommand, "\\s+(?![^\\[]*\\])")
                .Select(x => x.Replace("[", ""))
                .Select(x => x.Replace("]", ""))
                .ToArray();
            string action = tokens[0];
            if(tokens.Length > 1)
            {
                string arg1 = tokens[1];
                if(action == "add")
                {
                    this.AddSpaceObjects(arg1, tokens);
                } else if (action == "list")
                {
                    this.ListSpaceObjects(arg1);
                }else if (action == "print")
                {
                    this.PrintGalaxyInfo(tokens[1]);
                }
            } else if (action == "stats")
            {
                this.GenerateStats();
            }
        }

        private void AddSpaceObjects(string aArg1, string[] aTokens)
        {
            switch (aArg1)
            {
                case "galaxy":
                    this.AddGalaxy(aTokens[2], aTokens[3], aTokens[4]);
                    break;
                case "star":
                    this.AddStar(aTokens[2], aTokens[3], aTokens[4],
                        aTokens[5], aTokens[6], aTokens[7]);
                    break;
                case "planet":
                    this.AddPlanet(aTokens[2], aTokens[3], aTokens[4], aTokens[5]);
                    break;
                case "moon":
                    this.AddMoon(aTokens[2], aTokens[3]);
                    break;
            }
        }

        private void ListSpaceObjects(string aArg1)
        {
            switch (aArg1)
            {
                case "galaxies":
                    this.ListGalaxies();
                    break;
                case "stars":
                    this.ListStars();
                    break;
                case "planets":
                    this.ListPlanets();
                    break;
                case "moons":
                    this.ListMoons();   
                    break;
            }
        }

        private void AddGalaxy(string aName, string aGalaxyType, string aAgeStr) 
        {
            decimal age = decimal.Parse(aAgeStr.Substring(0, aAgeStr.Length - 1));
            AgeType ageType = (AgeType) Enum.Parse(typeof(AgeType), "" + aAgeStr[aAgeStr.Length - 1]);
            string galaxyTypeEnumName = char.ToUpper(aGalaxyType[0]) + aGalaxyType.Substring(1);
            GalaxyType galaxyType = (GalaxyType) Enum.Parse(typeof(GalaxyType), galaxyTypeEnumName);
            Galaxy galaxy = new Galaxy(aName, age, ageType, galaxyType);

            this.galaxies.Add(galaxy);
        }

        private void AddStar(string aGalaxyName, string aStarName, string aMassStr,
            string aSizeStr, string aTempStr, string aLuminosityStr)  
        {
            decimal mass = decimal.Parse(aMassStr);
            decimal size = decimal.Parse(aSizeStr);
            decimal luminosity = decimal.Parse(aLuminosityStr);
            decimal temp = decimal.Parse(aTempStr);
            Star star = new Star(aStarName, mass, size, luminosity, temp);
            Galaxy galaxy = this.galaxies.SingleOrDefault(x => x.Name == aGalaxyName);

            galaxy.Stars.Add(star);
        }

        private void AddPlanet(string aStarName, string aPlanetName, 
                               string aPlanetTypeStr, string aIsSupportLifeStr)
        {
            PlanetType planetType = Enum.GetValues(typeof(PlanetType))
                .Cast<PlanetType>()
                .SingleOrDefault(x => x.GetFriendlyName() == aPlanetTypeStr);
            bool isLiveable = aIsSupportLifeStr == "yes";
            Planet planet = new Planet(aPlanetName, planetType, isLiveable);
            Star star = this.galaxies.Where(x => x.Stars.Any(x => x.Name == aStarName))
                .SelectMany(x => x.Stars)
                .SingleOrDefault(x => x.Name == aStarName);

            star.Planets.Add(planet);
        }

        private void AddMoon(string aPlanetName, string aMoonName)
        {
            Moon moon = new Moon(aMoonName);
            Planet planet = this.galaxies.Where(x => x.Stars.Any(x => x.Planets.Any(x => x.Name == aPlanetName)))
                .SelectMany(x => x.Stars.SelectMany(x => x.Planets))
                .SingleOrDefault(x => x.Name == aPlanetName);

            planet.Moons.Add(moon);
        }

        private void ListGalaxies()
        {
            var galaxyNames = this.galaxies.Select(x => x.Name).ToList();
            
            Console.WriteLine("--- List of all researched galaxies ---");
            if (!galaxyNames.Any())
            {
                Console.WriteLine("none");
            }
            else
            {
                Console.WriteLine(string.Join(Constants.CommaSeparator, galaxyNames));
            }

            Console.WriteLine("--- End of galaxies list ---");
        }

        private void ListStars()
        {
            var starNames = this.galaxies.SelectMany(x => x.Stars)
                .Select(x => x.Name)
                .ToList();

            Console.WriteLine("--- List of all researched stars ---");
            if (!starNames.Any())
            {
                Console.WriteLine("none");
            }
            else
            {
                Console.WriteLine(string.Join(Constants.CommaSeparator, starNames));
            }

            Console.WriteLine("--- End of stars list ---");
        }

        private void ListPlanets()
        {
            var planetNames = this.galaxies.SelectMany(x => x.Stars)
                .SelectMany(x => x.Planets)
                .Select(x => x.Name)
                .ToList();

            Console.WriteLine("--- List of all researched planets ---");    
            if (!planetNames.Any())
            {
                Console.WriteLine("none");
            }
            else
            {
                Console.WriteLine(string.Join(Constants.CommaSeparator, planetNames));
            }

            Console.WriteLine("--- End of planets list ---");
        }

        private void ListMoons()
        {
            var moonNames = this.galaxies.SelectMany(x => x.Stars)
                .SelectMany(x => x.Planets)
                .SelectMany(x => x.Moons)
                .Select(x => x.Name)
                .ToList();

            Console.WriteLine("--- List of all researched moons ---");
            if (!moonNames.Any())
            {
                Console.WriteLine("none");
            }
            else
            {
                Console.WriteLine(string.Join(Constants.CommaSeparator, moonNames));
            }

            Console.WriteLine("--- End of moons list ---");
        }

        private void GenerateStats()
        {
            int galaxiesCount = this.galaxies.Count();
            int starsCount = this.galaxies.SelectMany(x => x.Stars).Count();
            int planetsCount = this.galaxies
                .SelectMany(x => x.Stars)
                .SelectMany(x => x.Planets)
                .Count();
            int moonsCount = this.galaxies
                .SelectMany(x => x.Stars)
                .SelectMany(x => x.Planets)
                .SelectMany(x => x.Moons)
                .Count();


            Console.WriteLine("--- Stats ---");

            Console.WriteLine($"Galaxies: {galaxiesCount}");
            Console.WriteLine($"Stars: {starsCount}");
            Console.WriteLine($"Planets: {planetsCount}");
            Console.WriteLine($"Moons: {moonsCount}");

            Console.WriteLine("--- End of stats ---");
        }

        private void PrintGalaxyInfo(string aGalaxyName)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var galaxy = this.galaxies.SingleOrDefault(x => x.Name == aGalaxyName);
            var stars = galaxy.Stars.OrderBy(x => x.StarClass);
            string galaxyAge = "" + galaxy.Age + galaxy.AgeType;
            
            Console.WriteLine($"--- Data for {aGalaxyName} galaxy ---");
            Console.WriteLine($"Type: {galaxy.GalaxyType.GetFriendlyName()}");
            Console.WriteLine($"Age: {galaxyAge}");
            if (!stars.Any())
            {
                Console.WriteLine("Stars: none");
            }
            else
            {
                Console.WriteLine("Stars:");
                foreach (var star in stars)
                {
                    var planets = star.Planets;
                    Console.WriteLine($"   - Name: {star.Name}");
                    Console.WriteLine($"     Class: {star.StarClass} ({star.Mass}, {star.Size}, {star.Temperature}, {star.Luminosity})");

                    if (!planets.Any())
                    {
                        Console.WriteLine("     Planets: none");
                    }
                    else
                    {
                        Console.WriteLine("     Planets:");
                        foreach (var planet in planets)
                        {
                            var moons = planet.Moons;
                            string isSupportLive = planet.IsLiveable ? "yes": "no";
                            Console.WriteLine($"        o Name: {planet.Name}");
                            Console.WriteLine($"          Type: {planet.PlanetType.GetFriendlyName()}");
                            Console.WriteLine($"          Support life: {isSupportLive}");

                            if (!moons.Any())
                            {
                                Console.WriteLine("          Moons: none");
                            }
                            else
                            {
                                Console.WriteLine("          Moons:");
                                foreach (var moon in moons)
                                {
                                    char blackSquare = '\u25A0';
                                    Console.WriteLine($"            {blackSquare}  {moon.Name}");
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"--- End of data for {aGalaxyName} galaxy ---");
        }
    }
}
