using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    private GameObject _mapCell;
    private Transform _parent;
    //生成全部地图
    //确定站立位置，移动地图
    public void Display(MapInfo map,int lastMap){
        _mapCell = Resources.Load("Prefabs/MapCell") as GameObject;
        _parent = this.transform;
        int count = 0;
        for (int i = 0; i < map.xRange;i++){
            for (int j = 0; j < map.yRange;j++){
                DisplayMapUnit(map.cellList[count], map.cellState[count], i, j);
                count++;
            }
        }
    }

    void DisplayMapUnit(int unitType,int unitState,int x,int y){
        Sprite sprite = Resources.Load("MapUnits/" + unitType,typeof(Sprite)) as Sprite;
        GameObject unit = Instantiate(_mapCell) as GameObject;

        unit.transform.SetParent(_parent);
        unit.transform.localScale = Vector2.one;
        unit.GetComponent<Image>().sprite = sprite;
        unit.transform.localPosition = new Vector2(100 * x, 100 * y);
    }
}
