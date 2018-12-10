using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleUnitInfo : MonoBehaviour
{
    private Image avatar;
    private Text nameText;
    private Slider hpSlider;
    private Image hpFillImage;

    private Button[] buffListBtn;

    private void Awake()
    {
        avatar = GetComponentInChildren<Image>();
        nameText = GetComponentInChildren<Text>();
        hpSlider = GetComponentInChildren<Slider>();
        hpFillImage = hpSlider.fillRect.GetComponent<Image>();
        buffListBtn = GetComponentsInChildren<Button>();
    }

    public void Init(string avataPath,string name,float hpPercent=1.0f,List<int> buffs=null)
    {
        avatar.sprite = Resources.Load(avataPath,typeof(Sprite)) as Sprite;
        nameText.text = name;
        UpdateHp(hpPercent);
        UpdateBuffs(buffs);
    }

    public void UpdateHp(float hpPercent){
        hpSlider.value = hpPercent;
        hpFillImage.color = GetValueColor(hpPercent);
    }

    public void UpdateBuffs(List<int> buffs){

        for (int i = 0; i < buffs.Count;i++){
            if (i < buffListBtn.Length)
            {
                buffListBtn[i].gameObject.name = "buff|" + buffs[i];
                Sprite sprite = Resources.Load("Buffs/" + buffs[i], typeof(Sprite)) as Sprite;
                buffListBtn[i].transform.GetComponent<Image>().sprite = sprite;
                buffListBtn[i].transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
        for (int i = buffs.Count; i < buffListBtn.Length;i++){
            buffListBtn[i].gameObject.name = "Empty";
            buffListBtn[i].transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }

    public void ShowBattleTip(int colorType, string content, bool shortTime = true)
    {
        GameObject f = Instantiate(Resources.Load("BattleTip")) as GameObject;
        f.SetActive(true);
        f.transform.SetParent(transform);
        f.transform.localPosition = transform.localPosition;//Todo
        Text t = f.GetComponentInChildren<Text>();
        t.text = content;
        //Todo word size
        t.color = Color.red;//Todo
        TweenController.Instance.PopIn(f.transform);
    }


    Color GetValueColor(float value)
    {
        Color c = new Color();

        if (value > 0.5)
            c = new Color((1f - value) * 300f / 255f, 150f / 255F, 0F, 1f);
        else if (value <= 0)
            c = new Color(1f, 1f, 1f, 0f);
        else
            c = new Color(150f / 255f, value * 300f / 255f, 0f, 1f);
        return c;
    }
}
