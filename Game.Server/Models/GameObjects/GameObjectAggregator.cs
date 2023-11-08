using Game.Server.Models.Maps;

namespace Game.Server.Models.GameObjects
{
    internal class GameObjectAggregator
    {
        public GameObjectAggregator()
        {
            Attributes = new List<GameObjectToAttribute>(0);
            Area = new List<GameObjectPosition>(0);
            PeriodicActions = new List<PeriodicAction>(0);
            Interactions = new List<GameObjectInteraction>(0);
            ChangedAttributes = new List<GameObjectToAttribute>();
        }

        public GameObject GameObject { get; set; }

        public List<GameObjectToAttribute> Attributes { get; set; }

        public List<GameObjectPosition> Area { get; set; }

        public List<PeriodicAction> PeriodicActions { get; set; }

        //todo: Dictionary<string, GameObjectInteraction> - Key - GameObjectType, 
        public List<GameObjectInteraction> Interactions { get; set; }

        public List<GameObjectToAttribute> ChangedAttributes { get; }

        public Coordiante RootCell => Area.First(p => p.IsRoot).Coordiante;

        public IReadOnlyCollection<Coordiante> Cells => Area.Select(a => a.Coordiante).ToArray();


        public void ExpandArea(Coordiante coordiante, bool isBlocking = false) => Area.Add(new GameObjectPosition(GameObject.Id, coordiante, false, false));

        public T GetAttributeValue<T>(GameObjectAttribute<T> gameObjectAttribute) => GetAttributeValue<T>(gameObjectAttribute.Name);

        public bool AttributeExists(string attribute) => Attributes.Any(a => a.AttributeType == attribute);

        public void SetAttributeValue<T>(GameObjectAttribute<T> gameObjectAttribute, T value) 
        {
            var existing = Attributes.FirstOrDefault(a => a.AttributeType == gameObjectAttribute.Name);
            if (existing != null)
            {
                var newAttribute = existing with { Value = value };
                Attributes.Remove(existing);
                Attributes.Add(newAttribute);

                ChangedAttributes.Add(newAttribute);
            }
            else
            {
                var newAttribute = new GameObjectToAttribute(GameObject.Id, gameObjectAttribute.Name, value);
                Attributes.Add(newAttribute);
                ChangedAttributes.Add(newAttribute);
            }
        }

        public void ModifyAttribute<T>(GameObjectAttribute<T> gameObjectAttribute, Func<T, T> modifier)
        {
            var existing = Attributes.First(a => a.AttributeType == gameObjectAttribute.Name);
            var newValue = modifier((T)existing.Value);
            var newAttribute = existing with { Value = newValue };
            Attributes.Remove(existing);
            Attributes.Add(newAttribute);

            ChangedAttributes.Add(newAttribute);
        }

        internal T GetAttributeValue<T>(string attributeType) 
        {
            var attribute = Attributes.FirstOrDefault(a => a.AttributeType == attributeType);
            if (attribute == null)
                throw new ArgumentOutOfRangeException($"not fount atribute [{attributeType}] from object {GameObject.Id} ({GameObject.ObjectType})");

            return (T)attribute.Value;
        } 
    }


    public class GameObjectAttribute<T>
    {
        public string Name { get; init; }

        public GameObjectAttribute(string name)
        {
            Name = name;
        }
    }
}