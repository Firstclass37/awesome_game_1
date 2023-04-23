using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Game
{
    internal class PathBuilder
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

        public Coordiante[] FindPath(int[,] map, Coordiante start, Coordiante end)
        {
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };
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

                for (int i = 0; i < dx.Length; i++)
                {
                    var nextX = currentNode.X + dx[i];
                    var nextY = currentNode.Y + dy[i];

                    if (nextX < 0 || nextY < 0 || nextX >= map.GetLength(0) || nextY >= map.GetLength(1))
                        continue;

                    if (map[nextX, nextY] == 1)
                        continue;

                    var newCost = currentNode.Cost + 1;
                    var nextNode = closedList.FirstOrDefault(n => n.X == nextX && n.Y == nextY);
                    if (nextNode != null && newCost >= nextNode.Cost)
                        continue;

                    nextNode = openList.FirstOrDefault(n => n.X == nextX && n.Y == nextY);
                    if (nextNode == null || newCost < nextNode.Cost)
                    {
                        var h = Math.Abs(nextX - end.X) + Math.Abs(nextY - end.Y);
                        var f = newCost + h;

                        if (nextNode == null)
                        {
                            nextNode = new Node(nextX, nextY, newCost, f, currentNode);
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
            return Array.Empty<Coordiante>();
        }

        private Coordiante[] CreatePath(Node node)
        {
            var path = new List<Node>();
            while (node != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Reverse();
            return path.Select(n => new Coordiante(n.X, n.Y)).ToArray();
        }
    }
}