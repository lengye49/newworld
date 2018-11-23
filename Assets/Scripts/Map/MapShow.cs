using UnityEngine;
using System.Collections.Generic;
public class MapShow : MonoBehaviour
{
    private GameObject _mapCell;
    private float cellSize = 0.64f;

    public void Display(Grid[,] gridList, int rowsCount, int columnsCount,int groundType)
    {
        _mapCell = Resources.Load("Prefabs/MapUnit") as GameObject;
        int count = 0;
        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnsCount; j++)
            {
                DisplayMapUnit(gridList[i, j].type, i, j,groundType);
                count++;
            }
        }
    }

    //需要优化逻辑
    void DisplayMapUnit(int unitType, int x, int y,int groundType)
    {
        Sprite sprite = Resources.Load("MapUnits/" + groundType, typeof(Sprite)) as Sprite;
        Sprite item = Resources.Load("MapUnits/" + unitType, typeof(Sprite)) as Sprite;
        GameObject unit = Instantiate(_mapCell) as GameObject;

        unit.gameObject.name = x + "," + y;
        unit.transform.SetParent(transform);
        unit.transform.localScale = Vector2.one;
        unit.GetComponent<SpriteRenderer>().sprite = sprite;
        unit.GetComponentsInChildren<SpriteRenderer>()[1].sprite = item;
        unit.transform.localPosition = new Vector2(cellSize * x, cellSize * y);
    }

    public Vector2 GetPos(Grid grid){
        return new Vector2(cellSize * grid.x, cellSize * grid.y);
    }

    public List<Vector2> GetPathPos(List<Grid> path){
        List<Vector2> p = new List<Vector2>();
        foreach(Grid g in path){
            p.Add(GetPos(g));
        }
        //Debug.Log(p);
        return p;
    }

}
