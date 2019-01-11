//这个脚本用于存放玩家放在箱子里的东西，暂不处理

using UnityEngine;
using System.Text;
using System.Collections;

public class Chest : Inventroy
{
    private GameObject slotPrefab;
    public override void Awake()
    {
        slotPrefab = Resources.Load("Prefabs/slot") as GameObject;
    }

    public void ShowChest(int count,string playerStorage){
        slotNum = count;
        ResetSlot(slotPrefab);
        base.Awake();

        //读取物品todo
    }

    //public void SaveData()
    //{
    //    StringBuilder sb = new StringBuilder();//用来保存物品信息的字符串
    //    foreach (Slot slot in slotArray)
    //    {
    //        if (slot.transform.childCount > 0)
    //        {
    //            ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
    //            sb.Append(itemUI.Item.ID + "," + itemUI.Amount + ";");//用逗号分隔一个物品中的ID和数量，用 - 分隔多个物品
    //        }
    //        else
    //        {
    //            sb.Append("0;");//如果物品槽里没有物品就是0
    //        }
    //    }
    //    PlayerData._player.QianKunDaiStorage = sb.ToString();
    //}

    //public void LoadData()
    //{
    //    string str = PlayerData._player.QianKunDaiStorage;
    //    string[] itemArray = str.Split(';');//按照  -  分隔多个物品
    //    for (int i = 0; i < itemArray.Length - 1; i++)//长度减1是因为最后一个字符是 “-”，不需要取它
    //    {
    //        string itemStr = itemArray[i];
    //        if (itemStr != "0")
    //        {
    //            string[] temp = itemStr.Split(',');//按照逗号分隔这个物品的信息（ID和Amoun数量）
    //            int id = int.Parse(temp[0]);
    //            Item item = LoadTxt.Instance.ReadItem(id);
    //            int amount = int.Parse(temp[1]);
    //            for (int j = 0; j < amount; j++)//执行Amount次StoreItem方法，一个一个的存
    //            {
    //                slotArray[i].StoreItem(item);
    //            }
    //        }
    //    }
    //}

}
