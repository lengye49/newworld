using UnityEngine;
using System.Collections.Generic;
public class MapShow : MonoBehaviour
{
    private GameObject _mapCell;
    private float cellSize = 0.64f;
    private float offsetX;
    private float offsetY;
    Sprite landSprite;

    public void Display(Grid[,] gridList, int rowsCount, int columnsCount,int landType)
    {
        offsetX = -0.32f * columnsCount;
        offsetY = -0.32f * rowsCount;
        landSprite = Resources.Load("MapUnits/" + landType, typeof(Sprite)) as Sprite;
        _mapCell = Resources.Load("Prefabs/MapUnit") as GameObject;
        int count = 0;
        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnsCount; j++)
            {
                DisplayMapUnit(gridList[i, j].type, i, j);
                count++;
            }
        }
    }

    //需要优化逻辑
    void DisplayMapUnit(int unitType, int x, int y)
    {
        GameObject unit = Instantiate(_mapCell) as GameObject;

        unit.gameObject.name = x + "," + y;
        unit.transform.SetParent(transform);
        unit.transform.localScale = Vector2.one;
        unit.GetComponent<SpriteRenderer>().sprite = landSprite;
        unit.transform.localPosition = new Vector2(cellSize * x + offsetX, cellSize * y + offsetY);

        if (unitType > 0)
        {
            Sprite formSprite = Resources.Load("LandForms/" + unitType, typeof(Sprite)) as Sprite;
            unit.GetComponentsInChildren<SpriteRenderer>()[1].sprite = formSprite;
        }

    }

    public Vector2 GetPos(Grid grid){
        return new Vector2(cellSize * grid.x+offsetX, cellSize * grid.y+offsetY);
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
