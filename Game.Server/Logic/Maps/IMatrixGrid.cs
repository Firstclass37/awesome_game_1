using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps
{
    internal interface IMatrixGrid
    {
        Coordiante GetCoordinateFor(Coordiante coordiante);
    }

    internal class MatrixGrid : IMatrixGrid
    {
        private readonly IStorage _storage;
        private readonly Lazy<Dictionary<Coordiante, Coordiante>> _mapper;

        public MatrixGrid(IStorage storage)
        {
            _storage = storage;
            _mapper = new Lazy<Dictionary<Coordiante, Coordiante>>(CreateMap);
        }

        public Coordiante GetCoordinateFor(Coordiante coordiante) => _mapper.Value.TryGetValue(coordiante, out var index) ? index : throw new ArgumentOutOfRangeException($"{coordiante} was not found");
   
    
        private Dictionary<Coordiante, Coordiante> CreateMap()
        {
            var cells = _storage.Find<IsometricMapCell>(t => true);
            var container = new Dictionary<Coordiante, Coordiante>();
            var index = new Coordiante(0, 0);
            var firstCell = cells.First();
            container.Add(firstCell.Coordiante, index);
            BuildMapper(firstCell, index, container);
            return container;
        }

        private void BuildMapper(IsometricMapCell isometricCell, Coordiante index, Dictionary<Coordiante, Coordiante> container)
        {
            var actualCell = _storage.Get<IsometricMapCell>(isometricCell.Id);
            var neigboursToCheck = actualCell.Neighbors.Where(n => container.ContainsKey(n.Key.Coordiante) == false).ToArray();
            foreach(var neigbour in neigboursToCheck)
            {
                Coordiante coordiante;
                if (neigbour.Value == Direction.Top)
                    coordiante = new Coordiante(index.X, index.Y +1);
                else if (neigbour.Value == Direction.Bottom)
                    coordiante = new Coordiante(index.X, index.Y - 1);
                else if (neigbour.Value == Direction.Left)
                    coordiante = new Coordiante(index.X - 1, index.Y);
                else if (neigbour.Value == Direction.Right)
                    coordiante = new Coordiante(index.X + 1, index.Y);
                else
                    throw new Exception("WTF");

                container.TryAdd(neigbour.Key.Coordiante, coordiante);
                BuildMapper(neigbour.Key, coordiante, container);
            }
        }
    }
}