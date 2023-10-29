using Game.Server.Common.Extentions;
using Game.Server.DataAccess;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Systems;
using Game.Server.Logic.Weapons;
using Game.Server.Models.Constants;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.Characters.Attack
{
    internal class AttackSystem : ISystem
    {
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IStorage _storage;
        private readonly IArsenal _arsenal;

        public AttackSystem(IGameObjectAgregatorRepository gameObjectAgregatorRepository, IGameObjectAccessor gameObjectAccessor, IArsenal arsenal, IStorage storage)
        {
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _gameObjectAccessor = gameObjectAccessor;
            _arsenal = arsenal;
            _storage = storage;
        }

        public void Process(double gameTimeSeconds)
        {
            //var a = (int)gameTimeSeconds;
            //if (a % 2 != 0)
            //    return;

            var canAttackCharacters = _gameObjectAccessor.FindAll(CharacterTypes.Default);
            var targetPool = canAttackCharacters.GroupBy(c => c.RootCell, c => c).ToDictionary(c => c.Key, c => c.ToList());

            foreach (var character in canAttackCharacters.Mix())
            {
                var weaponMeta = _arsenal.Get(character.GetAttributeValue(AttackAttributes.Weapon));

                var target = weaponMeta.TargetLocator.FindTarget(character, targetPool);

                if (target == null)
                {
                    if (character.GetAttributeValue(CharacterAttributes.CharacterState) == CharacterState.InCombat)
                    {
                        character.SetAttributeValue(CharacterAttributes.CharacterState, CharacterState.Free);
                        _gameObjectAgregatorRepository.Update(character);
                    }
                        
                    continue;
                }

                if (!weaponMeta.ActivateCondition.CanUse(character, target, gameTimeSeconds))
                    continue;

                var attackSuccesful = weaponMeta.Activator.Activate(character, target, gameTimeSeconds);
                if (attackSuccesful) 
                {
                    character.SetAttributeValue(CharacterAttributes.CharacterState, CharacterState.InCombat);
                    _gameObjectAgregatorRepository.Update(character);

                    if (!_storage.Exists<GameObject>(target.GameObject.Id))
                        targetPool[target.RootCell].Remove(target);
                }
            }
        }
    }
}