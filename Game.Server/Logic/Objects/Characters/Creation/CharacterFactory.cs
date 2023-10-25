using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Creation
{
    internal class CharacterFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(CharacterTypes.Default)
                .AddArea(root, area)
                .AddAttribute(AttrituteTypes.Interactable)
                .AddAttribute(CharacterAttributes.AttackDistance, 1)
                .AddAttribute(CharacterAttributes.LastAttackTime, 0)
                .AddAttribute(CharacterAttributes.AttackSpeed, 1.0D)
                .AddAttribute(CharacterAttributes.Speed, 1.0d)
                .Build();
        }
    }
}