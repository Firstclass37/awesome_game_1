using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Requirements
{
    internal class OnlyTypeRequirement: ICreationRequirement
    {
        private readonly string[] _buildingTypes;

        public OnlyTypeRequirement(string[] buildingTypes)
        {
            _buildingTypes = buildingTypes;
        }

        public OnlyTypeRequirement(string buildingType) 
        {
            _buildingTypes = new string[] { buildingType };
        }

        public bool Satisfy(Coordiante coordiante, Dictionary<Coordiante, GameObjectAggregator> area)
        {
            return area.Values.All(b => _buildingTypes.Contains(b.GameObject.ObjectType));
        }
    }
}