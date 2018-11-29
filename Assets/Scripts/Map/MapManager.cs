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

    private List<MapData> mapDataList;
    private List<GameObject> containerList;

    private MapData mapDataNow;
    private MapShow mapShowNow;
    private Grid gridNow;

    private void Start()
    {
        mapDataList = new List<MapData>();
        containerList = new List<GameObject>();
        GenerateNewMap(1);

        gridNow = mapDataNow.GetGrid(0, 0);

    }

    void GenerateNewMap(int mapId){
        Debug.Log("Loading Map...");
        MapInfo m = LoadTxt.Instance.ReadMapInfo(mapId);
        MapData md = new MapData(m);
        mapDataNow = md;
        mapDataList.Add(md);

        GameObject _container = new GameObject("MapContainer" + mapDataList.Count);
        MapShow ms = _container.AddComponent<MapShow>();
        mapShowNow = ms;
        _container.transform.localPosition = new Vector3(0, 0, -mapDataList.Count);
        _container.transform.localScale = Vector3.one;
        containerList.Add(_container);

        ms.Display(md.gridList, md.Rows, md.Columns, md.LandType);
    }

    public void InitCharacterPos(){
        Vector2 pos = mapShowNow.GetPos(gridNow);
        CharacterAction.Instance.SetPosition(pos);
    }

    public void MoveToPoint(string pointStr){
        Debug.Log("MapManager --> MoveTo" + pointStr);
        string[] strs = pointStr.Split(',');
        int x = int.Parse(strs[0]);
        int y = int.Parse(strs[1]);
        Grid grid = mapDataNow.GetGrid(x,y);
        List<Vector2> road = mapShowNow.GetPathPos(mapDataNow.FindPath(gridNow, grid));
        CharacterAction.Instance.MoveToPos(road);
        gridNow = mapDataNow.GetGrid(x, y);
    }

}
