using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleUnitInfo : MonoBehaviour
{
    private Image avatar;
    private Text nameText;
    private Slider hpSlider;
    private Slider mpSlider;
    private Image hpFillImage;
    private Image mpFillImage;

    private Button[] buffListBtn;

    private void Awake()
    {
        avatar = GetComponentInChildren<Image>();
        nameText = GetComponentInChildren<Text>();

        hpSlider = GetComponentsInChildren<Slider>()[0];
        mpSlider = GetComponentsInChildren<Slider>()[1];
        hpFillImage = hpSlider.fillRect.GetComponent<Image>();
        mpFillImage = mpSlider.fillRect.GetComponent<Image>();

        buffListBtn = GetComponentsInChildren<Button>();
    }

    public void Init(string avataPath,string name,float hpPercent=1.0f,List<int> buffs=null)
    {
        avatar.sprite = Resources.Load(avataPath,typeof(Sprite)) as Sprite;
        nameText.text = name;
        UpdateHp(hpPercent);
        UpdateBuffs(buffs);
    }

    public void LoseHp(int value,float percent){
        WarningTip.Instance.ShowTip(2, "-" + value, transform);
        UpdateHp(percent);
    }

    public void AddHp(int value,float percent){
        WarningTip.Instance.ShowTip(1, "+" + value, transform);
        UpdateHp(percent);
    }

    void UpdateHp(float hpPercent){
        hpSlider.value = hpPercent;
        hpFillImage.color = GetValueColor(hpPercent);
    }

    public void LoseMp(int value,float percent){
        WarningTip.Instance.ShowTip(3, "-" + value, transform);
        UpdateMp(percent);
    }

    public void AddMp(int value,float percent){
        WarningTip.Instance.ShowTip(3, "+" + value, transform);
        UpdateMp(percent);
    }

    public void UpdateMp(float mpPercent){
        mpSlider.value = mpPercent;
        mpFillImage.color = GetValueColor(mpPercent);
    }

    public void LoseShield(int value,float percent){
        WarningTip.Instance.ShowTip(4, "-" + value, transform);
        UpdateMp(percent);
    }

    public void AddShield(int value, float percent)
    {
        WarningTip.Instance.ShowTip(4, "+" + value, transform);
        UpdateMp(percent);
    }

    //public void UpdateShield(List<){
    //    shieldText.text = percent * 100f + "%";
    //}


    public void UpdateBuffs(Dictionary<int, int> buffs){
        List<int> keys = new List<int>();
        foreach (int key in buffs.Keys)
            keys.Add(key);
        UpdateBuffs(keys);
    }

    void UpdateBuffs(List<int> buffs){

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
