using Game.Server.Events.Core;
using Game.Server.Events.List.Movement;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Ui;
using My_awesome_character.Core.Ui.Extentions;

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

            _eventAggregator.GetEvent<GameEvent<GameObjectPositionChangingEvent>>().Subscribe(Move);
            _eventAggregator.GetEvent<GameEvent<GameObjectPositionChangedEvent>>().Subscribe(OnChanged);
        }

        public void OnStart()
        {
        }

        public void Process(double gameTime)
        {

        }

        private void Move(GameObjectPositionChangingEvent @event)
        {
            if (@event.GameObjectType != CharacterTypes.Default)
                return;

            var target = new CoordianteUI(@event.TargetPosition.X, @event.TargetPosition.Y);
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var character = map.CharacterContainer.GetNamedNode<character>(SceneNames.Character(@event.GameObjectId));
            character.MoveTo(target, (float)@event.Speed, p => map.GetLocalPosition(p));
        }

        private void OnChanged(GameObjectPositionChangedEvent @event)
        {
            if (@event.GameObjectType != CharacterTypes.Default)
                return;

            var newPosition = new CoordianteUI(@event.NewPosition.X, @event.NewPosition.Y);
            var character = _sceneAccessor.FindFirst<Map>(SceneNames.Map).CharacterContainer.GetNamedNode<character>(SceneNames.Character(@event.GameObjectId));
            character.MapPosition = newPosition;
        }
    }
}