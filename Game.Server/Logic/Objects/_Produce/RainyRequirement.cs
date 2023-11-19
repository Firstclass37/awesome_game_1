using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Server.Logic.Objects._Produce
{
    internal class RainyRequirement : IProduceRequirement
    {
        public bool Can(GameObjectAggregator gameObjectAggregator)
        {
            throw new NotImplementedException();
        }
    }
}
