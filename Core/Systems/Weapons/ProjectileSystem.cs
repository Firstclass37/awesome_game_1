using Game.Server.Events.Core;
using Game.Server.Events.List;
using Game.Server.Events.List.Homes;
using Game.Server.Events.List.Movement;
using Godot;
using My_awesome_character.Common.Extentions;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Helpers;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Weapons
{
    internal class ProjectileSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;

        public ProjectileSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _sceneAccessor = sceneAccessor;

            eventAggregator.SubscribeGameEvent<ObjectCreatedEvent>(OnCreated);
            eventAggregator.SubscribeGameEvent<GameObjectDestroiedEvent>(OnDestroied);
            eventAggregator.SubscribeGameEvent<GameObjectPositionChangingEvent>(OnMoveStart);
            eventAggregator.SubscribeGameEvent<GameObjectPositionChangedEvent>(OnMoveEnd);
        }

        public void OnStart()
        {
        }

        public void Process(double gameTime)
        {
        }

        private void OnCreated(ObjectCreatedEvent @event)
        {
            if (@event.ObjectType != ProjectileTypes.Stone)
                return;

            var existing = _sceneAccessor.GetScene<Projectile>(SceneNames.Projectile(@event.Id));
            if (existing != null)
                return;

            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var projectile = SceneFactory.Create<Projectile>(SceneNames.Projectile(@event.Id), ScenePaths.Projectile);
            map.AddChild(projectile, forceReadableName: true);

            projectile.Id = @event.Id;
            projectile.MapPosition = new CoordianteUI(@event.Root.X, @event.Root.Y);
            projectile.Position = GetPosition(map, projectile, projectile.MapPosition);
            projectile.Scale = new Vector2(0.5f, 0.5f);
            projectile.SetTexture(new ProjectileTextureSelector().Select(@event.ObjectType));

            GD.Print($"object {@event.Id} {@event.ObjectType} created at {@event.Root}");
        }

        private void OnDestroied(GameObjectDestroiedEvent @event)
        {
            if (@event.ObjectType != ProjectileTypes.Stone)
                return;

            var projectile = _sceneAccessor.GetScene<Projectile>(SceneNames.Projectile(@event.ObjectId));
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);

            map.RemoveChild(projectile);
        }

        private void OnMoveStart(GameObjectPositionChangingEvent @event)
        {
            if (@event.GameObjectType != ProjectileTypes.Stone)
                return;

            var target = new CoordianteUI(@event.TargetPosition.X, @event.TargetPosition.Y);
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var projectile = _sceneAccessor.GetScene<Projectile>(SceneNames.Projectile(@event.GameObjectId));
            projectile.MoveTo(target, (float)@event.Speed, p => GetPosition(map, projectile, p));
        }

        private void OnMoveEnd(GameObjectPositionChangedEvent @event)
        {
            if (@event.GameObjectType != ProjectileTypes.Stone)
                return;

            var newPosition = new CoordianteUI(@event.NewPosition.X, @event.NewPosition.Y);
            var projectile = _sceneAccessor.GetScene<Projectile>(SceneNames.Projectile(@event.GameObjectId));
            if (projectile != null)
                projectile.MapPosition = newPosition;
        }


        private Vector2 GetPosition(Map map, Projectile projectile, CoordianteUI coordiante)
        {
            var position = map.GetLocalPosition(coordiante);
            position.Y = position.Y - 30;
            return position;
        }
    }
}