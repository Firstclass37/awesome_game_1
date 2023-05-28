namespace My_awesome_character.Core.Game.Buildings.Requirements
{
    public interface IBuildRequirement
    {
        bool CanBuild(MapCell[] area);
    }
}