using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Server.Logic._Extentions
{
    internal static class GameObjectAgregatorExtentions
    {
        public static bool Interactable(this GameObjectAggregator gameObjectAggregator)
        {
            return gameObjectAggregator.Attributes.Any(a => a.AttributeType == AttrituteTypes.Interactable);
        }
    }
}