using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Unknown;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Ui;
using System.Linq;

namespace My_awesome_character.Core.Systems
{
    internal class MovementSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IStorage _storage;

        public MovementSystem(ISceneAccessor sceneAccessor, IStorage storage)
        {
            _sceneAccessor = sceneAccessor;
            _storage = storage;
        }

        public void Process(double gameTime)
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var game = _sceneAccessor.FindFirst<Node2D>(SceneNames.Game);
            var lazyCharacters = _sceneAccessor.FindAll<character>().Where(c => !c.IsBusy).ToArray();

            foreach (var character in lazyCharacters)
            {
                var actualMovement = _storage.FindFirstOrDefault<CharacterMovement>(m => m.CharacterId == character.Id && m.Actual);
                if (actualMovement == null)
                    continue;

                character.MoveTo(actualMovement.Path, mc => game.ToLocal(map.GetGlobalPositionOf(mc)), () => EndMovement(actualMovement));
            }
        }

        private void EndMovement(CharacterMovement characterMovement)
        {
            characterMovement.Actual = false;
            _storage.Update(characterMovement);
        }
    }
}