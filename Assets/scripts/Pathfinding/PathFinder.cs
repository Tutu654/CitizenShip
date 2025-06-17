using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closedList = new List<OverlayTile>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentOverlayTile);
            closedList.Add(currentOverlayTile);

            if (currentOverlayTile == end)
            {
                return GetFinishedList(start, end);
            }

            var neighbourTiles = GetNeighbourTiles(currentOverlayTile);

            foreach (var neighbour in neighbourTiles)
            {
                if(neighbour.isBlocked || closedList.Contains(neighbour))
                {
                    continue;
                }

                neighbour.G = GetManhatanDistance(start, neighbour);
                neighbour.H = GetManhatanDistance(end, neighbour);

                neighbour.previous = currentOverlayTile;

                if (!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }
        }

        return new List<OverlayTile>();
    }

    private List<OverlayTile> GetFinishedList(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> finishedList = new List<OverlayTile>();

        OverlayTile currentTile = end;

        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }

        finishedList.Reverse();

        return finishedList;
    }

    private int GetManhatanDistance(OverlayTile start, OverlayTile neighbour)
    {
        return (int)(MathF.Abs(start.gridLocation.x - neighbour.gridLocation.x) + MathF.Abs(start.gridLocation.y - neighbour.gridLocation.y));
    }

    private List<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTIle)
    {
        var map = MapManager.Instance.map;
        List<OverlayTile> neighbours = new List<OverlayTile>();

        //top
        Vector2Int locationCheck = new Vector2Int(currentOverlayTIle.gridLocation.x, currentOverlayTIle.gridLocation.y + 1);

        if (map.ContainsKey(locationCheck))
        {
            neighbours.Add(map[locationCheck]);
        }

        //bottom
        locationCheck = new Vector2Int(currentOverlayTIle.gridLocation.x, currentOverlayTIle.gridLocation.y - 1);

        if (map.ContainsKey(locationCheck))
        {
            neighbours.Add(map[locationCheck]);
        }

        //right
        locationCheck = new Vector2Int(currentOverlayTIle.gridLocation.x + 1, currentOverlayTIle.gridLocation.y);

        if (map.ContainsKey(locationCheck))
        {
            neighbours.Add(map[locationCheck]);
        }

        //left
        locationCheck = new Vector2Int(currentOverlayTIle.gridLocation.x - 1, currentOverlayTIle.gridLocation.y);

        if (map.ContainsKey(locationCheck))
        {
            neighbours.Add(map[locationCheck]);
        }

        return neighbours;
    }
}
