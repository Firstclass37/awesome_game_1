using Game.Server.Events.Core;
using Game.Server.Events.List.Character;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Character
{
    internal class MovementSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IEventAggregator _eventAggregator;

        public MovementSystem(ISceneAccessor sceneAccessor, IEventAggregator eventAggregator)
        {
            _sceneAccessor = sceneAccessor;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<GameEvent<CharacterMoveEvent>>().Subscribe(Move);
        }

        public void OnStart()
        {
        }

        public void Process(double gameTime)
        {

        }

        private void Move(CharacterMoveEvent @event)
        {
            var target = new CoordianteUI(@event.NewPosition.X, @event.NewPosition.Y);

            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var character = _sceneAccessor.FindFirst<character>(SceneNames.Character(@event.CharacterId));
            character.MoveTo(target, @event.Speed, p => map.GetLocalPosition(p));
        }
    }
}