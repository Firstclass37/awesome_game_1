using Game.Server.Models.Maps;

namespace My_awesome_character.Core.Game.Buildings.Requirements
{
    public interface IBuildRequirement
    {
        bool CanBuild(Coordiante[] area);
    }
}