using Game.Server.Logic.Systems;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Logic.Maps;
using Game.Server.DataAccess;
using Game.Server.Events.Core;
using Game.Server.Logic.Objects._Core;
using Game.Server.Events.Core.Extentions;
using Game.Server.Events.List.Homes;

namespace Game.Server.Logic.Objects._Systems
{
    internal class ProduceSystem : ISystem
    {
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IStorageCacheDecorator _gameObjectPositionCacheDecorator;
        private readonly IEventAggregator _eventAggregator;

        private readonly ITypesCollection<IProduceRequirement> _requirements;
        private readonly ITypesCollection<IProduceAction> _actions;

        public ProduceSystem(IGameObjectAgregatorRepository gameObjectAgregatorRepository, 
            IGameObjectAccessor gameObjectAccessor, IStorageCacheDecorator gameObjectPositionCacheDecorator, 
            IEventAggregator eventAggregator, ITypesCollection<IProduceRequirement> requirements,
            ITypesCollection<IProduceAction> actions)
        {
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _gameObjectAccessor = gameObjectAccessor;
            _gameObjectPositionCacheDecorator = gameObjectPositionCacheDecorator;
            _eventAggregator = eventAggregator;
            _requirements = requirements;
            _actions = actions;
        }

        public void Process(double gameTimeSeconds)
        {
            var manufactorings = _gameObjectPositionCacheDecorator
                .GetObjectsWithAttributes(ManufactureAttributesTypes.LastProduceTime)
                .Select(id => _gameObjectAccessor.Get(id))
                .ToArray();

            foreach (var manufactoring in manufactorings)
            {
                var lastProduceTime = manufactoring.GetAttributeValue(ManufactureAttributes.LastProduceTime);
                var queueSize = manufactoring.GetAttributeValue(ManufactureAttributes.ProduceQueueSize);
                var speed = manufactoring.GetAttributeValue(ManufactureAttributes.PrduceSpeedSeconds);
                var isWorking = manufactoring.GetAttributeValue(ManufactureAttributes.Working);

                if (lastProduceTime == 0.0)
                {
                    manufactoring.SetAttributeValue(ManufactureAttributes.LastProduceTime, gameTimeSeconds);
                    _gameObjectAgregatorRepository.Update(manufactoring);
                    continue;
                }

                if (queueSize == 0)
                    continue;

                if (speed > 0 && isWorking == false && RequirementsSatisfy(manufactoring))
                {
                    _eventAggregator.PublishGameEvent(new ProduceStartEvent { BuildingId = manufactoring.GameObject.Id, Speed = speed, QueueSize = queueSize, Root = manufactoring.RootCell });
                    manufactoring.SetAttributeValue(ManufactureAttributes.Working, true);

                    _gameObjectAgregatorRepository.Update(manufactoring);
                }

                if (speed == 0 || gameTimeSeconds - lastProduceTime > speed)
                {
                    manufactoring.SetAttributeValue(ManufactureAttributes.LastProduceTime, gameTimeSeconds);
                    manufactoring.SetAttributeValue(ManufactureAttributes.Working, false);

                    if (Produce(manufactoring) && queueSize != -1)
                        manufactoring.ModifyAttribute(ManufactureAttributes.ProduceQueueSize, q => q - 1);

                    _gameObjectAgregatorRepository.Update(manufactoring);
                }
            }
        }

        private bool Produce(GameObjectAggregator gameObject)
        {
            var customActionType = gameObject.GetAttributeValue(ManufactureAttributes.ProduceAction);
            if (customActionType == null)
                throw new ArgumentException($"no action found for manufactoring {gameObject.GameObject.Id}");

            return _actions.Find(customActionType.TypeName).Produce(gameObject);
        }

        private bool RequirementsSatisfy(GameObjectAggregator gameObject)
        {
            var requirements = gameObject.GetAttributeValue(ManufactureAttributes.Requirements).Select(r => _requirements.Find(r.TypeName)).ToArray();
            return requirements.All(r => r.Can(gameObject));
        }
    }
}