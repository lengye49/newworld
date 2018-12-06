using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{


    private BattleLog battleLog;
    private BattleUnitInfo playerBattleInfo;
    private BattleUnitInfo enemyBattleInfo;
    private BattleTime battleTime;

    public Text distanceText;


    private void Awake()
    {
        battleLog = GetComponentInChildren<BattleLog>();
        playerBattleInfo = GetComponentsInChildren<BattleUnitInfo>()[0];
        enemyBattleInfo = GetComponentsInChildren<BattleUnitInfo>()[1];
        battleTime = GetComponentInChildren<BattleTime>();
    }

    public void InitBattle(){
        PanelController.Instance.MoveIn(gameObject);
        battleLog.Init();
        playerBattleInfo.Init("PlayerAvatar/" + PlayerData._player.Profession, PlayerData._player.Name);
    }

    public void InitFight(BattleUnit enemy,float distance)
    {
        battleTime.Init();
        enemyBattleInfo.Init("NpcAvatar/" + enemy.Avatar, enemy.Name);
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
