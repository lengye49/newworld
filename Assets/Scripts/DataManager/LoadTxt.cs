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

    public MapInfo ReadMapInfo(int mapId)
    {
        strs = ReadTxtFile("Spec/map_" + mapId);
        int id = int.Parse(GetDataByRowAndCol(strs, 1, 0));
        int type = int.Parse(GetDataByRowAndCol(strs, 1, 1));
        int x = int.Parse(GetDataByRowAndCol(strs, 1, 2));
        int y = int.Parse(GetDataByRowAndCol(strs, 1, 3));
        int blocks = int.Parse(GetDataByRowAndCol(strs, 1, 4));
        int designType = int.Parse(GetDataByRowAndCol(strs, 1, 5));
        int[] designList = null;
        if (designType > 0)
            designList = Algorithms.SplitStrToInts(GetDataByRowAndCol(strs, 1, 6));
        return new MapInfo(id, type, x, y, blocks, designType, designList);
    }

    public Npc ReadNpc(int npcId){
        strs = ReadTxtFile("Spec/Npc");
        for (int i = 0; i < strs.Length;i++){
            int id = int.Parse(GetDataByRowAndCol(strs, 1, 0));
            if (id != npcId)
                continue;
            string npcname = GetDataByRowAndCol(strs, 1, 1);
            string desc = GetDataByRowAndCol(strs, 1, 2);
            int level = int.Parse(GetDataByRowAndCol(strs, 1, 3));
            int levelInc = int.Parse(GetDataByRowAndCol(strs, 1, 4));
            int gender = int.Parse(GetDataByRowAndCol(strs, 1, 5));
            int[] dialogues = Algorithms.SplitStrToInts(GetDataByRowAndCol(strs, 1, 6));
            int model = int.Parse(GetDataByRowAndCol(strs, 1, 7));
            int image = int.Parse(GetDataByRowAndCol(strs, 1, 8));
            int[] skills = Algorithms.SplitStrToInts(GetDataByRowAndCol(strs, 1, 9));
            int[] friends = Algorithms.SplitStrToInts(GetDataByRowAndCol(strs, 1, 10));
            int mate = int.Parse(GetDataByRowAndCol(strs, 1, 11));
            int[] enemies = Algorithms.SplitStrToInts(GetDataByRowAndCol(strs, 1, 12));
            string[] nickNames = Algorithms.SplitStrToStrs(GetDataByRowAndCol(strs, 1, 13));
            Npc npc = new Npc(id, npcname, desc, level, levelInc, gender, dialogues, model, image, skills, friends, mate, enemies, nickNames);
            return npc;
        }
        Debug.Log("Npc " + npcId + " does NOT exist!");
        return null;
    }

    public NpcModel ReadNpcModel(int modelId)
    {
        strs = ReadTxtFile("Spec/NpcModel");
        for (int i = 0; i < strs.Length; i++)
        {
            int id = int.Parse(GetDataByRowAndCol(strs, 1, 0));
            if (id != modelId)
                continue;
            int hp = int.Parse(GetDataByRowAndCol(strs, 1, 1));
            int hpInc = int.Parse(GetDataByRowAndCol(strs, 1, 2));
            int mp = int.Parse(GetDataByRowAndCol(strs, 1, 3));
            int mpInc = int.Parse(GetDataByRowAndCol(strs, 1, 4));
            int atk = int.Parse(GetDataByRowAndCol(strs, 1, 5));
            int atkInc = int.Parse(GetDataByRowAndCol(strs, 1, 6));

            NpcModel model = new NpcModel(id, hp, hpInc, mp, mpInc, atk, atkInc);
            return model;
        }
        Debug.Log("NpcModel " + modelId + " does NOT exist!");
        return null;
    }

    public NpcTitle ReadNpcTitle(int titleId)
    {
        strs = ReadTxtFile("Spec/NpcTitle");
        for (int i = 0; i < strs.Length; i++)
        {
            int id = int.Parse(GetDataByRowAndCol(strs, 1, 0));
            if (id != titleId)
                continue;
            string titlename = GetDataByRowAndCol(strs, 1, 1);
            int hpBonus = int.Parse(GetDataByRowAndCol(strs, 1, 2));
            int mpBonus = int.Parse(GetDataByRowAndCol(strs, 1, 3));
            int atkBonus = int.Parse(GetDataByRowAndCol(strs, 1, 4));
            int defBonus = int.Parse(GetDataByRowAndCol(strs, 1, 5));
            int speedBonus = int.Parse(GetDataByRowAndCol(strs, 1, 6));

            NpcTitle title = new NpcTitle(id, titlename, hpBonus, mpBonus, atkBonus, defBonus, speedBonus);
            return title;
        }
        Debug.Log("NpcModel " + titleId + " does NOT exist!");
        return null;
    }

    public Skill ReadSkill(int skillId){
        return new Skill();
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

    public int[] GetLandForms(int landType){
        switch(landType){
            case 0:
                return new int[] { 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1 };
            case 1:
                return new int[] { 1, 1, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1 };
            case 2:
                return new int[] { 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0 };
            case 3:
                return new int[] { 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0 };
            case 4:
                return new int[] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 1 };
            case 5:
                return new int[] { 0, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1 };
            case 6:
                return new int[] { 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1 };
            case 7:
                return new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1 };
            default:
                Debug.Log("Unknown LandType = " + landType);
                return new int[] { 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1 };
        }
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
