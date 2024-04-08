using System.Collections.Generic;
using System.Windows;

private List<Point> AStar(Point start, Point goal)
{
    var openList = new List<Point>();
    var closedList = new List<Point>();
    var cameFrom = new Dictionary<Point, Point>();
    var gScore = new Dictionary<Point, double>();
    var fScore = new Dictionary<Point, double>();

    openList.Add(start);
    gScore[start] = 0;
    fScore[start] = Heuristic(start, goal);

    while (openList.Any())
    {
        var current = openList.OrderBy(p => fScore.ContainsKey(p) ? fScore[p] : double.MaxValue).First();

        if (current == goal)
        {
            return ReconstructPath(cameFrom, current);
        }

        openList.Remove(current);
        closedList.Add(current);

        foreach (var neighbor in GetNeighbors(current))
        {
            if (closedList.Contains(neighbor))
            {
                continue;
            }

            var tentativeGScore = (gScore.ContainsKey(current) ? gScore[current] : double.MaxValue) + 1;

            if (!openList.Contains(neighbor) || tentativeGScore < (gScore.ContainsKey(neighbor) ? gScore[neighbor] : double.MaxValue))
            {
                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = tentativeGScore + Heuristic(neighbor, goal);

                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
            }
        }
    }

    return null; // No path found 
}
