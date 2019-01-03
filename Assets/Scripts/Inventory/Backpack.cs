using UnityEngine;
using System.Collections;

public class Backpack : Inventroy
{
    private static Backpack _instance;
    private GameObject slotPrefab;
    public static Backpack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("KnapscakPanel").GetComponent<Backpack>();
            }
            return _instance;
        }
    }

    public override void Awake()
    {
        //get &backpack count
        int count = DataManager.Instance.GetBackpackCount();
        //Instatiate knapscak slots
        slotPrefab = Resources.Load("Prefabs/slot") as GameObject;
        for (int i = 0; i < count; i++)
        {
            GameObject slot = Instantiate(slotPrefab) as GameObject;
            ResetSlot(slot);
        }

        base.Awake();
    }

    void ResetSlot(GameObject g)
    {
        g.transform.SetParent(transform);
        g.transform.localScale = Vector3.one;
        g.transform.localPosition = Vector3.zero;
    }
}




