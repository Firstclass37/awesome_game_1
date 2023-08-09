using Game.Server.Logic.Objects._Core;

namespace Game.Server.Logic.Core
{
    internal interface IBuildingAvailability
    {
        bool IsAvailable(IGameObjectMetadata objectMetadata);
    }
}