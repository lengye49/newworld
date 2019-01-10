using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class zTest : MonoBehaviour {


    void Start()
    {
        Debug.Log("ZTest is on " + this.gameObject.name);
        //TestEnterBattle();
    }

    void TestEnterBattle(){
        GameObject g = Resources.Load("Prefabs/EnterBattle") as GameObject;
        GameObject enterBattle = Instantiate(g) as GameObject;
        enterBattle.transform.SetParent(transform);
        enterBattle.GetComponent<BattleEnter>().Show();
    }

    void TestMapDataNew(){
        //MapInfo mi = new MapInfo();
        //mi.Id = 1;
        //mi.BlockCount = 2000;
        //mi.IsDesigned = false;
        //mi.Type = 1;
        //mi.xRange = 100;
        //mi.yRange = 100;
        //MapData md = new MapData(mi);
        //for (int i = 0; i < md.Rows;i++){
        //    for (int j = 0; j < md.Columns;j++){
        //        Debug.Log(md.gridList[i, j].type);
        //    }
        //}
    }

    void TestImage(){
        GameObject cell = Resources.Load("Prefabs/MapCell") as GameObject;
        Sprite sprite = Resources.Load("MapUnits/" + 0, typeof(Sprite)) as Sprite;
        GameObject _mapCell = Instantiate(cell) as GameObject;
        _mapCell.transform.SetParent(this.transform);
        _mapCell.transform.localScale = Vector2.one;
        _mapCell.GetComponent<Image>().sprite = sprite;
        _mapCell.transform.localPosition = Vector2.zero;
    }

    void TestMap(){
        //MapInfo m = LoadTxt.Instance.ReadMapInfo(1);
        //MapData md = new MapDataOld(m);
        
        //GameObject _container = new GameObject();
        //_container.name = "MapContainer";
        //MapShow ms = _container.AddComponent<MapShow>();
        //_container.transform.localPosition = Vector2.zero;
        //_container.transform.localScale = Vector2.one;
        //ms.Display(md.gridList, md.Rows, md.Columns,m.groundType);
    }


    void TestInventory()
    {
        Debug.Log("Inventory Test************Start");
        BeiBao.Instance.StoreItem(1);
        Debug.Log("Inventory Test************END");
    }

    void TestStringBuilder(){
        Debug.Log("StringBuilder Test************Start");
        StringBuilder sb = new StringBuilder();
        sb.Append("Hello,hello,");
        sb.Append("You little thing.");
        Debug.Log(sb);
        Debug.Log("StringBuilder Test************END");
    }


    void Update () {
		
	}
}
