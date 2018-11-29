using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapDataOld
{
    private MapInfoOld mapInfo;
    public Grid[,] gridList;
    private List<Grid> emptyGrids;

    public int Rows
    {
        get { return mapInfo.xRange; }
    }
    public int Columns
    {
        get { return mapInfo.yRange; }
    }

    public MapDataOld(MapInfoOld mapInfo)
    {
        this.mapInfo = mapInfo;
        InitMapData();
    }

    public Grid GetGrid(int x,int y){
        return gridList[x, y];
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

    void AddNpcs(List<int> npcList)
    {
        for (int i = 0; i < npcList.Count; i++)
        {
            int index = Algorithms.GetIndexByRange(0, emptyGrids.Count);
            emptyGrids[index].type = npcList[i];
            emptyGrids.RemoveAt(index);
        }
    }

    void InitGridList()
    {
        gridList = new Grid[mapInfo.xRange, mapInfo.yRange];
        emptyGrids = new List<Grid>();
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


    private ArrayList openList;
    private ArrayList closeList;
    private List<Grid> path;
    private string road;
    /// <summary>
    /// 简单的A星寻路算法,不走对角线
    /// </summary>
    public List<Grid> FindPath(Grid endGrid, Grid startGrid)
    {
        Debug.Log("A* Started! Looking" + endGrid.x + "," + endGrid.y + "-->" + startGrid.x + "," + startGrid.y);
        path = new List<Grid>();

        if (!gridList[startGrid.x, startGrid.y].isOpen)
            return path;

        road = "";
        openList = new ArrayList();
        closeList = new ArrayList();
        ResetGridState();

        openList.Add(gridList[startGrid.x, startGrid.y]);
        Grid current = openList[0] as Grid;

        while (openList.Count > 0 && (startGrid.x != endGrid.x || startGrid.y != endGrid.y))
        {
            current = openList[0] as Grid;
            if (current.x == endGrid.x && current.y == endGrid.y)
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

                    _grid.h = Mathf.Abs(endGrid.x - _grid.x) + Mathf.Abs(endGrid.y - _grid.y);
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

    List<Grid> GridNeighbour(Grid org)
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
