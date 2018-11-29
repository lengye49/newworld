using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapDataNew
{
    public int Id;
    public int Rows;
    public int Columns;
    private int LandType;
    private int BlockCount;
    public Grid[,] gridList;

    private int[] OriginList;
    private List<Grid> emptyGrids;


    public MapDataNew(MapInfoNew mapInfo)
    {
        this.Id = mapInfo.id;
        Rows = mapInfo.xRange;
        Columns = mapInfo.yRange;
        LandType = mapInfo.type;
        BlockCount = mapInfo.blockCount;
        OriginList = mapInfo.cellList;
        InitMapData();
    }

    public Grid GetGrid(int x,int y){
        return gridList[x, y];
    }

    //void InitLandForms(){
    //    int iSet = 0;
    //    int jSet = 0;
    //    int maxPieceCount = Mathf.Max(100, Rows * Columns / 20);
    //    for (int i = 0; i < Rows;i++){
    //        for (int j = 0; j < Columns;j++){
    //            if (i < iSet && j < jSet)
    //                continue;

    //            int r = Random.Range(0, 100);
    //            if(r<ConfigData.LandFormsProp[LandType]){
    //                int totalCount = Random.Range(1, maxPieceCount);
    //                int row = Random.Range(totalCount / 4, totalCount * 3 / 4);
    //                row = Mathf.Max(1, row);
    //                int column = totalCount / row;

    //                //todo 简化，将地貌概率列表存为文件读取
    //                int formType = Algorithms.GetResultByWeight(ConfigData.LandFormsList, LandType);
    //                SetLandFormPiece(i, j, row, column,formType);

    //                iSet += row;
    //                jSet += column;
    //            }
    //        }
    //    }
    //}

    //void SetLandFormPiece(int xPos,int yPos,int x,int y,int formType){
    //    for (int i = 0; i < x;i++){
    //        int column = Random.Range(0, y / 10);
    //        column = Mathf.Max(1, column);
    //        int startColumn = Random.Range(0, y - column);
    //        for (int j = startColumn; j < startColumn + column;j++){
    //            gridList[xPos + i, yPos + j].type = formType;
    //        }
    //    }
    //}


    void InitMapData()
    {
        List<Grid> pickedUnset = new List<Grid>();
        Stack gridStack = new Stack(); ;
        Grid thisGrid;
        Grid nextGrid;
        List<Grid> neighbours;

        InitGridList();

        UnityEngine.Random.InitState((int)Time.time);

        //1. 确定起始点
        int x = Random.Range(0, Rows);
        int y = Random.Range(0, Columns);
        thisGrid = gridList[x, y];
        thisGrid.isPicked = true;
        pickedUnset.Add(thisGrid);
        gridStack.Push(thisGrid);

        //2. 逐步生成
        for (int i = 0; i < Rows * Columns - BlockCount;i++){
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

        //4. 寻找出口
        SetExits(pickedUnset);


        //5. 添加NPC
        List<int> npcList = new List<int>();
        npcList.Add(1001);
        npcList.Add(1002);

        //AddNpcs(npcList);

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

    //List<Grid> GetMapEnds(List<Grid> orgs)
    //{
    //    List<Grid> ends = new List<Grid>();
    //    List<Grid> neighbours = new List<Grid>();
    //    int num;
    //    for (int i = 0; i < orgs.Count; i++)
    //    {
    //        num = 0;//用于计数可走的点
    //        neighbours = GridNeighbour(orgs[i]);
    //        for (int j = 0; j < neighbours.Count; j++)
    //        {
    //            if (orgs.Contains(neighbours[j]))
    //                num++;
    //        }
    //        if (num <= 1)
    //            ends.Add(orgs[i]);
    //    }
    //    return ends;
    //}

    void SetExits(List<Grid> pickedUnset){
        Grid west = null;
        Grid east = null;
        Grid north = null;
        Grid south = null;
        for (int i = 0; i < Rows;i++){
            for (int j = 0; j < Columns;j++){
                if (!gridList[i, j].isPicked)
                    continue;

                if(west==null)
                    west = gridList[i, j];
                else if(j < west.y)
                    west = gridList[i, j];
                    
                if (east == null)
                    east = gridList[i, j];
                else if(j>east.y)
                    east= gridList[i, j];

                if(i!=west.x && i!=east.x){
                    if (south == null)
                        south = gridList[i, j];
                    else if (i < south.x)
                        south = gridList[i, j];

                    if (north==null)
                        north= gridList[i, j];
                    else if(i>north.x)
                        north= gridList[i, j];
                }

            }
        }


        east.type = 11;
        south.type = 12;
        west.type = 13;
        north.type = 14;

        pickedUnset.Remove(east);
        pickedUnset.Remove(south);
        pickedUnset.Remove(west);
        pickedUnset.Remove(north);
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
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j <Columns; j++)
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



}
