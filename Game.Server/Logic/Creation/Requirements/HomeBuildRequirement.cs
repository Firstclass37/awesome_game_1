using Game.Server.Models.Maps;

namespace My_awesome_character.Core.Game.Buildings.Requirements
{
    public class HomeBuildRequirement : IBuildRequirement
    {
        public bool CanBuild(Coordiante[] area)
        {
            return area.All(c => c.CellType == MapCellType.Groud);
        }
    }
}