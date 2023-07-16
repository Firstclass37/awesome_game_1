using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Server.Logic.Objects.AluminumMine
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2X2Getter;

        public string ObjectType => BuildingTypes.AluminumMine;

        public string Description => "Aluminum Mine";

        public Dictionary<int, int> BasePrice => new Dictionary<int, int> 
        {
            { ResourceType.Steel, 45 }
        };

        public IAreaGetter AreaGetter => _area2X2Getter;

        public ICreationRequirement CreationRequirement => throw new NotImplementedException();

        public IGameObjectFactory GameObjectFactory => throw new NotImplementedException();
    }
}
