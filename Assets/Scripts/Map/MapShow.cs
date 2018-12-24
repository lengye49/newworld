using UnityEngine;
using System.Collections.Generic;
public class MapShow : MonoBehaviour
{
    private GameObject _mapCell;
    private float cellSize = 0.64f;
    private float offsetX;
    private float offsetY;
    private MapData data;
    Sprite landSprite;
    Sprite borderSprite;

    public void Display(MapData mapData)//Grid[,] gridList, int rowsCount, int columnsCount,int landType)
    {
        data = mapData;
        offsetX = -0.32f * data.Columns;
        offsetY = -0.32f * data.Rows;
        landSprite = Resources.Load("MapUnits/" + data.LandType, typeof(Sprite)) as Sprite;
        borderSprite = Resources.Load("LandForms/99", typeof(Sprite)) as Sprite;
        _mapCell = Resources.Load("Prefabs/MapUnit") as GameObject;
        for (int i = -5; i < data.Rows + 5; i++)
        {
            for (int j = -5; j < data.Columns + 5; j++)
            {
                if (i < 0 || i >= data.Rows || j < 0 || j >= data.Rows)
                    DisplayBorder(i, j);
                else
                    DisplayMapUnit(i, j);//data.gridList[i, j].type
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


    void DisplayMapUnit(int x, int y)
    {
        GameObject unit = AddUnit(x, y);
        unit.GetComponent<SpriteRenderer>().sprite = landSprite;
        SpriteRenderer itemRenderer = unit.GetComponentsInChildren<SpriteRenderer>()[1];
        Sprite formSprite;
        string path;
        switch(data.gridList[x,y].type){
            case 1:
                path = "LandForms/" + data.gridList[x, y].param;
                break;
            case 2:
                path = "NpcAvatar/";
                break;
            case 3:
                path = "MapTreasure/";
                break;
            case 4:
                path = "MapPortal/";
                break;
            case 5:
                path = "Items/";
                break;
            default:
                path = "";
                break;
        }
        

        if (unitType > 0 && unitType<=1000)
        {
            formSprite = Resources.Load("LandForms/" + unitType, typeof(Sprite)) as Sprite;
            unit.GetComponentsInChildren<SpriteRenderer>()[1].sprite = formSprite;
        }else if(unitType>1000){
            formSprite = Resources.Load("NpcAvatar/" + (unitType-1000), typeof(Sprite)) as Sprite;
            unit.GetComponentsInChildren<SpriteRenderer>()[1].sprite = formSprite;
        }
    }


    void DisplayBorder(int x,int y){
        GameObject unit = AddUnit(x, y);

        unit.GetComponent<BoxCollider2D>().enabled = false;//取消碰撞

        unit.GetComponent<SpriteRenderer>().sprite = landSprite;
        unit.GetComponentsInChildren<SpriteRenderer>()[1].sprite = borderSprite;
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
