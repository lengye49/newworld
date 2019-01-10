using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    private bool isToolTipShow = false;//提示框是否在显示状态
    private Canvas canvas;//Canva物体

    private ItemUI pickedItem;//鼠标选中的物品的脚本组件，用于制作拖动功能 
    public ItemUI PickedItem { get { return pickedItem; } }
    private bool isPickedItem = false;//鼠标是否选中该物品
    public bool IsPickedItem { get { return isPickedItem; } }

    private Slot lastHoldingSlot;

    void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
        pickedItem.Hide();//开始为隐藏状态

    }

    void Update()
    {
        if (isToolTipShow == true && isPickedItem == false) //控制提示框跟随鼠标移动
        {
            Vector2 postionToolTip;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out postionToolTip);
            ItemTip.Instance.SetLocalPosition(postionToolTip);//设置提示框的位置，二维坐标会自动转化为三维坐标但Z坐标为0
        }
        else if (isPickedItem == true) //控制盛放物品的容器UI跟随鼠标移动
        {
            Vector2 postionPickeItem;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out postionPickeItem);
            pickedItem.SetLocalPosition(postionPickeItem);//设置容器的位置，二维坐标会自动转化为三维坐标但Z坐标为0
        }
        //物品丢弃功能：
        if (isPickedItem == true && Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) == false)//UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) == false 表示判断鼠标是否正在在UI上
        {
            isPickedItem = false;
            pickedItem.Hide();
        }

        //if(Input.GetKeyDown(KeyCode.Escape)){
        //    if(isPickedItem){

        //    }
        //}

    }


    //显示提示框方法
    public void ShowToolTip(string content)
    {
        if (this.isPickedItem == true) return;//如果物品槽中的物品被捡起了，那就不需要再显示提示框了
        ItemTip.Instance.Show(content);
        isToolTipShow = true;
    }
    //隐藏提示框方法
    public void HideToolTip()
    {
        ItemTip.Instance.Hide();
        isToolTipShow = false;
    }

    //获取（拾取）物品槽里的指定数量的（amount）物品UI
    public void PickUpItem(Item item, int amount)
    {
        PickedItem.SetItem(item, amount);
        this.isPickedItem = true;
        PickedItem.Show();//获取到物品之后把跟随鼠标的容器（用来盛放捡起的物品的容器）显示出来
        ItemTip.Instance.Hide();//同时隐藏物品信息提示框

        //控制盛放物品的容器UI跟随鼠标移动
        Vector2 postionPickeItem;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out postionPickeItem);
        pickedItem.SetLocalPosition(postionPickeItem);//设置容器的位置，二维坐标会自动转化为三维坐标但Z坐标为0

    }

    //从鼠标上减少（移除）指定数量的物品
    public void ReduceAmountItem(int amount = 1)
    {
        this.pickedItem.RemoveItemAmount(amount);
        if (pickedItem.Amount <= 0)
        {
            isPickedItem = false;
            PickedItem.Hide();//如果鼠标上没有物品了那就隐藏了
        }
    }

    //点击保存按钮，保存当前物品信息
    //public void SaveInventory()
    //{
    //    BeiBao.Instance.SaveInventory();
    //    QianKunDai.Instance.SaveInventory();
    //    Chest.Instance.SaveInventory();
    //    CharacterPanel.Instance.SaveInventory();
    //    Forge.Instance.SaveInventory();
    //    PlayerPrefs.SetInt("CoinAmount", GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount);//保存玩家金币
    //}

    ////点击加载按钮，加载当前物品
    //public void LoadInventory()
    //{
    //    BeiBao.Instance.LoadInventory();
    //    QianKunDai.Instance.LoadInventory();
    //    Chest.Instance.LoadInventory();
    //    CharacterPanel.Instance.LoadInventory();
    //    Forge.Instance.LoadInventory();
    //    //加载玩家金币
    //    if (PlayerPrefs.HasKey("CoinAmount") == true)
    //    {
    //        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount = PlayerPrefs.GetInt("CoinAmount");
    //    }
    //}
}
