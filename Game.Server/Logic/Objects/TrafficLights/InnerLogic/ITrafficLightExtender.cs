using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.TrafficLights.InnerLogic
{
    internal interface ITrafficLightExtender
    {
        bool TryExtend(Coordiante coordiante);
    }
}