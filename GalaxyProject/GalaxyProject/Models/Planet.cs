using System.Collections.Generic;
using GalaxyProject.Enums;

namespace GalaxyProject.Models
{
    public class Planet : ABaseSpaceObject
    {
        public Planet(string aName, PlanetType aPlanetType, bool aIsLiveable)
            : base(aName)
        {
            this.PlanetType = aPlanetType;
            this.IsLiveable = aIsLiveable;
            this.Moons = new List<Moon>();
        }
        
        public PlanetType PlanetType { get; set; }  

        public bool IsLiveable { get; set; }   

        public List<Moon> Moons { get; set; }   
    }
}
