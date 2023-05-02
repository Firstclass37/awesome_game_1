using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Game.Movement.Path
{
    internal class PathSearcher: IPathSearcher
    {
        public T[] Search<T>(T start, T end, PathSearcherSetting<T> setting) 
        {
            var open = new List<T> { start };
            var pathMap = new Dictionary<T, T>();

            var gScore = new Dictionary<T, double> { { start, 0 } };
            var fScore = new Dictionary<T, double> { { start, setting.FScoreStrategy.Get(start, end, 0) } };

            while (open.Count > 0)
            {
                var current = SelectMin(fScore, open);
                if (current.Equals(end))
                    return BuildPath(pathMap, end);

                open.Remove(current);

                var neighbors = setting.NeighborsSearchStrategy.Search(current);
                for (var i = 0; i < neighbors.Length; i++)
                {
                    var neighbor = neighbors[i];
                    var currentGScore = gScore[current] + setting.GScoreStrategy.Get(current, neighbor);
                    if (!gScore.ContainsKey(neighbor) || currentGScore < gScore[neighbor])
                    {
                        pathMap.Remove(neighbor);
                        pathMap.Add(neighbor, current);

                        gScore.Remove(neighbor);
                        gScore.Add(neighbor, currentGScore);

                        fScore.Remove(neighbor);
                        fScore.Add(neighbor, setting.FScoreStrategy.Get(neighbor, end, currentGScore));
                        if (!open.Contains(neighbor))
                            open.Add(neighbor);
                    }
                }
            }
            return Array.Empty<T>();
        }

        private T[] BuildPath<T>(Dictionary<T, T> pathMap, T end)
        {
            var path = new List<T> { end };
            var keys = pathMap.Keys.Reverse().ToArray(); 

            var last = end;
            for (var i = 0; i < keys.Length; i++)
            {
                var currKey = keys[i];
                var currVal = pathMap[currKey];

                if (last.Equals(currKey))
                {
                    last = currVal;
                    path.Add(currVal);
                }
            }
            return path.ToArray().Reverse().ToArray();
        }

        private T SelectMin<T>(Dictionary<T, double> fScores, List<T> open) => 
             open.Count > 0 ? open.OrderBy(k => fScores[k]).First() : default;
    }
}