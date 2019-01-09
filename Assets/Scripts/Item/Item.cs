using UnityEngine;
using System.Collections;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public ItemQuality Quality { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }//容量
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string Sprite { get; set; }//用于后期查找UI精灵路径
    public string Effect { get; set; }
    public int Skill { get; set; }

    public Item()
    {
        this.ID = -1;//表示这是一个空的物品类
    }

    public Item(int id, string name, ItemType type, ItemQuality quality, string description, int capaticy, int buyPrice, int sellPrice, string sprite,string effect="")
    {
        this.ID = id;
        this.Name = name;
        this.Type = type;
        this.Quality = quality;
        this.Description = description;
        this.Capacity = capaticy;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.Sprite = sprite;
        this.Effect = effect;
        this.Skill = 100;
    }

    /// <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType
    {
        Consumable,//消耗品
        Equipment,//装备
        Weapon,//武器
        Armor,
        Shoes,
        Neckless,
        Waist,
        Bracelet,
        Material, //材料
        Tailsman,//法宝
    }
    /// <summary>
    /// 品质
    /// </summary>
    public enum ItemQuality
    {
        Common,//一般的 灰
        Uncommon,//不寻常的 白
        Rare,//稀有的 绿
        Epic,//史诗级的 蓝
        Legendary,//传奇的 紫
        Artifact//神器 橙
    }

    //得到提示框应该显示的内容
    public virtual string GetToolTipText()
    {
        string strItemType = "";
        switch (Type)
        {
            case ItemType.Consumable:
                strItemType = "消耗品";
                break;
            case ItemType.Equipment:
                strItemType = "装备";
                break;
            case ItemType.Weapon:
                strItemType = "武器";
                break;
            case ItemType.Material:
                strItemType = "材料";
                break;
        }
        string strItemQuality = "";
        switch (Quality)
        {
            case ItemQuality.Common:
                strItemQuality = "一般";
                break;
            case ItemQuality.Uncommon:
                strItemQuality = "不寻常";
                break;
            case ItemQuality.Rare:
                strItemQuality = "稀有";
                break;
            case ItemQuality.Epic:
                strItemQuality = "史诗";
                break;
            case ItemQuality.Legendary:
                strItemQuality = "传奇";
                break;
            case ItemQuality.Artifact:
                strItemQuality = "神器";
                break;
        }

        string color = ""; //用于设置提示框各个不同内容的颜色
        switch (Quality)
        {
            case ItemQuality.Common:
                color = "white";//白色
                break;
            case ItemQuality.Uncommon:
                color = "lime";//绿黄色
                break;
            case ItemQuality.Rare:
                color = "navy";//深蓝色
                break;
            case ItemQuality.Epic:
                color = "magenta";//洋红色
                break;
            case ItemQuality.Legendary:
                color = "orange";//橙黄色
                break;
            case ItemQuality.Artifact:
                color = "red";//红色
                break;
        }
        string text = string.Format("<color={0}>{1}</color>\n<color=yellow><size=32>介绍：{2}</size></color>\n<color=red><size=32>容量：{3}</size></color>\n<color=green><size=32>物品类型：{4}</size></color>\n<color=blue><size=32>物品质量：{5}</size></color>\n<color=orange>购买价格$：{6}</color>\n<color=red>出售价格$：{7}</color>", color, Name, Description, Capacity, strItemType, strItemQuality, BuyPrice, SellPrice);
        return text;
    }

}
