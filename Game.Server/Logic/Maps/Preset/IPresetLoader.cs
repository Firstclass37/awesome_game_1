using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Maps.Preset
{
    internal record Cell(Coordiante Coordinate, Neightbor[] Neightbors);

    internal record Neightbor(Coordiante Coordinate, Direction Direction);

    internal interface IPresetLoader
    {
        IReadOnlyCollection<Cell> Load();
    }
}