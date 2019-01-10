using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
public class MapTreasureWindow : Inventroy
{
    private Text NameTxt;
    private Text DescTxt;
    private Button[] ChoiceBtns;


    public override void Awake()
    {
        OpenWindow();

        slotPrefab = Resources.Load("Prefabs/TreasureSlot") as GameObject;
        Text[] texts = GetComponentsInChildren<Text>();
        NameTxt = texts[0];
        DescTxt = texts[1];

    }

    public void ShowWindow(MapTreasure treasure){
        slotNum = treasure.rewards.Count;
        ResetSlot();
        LoadRewards(treasure.rewards);

        DescTxt.text = treasure.Desc;
    }

    void LoadRewards(Dictionary<int,int> rewards){
        int index = 0;
        foreach(int key in rewards.Keys){
            Item item = LoadTxt.Instance.ReadItem(key);
            if (item == null)
                continue;
            for (int i = 0; i < rewards[key];i++){
                slotArray[index].StoreItem(item);
            }
            index++;
        }
    }

    public void CollectAll(){
        for (int i = 0; i < slotArray.Length;i++){

        }
    }



    public void OpenWindow(){
        transform.localPosition = Vector3.zero;
    }

    public void CloseWindow(){
        transform.localPosition = new Vector3(-5000, 0, 0);
    }

}
