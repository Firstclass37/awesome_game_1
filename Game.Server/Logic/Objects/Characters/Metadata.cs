using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.Characters.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.Characters
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly CharacterFactory _characterFactory;

        public Metadata(CharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
        }

        public string ObjectType => CharacterTypes.Default;

        public string Description => "Default character";

        public AreaSize Size => AreaSize.Area1x1;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(new string[] { GroundTypes.Ground, BuildingTypes.Road });

        public IGameObjectFactory GameObjectFactory => _characterFactory;

        public Price BasePrice => Price.Free;
    }
}