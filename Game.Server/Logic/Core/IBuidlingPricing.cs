using Game.Server.Logic.Objects._Core;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Core
{
    internal interface IBuidlingPricing
    {
        Price GetActualPriceFor(IGameObjectMetadata objectMetadata);
    }
}