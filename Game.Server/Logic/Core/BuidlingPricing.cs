using Game.Server.Logic.Objects._Core;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Core
{
    internal class BuidlingPricing : IBuidlingPricing
    {
        public Price GetActualPriceFor(IGameObjectMetadata objectMetadata)
        {
            return objectMetadata.BasePrice;
        }
    }
}