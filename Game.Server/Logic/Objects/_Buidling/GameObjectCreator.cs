using Game.Server.Logic.Objects._Core;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal class GameObjectCreator : IGameObjectCreator
    {
        private readonly IGameObjectMetadata[] _metadatas;

        public GameObjectCreator(IGameObjectMetadata[] metadatas)
        {
            _metadatas = metadatas;
        }

        public GameObjectAggregator Create(string objectType, Coordiante point, object args)
        {
            var metadata = _metadatas.FirstOrDefault(m => m.ObjectType == objectType);
            if (metadata == null)
                throw new ArgumentException($"metadata for object {objectType} was not found");

            throw new NotImplementedException();
        }
    }
}