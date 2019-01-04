using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public GameObject itemPrefab;//需要存储的物品预设

    /// <summary>
    ///(重点) 向物品槽中添加（存储）物品，如果自身下面已经有Item了，那就Item.amount++;
    /// 如果没有，那就根据ItemPrefab去实例化一个Item，放在其下面
    /// </summary>
    public void StoreItem(Item item)
    {
        if (this.transform.childCount == 0)//如果这个物品槽下没有物品，那就实例化一个物品
        {
            GameObject itemGO = Instantiate(itemPrefab) as GameObject;
            itemGO.transform.SetParent(this.transform);//设置物品为物品槽的子物体
            itemGO.transform.localScale = Vector3.one;//正确保存物品的缩放比例
            itemGO.transform.localPosition = Vector3.zero;//设置物品的局部坐标，为了与其父亲物品槽相对应
            itemGO.GetComponent<ItemUI>().SetItem(item);//更新ItemUI
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddItemAmount();//默认添加一个
        }
    }

    //根据索引器得到当前物品槽中的物品类型
    public Item.ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.Type;
    }

    //根据索引器得到当前物品槽中的物品ID
    public int GetItemID()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }

    //判断物品个数是否超过物品槽的容量Capacity
    public bool isFiled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capacity; //true表示当前数量大于等于容量，false表示当前数量小于容量
    }

    //接口实现的方法，鼠标覆盖时触发
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        if (this.transform.childCount > 0)
        {
            string toolTipText = this.transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);//显示提示框
        }
    }
    //接口实现的方法，鼠标离开时触发
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        if (this.transform.childCount > 0)
        {
            InventoryManager.Instance.HideToolTip();//隐藏提示框
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)//为虚函数，方便子类EquipmentSlot重写
    {
        Debug.Log("OnPointerDown");
        if (eventData.button == PointerEventData.InputButton.Right)//鼠标右键点击直接实现穿戴，不经过拖拽
        {
            if (transform.childCount > 0 && InventoryManager.Instance.IsPickedItem == false)//需要穿戴的物品得有，并且鼠标上要没有物品，否则就发生：当鼠标上有物品，在其他物品上点击鼠标右键也能穿上这种情况。
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                if (currentItemUI.Item is Equipment || currentItemUI.Item is Weapon)//只有装备和物品才可以穿戴
                {
                    Debug.Log("Wear Equip....." + currentItemUI.Item.Name);
                    Item currentItem = currentItemUI.Item;//临时存储物品信息，防止物品UI销毁导致物品空指针
                    currentItemUI.RemoveItemAmount(1);//当前物品槽中的物品减少1个
                    if (currentItemUI.Amount <= 0)//物品槽中的物品没有了
                    {
                        DestroyImmediate(currentItemUI.gameObject);//立即销毁物品槽中的物品
                        InventoryManager.Instance.HideToolTip();//隐藏该物品的提示框
                    }
                    CharacterPanel.Instance.PutOn(currentItem);//直接穿戴
                }
                if(currentItemUI.Item is Consumable){
                    Debug.Log("Use Item....." + currentItemUI.Item.Name);
                }
            }
        }
        if (eventData.button != PointerEventData.InputButton.Left) return;  
        //只有鼠标左键能够点击物品拖拽
        //情况分析如下：
        //一：自身是空
        ///1.pickedItem != null(即IsPickedItem == true)，pickedItem放在这个位置
        ////①按下Ctrl键，放置当前鼠标上的物品的一个
        ////②没有按下Ctrl键，放置当前鼠标上物品的所有
        ///2.pickedItem==null(即IsPickedItem == false)，不做任何处理
        //二：自身不是空
        ///1.pickedItem != null(即 IsPickedItem == true)
        ////①自身的id == pickedItem.id
        //////A.按下Ctrl键，放置当前鼠标上的物品的一个
        //////B.没有按下Ctrl键，放置当前鼠标上物品的所有
        ///////a.可以完全放下
        ///////b.只能放下其中一部分
        ////②自身的id != pickedItem.id，pickedItem跟当前物品交换
        ///2.pickedItem == null(即IsPickedItem == false)
        ////①按下Ctrl键，取得当前物品槽中物品的一半
        ////②没有按下Ctrl键，把当前物品槽里面的物品放到鼠标上

        //格子里有东西
        if (transform.childCount > 0) 
        {
            //取得当前格子中的物品
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
            //当前鼠标上没东西
            if (InventoryManager.Instance.IsPickedItem == false)
            {
                //①按下Ctrl键，取得当前物品槽中物品的一半
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    int amountPicked = (currentItem.Amount + 1) / 2;//如果物品为偶数就拾取刚好一半，如果为奇数就拾取一半多一个
                    InventoryManager.Instance.PickUpItem(currentItem.Item, amountPicked);
                    int amountRemained = currentItem.Amount - amountPicked;//拾取后剩余的物品个数
                    if (amountRemained <= 0)
                    {
                        Destroy(currentItem.gameObject);
                    }
                    else
                    {
                        currentItem.SetAmount(amountRemained);
                    }
                }
                else
                {
                    InventoryManager.Instance.PickUpItem(currentItem.Item, currentItem.Amount);
                    Destroy(currentItem.gameObject);
                }
            }
            //鼠标上有东西
            else
            {
                ////①自身的id == pickedItem.id
                //////A.按下Ctrl键，放置当前鼠标上的物品的一个
                //////B.没有按下Ctrl键，放置当前鼠标上物品的所有
                ///////a.可以完全放下
                ///////b.只能放下其中一部分
                ////②自身的id != pickedItem.id，pickedItem跟当前物品交换

                //鼠标上的东西跟当前格子里的物品一样，且该物品的容量>1，则把鼠标上的东西叠加到当前格子上，否则互换
                if (currentItem.Item.ID == InventoryManager.Instance.PickedItem.Item.ID)
                {
                    if(currentItem.Item.Capacity > 1){
                        int spaceLeft = currentItem.Item.Capacity - currentItem.Amount;
                        //如果有足够的空间，则全部放下，否则只放一部分
                        if (spaceLeft>0){
                            if(spaceLeft> InventoryManager.Instance.PickedItem.Amount){
                                currentItem.AddItemAmount(InventoryManager.Instance.PickedItem.Amount); 
                            }else{
                                currentItem.AddItemAmount(spaceLeft);
                            }
                            InventoryManager.Instance.ReduceAmountItem(spaceLeft);
                        }
                    }
                }
                else
                {
                    //保存当前鼠标捡起物品，用于和物品槽中的物品交换
                    Item pickedItemTemp = InventoryManager.Instance.PickedItem.Item;
                    int pickedItemAmountTemp = InventoryManager.Instance.PickedItem.Amount;

                    //保存当前物品槽中的物品，用于和鼠标上的物品交换
                    Item currentItemTemp = currentItem.Item;
                    int currentItemAmountTemp = currentItem.Amount;
                    //两者交换
                    currentItem.SetItem(pickedItemTemp, pickedItemAmountTemp);//把当前鼠标上的物品放入物品槽
                    InventoryManager.Instance.PickedItem.SetItem(currentItemTemp, currentItemAmountTemp);//把当前物品槽中的物品放在鼠标上
                }
            }
        }
        //格子是空的，放下所有
        else
        {
            if (InventoryManager.Instance.IsPickedItem == true)
            {
                for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
                {
                    this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                }
                InventoryManager.Instance.ReduceAmountItem(InventoryManager.Instance.PickedItem.Amount);
                
            }
        }
    }
}
