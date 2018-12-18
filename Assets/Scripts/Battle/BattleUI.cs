using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    private BattleLog battleLog;
    public BattleUnitInfo PlayerBattleInfo { get; private set; }
    public BattleUnitInfo EnemyBattleInfo { get; private set; }
    private BattleTime battleTime;
    private BattleHotKeys hotkeys;


    public Text distanceText;


    private void Awake()
    {
        battleLog = GetComponentInChildren<BattleLog>();
        PlayerBattleInfo = GetComponentsInChildren<BattleUnitInfo>()[0];
        EnemyBattleInfo = GetComponentsInChildren<BattleUnitInfo>()[1];
        battleTime = GetComponentInChildren<BattleTime>();
        hotkeys = GetComponentInChildren<BattleHotKeys>();
    }

    public void InitBattle(){
        PanelController.Instance.MoveIn(gameObject);
        hotkeys.UpdateHotKeys();
        battleLog.Init();
    }

    public void InitFight(float distance)
    {
        battleTime.Init();
        UpdateDistance(distance);
    }


    public void UpdateDistance(float d){
        distanceText.text = d.ToString();
    }

    public void AddLog(string log,int isGood=0)
    {
        battleLog.AddLog(log, isGood);
    }


}
