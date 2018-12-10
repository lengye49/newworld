using UnityEngine;
using UnityEngine.UI;

public class BattleHotKeys : MonoBehaviour {

    private Button[] hotKeys;

    private void Awake()
    {
        hotKeys = GetComponentsInChildren<Button>();
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
            SetHotKey(index, null, false);
        }
        else
        {
            //ReadItemInfo
            Item item = LoadTxt.Instance.ReadItem(itemId);
            int itemCount = PlayerData._player.ItemCountInBackpack(itemId);
            bool isActive = itemCount > 0;
            Sprite sprite = Resources.Load("Items/" + item.Sprite, typeof(Sprite)) as Sprite;
            SetHotKey(index, sprite, isActive, itemId, itemCount);
        }
    }

    void SetHotKeySkills(int index)
    {
        int skillId = PlayerPrefs.GetInt("HotKeySkills" + (index - 5), 0);
        if (skillId == 0)
        {
            SetHotKey(index, null, false);
        }
        else
        {
            //ReadSkillInfo
            Skill skill = LoadTxt.Instance.ReadSkill(skillId);
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
        hotKeys[index].gameObject.GetComponent<Text>().text = countText;

        hotKeys[index].interactable = active;
    }



}
