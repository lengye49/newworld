using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// 处理角色面板上的装备物品槽类（有特殊限制的Slot：只能存放装备），不同格子只能存放这个格子应该有的装备。
/// </summary>
public class TreasureSlot : Slot
{
    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)//重写父类Slot的方法
    {
        //右键直接获取物品
        if (eventData.button == PointerEventData.InputButton.Right)//鼠标右键点击直接实现脱掉，不经过拖拽
        {
            if (transform.childCount > 0 && InventoryManager.Instance.IsPickedItem == false)//需要脱掉的物品得有，并且鼠标上要没有物品，否则就发生：当鼠标上有物品，在其他物品上点击鼠标右键也能脱掉这种情况。
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();

                //Item item = currentItemUI.Item;//临时保存物品信息，防止下面一销毁就没了
                for (int i = 0; i < currentItemUI.Amount; i++)
                {
                    BeiBao.Instance.StoreItem(currentItemUI.Item);
                }

                DestroyImmediate(currentItemUI.gameObject);//立即销毁物品槽中的物品
                InventoryManager.Instance.HideToolTip();//隐藏该物品的提示框
            }
        }
        //宝箱不需要左键拖动物品
    }
}