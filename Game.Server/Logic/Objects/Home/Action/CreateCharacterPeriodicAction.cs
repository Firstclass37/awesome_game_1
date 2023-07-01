using Game.Server.Events.Core;
using Game.Server.Logic.Building;
using Game.Server.Models.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Server.Logic.Objects.Home.Action
{
    internal class CreateCharacterPeriodicAction : IPeriodicAction
    {
        public void Trigger(GameObjectAggregator gameObject)
        {
            var @event = new CharacterCreationRequestEvent { InitPosition = spawnCell };
            var action = () =>
            {
                var actualCell = map.GetActualCell(new Coordiante(spawnCell.X, spawnCell.Y));
                if (actualCell.CellType == MapCellType.Road || actualCell.Tags.Contains(MapCellTags.Trap))
                    _eventAggregator.GetEvent<GameEvent<CharacterCreationRequestEvent>>().Publish(@event);
            };
            return new CommonPeriodicAction(action, 5, SystemNode.GameTime);
        }
    }
}
