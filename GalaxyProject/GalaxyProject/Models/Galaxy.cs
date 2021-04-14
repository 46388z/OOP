using System.Collections.Generic;
using GalaxyProject.Enums;

namespace GalaxyProject.Models
{
    public class Galaxy : ABaseSpaceObject
    {
        public Galaxy(string aName, decimal aAge, AgeType aAgeType, GalaxyType aGalaxyType)
            : base(aName)
        {
            this.Age = aAge;
            this.AgeType = aAgeType;
            this.GalaxyType = aGalaxyType;
            this.Stars = new List<Star>();
        }

        public decimal Age { get; set; }

        public AgeType AgeType { get; set; }

        public GalaxyType GalaxyType { get; set; }  

        public List<Star> Stars { get; set; }   
    }
}
