using System.Collections.Generic;
using GalaxyProject.Enums;

namespace GalaxyProject.Models
{
    public class Star : ABaseSpaceObject
    {
        public Star(string aName, decimal aMass, decimal aSize,
                    decimal aLuminosity, int aTemperature)
            : base(aName)
        {
            this.Mass = aMass;
            this.Size = aSize;
            this.Luminosity = aLuminosity;
            this.Temperature = aTemperature;
            this.SetStarClass();
            this.Planets = new List<Planet>();
        }

        public decimal Mass { get; set; }

        public decimal Size { get; set; }

        public decimal Luminosity { get; set; }

        public int Temperature { get; set; }

        public StarClass StarClass { get; private set; }

        public List<Planet> Planets { get; set; }

        // Автоматично определя класа на звездата
        private void SetStarClass()
        {
            decimal radius = this.Size / 2;
            if(this.Temperature >= 30_000
                || this.Luminosity >= 30_000
                || this.Mass >= 16
                || radius >= 6.6M)
            {
                this.StarClass = StarClass.O;
            }
            else if ((this.Temperature >= 3700 && this.Temperature < 5200)
                || (this.Luminosity >= 0.08M && this.Luminosity < 0.6M)
                || (this.Mass >= 0.45M && this.Mass < 0.8M)
                || (radius >= 0.7M && radius < 0.96M))
            {
                this.StarClass = StarClass.K;
            }
            else if ((this.Temperature >= 5200 && this.Temperature < 6000)
                || (this.Luminosity >= 0.6M && this.Luminosity < 1.5M)
                || (this.Mass >= 0.8M && this.Mass < 1.04M)
                || (radius >= 0.96M && radius < 1.15M))
            {
                this.StarClass = StarClass.G;
            }
            else if ((this.Temperature >= 6000 && this.Temperature < 7500)
               || (this.Luminosity >= 1.5M && this.Luminosity < 5)
               || (this.Mass >= 1.04M && this.Mass < 1.4M)
               || (radius >= 1.15M && radius < 1.4M))
            {
                this.StarClass = StarClass.F;
            }
            else if ((this.Temperature >= 7500 && this.Temperature < 10_000)
               || (this.Luminosity >= 5 && this.Luminosity < 25)
               || (this.Mass >= 1.4M && this.Mass < 2.1M)
               || (radius >= 1.4M && radius < 1.8M))
            {
                this.StarClass = StarClass.A;
            }
            else if ((this.Temperature >= 10_000 && this.Temperature < 30_000)
               || (this.Luminosity >= 25 && this.Luminosity < 30_000)
               || (this.Mass >= 2.1M && this.Mass < 16)
               || (radius >= 1.8M && radius < 6.6M))
            {
                this.StarClass = StarClass.B;
            }else if((this.Temperature >= 2400 && this.Temperature < 3700)
                 || this.Luminosity <= 0.08M
                 || (this.Mass >= 0.08M && this.Mass < 0.45M)
                 || (radius <= 0.7M))
            {
                this.StarClass = StarClass.M;
            }
        }
    }
}
