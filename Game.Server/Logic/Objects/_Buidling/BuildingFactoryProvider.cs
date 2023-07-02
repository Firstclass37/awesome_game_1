using Autofac;
using Game.Server.Logic.Objects.Home.Creation;
using Game.Server.Logic.Objects.PowerStations.Creation;
using Game.Server.Logic.Objects.Roads.Createtion;
using Game.Server.Logic.Objects.UranusMine.Creation;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects._Buidling
{
    internal class BuildingFactoryProvider : IBuildingFactoryProvider
    {
        public IGameObjectFactory GetFor(string buildingType)
        {
            throw new NotImplementedException();
        }
    }
}