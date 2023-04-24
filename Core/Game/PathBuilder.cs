using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Game
{
    internal class PathBuilder : IPathBuilder
    {
        private class Node
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Cost { get; set; }
            public int F { get; set; }
            public Node Parent { get; set; }

            public Node(int x, int y, int cost, int f, Node parent)
            {
                X = x;
                Y = y;
                Cost = cost;
                F = f;
                Parent = parent;
            }
        }

        public MapCell[] FindPath(MapCell start, MapCell end, INeighboursAccessor neighboursAccessor)
        {
            var openList = new List<Node>();
            var closedList = new List<Node>();

            var startNode = new Node(start.X, start.Y, 0, 0, null);

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                var currentNode = openList.OrderBy(n => n.F).First();
                if (currentNode.X == end.X && currentNode.Y == end.Y)
                    return CreatePath(currentNode);

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach(var neighbour in neighboursAccessor.GetNeighboursOf(new MapCell(currentNode.X, currentNode.Y)))
                {
                    //todo: добавить суловие, при котором поле заблокировано для хотьбы
                    //if (по текущему полю нельзя ходить)
                    //    continue;

                    var newCost = currentNode.Cost + 1;
                    var nextNode = closedList.FirstOrDefault(n => n.X == neighbour.X && n.Y == neighbour.Y);
                    if (nextNode != null && newCost >= nextNode.Cost)
                        continue;

                    nextNode = openList.FirstOrDefault(n => n.X == neighbour.X && n.Y == neighbour.Y);
                    if (nextNode == null || newCost < nextNode.Cost)
                    {
                        var h = Math.Abs(neighbour.X - end.X) + Math.Abs(neighbour.Y - end.Y);
                        var f = newCost + h;

                        if (nextNode == null)
                        {
                            nextNode = new Node(neighbour.X, neighbour.Y, newCost, f, currentNode);
                            openList.Add(nextNode);
                        }
                        else
                        {
                            nextNode.Cost = newCost;
                            nextNode.F = f;
                            nextNode.Parent = currentNode;
                        }
                    }
                }
            }
            return Array.Empty<MapCell>();
        }

        private MapCell[] CreatePath(Node node)
        {
            var path = new List<Node>();
            while (node != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Reverse();
            return path.Select(n => new MapCell(n.X, n.Y)).ToArray();
        }
    }
}