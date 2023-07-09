using Game.Server.Logic.Objects._Interactions;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Server.Logic.Objects.UranusMine.Interaction
{
    internal class UranusMineInteraction : ICharacterInteraction
    {
        public void Interact(GameObjectAggregator gameObject, Character character, Coordiante interactionPoint)
        {
            //return new CommonInteractionAction(c =>
            //{
            //    _eventAggregator.GetEvent<GameEvent<TakeDamageCharacterEvent>>().Publish(new TakeDamageCharacterEvent { CharacterId = c.Id, Damage = 1000 });
            //    _resourceManager.Increase(ResourceType.Uranus, 2);
            //});

            //return new CommonInteractionAction(c =>
            //{
            //    _eventAggregator.PublishGameEvent(new TakeDamageCharacterEvent { CharacterId = c.Id, Damage = 1000 });
            //    _eventAggregator.PublishGameEvent(new BuildingDelayedActionEvent
            //    {
            //        BuidlingId = buildingId,
            //        DelaySec = 5,
            //        Event = () => _resourceManager.Increase(ResourceType.Uranus, 2)
            //    });
            //});
        }
    }
}