using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldMap : MonoBehaviour
{
    private static WorldMap _instance;
    public static WorldMap Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("WorldMap").GetComponent<WorldMap>();
            }
            return _instance;
        }
    }

    //x: 0-999 y:0-999
    private Vector2Int playerMapPos; //小地图当前经纬度
    private Vector2Int playerBigMapIndex //大地图当前序号
    {
        get { return new Vector2Int((int)playerMapPos.x / 100, (int)playerMapPos.y / 100); }
    } 
    private Vector2Int playerMinMapIndex //小地图当前序号
    {
        get{
            return new Vector2Int(playerMapPos.x % 100, playerMapPos.y % 100);
        }
    }

    private Image posMark;
    private void Start()
    {
        posMark = GetComponentInChildren<Image>();
    }

    bool isShowMax = false;
    void UpdateShow(){
        if(isShowMax){
            ShowMaxMap();
        }else{
            ShowMinMap();
        }
    }

    void ShowMinMap()
    {
        ShowMinMap(playerMinMapIndex);
    }

    void ShowMinMap(Vector2Int index){
        Debug.Log("Showing MinMap...");
    }

    void ShowMaxMap(){
        Debug.Log("Showing BigMap...");
    }

}
