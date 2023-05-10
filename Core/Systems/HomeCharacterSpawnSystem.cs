using My_awesome_character.Core.Game.Events;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;

namespace My_awesome_character.Core.Systems
{
    internal class HomeCharacterSpawnSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IEventAggregator _eventAggregator;

        public HomeCharacterSpawnSystem(ISceneAccessor sceneAccessor, IEventAggregator eventAggregator)
        {
            _sceneAccessor = sceneAccessor;
            _eventAggregator = eventAggregator;
        }

        public void OnStart()
        {
        }

        public void Process(double gameTime)
        {
            var homes = _sceneAccessor.FindAll<Home>();
            
            foreach (var home in homes) 
            {
                var needFire = (gameTime - home.LastFireTime) > home.SpawnEverySecond;
                if (needFire)
                {
                    home.LastFireTime = gameTime;
                    _eventAggregator.GetEvent<GameEvent<CharacterCreationRequestEvent>>().Publish(new CharacterCreationRequestEvent { InitPosition = home.SpawnCell });
                }
            }
        }
    }
}