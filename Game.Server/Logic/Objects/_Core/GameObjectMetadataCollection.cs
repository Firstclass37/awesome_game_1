namespace Game.Server.Logic.Objects._Core
{
    internal class GameObjectMetadataCollection : IGameObjectMetadataCollection
    {
        private readonly IGameObjectMetadata[] _matadatas;

        public GameObjectMetadataCollection(IGameObjectMetadata[] matadatas)
        {
            _matadatas = matadatas;
        }

        public IGameObjectMetadata Get(string objectType)
        {
            var meta = _matadatas.FirstOrDefault(m => m.ObjectType == objectType);
            return meta != null ? meta : throw new ArgumentOutOfRangeException($"metadata for object type {objectType} was not found");
        }
    }
}