using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleHotKeys : MonoBehaviour {

    private Button[] hotKeys;
    private Item[] hotKeyItems;
    private Skill[] hotKeySkills;
    private BattleManager manager;

    private void Awake()
    {
        PlayerPrefs.SetInt("HotKeyItem0", 1);
        PlayerPrefs.SetInt("HotKeySkill0", 1);
        hotKeys = GetComponentsInChildren<Button>();
        manager = GetComponentInParent<BattleManager>();
        hotKeyItems = new Item[5];
        hotKeySkills = new Skill[10];
    }

    public void UpdateHotKeys(){
        for (int i = 0; i < 15;i++){
            if(i<5){
                SetHotKeyItems(i);
            }else{
                SetHotKeySkills(i);
            }
        }
    }

    void SetHotKeyItems(int index)
    {
        int itemId = PlayerPrefs.GetInt("HotKeyItem" + index, 0);
        if (itemId== 0)
        {
            hotKeyItems[index] = null;
            SetHotKey(index, null, false);
        }
        else
        {
            //ReadItemInfo
            Item item = LoadTxt.Instance.ReadItem(itemId);
            hotKeyItems[index] = item;
            int itemCount = PlayerData._player.ItemCountInBackpack(itemId);
            bool isActive = itemCount > 0;
            Sprite sprite = Resources.Load("Items/" + item.Sprite, typeof(Sprite)) as Sprite;
            SetHotKey(index, sprite, isActive, itemId, itemCount);
        }
    }

    void SetHotKeySkills(int index)
    {
        int skillId = PlayerPrefs.GetInt("HotKeySkill" + (index - 5), 0);
        if (skillId == 0)
        {
            hotKeySkills[index - 5] = null;
            SetHotKey(index, null, false);
        }
        else
        {
            //ReadSkillInfo
            Skill skill = LoadTxt.Instance.ReadSkill(skillId);
            hotKeySkills[index - 5] = skill;
            Sprite sprite = Resources.Load("Skills/" + skill.Sprite, typeof(Sprite)) as Sprite;
            SetHotKey(index, sprite, true, skillId);
        }
    }

    void SetHotKey(int index, Sprite sprite,bool active,int id=0, int count=0)
    {
        hotKeys[index].GetComponent<Image>().sprite = sprite;

        string nameText = id > 0 ? id.ToString() : "Empty";
        hotKeys[index].gameObject.name = nameText;

        string countText = count > 0 ? count.ToString() : "";
        hotKeys[index].gameObject.GetComponentInChildren<Text>().text = countText;

        hotKeys[index].interactable = active;
    }

    //todo 检测是否可用

    public void UseHotKey(int index){
        Debug.Log("Use Skill " + index);
        if(index<5)
        {
            Item item = hotKeyItems[index];
            if(item!=null){
                manager.PlayerUseItem(item);
            }
        }
        else
        {
            Skill skill = hotKeySkills[index - 5];
            if (skill != null)
                manager.PlayerCastSkill(skill);
        }
    }

}
