namespace Game.Server.Logic.Objects._Core
{
    internal interface IGameObjectMetadataCollection
    {
        IGameObjectMetadata Get(string objectType);
    }
}