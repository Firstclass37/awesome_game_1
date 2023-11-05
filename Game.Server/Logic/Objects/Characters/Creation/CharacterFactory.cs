using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.Constants.Attributes;
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
                .AddAttribute(AttackAttributes.Weapon, WeaponsTypes.Stone)
                .AddAttribute(AttackAttributes.LastAttackTime, 0)
                .AddAttribute(AttackAttributes.LastTarget, null)
                .AddAttribute(MovementAttributesTypes.Speed, 1.5d)
                .AddAttribute(CharacterAttributes.CharacterState, CharacterState.Free)
                .AddAttribute(HealthAttributes.Health, 100)
                .Build();
        }
    }
}