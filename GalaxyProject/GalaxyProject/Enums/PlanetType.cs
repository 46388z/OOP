
using System.ComponentModel;

namespace GalaxyProject.Enums
{
    public enum PlanetType
    {
        [Description("terrestrial")]
        Terrestrial,
        [Description("giant planet")]
        GiantPlanet,
        [Description("ice giant")]
        IceGiant,
        [Description("mesoplanet")]
        Mesoplanet,
        [Description("mini-neptune")]
        MiniNeptune,
        [Description("planetar")]
        Planetar,
        [Description("super-earth")]
        SuperEarth,
        [Description("super-jupiter")]
        SuperJupiter,
        [Description("sub-earth")]
        SubEarth
    }
}
