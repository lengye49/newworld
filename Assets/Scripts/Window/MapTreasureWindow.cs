using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
public class MapTreasureWindow : Inventroy
{
    private Text NameTxt;
    private Text DescTxt;
    private Button[] ChoiceBtns;
    private GameObject slotPrefab;
    private Transform slotContainer;

    public override void Awake()
    {
        slotPrefab = Resources.Load("Prefabs/TreasureSlot") as GameObject;
        Text[] texts = GetComponentsInChildren<Text>();
        NameTxt = texts[0];
        DescTxt = texts[1];
        slotContainer = GetComponentInChildren<GridLayoutGroup>().transform;
    }

    public void ShowWindow(MapTreasure treasure){
        OpenWindow();
        ClearContainer();
        slotNum = treasure.Rewards.Count;
        ResetSlot(slotPrefab, slotContainer);
        base.Awake();

        LoadRewards(treasure.Rewards);

        NameTxt.text = treasure.Name;
        DescTxt.text = treasure.Desc;
    }

    void LoadRewards(Dictionary<int,int> rewards){
        int index = 0;
        foreach(int key in rewards.Keys){
            Item item = LoadTxt.Instance.ReadItem(key);
            Debug.Log("Item" + index + "=" + item.Name);
            if (item == null)
                continue;
            for (int i = 0; i < rewards[key];i++){
                slotArray[index].StoreItem(item);
            }
            index++;
        }
    }

    public void CollectAll(){
        TransferItemToBeiBao();
    }

    void ClearContainer(){

        TreasureSlot[] slots = slotContainer.GetComponentsInChildren<TreasureSlot>();
        for (int i = 0; i < slots.Length;i++){
            DestroyImmediate(slots[i].gameObject);
        }
    }

    public void OpenWindow(){
        PanelController.Instance.MoveIn(gameObject);
    }

    public void CloseWindow(){
        PanelController.Instance.MoveOut(gameObject);
    }

}
