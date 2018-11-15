using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapData
{
    private MapInfo mapInfo;
    private Grid[,] gridList;
    private List<Grid> emptyGrids;

    public int Rows
    {
        get { return mapInfo.xRange; }
    }
    public int Columns
    {
        get { return mapInfo.yRange; }
    }

    public MapData(MapInfo mapInfo)
    {
        this.mapInfo = mapInfo;
        InitMapData();
    }

    void InitGridList()
    {
        gridList = new Grid[mapInfo.xRange, mapInfo.yRange];
        int count = 0;
        for (int i = 0; i < mapInfo.xRange; i++)
        {
            for (int j = 0; j < mapInfo.yRange; j++)
            {
                gridList[i, j] = new Grid(i, j);
                if (mapInfo.cellList[count] == 0)
                    emptyGrids.Add(gridList[i, j]);
                else
                    gridList[i, j].type = mapInfo.cellList[count];
                count++;
            }
        }
    }

    void AddNpcs(List<int> npcList)
    {
        for (int i = 0; i < npcList.Count; i++)
        {
            int index = Algorithms.GetIndexByRange(0, emptyGrids.Count);
            emptyGrids[index].type = npcList[i];
            emptyGrids.RemoveAt(index);
        }
    }

    void InitMapData()
    {
        InitGridList();

        //ToDo GetNpcList
        List<int> npcList = new List<int>();
        npcList.Add(1001);
        npcList.Add(1002);

        AddNpcs(npcList);
    }


    private ArrayList openList;
    private ArrayList closeList;
    private List<Grid> path;
    private string road;
    /// <summary>
    /// 简单的A星寻路算法,不走对角线
    /// </summary>
    public List<Grid> FindPath(int startX, int startY, int endX, int endY)
    {
        Debug.Log("A* Started! Looking" + endX + "," + endY + "-->" + startX + "," + startY);
        path = new List<Grid>();

        if (!gridList[startX, startY].isOpen)
            return path;

        road = "";
        openList = new ArrayList();
        closeList = new ArrayList();
        ResetGridState();

        openList.Add(gridList[startX, startY]);
        Grid current = openList[0] as Grid;

        while (openList.Count > 0 && (startX != endX || startY != endY))
        {
            current = openList[0] as Grid;
            if (current.x == endX && current.y == endY)
            {
                Debug.Log("Path Found!");
                GenerateRoad(current);
                Debug.Log(road);
                return path;
            }
            foreach (Grid _grid in GridNeighbour(current))
            {
                if (_grid.IsWalkable() && !closeList.Contains(_grid))
                {

                    int g = current.g + 1;
                    if (_grid.g == 0 || _grid.g > g)
                    {
                        _grid.g = g;
                        _grid.parent = current;
                    }

                    _grid.h = Mathf.Abs(endX - _grid.x) + Mathf.Abs(endY - _grid.y);
                    _grid.f = _grid.g + _grid.h;
                    if (!openList.Contains(_grid))
                        openList.Add(_grid);

                    //根据f值进行升序排序
                    openList.Sort();
                }
            }
            closeList.Add(current);
            openList.Remove(current);

            if (openList.Count == 0)
                Debug.Log("UnReachable Point!");
        }
        return path;
    }

    void GenerateRoad(Grid g)
    {
        road += "(" + g.x + "," + g.y + ")";
        path.Add(g);
        if (g.parent != null)
            GenerateRoad(g.parent);
    }

    void ResetGridState()
    {
        for (int i = 0; i < mapInfo.xRange; i++)
        {
            for (int j = 0; j < mapInfo.yRange; j++)
            {
                gridList[i, j].parent = null;
                gridList[i, j].g = 0;
                gridList[i, j].f = 0;
                gridList[i, j].h = 0;
            }
        }
    }

    public List<Grid> GridNeighbour(Grid org)
    {
        List<Grid> neighbour = new List<Grid>();
        if (org.x != 0)
            neighbour.Add(gridList[org.x - 1, org.y]);
        if (org.y != 0)
            neighbour.Add(gridList[org.x, org.y - 1]);
        if (org.x != mapInfo.yRange - 1)
            neighbour.Add(gridList[org.x + 1, org.y]);
        if (org.y != mapInfo.xRange - 1)
            neighbour.Add(gridList[org.x, org.y + 1]);
        return neighbour;
    }

    List<Grid> UnpickedGridNeighbour(Grid org)
    {
        List<Grid> neighbour = new List<Grid>();
        if (org.x != 0 && !gridList[org.x - 1, org.y].isPicked)
        {
            neighbour.Add(gridList[org.x - 1, org.y]);
        }
        if (org.y != 0 && !gridList[org.x, org.y - 1].isPicked)
        {
            neighbour.Add(gridList[org.x, org.y - 1]);
        }
        if (org.x != mapInfo.yRange - 1 && !gridList[org.x + 1, org.y].isPicked)
        {
            neighbour.Add(gridList[org.x + 1, org.y]);
        }
        if (org.y != mapInfo.xRange - 1 && !gridList[org.x, org.y + 1].isPicked)
        {
            neighbour.Add(gridList[org.x, org.y + 1]);
        }
        return neighbour;
    }

}
