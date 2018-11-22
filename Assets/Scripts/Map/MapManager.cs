using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    private List<MapData> mapDataList;
    private List<GameObject> containerList;

    private void Start()
    {
        mapDataList = new List<MapData>();
        containerList = new List<GameObject>();
        GenerateNewMap(1);
        GenerateNewMap(1);
    }

    void GenerateNewMap(int mapId){
        Debug.Log("Loading Map...");
        MapInfo m = LoadTxt.Instance.ReadMap(mapId);
        MapData md = new MapData(m);
        mapDataList.Add(md);

        GameObject _container = new GameObject("MapContainer" + mapDataList.Count);
        MapShow ms = _container.AddComponent<MapShow>();
        _container.transform.localPosition = new Vector3(0, 0, -mapDataList.Count);
        _container.transform.localScale = Vector3.one;
        containerList.Add(_container);

        ms.Display(md.gridList, md.Rows, md.Columns, m.groundType);
    }

}
