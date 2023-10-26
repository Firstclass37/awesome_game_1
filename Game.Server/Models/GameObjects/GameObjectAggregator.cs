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
        }

        public GameObject GameObject { get; set; }

        public List<GameObjectToAttribute> Attributes { get; set; }

        public List<GameObjectPosition> Area { get; set; }

        public List<PeriodicAction> PeriodicActions { get; set; }

        public List<GameObjectInteraction> Interactions { get; set; }


        public Coordiante RootCell => Area.First(p => p.IsRoot).Coordiante;

        public IReadOnlyCollection<Coordiante> Cells => Area.Select(a => a.Coordiante).ToArray();


        public void ExpandArea(Coordiante coordiante, bool isBlocking = false) => Area.Add(new GameObjectPosition(GameObject.Id, coordiante, false, false));

        public T GetAttributeValue<T>(GameObjectAttribute<T> gameObjectAttribute) => GetAttributeValue<T>(gameObjectAttribute.Name);

        public bool AttributeExists(string attribute) => Attributes.Any(a => a.AttributeType == attribute);

        public void SetAttributeValue<T>(GameObjectAttribute<T> gameObjectAttribute, T value) 
        {
            var existing = Attributes.First(a => a.AttributeType == gameObjectAttribute.Name);
            var newAttribute = existing with { Value = value };
            Attributes.Remove(existing);
            Attributes.Add(newAttribute);
        }

        public void ModifyAttribute<T>(GameObjectAttribute<T> gameObjectAttribute, Func<T, T> modifier)
        {
            var existing = Attributes.First(a => a.AttributeType == gameObjectAttribute.Name);
            var newValue = modifier((T)existing.Value);
            Attributes.Remove(existing);
            Attributes.Add(existing with { Value = newValue });
        }

        internal T GetAttributeValue<T>(string attributeType) => (T)(Attributes.FirstOrDefault(a => a.AttributeType == attributeType)?.Value ?? throw new ArgumentOutOfRangeException($"not fount atribute [{attributeType}] from object {GameObject.Id}({GameObject.ObjectType})"));
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