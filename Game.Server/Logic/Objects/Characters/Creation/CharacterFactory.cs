﻿using Game.Server.DataBuilding;
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
                .AddAttribute(AttackAttributes.Distance, 1)
                .AddAttribute(AttackAttributes.LastAttackTime, 0)
                .AddAttribute(AttackAttributes.Damage, 40)
                .AddAttribute(AttackAttributes.LastTarget, null)
                .AddAttribute(AttackAttributes.Speed, 1.0D)
                .AddAttribute(CharacterAttributes.Speed, 1.0d)
                .AddAttribute(CharacterAttributes.CharacterState, CharacterState.Free)
                .AddAttribute(HealthAttributes.Health, 100)
                .Build();
        }
    }
}