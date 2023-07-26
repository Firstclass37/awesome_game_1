using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.UranusMine.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.UranusMine.Creation
{
    internal class UranusMineFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.UranusMine)
                .AddArea(root, area)
                .AddInteraction<UranusMineInteraction>()
                .AddAttribute(AttrituteTypes.Interactable)
                .Build();
        }
    }
}