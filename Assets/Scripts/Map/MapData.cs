using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapData
{
    public int Id;
    public int Rows;
    public int Columns;
    public int LandType;
    public Grid[,] gridList;
    public Grid startGrid;

    private int[] OriginList;
    private List<Grid> emptyGrids;
    private int BlockCount;
    private int[] LandForms;

    public Dictionary<int, Npc> npcs = new Dictionary<int, Npc>();
    public Dictionary<int, MapTreasure> treasures = new Dictionary<int, MapTreasure>();
    public Dictionary<int, MapPortal> portals = new Dictionary<int, MapPortal>();
    public Dictionary<int, MapPickableItem> pickableItems = new Dictionary<int, MapPickableItem>();
    public Dictionary<int, MapEvent> events = new Dictionary<int, MapEvent>();

    public MapData(MapInfo mapInfo)
    {
        this.Id = mapInfo.Id;
        Rows = mapInfo.xRange ;
        Columns = mapInfo.yRange ;
        LandType = mapInfo.Type;
        BlockCount = mapInfo.BlockCount;
        OriginList = mapInfo.DegisnList;
        LandForms = LoadTxt.Instance.GetLandForms(LandType);
        InitMapData();
    }

    public Grid GetGrid(int x, int y)
    {
        return gridList[x, y];
    }

    private List<Grid> pickedUnset;
    void InitMapData()
    {
        Debug.Log("Initing Map, Rows = " + Rows + ",Columnns = " + Columns + ",Blocks = " + BlockCount);

        Stack gridStack = new Stack(); 
        Grid thisGrid;
        Grid nextGrid;
        List<Grid> neighbours;
        pickedUnset = new List<Grid>();

        InitGridList();

        //UnityEngine.Random.InitState((int)Time.time);

        //1. 确定起始点
        int x = Random.Range(0, Rows);
        int y = Random.Range(0, Columns);
        thisGrid = gridList[x, y];
        thisGrid.isPicked = true;
        startGrid = thisGrid;
        pickedUnset.Add(thisGrid); 
        gridStack.Push(thisGrid);

        Debug.Log("Setting first point = (" + x + "," + y + ")");

        //2. 逐步生成
        for (int i =0; i < Rows * Columns - BlockCount; i++)
        {
            neighbours = new List<Grid>();
            do
            {
                thisGrid = gridStack.Pop() as Grid;
                neighbours = UnpickedGridNeighbour(thisGrid);
            } while (neighbours.Count <= 0);
            nextGrid = RandomGrid(neighbours);
            gridStack.Push(thisGrid);
            gridStack.Push(nextGrid);
            nextGrid.isPicked = true;
            pickedUnset.Add(nextGrid);
        }

        //3. 寻找端点
        //List<Grid> mapEnds = GetMapEnds(pickedUnset);

        //4. 添加固定出口
        SetExits();


        //5. 添加NPC
        Npc npc = LoadTxt.Instance.ReadNpc(10000);
        Debug.Log("Npc Image = " + npc.Image);
        npcs.Add(1, npc);
        npcs.Add(2, npc);
        npcs.Add(3, npc);
        npcs.Add(4, npc);
        npcs.Add(5, npc);
        npcs.Add(6, npc);
        npcs.Add(7, npc);
        npcs.Add(8, npc);
        npcs.Add(9, npc);
        npcs.Add(10, npc);
        npcs.Add(11, npc);
        npcs.Add(12, npc);
        npcs.Add(13, npc);
        npcs.Add(14, npc);
        npcs.Add(15, npc);
        npcs.Add(16, npc);
        foreach(int key in npcs.Keys){
            AddInteractiveItem(2, key);
        }

        //6. 添加宝箱
        MapTreasure treasure = new MapTreasure();
        treasures.Add(1, treasure);
        treasures.Add(2, treasure);
        treasures.Add(3, treasure);
        treasures.Add(4, treasure);
        treasures.Add(5, treasure);
        foreach(int key in treasures.Keys){
            AddInteractiveItem(3, key);
        }

        //7. 添加随机传送
        MapPortal portal = new MapPortal();
        portals.Add(1, portal);
        portals.Add(2, portal);
        portals.Add(3, portal);
        foreach(int key in portals.Keys){
            AddInteractiveItem(4, key);
        }

        //8. 添加可拾取物品
        MapPickableItem pickableItem = new MapPickableItem();
        pickableItems.Add(1, pickableItem);
        pickableItems.Add(2, pickableItem);
        pickableItems.Add(3, pickableItem);
        foreach(int key in pickableItems.Keys){
            AddInteractiveItem(5, key);
        }

        pickedUnset = new List<Grid>();

        //9. 添加事件

        //10. 处理阻挡物的类型
        SetUnpickedGrids();

    }

    void AddInteractiveItem(int type,int param){
        int r = Random.Range(0, pickedUnset.Count);
        pickedUnset[r].type = type;
        pickedUnset[r].param = param;
        pickedUnset.RemoveAt(r);
    }

    void InitGridList()
    {
        gridList = new Grid[Rows, Columns];
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                gridList[i, j] = new Grid(i, j);
            }
        }
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
        if (org.x != Columns - 1 && !gridList[org.x + 1, org.y].isPicked)
        {
            neighbour.Add(gridList[org.x + 1, org.y]);
        }
        if (org.y != Rows - 1 && !gridList[org.x, org.y + 1].isPicked)
        {
            neighbour.Add(gridList[org.x, org.y + 1]);
        }
        return neighbour;
    }

    Grid RandomGrid(List<Grid> gridPool)
    {
        int r = Random.Range(0, gridPool.Count);
        return gridPool[r];
    }

    void SetExits()
    {
        //Debug.Log("Finding Exits...");
        Grid west = null;
        Grid east = null;
        Grid north = null;
        Grid south = null;
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (!gridList[i, j].isPicked)
                    continue;

                if (west == null)
                    west = gridList[i, j];
                else if (j < west.y)
                    west = gridList[i, j];

                if (east == null)
                    east = gridList[i, j];
                else if (j > east.y)
                    east = gridList[i, j];

                if (i != west.x && i != east.x)
                {
                    if (south == null)
                        south = gridList[i, j];
                    else if (i < south.x)
                        south = gridList[i, j];

                    if (north == null)
                        north = gridList[i, j];
                    else if (i > north.x)
                        north = gridList[i, j];
                }

            }
        }
        DebugExists("east", east);
        DebugExists("south", south);
        DebugExists("west", west);
        DebugExists("north", north);

        east.type = 11;
        south.type = 12;
        west.type = 13;
        north.type = 14;

        pickedUnset.Remove(east);
        pickedUnset.Remove(south);
        pickedUnset.Remove(west);
        pickedUnset.Remove(north);
    }

    void DebugExists(string str,Grid grid){
        if (grid != null)
            Debug.Log(str + " exit position = " + grid.x + "," + grid.y);
        else
            Debug.Log(str + " exit can not find!");
    }




    void SetUnpickedGrids()
    {
        Debug.Log("Setting Blocks...");
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (gridList[i, j].isPicked)
                    continue;
                int neighbourForm = GetNeighbourForm(gridList[i, j]);

                //新障碍分三种情况：跟上1个障碍一样、为空、重新生成1个障碍
                int r = Random.Range(0, 100);
                if (r <= 33)
                {
                    gridList[i, j].type = 1;
                    gridList[i, j].param = neighbourForm;
                }
                else if (r <= 66)
                    gridList[i, j].type = 0;
                else
                    gridList[i, j].type = 1;
                       gridList[i,j].param = GetNewLandForm();
            }
        }
    }

    /// <summary>
    /// 找相邻位置的障碍物类型，只看左和下的就行，右和上的还轮不到
    /// </summary>
    /// <returns>The neighbour forms.</returns>
    /// <param name="org">Org.</param>
    int GetNeighbourForm(Grid org)
    {

        if (org.x != 0)
        {
            Grid left = gridList[org.x - 1, org.y];
            if (!left.isPicked && left.type == 1)
                return left.param;
        }

        if (org.y != 0)
        {
            Grid under = gridList[org.x, org.y - 1];
            if (!under.isPicked && under.type == 1)
                return under.param;
        }

        return 0;
    }

    int GetNewLandForm()
    {
        return Algorithms.GetResultByWeight(LandForms);
    }


    public List<Grid> OpenNeighbours(List<Grid> grids){
        List<Grid> newOpenGrids = new List<Grid>();
        for (int i = 0; i < grids.Count;i++){
            List<Grid> neighbours = GridNeighbour(grids[i]);
            for (int j = 0; j < neighbours.Count; j++)
            {
                if (!neighbours[j].isOpen && !newOpenGrids.Contains(neighbours[j]))
                {
                    newOpenGrids.Add(neighbours[j]);
                }
            }
        }
        return newOpenGrids;
    }


    #region 寻路
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
            {
                Debug.Log("UnReachable Point!");
                return null;
            }
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
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
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
        if (org.x != Columns - 1)
            neighbour.Add(gridList[org.x + 1, org.y]);
        if (org.y != Rows - 1)
            neighbour.Add(gridList[org.x, org.y + 1]);
        return neighbour;
    }

#endregion

}
