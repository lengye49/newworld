using UnityEngine;
using System.Collections.Generic;
public class MapShow : MonoBehaviour
{
    private GameObject _mapCell;
    private float cellSize = 0.64f;
    private float offsetX;
    private float offsetY;
    Sprite landSprite;
    Sprite borderSprite;

    public void Display(Grid[,] gridList, int rowsCount, int columnsCount,int landType)
    {
        offsetX = -0.32f * columnsCount;
        offsetY = -0.32f * rowsCount;
        landSprite = Resources.Load("MapUnits/" + landType, typeof(Sprite)) as Sprite;
        borderSprite = Resources.Load("LandForms/99", typeof(Sprite)) as Sprite;
        _mapCell = Resources.Load("Prefabs/MapUnit") as GameObject;
        for (int i = -5; i < rowsCount+5; i++)
        {
            for (int j = -5; j < columnsCount+5; j++)
            {
                if (i < 0 || i >= rowsCount || j < 0 || j >= rowsCount)
                    DisplayBorder(i, j);
                else
                    DisplayMapUnit(gridList[i, j].type, i, j);
                
            }
        }

    }

    GameObject AddUnit(int x,int y){
        GameObject unit = Instantiate(_mapCell) as GameObject;
        unit.gameObject.name = x + "," + y;
        unit.transform.SetParent(transform);
        unit.transform.localScale = Vector2.one;
        unit.transform.localPosition = new Vector2(cellSize * x + offsetX, cellSize * y + offsetY);
        return unit;
    }

    //需要优化逻辑
    void DisplayMapUnit(int unitType, int x, int y)
    {
        GameObject unit = AddUnit(x, y);
        unit.GetComponent<SpriteRenderer>().sprite = landSprite;
        if (unitType > 0)
        {
            Sprite formSprite = Resources.Load("LandForms/" + unitType, typeof(Sprite)) as Sprite;
            unit.GetComponentsInChildren<SpriteRenderer>()[1].sprite = formSprite;
        }
    }


    void DisplayBorder(int x,int y){
        GameObject unit = AddUnit(x, y);
        unit.GetComponent<SpriteRenderer>().sprite = borderSprite;
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
