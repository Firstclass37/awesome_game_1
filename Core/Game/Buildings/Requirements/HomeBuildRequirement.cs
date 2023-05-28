using My_awesome_character.Core.Constatns;
using System.Linq;

namespace My_awesome_character.Core.Game.Buildings.Requirements
{
    public class HomeBuildRequirement : IBuildRequirement
    {
        public bool CanBuild(MapCell[] area)
        {
            return area.All(c => c.CellType == MapCellType.Groud);
        }
    }
}