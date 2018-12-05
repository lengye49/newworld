using UnityEngine;
using UnityEngine.UI;

public class BattleHotKeys : MonoBehaviour {

    private Button[] hotKeys;

    private void Awake()
    {
        hotKeys = GetComponentsInChildren<Button>();
    }

    public void InitHotKeys(){
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
        if (PlayerPrefs.GetInt("HotKeyItem" + index, 0) == 0)
        {
            SetHotKey(index, null, "", false);
        }
        else
        {
            //ReadItemInfo
        }
    }

    void SetHotKeySkills(int index)
    {
        if (PlayerPrefs.GetInt("HotKeySkills" + (index - 5), 0) == 0)
        {
            SetHotKey(index, null, "", false);
        }
        else
        {
            //ReadSkillInfo
        }
    }

    void SetHotKey(int index, Sprite sprite,string text,bool active){
        hotKeys[index].GetComponent<Image>().sprite = sprite;
        hotKeys[index].gameObject.GetComponent<Text>().text = text;
        hotKeys[index].interactable = active;
    }

    //快捷键：鼠标悬停会显示tips，鼠标点击会释放/使用

}
