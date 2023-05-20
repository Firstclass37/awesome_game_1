using My_awesome_character.Core.Constatns;
using System.Linq;

namespace My_awesome_character.Core.Game.Buildings
{
    public class UranusMineBuildRequirement : IBuildRequirement
    {
        public bool CanBuild(MapCell[] area)
        {
            return area.All(c => c.CellType == MapCellType.Resource);
        }
    }
}