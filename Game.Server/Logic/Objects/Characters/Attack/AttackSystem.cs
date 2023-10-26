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

        public AttackSystem(IGameObjectAgregatorRepository gameObjectAgregatorRepository, IGameObjectAccessor gameObjectAccessor, 
            IAreaCalculator areaCalculator, ICharacterDamageService characterDamageService)
        {
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _gameObjectAccessor = gameObjectAccessor;
            _areaCalculator = areaCalculator;
            _characterDamageService = characterDamageService;
        }

        public void Process(double gameTimeSeconds)
        {
            //var a = (int)gameTimeSeconds;
            //if (a % 2 != 0)
            //    return;

            var canAttackCharacters = _gameObjectAccessor.FindAll(CharacterTypes.Default)
                .Where(c => gameTimeSeconds - c.GetAttributeValue(AttackAttributes.LastAttackTime) > c.GetAttributeValue(AttackAttributes.Speed))
                .ToArray();
            var coordinatesGroups = canAttackCharacters.GroupBy(c => c.RootCell, c => c).ToDictionary(c => c.Key, c => c);

            var alreadDead = new HashSet<Guid>();
            foreach (var character in canAttackCharacters.Mix()) 
            {
                if (alreadDead.Contains(character.GameObject.Id))
                    continue;

                var damageArea = _areaCalculator.GetAreaCross(character.RootCell, character.GetAttributeValue(AttackAttributes.Distance));
                var firstTarget = damageArea
                    .Where(a => coordinatesGroups.ContainsKey(a))
                    .SelectMany(a => coordinatesGroups[a])
                    .FirstOrDefault(c => !alreadDead.Contains(c.GameObject.Id) && c.GameObject.PlayerId != character.GameObject.PlayerId);

                if (firstTarget == null)
                    continue;

                _characterDamageService.InstantKill(new Character(firstTarget));
                character.SetAttributeValue(AttackAttributes.LastAttackTime, gameTimeSeconds);
                _gameObjectAgregatorRepository.Update(character);

                alreadDead.Add(firstTarget.GameObject.Id);
            }
        }
    }
}