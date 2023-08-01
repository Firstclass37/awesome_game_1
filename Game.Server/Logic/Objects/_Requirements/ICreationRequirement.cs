using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Requirements
{
    internal interface ICreationRequirement
    {
        bool Satisfy(Coordiante root, Dictionary<Coordiante, GameObjectAggregator> area);
    }
}