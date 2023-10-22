using Game.Server.DataBuilding;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Creation
{
    internal class CharacterFactory : IGameObjectFactory
    {
        private readonly IAreaCalculator _areaCalculator;
        private readonly int _damageAreaSize = 1;

        public CharacterFactory(IAreaCalculator areaCalculator)
        {
            _areaCalculator = areaCalculator;
        }

        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(CharacterTypes.Default)
                .AddArea(root, area)
                .AddAttribute(AttrituteTypes.Interactable)
                .AddAttribute(CharacterAttributes.DamageArea, _areaCalculator.GetAreaCross(root, _damageAreaSize))
                .AddAttribute(CharacterAttributes.Speed, 1.0d)
                .Build();
        }
    }
}