using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterPanel : Inventroy
{

    private static CharacterPanel _instance;
    public static CharacterPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();
            }
            return _instance;
        }
    }

    private Text propertyText;//对角色属性面板中Text组件的引用
    private Player player;//对角色脚本的引用

    public override void Awake()
    {
        base.Awake();
        propertyText = GetComponentInChildren<Text>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        UpdatePropertyText();
    }

    private void UpdatePropertyText()
    {
        propertyText.text = PlayerData._player.PropertyInfo;
    }

    //直接穿戴功能（不需拖拽）
    public void PutOn(Item item)
    {
        Item exitItem = null;//临时保存需要交换的物品
        foreach (Slot slot in slotArray)//遍历角色面板中的物品槽，查找合适的的物品槽
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
            if (equipmentSlot.IsRightItem(item)) //判断物品是否合适放置在该物品槽里
            {
                if (equipmentSlot.transform.childCount > 0)//判断角色面板中的物品槽合适的位置是否已经有了装备
                {
                    ItemUI currentItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    exitItem = currentItemUI.Item;
                    currentItemUI.SetItem(item, 1);
                }
                else
                {
                    equipmentSlot.StoreItem(item);
                }
                break;
            }
        }
        if (exitItem != null)
        {
            Knapscak.Instance.StoreItem(exitItem);//把角色面板上是物品替换到背包里面
        }
        UpdatePropertyText();//更新显示角色属性值
    }

    //脱掉装备功能（不需拖拽）
    public void PutOff(Item item)
    {
        Knapscak.Instance.StoreItem(item);//把角色面板上是物品替换到背包里面
        UpdatePropertyText();
    }
}
