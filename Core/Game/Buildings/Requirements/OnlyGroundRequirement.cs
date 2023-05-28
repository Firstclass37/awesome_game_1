using System.Linq;

namespace My_awesome_character.Core.Game.Buildings.Requirements
{
    internal class OnlyGroundRequirement : IBuildRequirement
    {
        public bool CanBuild(MapCell[] area)
        {
            return area.All(a => a.CellType == Constatns.MapCellType.Groud);
        }
    }
}