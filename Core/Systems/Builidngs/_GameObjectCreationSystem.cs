using Game.Server.Events.Core;
using Game.Server.Events.List.Homes;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_awesome_character.Core.Systems.Builidngs
{
    internal class GameObjectCreationSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public GameObjectCreationSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<ObjectCreatedEvent>>().Subscribe(OnCreated);
        }

        private void OnCreated(ObjectCreatedEvent @event)
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);


            if (@event.ObjectType == GroundTypes.Ground)
            {
                var root = new Coordiante(@event.Root.X, @event.Root.Y);
                var area = @event.Area.Select(c => new Coordiante(c.X, c.Y)).ToArray();

                map.SetCell(@event.Id, root, area, MapLayers.GroundLayer, Tiles.Ground);
            }
        }

        public void Process(double gameTime)
        {
        }
    }
}