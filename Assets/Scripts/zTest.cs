using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class zTest : MonoBehaviour {

    void Start()
    {
        //TestImage();
        TestMap();
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
        MapInfo m = LoadTxt.Instance.ReadMap(1);
        //this.GetComponent<MapDisplay>().Display(m,2);
    }


    void TestInventory()
    {
        Debug.Log("Inventory Test************Start");
        Knapscak.Instance.StoreItem(1);
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
