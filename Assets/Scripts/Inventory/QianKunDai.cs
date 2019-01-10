using UnityEngine;
using System.Collections;
using System.Text;

public class QianKunDai : Inventroy
{
    private static QianKunDai _instance;
    private GameObject slotPrefab;
    public static QianKunDai Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("KnapscakPanel").GetComponent<QianKunDai>();
            }
            return _instance;
        }
    }

    public override void Awake()
    {
        slotNum = DataManager.Instance.GetBackpackCount();
        slotPrefab = Resources.Load("Prefabs/slot") as GameObject;
        ResetSlot();

        base.Awake();
    }

    public void SaveData()
    {
        StringBuilder sb = new StringBuilder();//用来保存物品信息的字符串
        foreach (Slot slot in slotArray)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                sb.Append(itemUI.Item.ID + "," + itemUI.Amount + ";");//用逗号分隔一个物品中的ID和数量，用 ; 分隔多个物品
            }
            else
            {
                sb.Append("0;");//如果物品槽里没有物品就是0
            }
        }
        PlayerData._player.QianKunDaiStorage = sb.ToString();
    }

    public void LoadData()
    {
        string str = PlayerData._player.QianKunDaiStorage;
        string[] itemArray = str.Split(';');//按照  -  分隔多个物品
        for (int i = 0; i < itemArray.Length - 1; i++)//长度减1是因为最后一个字符是 “;”，不需要取它
        {
            string itemStr = itemArray[i];
            if (itemStr != "0")
            {
                string[] temp = itemStr.Split(',');//按照逗号分隔这个物品的信息（ID和Amoun数量）
                int id = int.Parse(temp[0]);
                Item item = LoadTxt.Instance.ReadItem(id);
                int amount = int.Parse(temp[1]);
                for (int j = 0; j < amount; j++)//执行Amount次StoreItem方法，一个一个的存
                {
                    slotArray[i].StoreItem(item);
                }
            }
        }
    }
}




