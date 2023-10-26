using Game.Server.Common.Extentions;
using Game.Server.DataAccess;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Systems;
using Game.Server.Models.Constants;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GamesObjectList;

namespace Game.Server.Logic.Objects.Characters.Attack
{
    internal class AttackSystem : ISystem
    {
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IAreaCalculator _areaCalculator;
        private readonly ICharacterDamageService _characterDamageService;
        private readonly IMover _mover;

        public AttackSystem(IGameObjectAgregatorRepository gameObjectAgregatorRepository, IGameObjectAccessor gameObjectAccessor,
            IAreaCalculator areaCalculator, ICharacterDamageService characterDamageService, IMover mover)
        {
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _gameObjectAccessor = gameObjectAccessor;
            _areaCalculator = areaCalculator;
            _characterDamageService = characterDamageService;
            _mover = mover;
        }

        public void Process(double gameTimeSeconds)
        {
            //var a = (int)gameTimeSeconds;
            //if (a % 2 != 0)
            //    return;

            var canAttackCharacters = _gameObjectAccessor.FindAll(CharacterTypes.Default);
            var coordinatesGroups = canAttackCharacters.GroupBy(c => c.RootCell, c => c).ToDictionary(c => c.Key, c => c);

            var alreadDead = new HashSet<Guid>();
            foreach (var character in canAttackCharacters.Mix())
            {
                if (alreadDead.Contains(character.GameObject.Id))
                    continue;

                var damageArea = _areaCalculator.GetAreaCross(character.RootCell, character.GetAttributeValue(AttackAttributes.Distance));
                var lastTarget = character.GetAttributeValue(AttackAttributes.LastTarget);
                var firstTarget = damageArea
                    .Where(a => coordinatesGroups.ContainsKey(a))
                    .SelectMany(a => coordinatesGroups[a])
                    .Where(c => !alreadDead.Contains(c.GameObject.Id))
                    .Where(c => c.GameObject.Id == lastTarget || c.GameObject.PlayerId != character.GameObject.PlayerId)
                    .FirstOrDefault();

                if (firstTarget == null)
                {
                    if (character.GetAttributeValue(CharacterAttributes.CharacterState) == CharacterState.Attack)
                    {
                        character.SetAttributeValue(CharacterAttributes.CharacterState, CharacterState.Free);
                        _gameObjectAgregatorRepository.Update(character);
                    }
                        
                    continue;
                }

                if (gameTimeSeconds - character.GetAttributeValue(AttackAttributes.LastAttackTime) < character.GetAttributeValue(AttackAttributes.Speed))
                    continue;

                _mover.StopMoving(new Character(character));
                _characterDamageService.Damage(new Character(firstTarget), character.GetAttributeValue(AttackAttributes.Damage));
                character.SetAttributeValue(CharacterAttributes.CharacterState, CharacterState.Attack);
                character.SetAttributeValue(AttackAttributes.LastAttackTime, gameTimeSeconds);
                character.SetAttributeValue(AttackAttributes.LastTarget, firstTarget.GameObject.Id);
                _gameObjectAgregatorRepository.Update(character);

                alreadDead.Add(firstTarget.GameObject.Id);
            }
        }
    }
}