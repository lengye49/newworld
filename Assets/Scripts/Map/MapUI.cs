//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class MapUI : MonoBehaviour
//{
//    private GameObject _mapCell;
//    private Transform _parent;
//    //生成全部地图
//    //确定站立位置，移动地图
//    public void Display(Grid[,] gridList,int x,int y){
//        _mapCell = Resources.Load("Prefabs/MapCell") as GameObject;
//        _parent = this.transform;
//        int count = 0;
//        for (int i = 0; i < x;i++){
//            for (int j = 0; j < y;j++){
//                DisplayMapUnit(gridList[i,j].type, i, j);
//                count++;
//            }
//        }
//    }

//    void DisplayMapUnit(int unitType,int x,int y){
//        Sprite sprite = Resources.Load("MapUnits/" + unitType,typeof(Sprite)) as Sprite;
//        GameObject unit = Instantiate(_mapCell) as GameObject;

//        unit.transform.SetParent(_parent);
//        unit.transform.localScale = Vector2.one;
//        unit.GetComponent<Image>().sprite = sprite;
//        unit.transform.localPosition = new Vector2(100 * x, 100 * y);
//    }
    
//}
