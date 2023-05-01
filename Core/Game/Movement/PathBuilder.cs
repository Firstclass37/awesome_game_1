﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Game.Movement
{
    internal class PathBuilder : IPathBuilder
    {
        private class Node
        {
            public MapCell Cell;
            public int Cost { get; set; }
            public int F { get; set; }
            public Node Parent { get; set; }

            public Node(MapCell cell, int cost, int f, Node parent)
            {
                Cell = cell;
                Cost = cost;
                F = f;
                Parent = parent;
            }
        }

        public MapCell[] FindPath(MapCell start, MapCell end, INeighboursSelector neighboursSelector)
        {
            var openList = new List<Node>();
            var closedList = new List<Node>();

            var startNode = new Node(start, 0, 0, null);

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                var currentNode = openList.OrderBy(n => n.F).First();
                if (currentNode.Cell == end)
                    return CreatePath(currentNode);

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (var neighbour in neighboursSelector.GetNeighboursOf(currentNode.Cell))
                {
                    //todo: добавить суловие, при котором поле заблокировано для хотьбы
                    //if (по текущему полю нельзя ходить)
                    //    continue;

                    var newCost = currentNode.Cost + 1;
                    var nextNode = closedList.FirstOrDefault(n => n.Cell == neighbour);
                    if (nextNode != null && newCost >= nextNode.Cost)
                        continue;

                    nextNode = openList.FirstOrDefault(n => n.Cell == neighbour);
                    if (nextNode == null || newCost < nextNode.Cost)
                    {
                        var h = Math.Abs(neighbour.X - end.X) + Math.Abs(neighbour.Y - end.Y);
                        var f = newCost + h;

                        if (nextNode == null)
                        {
                            nextNode = new Node(neighbour, newCost, f, currentNode);
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
            return path.Select(n => n.Cell).Skip(1).ToArray();
        }
    }
}