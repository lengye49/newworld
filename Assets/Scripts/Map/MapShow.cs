﻿using UnityEngine;
using System.Collections.Generic;
public class MapShow : MonoBehaviour
{
    private GameObject _mapCell;
    private GameObject[,] cells;
    private float cellSize = 0.64f;
    private float offsetX;
    private float offsetY;
    private MapData data;
    Sprite landSprite;
    Sprite borderSprite;

    public void Display(MapData mapData)
    {
        data = mapData;

        cells = new GameObject[data.Rows, data.Columns];
        offsetX = -0.32f * data.Columns;
        offsetY = -0.32f * data.Rows;
        landSprite = Resources.Load("MapUnits/" + data.LandType, typeof(Sprite)) as Sprite;
        borderSprite = Resources.Load("LandForms/99", typeof(Sprite)) as Sprite;
        _mapCell = Resources.Load("Prefabs/MapUnit") as GameObject;

        //刚开始不用全部Display，只Display角色看到的和边界就可以。
        for (int i = -5; i < data.Rows + 5; i++)
        {
            for (int j = -5; j < data.Columns + 5; j++)
            {
                if (i < 0 || i >= data.Rows || j < 0 || j >= data.Rows)
                    DisplayBorder(i, j);
                else
                    DisplayMapUnit(i, j);
            }
        }

    }

    GameObject GetUnit(int x,int y){
        if (cells[x, y] == null)
        {
            cells[x, y] = AddUnit(x, y);
        }
        return cells[x, y];
    }

    GameObject AddUnit(int x,int y){
        GameObject unit = Instantiate(_mapCell) as GameObject;
        unit.gameObject.name = x + "," + y;
        unit.transform.SetParent(transform);
        unit.transform.localScale = Vector2.one;
        unit.transform.localPosition = new Vector2(cellSize * x + offsetX, cellSize * y + offsetY);
        return unit;
    }


    public void DisplayMapUnit(int x, int y)
    {
        GameObject unit = GetUnit(x, y);
        unit.GetComponent<SpriteRenderer>().sprite = landSprite;
        SpriteRenderer itemRenderer = unit.GetComponentsInChildren<SpriteRenderer>()[1];

        string path;
        if (!data.gridList[x, y].isOpen)
        {
            path = "LandForms/99";
        }
        else
        {
            int param = data.gridList[x, y].param;
            switch (data.gridList[x, y].type)
            {
                case 1:
                    path = "LandForms/" + param;
                    break;
                case 2:
                    path = "NpcAvatar/" + data.npcs[param].Image;
                    break;
                case 3:
                    path = "MapTreasure/" + data.treasures[param].Image;
                    break;
                case 4:
                    path = "MapPortal/" + data.portals[param].Image;
                    break;
                case 5:
                    path = "Items/" + data.pickableItems[param].Image;
                    break;
                default:
                    path = "";
                    break;
            }
        }
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        itemRenderer.sprite = sprite;
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
