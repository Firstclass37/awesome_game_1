namespace Game.Server.Logic.Objects._Core
{
    internal interface ITypesCollection<TType>
    {
        TType Find(string name);
    }

    internal class TypesCollection<TType> : ITypesCollection<TType>
    {
        private readonly Dictionary<string, TType> _instances = new();

        public TypesCollection(IEnumerable<TType> instances)
        {
            _instances = instances.GroupBy(i => i.GetType().FullName).ToDictionary(i => i.Key, i => i.First());
        }

        public TType Find(string name) => _instances.ContainsKey(name)
            ? _instances[name]
            : throw new ArgumentOutOfRangeException($"{this.GetType().FullName} didn't found instanse for {name}");
    }
}