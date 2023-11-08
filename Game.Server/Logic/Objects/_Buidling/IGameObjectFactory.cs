using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal interface IGameObjectFactory
    {
        GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player);
    }
}