using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{

    private Button[] hotKeys;
    private BattleLog battleLog;
    private BattleUnitInfo playerBattleInfo;
    private BattleUnitInfo enemyBattleInfo;
    private BattleTime battleTime;

    public Text distanceText;


    private void Start()
    {
        hotKeys = GameObject.Find("HotKeys").GetComponentsInChildren<Button>();
        battleLog = GetComponentInChildren<BattleLog>();
        playerBattleInfo = GetComponentsInChildren<BattleUnitInfo>()[0];
        enemyBattleInfo = GetComponentsInChildren<BattleUnitInfo>()[1];
        battleTime = GetComponentInChildren<BattleTime>();
    }

    public void InitBattle(){

        battleLog.Init();

    }

    public void InitFight(BattleUnit enemy,float distance)
    {
        battleTime.Init();
        enemyBattleInfo.Init(enemy.avatar, enemy.name);
        UpdateDistance(distance);
    }


    public void UpdateEnemyHp(float percent){
        enemyBattleInfo.UpdateHp(percent);
    }

    public void UpdatePlayerHp(float percent)
    {
        playerBattleInfo.UpdateHp(percent);
    }

    public void UpdateDistance(float d){
        distanceText.text = d.ToString();
    }

    public void AddLog(string log,int isGood=0)
    {
        battleLog.AddLog(log, isGood);
    }



}
