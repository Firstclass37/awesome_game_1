using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Requirements
{
    internal interface ICreationRequirement
    {
        bool Satisfy(Dictionary<Coordiante, GameObjectAggregator> area);
    }
}