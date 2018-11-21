using UnityEngine;
public class MapShow : MonoBehaviour
{
    private GameObject _mapCell;
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

        unit.transform.SetParent(transform);
        unit.transform.localScale = Vector2.one;
        unit.GetComponent<SpriteRenderer>().sprite = sprite;
        unit.GetComponentsInChildren<SpriteRenderer>()[1].sprite = item;
        unit.transform.localPosition = new Vector2(0.64f * x, 0.64f * y);
    }

}
