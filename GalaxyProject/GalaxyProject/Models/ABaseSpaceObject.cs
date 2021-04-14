
namespace GalaxyProject.Models
{
    public abstract class ABaseSpaceObject
    {
        public string Name { get; set; }
        
        protected ABaseSpaceObject(string aName)
        {
            this.Name = aName;
        }
    }
}
