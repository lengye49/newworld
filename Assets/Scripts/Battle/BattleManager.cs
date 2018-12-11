using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleManager : MonoBehaviour
{

private BattleUI battleUI;
    private BattleLog battleLog;
    private float myNextTurn;
    private float enemyNextTurn;
    private float distance;
    private List<BattleUnit> enemys;

    private BattleUnit enemy;
    private BattleUnit player;
    //private AchieveActions _achieveActions;

    void Start()
    {
        battleUI = GetComponent<BattleUI>();
        battleLog = GetComponentInChildren<BattleLog>();
        TestBattle(10000, 10, 1);
    }

    void TestBattle(int npcId,int days,int title){
        Npc npc = LoadTxt.Instance.ReadNpc(npcId);
        List<Npc> es = new List<Npc>();
        es.Add(npc);
        List<int> ls = new List<int>();
        ls.Add(days);
        List<int> ts = new List<int>();
        ts.Add(title);

        StartBattle(es, ls, ts, false);
    }

    public void StartBattle(List<Npc> _enemys,List<int> _days,List<int> _titles,bool isAttacked)
    {
        battleUI.InitBattle();
        enemys = new List<BattleUnit>();
        player = new BattleUnit();

        for (int i = 0; i < _enemys.Count;i++){
            BattleUnit u = new BattleUnit(_enemys[i], _days[i],_titles[i]);
            enemys.Add(u);
        }

        battleLog.StartBattleLog(_enemys.Count, isAttacked, enemys[0].Name);
        StartAFight(isAttacked);
    }

    void StartAFight(bool isAttacked){
        distance = 10f;
        battleUI.InitFight(enemys[0],distance);
        enemys.RemoveAt(0);

        myNextTurn = 0;
        enemyNextTurn = 0;

        if(isAttacked)
            EnemyAction();
    }


    float Move(bool forward, float speed, bool isEnemyMove)
    {
        int f = forward ? -1 : 1;
        float d = Mathf.Max(0, distance + speed * f);
        float dis = distance - d;
        distance = d;

        battleLog.MoveLog(forward, dis, distance, isEnemyMove, enemy.Name);
        battleUI.UpdateDistance(distance);

        float t = dis / speed;
        if (isEnemyMove)
            enemyNextTurn += t;
        else
            myNextTurn += t;

        return t;
    }

    void CastSkill(Skill s, BattleUnit attacker, BattleUnit defender)
    {

        CheckBattleEnd();
    }


    string GetHitPart(string s)
    {
        string[] ss = s.Split('|');
        return ss[Algorithms.GetIndexByRange(0, ss.Length)];
    }



    void CheckBattleEnd()
    {
        if (enemy.Hp > 0)
        {
            CheckEnemyAction();
            return;
        }


        //CheckDrop

        StartCoroutine(WaitAndCheck());
    }

    IEnumerator WaitAndCheck()
    {

        yield return new WaitForSeconds(1f);
        CheckNextEnemy();

    }

    void CheckNextEnemy()
    {
        if (enemys.Count>0)
        {
            //SetEnemy();
        }
        else
        {

        }
    }



    public void JumpForward()
    {
        //Move(true, GameData._playerData.property[23], false);
        CheckEnemyAction();
    }
    public void JumpBackward()
    {
        //Move(false, GameData._playerData.property[23], false);
        CheckEnemyAction();
    }


    void CheckEnemyAction()
    {
        while (enemyNextTurn < myNextTurn)
        {
            EnemyAction();
        }
    }

    void EnemyAction()
    {
        //Todo 添加遮罩1s，用于展示敌方动作
        //Todo 生命过低时概率逃跑
        //Todo 根据距离选可释放的技能、根据优先级释放，距离不足则移动。
        //Todo 界面展示敌方的动作名称
        //Todo 增加时间
        //

    }

    //public void Capture()
    //{
    //    if (enemy.canCapture == 0 || (enemy.hp / enemyMaxHp > 0.5f) || captureFailTime >= 3)
    //        return;
    //    if (_gameData.GetUsedPetSpace() + enemy.canCapture > GameData._playerData.PetsOpen * 10)
    //    {
    //        _floating.CallInFloating("宠物笼空间不足!", 1);
    //        return;
    //    }

    //    myNextTurn++;
    //    SetPoint();
    //    float rate = 0.2f - enemy.level / 100 * 0.5f;

    //    rate *= GameData._playerData.CaptureRate;
    //    int i = Algorithms.GetIndexByRange(0, 10000);
    //    if (i < (int)(rate * 10000))
    //    {
    //        Pet p = new Pet();
    //        p.monsterId = enemy.monsterId;
    //        p.state = 0;
    //        p.alertness = enemy.level;
    //        p.speed = (int)enemy.speed;
    //        p.name = enemy.name;
    //        _gameData.AddPet(p);
    //        ui.AddLog("你捕获了新宠物: " + enemy.name, 0);

    //        //Achievement
    //        this.gameObject.GetComponentInParent<AchieveActions>().CapturePet();
    //        //战斗结束。。
    //        StartCoroutine(WaitAndCheck());
    //    }
    //    else
    //    {

    //        ui.AddLog("你试图抓捕" + enemy.name + "，但是失败了。", 0);
    //        CheckEnemyAction();
    //    }
    //}



    //void AddEffect(Skill s, bool isMyAtk, int dam)
    //{
    //    if (s.effectId == 0)
    //        return;
    //    if (s.effectId > 0)
    //    {
    //        int r = Algorithms.GetIndexByRange(0, 100);
    //        if (r < s.effectProp)
    //        {
    //            switch (s.effectId)
    //            {
    //                case 100:
    //                    if (isMyAtk)
    //                    {
    //                        enemyNextTurn += 2;
    //                        ui.AddLog(enemy.name + "受到[减速]效果，2秒内无法行动。", 0);
    //                    }
    //                    else
    //                    {
    //                        myNextTurn += 2;
    //                        ui.AddLog("你受到[减速]效果，2秒内无法行动。", 0);
    //                    }
    //                    break;
    //                case 101:
    //                    if (isMyAtk)
    //                    {
    //                        enemyNextTurn += 3;
    //                        ui.AddLog(enemy.name + "受到[减速]效果，3秒内无法行动。", 0);
    //                    }
    //                    else
    //                    {
    //                        myNextTurn += 3;
    //                        ui.AddLog("你受到[混乱]效果，3秒内无法行动。", 0);
    //                    }
    //                    break;
    //                case 102:
    //                    if (isMyAtk)
    //                    {
    //                        enemyNextTurn += 5;
    //                        ui.AddLog(enemy.name + "受到[魅惑]效果，5秒内无法行动。", 0);
    //                    }
    //                    else
    //                    {
    //                        myNextTurn += 5;
    //                        ui.AddLog("你受到[魅惑]效果，5秒内无法行动。", 0);
    //                    }
    //                    break;
    //                case 103:
    //                    if (isMyAtk)
    //                    {
    //                        enemyNextTurn += 4;
    //                        ui.AddLog(enemy.name + "受到[眩晕]效果，7秒内无法行动。", 0);
    //                    }
    //                    else
    //                    {
    //                        myNextTurn += 4;
    //                        ui.AddLog("你受到[眩晕]效果，7秒内无法行动", 0);
    //                    }
    //                    break;
    //                case 104:
    //                    if (isMyAtk)
    //                    {
    //                        enemyNextTurn += 4;
    //                        ui.AddLog(enemy.name + "受到[冰冻]效果，7秒内无法行动。", 0);
    //                    }
    //                    else
    //                    {
    //                        myNextTurn += 4;
    //                        _gameData.ChangeProperty(10, -5);
    //                        ui.AddLog("你受到[冰冻]效果，7秒内无法行动，温度-5℃。", 0);
    //                    }
    //                    break;
    //                case 105:
    //                    if (isMyAtk)
    //                    {
    //                        enemyNextTurn += 5;
    //                        ui.AddLog(enemy.name + "被石化了5秒。", 0);
    //                    }
    //                    else
    //                    {
    //                        myNextTurn += 5;
    //                        ui.AddLog("你被石化了5秒。", 0);
    //                    }
    //                    break;
    //                case 106:
    //                    if (isMyAtk)
    //                    {
    //                        _gameData.ChangeProperty(2, 5);
    //                        enemy.spirit -= 5;
    //                        ui.AddLog("你吸取了" + enemy.name + "5点精神。", 0);
    //                    }
    //                    else
    //                    {
    //                        _gameData.ChangeProperty(2, -5);
    //                        enemy.spirit += 5;
    //                        ui.AddLog(enemy.name + "吸取了你5点精神。", 0);
    //                    }
    //                    break;
    //                case 107:
    //                    if (isMyAtk)
    //                    {
    //                        enemyNextTurn += 3;
    //                        ui.AddLog(enemy.name + "受到[束缚]效果，3秒内无法行动。", 0);
    //                    }
    //                    else
    //                    {
    //                        myNextTurn += 3;
    //                        ui.AddLog("你受到[束缚]效果，3秒内无法行动。", 0);
    //                    }
    //                    break;
    //                case 109:
    //                    int hpPlus = (int)(dam * 0.3f);
    //                    if (isMyAtk)
    //                    {
    //                        _gameData.ChangeProperty(0, hpPlus);
    //                        ui.AddLog("并吸取了" + hpPlus + "点生命。", 0);
    //                    }
    //                    else
    //                    {
    //                        enemy.hp += hpPlus;
    //                        ui.AddLog("并吸取了" + hpPlus + "点生命。", 0);
    //                    }
    //                    break;
    //                case 110:
    //                    int hpPlus1 = (int)(dam * 0.5f);
    //                    if (isMyAtk)
    //                    {
    //                        _gameData.ChangeProperty(0, hpPlus1);
    //                        ui.AddLog("并吸取了" + hpPlus1 + "点生命。", 0);
    //                    }
    //                    else
    //                    {
    //                        enemy.hp += hpPlus1;
    //                        ui.AddLog("并吸取了" + hpPlus1 + "点生命。", 0);
    //                    }
    //                    break;
    //                case 111:
    //                    if (!isMyAtk)
    //                    {
    //                        _gameData.ChangeProperty(10, 5);
    //                        ui.AddLog("温度+5℃。", 0);
    //                    }
    //                    break;
    //                default:
    //                    Debug.Log("Incorrect effectId: " + s.effectId);
    //                    break;
    //            }
    //        }
    //        SetMyHpSlider();
    //        SetEnemyHpSlider();
    //    }
    //}
}
