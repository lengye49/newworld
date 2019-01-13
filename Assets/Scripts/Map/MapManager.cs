using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Camera.main.GetComponent<MapManager>();
            }
            return _instance;
        }
    }

    private Transform containerPool;
    private List<MapData> mapDataList;
    private List<GameObject> containerList;

    private MapData mapDataNow;
    private MapShow mapShowNow;
    private Grid gridNow;

    private List<Grid> pathList;
    private List<Grid> gridWaitingToOpen;

    private void Start()
    {
        containerPool = GameObject.FindWithTag("MapContainerPool").transform;
        mapDataList = new List<MapData>();
        containerList = new List<GameObject>();
        pathList = new List<Grid>();
        gridWaitingToOpen = new List<Grid>();

        GoToMap(1);
    }

    void GoToMap(int mapId){
        GenerateNewMap(mapId);

        CameraAction.Instance.SetBorders(mapDataNow.Rows, mapDataNow.Columns);

        gridNow = mapDataNow.startGrid;
        pathList.Add(gridNow);
        gridWaitingToOpen = mapDataNow.NewOpenGrids(pathList);
        OpenList();

        InitCharacterPos();
    }

    void GenerateNewMap(int mapId){
        Debug.Log("Loading Map...");
        MapInfo m = LoadTxt.Instance.ReadMapInfo(mapId);
        MapData md = new MapData(m);
        mapDataNow = md;
        mapDataList.Add(md);

        GameObject _container = new GameObject("MapContainer" + mapDataList.Count);
        _container.transform.SetParent(containerPool);

        MapShow ms = _container.AddComponent<MapShow>();
        mapShowNow = ms;
        _container.transform.localPosition = new Vector3(0, 0, -mapDataList.Count);
        _container.transform.localScale = Vector3.one;
        containerList.Add(_container);

        ms.Display(md);
    }

    public void InitCharacterPos(){
        Vector2 pos = mapShowNow.GetPos(gridNow);
        CharacterAction.Instance.SetPosition(pos);
    }

    public void MoveToPoint(string pointStr){
        if(CharacterAction.Instance.IsMoving)
        {
            Debug.Log("Player is still moving...");
            return;
        }

        Debug.Log("MapManager --> MoveTo" + pointStr);
        string[] strs = pointStr.Split(',');
        int x = int.Parse(strs[0]);
        int y = int.Parse(strs[1]);
        Grid grid = mapDataNow.GetGrid(x,y);

        if(!grid.IsWalkable()){
            Debug.Log("Blocked Point!");
            return;
        }

        if(grid==gridNow){
            MoveComplete();
            return;
        }

        List<Grid> pathGrids = mapDataNow.FindPath(gridNow, grid);

        if(pathGrids == null){
            Debug.Log("This point is not reachable!!");
            return;
        }
        pathList = pathGrids;
        List<Vector2> road = mapShowNow.GetPathPos(pathGrids);
        CharacterAction.Instance.MoveToPos(road,MoveComplete);
        gridNow = mapDataNow.GetGrid(x, y);
    }

    void MoveComplete(){
        //Debug.Log("MoveComplete, Requesting Point Action..." + gridNow.type);

        gridWaitingToOpen = mapDataNow.NewOpenGrids(pathList);
        OpenList();

        switch(gridNow.type){
            case 2:
                Debug.Log("Show Npc:");
                WindowManager.Instance.ShowWindow(mapDataNow.npcs[gridNow.param]);
                break;
            case 3:
                Debug.Log("Show Treasure Box:");
                WindowManager.Instance.ShowWindow(mapDataNow.treasures[gridNow.param]);
                break;
            case 4:
                Debug.Log("Show Portal:");
                WindowManager.Instance.ShowWindow(mapDataNow.portals[gridNow.param]);
                break;
            case 5:
                Debug.Log("Show Pickable Item:");
                WindowManager.Instance.ShowWindow(mapDataNow.pickableItems[gridNow.param]);
                break;
            case 6:
                Debug.Log("Show Map Event:");
                WindowManager.Instance.ShowWindow(mapDataNow.events[gridNow.param]);
                break;
            default:
                Debug.Log("Nothing Happened!");
                break;
        }

    }

    void OpenList(){
        for (int i = 0; i < gridWaitingToOpen.Count;i++){
            gridWaitingToOpen[i].isOpen = true;
            mapShowNow.DisplayMapUnit(gridWaitingToOpen[i].x, gridWaitingToOpen[i].y);
        }

        pathList.Clear();
        gridWaitingToOpen.Clear();
    }

}
