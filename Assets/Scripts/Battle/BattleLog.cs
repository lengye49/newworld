using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleLog : MonoBehaviour
{





















    private Text[] battleLogs;
    private int maxIndex;

    private void Start()
    {
        battleLogs = GetComponentsInChildren<Text>();
        maxIndex = battleLogs.Length - 1;
    }

    public void Init()
    {
        for (int i = 0; i < battleLogs.Length; i++)
        {
            battleLogs[i].text = "";
        }
    }

    /// <summary>
    /// Adds new battle log.
    /// </summary>
    /// <param name="s">S.</param>
    /// <param name="isGood">1Green 2Red 0Black.</param>
    public void AddLog(string s, int isGood)
    {
        for (int i = 0; i < maxIndex; i++)
        {
            battleLogs[i].text = battleLogs[i + 1].text;
            battleLogs[i].color = new Color(battleLogs[i + 1].color.r, battleLogs[i + 1].color.g, battleLogs[i + 1].color.b, battleLogs[i + 1].color.a - 0.1f);
        }

        battleLogs[maxIndex].text = "→" + s;
        if (isGood == 1)
            battleLogs[maxIndex].color = new Color(0f, 1f, 0f, 1f);
        else if (isGood == 2)
            battleLogs[maxIndex].color = new Color(1f, 0f, 0f, 1f);
        else
            battleLogs[maxIndex].color = new Color(1f, 1f, 1f, 1f);
    }

    /// <summary>
    /// 战斗开始的Log
    /// </summary>
    /// <param name="enemyNum">Enemy number.</param>
    /// <param name="isAttacked">If set to <c>true</c> is attacked.</param>
    /// <param name="enemyName">Enemy name.</param>
    public void StartBattleLog(int enemyNum,bool isAttacked,string enemyName){
        string content = "";
        if (isAttacked)
        {
            if (enemyNum > 1)
            {
                content = "注意，你被" + enemyNum + "名敌人围攻了!";
            }else{
                content = "注意，你被" + enemyName + "偷袭了！";
            }

        }else{
            if(enemyNum>1){
                content = "准备战斗，对手是" + enemyNum + "个敌人!";
            }else{
                content = "准备战斗，对手是" + enemyName + "!";
            }
        }
        AddLog(content, 0);
    }

    /// <summary>
    /// 移动的Log
    /// </summary>
    /// <param name="forward">If set to <c>true</c> forward.</param>
    /// <param name="deltaDis">Distance.</param>
    /// <param name="isEnemy">If set to <c>true</c> is enemy.</param>
    public void MoveLog(bool forward,float deltaDis,float distance,bool isEnemy,string enemyName)
    {
        string content = "";
        content += isEnemy ? enemyName : "你";
        content += forward ? "前进了" : "后退了";
        content += deltaDis + "米。";
        content += "敌距" + distance + "米";
        AddLog(content, 0);
    }


    public void CastSkillLog(bool skillName,int skillType, int value,bool isDodge,bool isEnemy,string enemyName,string hitPart){
        string content = "";
        content += isEnemy ? enemyName : "你";
        content += "释放了[" + skillName + "],";
        if (isDodge)
        {
            content += isEnemy ? "但是你灵巧的闪开了" : "但是被躲开了。";
        }
        else
        {
            string hitText = (isEnemy ? "击中了你的" : "击中了" + enemyName + "的") + hitPart;
            switch(skillType){
                case 0:
                case 1: 

                    content += "造成了" + value + "点伤害。";
                    break;
                case 2: 
                    content += "造成了" + value + "点治疗。";
                    break;
            }
        }
    }
}
