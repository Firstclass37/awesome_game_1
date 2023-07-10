using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Ui;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Game.Events;
using My_awesome_character.Core.Game.Events.Character;

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

            _eventAggregator.GetEvent<GameEvent<MovementCharacterPathEvent>>().Subscribe(Move);
            _eventAggregator.GetEvent<GameEvent<StopMovingRequest>>().Subscribe(OnStop);
        }

        public void OnStart()
        {
        }

        public void Process(double gameTime)
        {

        }

        private void OnStop(StopMovingRequest request)
        {
            var character = _sceneAccessor.FindFirst<character>(SceneNames.Character(request.CharacterId));
            character.StopMoving();
        }

        private void Move(MovementCharacterPathEvent @event)
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var game = _sceneAccessor.FindFirst<Node2D>(SceneNames.Game);
            var character = _sceneAccessor.FindFirst<character>(SceneNames.Character(@event.CharacterId));

            character.IsMoving = true;
            character.MoveTo(@event.Path, 
                mc => map.GetLocalPosition(mc),
                mc => _eventAggregator.GetEvent<GameEvent<CharacterPositionChangedEvent>>().Publish(new CharacterPositionChangedEvent { CharacterId = character.Id, NewPosition = mc }),
                () => OnMovementEnd(character));
        }

        private void OnMovementEnd(character character)
        {
            character.IsMoving = false;
            _eventAggregator.GetEvent<GameEvent<MovementEndEvent>>().Publish(new MovementEndEvent
            {
                MovementId = 0,
                ObjectId = character.Id,
                ObjectType = typeof(character)
            });
        }
    }
}