using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadTxt : Singleton<LoadTxt>
{
    //public static Dictionary<int, Gift> GiftDic;
    //public static Dictionary<int, Item> ItemDic;
    //public static Dictionary<int, DgEvent> DgEventDic;

    private string[][] strs;

    void Awake()
    {
    }

    public  List<Formula> ReadFormularFile(){
        List<Formula> fList = new List<Formula>();

        strs = ReadTxtFile("formula");
        //Todo 需求1个物品和多个物品，产出多个物品等信息
        for (int i = 0; i < strs.Length - 1; i++)
        {
            int id = int.Parse(GetDataByRowAndCol(strs, i + 1, 0));
            int item1 = int.Parse(GetDataByRowAndCol(strs, i + 1, 1));
            int count1 = int.Parse(GetDataByRowAndCol(strs, i + 1, 2));
            int item2 = int.Parse(GetDataByRowAndCol(strs, i + 1, 3));
            int count2 = int.Parse(GetDataByRowAndCol(strs, i + 1, 4));
            int resId= int.Parse(GetDataByRowAndCol(strs, i + 1, 5));
            Formula f = new Formula(item1, count1, item2, count2, resId);
            fList.Add(f);
        }
        return fList;
    }

    public List<Item> ReadItemFile(){
        List<Item> iList = new List<Item>();

        strs = ReadTxtFile("Spec/items");
        for (int i = 0; i < strs.Length - 1;i++){
            int id = int.Parse(GetDataByRowAndCol(strs, i + 1, 0));
            string itemName = GetDataByRowAndCol(strs, i + 1, 1);
            Item.ItemType itemType = (Item.ItemType)int.Parse(GetDataByRowAndCol(strs, i + 1, 2));
            Item.ItemQuality quality = (Item.ItemQuality)int.Parse(GetDataByRowAndCol(strs, i + 1, 3));
            string description = GetDataByRowAndCol(strs, i + 1, 4);
            int capaticy = int.Parse(GetDataByRowAndCol(strs, i + 1, 5));
            int buyPrice = int.Parse(GetDataByRowAndCol(strs, i + 1, 6));
            int sellPrice = int.Parse(GetDataByRowAndCol(strs, i + 1, 7));
            string sprite = GetDataByRowAndCol(strs, i + 1, 8);
            string effect = GetDataByRowAndCol(strs, i + 1, 9);
            Item item = new Item(id, itemName, itemType, quality, description, capaticy, buyPrice, sellPrice, sprite,effect);
            Debug.Log("Reading Item: id = " + id + ", name = " + itemName);
            iList.Add(item);
        }

        return iList;
    }

    //void LoadGift()
    //{
    //    strs = ReadTxt.ReadText("gifts");
    //    for (int i = 0; i < strs.Length - 1; i++)
    //    {
    //        Gift g = new Gift();
    //        g.id = int.Parse(ReadTxt.GetDataByRowAndCol(strs, i + 1, 0));
    //        g.name = ReadTxt.GetDataByRowAndCol(strs, i + 1, 1);
    //        g.desc = ReadTxt.GetDataByRowAndCol(strs, i + 1, 2);
    //        g.openReq = new List<int>();
    //        string s = ReadTxt.GetDataByRowAndCol(strs, i + 1, 3);
    //        if (s.Contains("|"))
    //        {
    //            string[] ss = s.Split('|');
    //            for (int j = 0; j < ss.Length; j++)
    //                g.openReq.Add(int.Parse(ss[j]));
    //        }
    //        else
    //        {
    //            g.openReq.Add(int.Parse(s));
    //        }

    //        g.type = int.Parse(ReadTxt.GetDataByRowAndCol(strs, i + 1, 4));
    //        g.value = int.Parse(ReadTxt.GetDataByRowAndCol(strs, i + 1, 5));
    //        GiftDic.Add(g.id, g);
    //    }
    //}

    //void LoadItem()
    //{
    //    strs = ReadTxt.ReadText("items");
    //    for (int i = 0; i < strs.Length - 1; i++)
    //    {
    //        Item it = new Item();
    //        it.id = int.Parse(ReadTxt.GetDataByRowAndCol(strs, i + 1, 0));
    //        it.name = ReadTxt.GetDataByRowAndCol(strs, i + 1, 1);
    //        it.price = int.Parse(ReadTxt.GetDataByRowAndCol(strs, i + 1, 2));
    //        it.desc = ReadTxt.GetDataByRowAndCol(strs, i + 1, 4);
    //        //            Debug.Log(it.name);
    //        ItemDic.Add(it.id, it);
    //    }
    //}

    //void LoadDgEvent()
    //{
    //    strs = ReadTxt.ReadText("events");
    //    for (int i = 0; i < strs.Length - 1; i++)
    //    {
    //        DgEvent d = new DgEvent();
    //        d.id = int.Parse(ReadTxt.GetDataByRowAndCol(strs, i + 1, 0));
    //        d.name = ReadTxt.GetDataByRowAndCol(strs, i + 1, 1);
    //        d.desc = ReadTxt.GetDataByRowAndCol(strs, i + 1, 2);
    //        d.opt1 = ReadTxt.GetDataByRowAndCol(strs, i + 1, 3);
    //        d.opt2 = ReadTxt.GetDataByRowAndCol(strs, i + 1, 4);
    //        d.opt3 = ReadTxt.GetDataByRowAndCol(strs, i + 1, 5);
    //        d.opt1Desc = ReadTxt.GetDataByRowAndCol(strs, i + 1, 6);
    //        d.opt2Desc = ReadTxt.GetDataByRowAndCol(strs, i + 1, 7);
    //        d.opt3Desc = ReadTxt.GetDataByRowAndCol(strs, i + 1, 8);
    //        //            Debug.Log(d.name);
    //        DgEventDic.Add(d.id, d);
    //    }
    //}


    public string[][] ReadTxtFile(string fileName)
    {
        string[][] textArray;
        TextAsset binAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;
        string[] lineArray = binAsset.text.Split("\r"[0]);//split the txt by return("/r"[0]);

        textArray = new string[lineArray.Length][];

        for (int i = 0; i < lineArray.Length; i++)
        {
            textArray[i] = lineArray[i].Split(','); //split the line by ','
        }

        return textArray;

    }

    public string GetDataByRowAndCol(string[][] textArray, int nRow, int nCol)
    {
        if (textArray.Length <= 0 || nRow >= textArray.Length)
            return "";
        if (nCol >= textArray[0].Length)
            return "";

        return textArray[nRow][nCol];
    }
}
