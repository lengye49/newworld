using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleUnitInfo : MonoBehaviour
{
    private Image avatar;
    private Text nameText;
    private Slider hpSlider;

    private Button[] buffListBtn;

    private void Start()
    {
        avatar = GetComponentInChildren<Image>();
        nameText = GetComponentInChildren<Text>();
        hpSlider = GetComponentInChildren<Slider>();
        buffListBtn = GetComponentsInChildren<Button>();
    }

    public void Init(string avataPath,string name,float hpPercent=1.0f,List<int> buffs=null)
    {

    }

    public void UpdateHp(float hpPercent){

    }

    public void UpdateBuffs(List<int> buffs){

    }
}
