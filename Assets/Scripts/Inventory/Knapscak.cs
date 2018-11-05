using UnityEngine;
using System.Collections;

public class Knapscak : Inventroy
{
    private static Knapscak _instance;
    private GameObject slotPrefab;
    public static Knapscak Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Knapscak").GetComponent<Knapscak>();
            }
            return _instance;
        }
    }

    public override void Start()
    {
        //get knapscak count
        int count = DataManager.Instance.GetKnapscakCount();
        slotPrefab = Resources.Load("Prefabs/slot") as GameObject;
        for (int i = 0; i < count;i++){
            GameObject slot = Instantiate(slotPrefab) as GameObject;
            ResetSlot(slot);
        }
        //Instatiate knapscak slots
        base.Start();

        //Test
        StoreItem(1);
    }

    void ResetSlot(GameObject g){
        g.transform.SetParent(transform);
        g.transform.localScale = Vector3.one;
        g.transform.localPosition = Vector3.zero;
    }
}
