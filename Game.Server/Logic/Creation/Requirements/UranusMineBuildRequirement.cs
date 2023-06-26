using Game.Server.Models.Maps;

namespace My_awesome_character.Core.Game.Buildings.Requirements
{
    public class UranusMineBuildRequirement : IBuildRequirement
    {
        public bool CanBuild(Coordiante[] area)
        {
            return area.All(c => c.CellType == MapCellType.Resource && c.Tags.Contains(MapCellTags.Resource_Uranus));
        }
    }
}