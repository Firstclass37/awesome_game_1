using AutoFixture;
using Game.Server.Logic.Maps;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Storage;
using NSubstitute;
using NUnit.Framework;

namespace Game.Server.Tests.Logic.Maps
{
    internal class MatrixGridTests
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void GetCoordinateFor_ForRootCell_ZeroCoordinate()
        {
            var storage = Substitute.For<IStorage>();
            var sut = new MatrixGrid(storage);
            var neigbour1 = _fixture.Build<IsometricMapCell>().With(c => c.Neighbors, new Dictionary<IsometricMapCell, Direction>()).Create();
            var neigbour2 = _fixture.Build<IsometricMapCell>().With(c => c.Neighbors, new Dictionary<IsometricMapCell, Direction>()).Create();
            var neigbour3 = _fixture.Build<IsometricMapCell>().With(c => c.Neighbors, new Dictionary<IsometricMapCell, Direction>()).Create();
            var neigbour4 = _fixture.Build<IsometricMapCell>().With(c => c.Neighbors, new Dictionary<IsometricMapCell, Direction>()).Create();
            var cell = _fixture.Build<IsometricMapCell>().With(n => n.Neighbors, new Dictionary<IsometricMapCell, Direction> 
            {
                { neigbour1, Direction.Top },
                { neigbour2, Direction.Bottom },
                { neigbour3, Direction.Left },
                { neigbour4, Direction.Right }
            }).Create();
            storage.Find<IsometricMapCell>(Arg.Any<Func<IsometricMapCell, bool>>()).Returns(new IsometricMapCell[] { cell });

            var index = sut.GetCoordinateFor(cell.Coordiante);

            Assert.AreEqual(new Coordiante(0, 0), index);
        }

        [TestCase(Direction.Left, -1, 0)]
        [TestCase(Direction.Right, 1, 0)]
        [TestCase(Direction.Top, 0, 1)]
        [TestCase(Direction.Bottom, 0, -1)]
        public void GetCoordinateFor_NeigbourOfRoot_RightCoordinate(Direction direction, int x, int y)
        {
            var storage = Substitute.For<IStorage>();
            var sut = new MatrixGrid(storage);
            var neigbour1 = _fixture.Build<IsometricMapCell>().With(c => c.Neighbors, new Dictionary<IsometricMapCell, Direction>()).Create();
            var neigbour2 = _fixture.Build<IsometricMapCell>().With(c => c.Neighbors, new Dictionary<IsometricMapCell, Direction>()).Create();
            var neigbour3 = _fixture.Build<IsometricMapCell>().With(c => c.Neighbors, new Dictionary<IsometricMapCell, Direction>()).Create();
            var neigbour4 = _fixture.Build<IsometricMapCell>().With(c => c.Neighbors, new Dictionary<IsometricMapCell, Direction>()).Create();
            var cell = _fixture.Build<IsometricMapCell>().With(n => n.Neighbors, new Dictionary<IsometricMapCell, Direction>
            {
                { neigbour1, Direction.Top },
                { neigbour2, Direction.Bottom },
                { neigbour3, Direction.Left },
                { neigbour4, Direction.Right }
            }).Create();
            storage.Find<IsometricMapCell>(Arg.Any<Func<IsometricMapCell, bool>>()).Returns(new IsometricMapCell[] { cell });

            var neigbour = cell.Neighbors.First(n => n.Value == direction).Key.Coordiante;
            var index = sut.GetCoordinateFor(neigbour);

            Assert.AreEqual(new Coordiante(x, y), index);
        }
    }
}