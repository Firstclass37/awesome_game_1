namespace Game.Server.Logic.Objects._Buidling
{
    internal interface IBuildingFactoryProvider
    {
        IGameObjectFactory GetFor(string buildingType);
    }
}