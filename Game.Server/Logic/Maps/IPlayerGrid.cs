using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps
{
    internal interface IPlayerGrid
    {
        IReadOnlyCollection<int> Players { get; }

        IReadOnlyCollection<Coordiante> GetAvailableFor(int playerId);

        int GetPlayerOf(Coordiante coordiante);

        bool IsAvailableFor(IReadOnlyCollection<Coordiante> area, int playerId);
    }

    internal class PlayerGrid: IPlayerGrid
    {
        private readonly Lazy<Dictionary<int, HashSet<Coordiante>>> _playersCoordinates;

        public PlayerGrid(IStorage storage)
        {
            _playersCoordinates = new Lazy<Dictionary<int, HashSet<Coordiante>>>(
                () => storage.Find<PlayerToPosition>(t => true).GroupBy(pp => pp.PlayerNumber).ToDictionary(p => p.Key, p => new HashSet<Coordiante>(p.Select(a => a.Coordinate))));
        }

        public IReadOnlyCollection<int> Players => _playersCoordinates.Value.Keys;

        public IReadOnlyCollection<Coordiante> GetAvailableFor(int playerId) =>
            _playersCoordinates.Value.ContainsKey(playerId) ? _playersCoordinates.Value[playerId] : throw new ArgumentOutOfRangeException($"no info about coordinates for player {playerId}");

        public int GetPlayerOf(Coordiante coordiante) => _playersCoordinates.Value.Single(g => g.Value.Contains(coordiante)).Key;

        public bool IsAvailableFor(IReadOnlyCollection<Coordiante> area, int playerId) =>
            _playersCoordinates.Value.ContainsKey(playerId) 
                ? area.All(p => _playersCoordinates.Value[playerId].Contains(p))
                : throw new ArgumentOutOfRangeException($"no info about coordinates for player {playerId}");
    }
}