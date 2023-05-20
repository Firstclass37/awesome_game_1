namespace My_awesome_character.Core.Game.Buildings
{
    public interface IBuildRequirement
    {
        bool CanBuild(MapCell[] area);
    }
}