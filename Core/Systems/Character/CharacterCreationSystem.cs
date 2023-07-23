using Game.Server.Events.Core;
using Game.Server.Events.List;
using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Character
{
    internal class CharacterCreationSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public CharacterCreationSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<CharacterCreatedEvent>>().Subscribe(OnCreaterionRequest);
        }

        private void OnCreaterionRequest(CharacterCreatedEvent obj)
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);

            var character = SceneFactory.Create<character>(SceneNames.Character(obj.CharacterId), ScenePaths.Character);
            map.AddChild(character, forceReadableName: true);
            character.Id = obj.CharacterId;
            character.MapPosition = new CoordianteUI(obj.Position.X, obj.Position.Y);
            character.Position = map.GetLocalPosition(character.MapPosition);
            character.Scale = new Vector2(0.8f, 0.8f);

            GD.Print($"character create {character.Id}");
        }

        public void Process(double gameTime)
        {
        }
    }
}