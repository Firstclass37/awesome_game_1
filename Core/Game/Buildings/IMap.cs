namespace My_awesome_character.Core.Game.Buildings
{
    public interface IMap
    {
        MapCell GetActualCell(Coordiante coordiante);
    }
}