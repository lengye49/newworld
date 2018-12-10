using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleManager : MonoBehaviour
{

    //public EventLog eventLog;

    private BattleUI battleUI;
    private BattleLog battleLog;
    private float myNextTurn;
    private float enemyNextTurn;
    private float distance;
    private List<BattleUnit> enemys;
    private BattleUnit enemy;
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



    /// <summary>
    /// Move forward,return moveTime;
    /// </summary>
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

    void Fight(Skill s)
    {
        //AddEffect(s, true, 0);
    }

    void Fight(float hit, float dodge, float vitalSensibility, float spirit, float atk, float def, Skill s, bool isMyAtk)
    {
        int hitRate = Algorithms.IsDodgeOrCrit(hit, dodge, vitalSensibility, spirit);
        string hitPart = "";
        if (hitRate == 0)
        {
            return;
        }
        else if (hitRate == 1)
        {
            hitPart = isMyAtk ? GetHitPart(enemy.HitBody) : "身体";
        }
        else
        {
            hitPart = isMyAtk ? GetHitPart(enemy.HitVital) : "头部";
        }

        int dam = Algorithms.CalculateDamage(atk, def, s, hitRate, isMyAtk);
        //      Debug.Log ("Atk = " + atk + ", Dam = " + dam);

        if (isMyAtk)
        {
            enemy.Hp -= dam;
            //SetEnemyHpSlider();
            //          if(s>0)
            //              AddEffect (LoadTxt.skillDic [skillId], isMyAtk, dam);
            battleUI.AddLog("你击中了" + enemy.Name + "的" + hitPart + "，造成" + dam + "点伤害。", 0);
        }
        else
        {
            //_gameData.ChangeProperty(0, -dam);
            SetMyHpSlider();
            //AddEffect(s, isMyAtk, dam);
            battleUI.AddLog(enemy.Name + "击中了你的" + hitPart + "，造成" + dam + "点伤害。", 0);
        }

        CheckBattleEnd();
    }

    string GetHitPart(string s)
    {
        string[] ss = s.Split('|');
        return ss[Algorithms.GetIndexByRange(0, ss.Length)];
    }

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

    void CheckBattleEnd()
    {
        if (enemy.Hp > 0)
        {
            CheckEnemyAction();
            return;
        }

        string s = "你击败了" + enemy.Name + "。";
        battleUI.AddLog(s, 1);

        //if (enemy.mapOpen > 0)
        //{
        //    if (GameData._playerData.MapOpenState[enemy.mapOpen] == 0)
        //    {
        //        GameData._playerData.MapOpenState[enemy.mapOpen] = 1;
        //        _gameData.StoreData("MapOpenState", _gameData.GetStrFromMapOpenState(GameData._playerData.MapOpenState));
        //        s = "他告诉你去往" + LoadTxt.MapDic[enemy.mapOpen].name + "的方向。";
        //        ui.AddLog(s, 1);
        //        eventLog.AddLog("你发现了去" + LoadTxt.MapDic[enemy.mapOpen].name + "的路。", true);
        //    }
        //    else
        //    {
        //        s = "他告诉你去往" + LoadTxt.MapDic[enemy.mapOpen].name + "的方向。";
        //        ui.AddLog(s, 1);
        //        s = "但是你已经知道了。";
        //        ui.AddLog(s, 1);
        //    }
        //}

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

    int GenerateItemId(int orgId)
    {
        int i = 0;
        int j = 0;
        //if (LoadTxt.MatDic[orgId].type == 3)
        //{
        //    i = Algorithms.GetIndexByRange(0, 10);
        //    j = _gameData.meleeIdUsedData;
        //    PlayerPrefs.SetInt("MeleeIdUsed", j++);
        //}
        //else if (LoadTxt.MatDic[orgId].type == 4)
        //{
        //    i = Algorithms.GetIndexByRange(0, 10);
        //    j = _gameData.rangedIdUsedData;
        //    PlayerPrefs.SetInt("RangedIdUsed", j++);
        //}

        return orgId * 10000 + i * 1000 + j;
    }


    void SetEnemyHpSlider()
    {
        //float hpPercent = enemy.hp / enemy.maxHp;
        //enemyHpFill.color = GetColor(enemy.hp / enemyMaxHp);
    }

    void SetMyHpSlider()
    {
        //myHpSlider.value = GameData._playerData.property[0] / GameData._playerData.property[1];
        //myHpFill.color = GetColor(GameData._playerData.property[0] / GameData._playerData.property[1]);
    }

   

    public void MeleeFight()
    {
        //      int skillId = LoadTxt.MatDic [(int)(GameData._playerData.MeleeId/10000)].skillId;
    //    myNextTurn += GameData._playerData.property[21];
    //    SetPoint();
    //    Fight(GameData._playerData.property[16], enemy.dodge, enemy.vitalSensibility, GameData._playerData.property[2], GameData._playerData.property[13], enemy.def, new Skill(), true);
    //    //Achievement
    //    _achieveActions.Fight("Melee");
    }
    public void RangedFight()
    {
        //      int skillId = LoadTxt.MatDic [(int)(GameData._playerData.RangedId/10000)].skillId;
        //myNextTurn += GameData._playerData.property[22];

        //float att = GameData._playerData.property[14];

        //int ammoId = GameData._playerData.AmmoId / 10000;
        //if (LoadTxt.MatDic[ammoId].property.ContainsKey(26))
        //    att *= (LoadTxt.MatDic[ammoId].property[26] / 100f + 1f);

        //if (LoadTxt.MatDic[ammoId].property.ContainsKey(14))
        //    att += LoadTxt.MatDic[ammoId].property[14];

        //SetPoint();
        //Fight(GameData._playerData.property[17], enemy.dodge, enemy.vitalSensibility, GameData._playerData.property[2], att, enemy.def, new Skill(), true);

        //GameData._playerData.AmmoNum--;
        //_gameData.StoreData("AmmoNum", GameData._playerData.AmmoNum);

        //if (GameData._playerData.AmmoNum == 0)
        //{
        //    GameData._playerData.AmmoId = 0;
        //    _gameData.StoreData("AmmoId", 0);
        //}

        ////Achievement
        //_achieveActions.Fight("Ranged");
    }
    public void MagicFight()
    {
        //myNextTurn += 1;
        //SetPoint();
        //_gameData.ChangeProperty(2, -(int)(LoadTxt.MatDic[GameData._playerData.MagicId / 10000].castSpirit * GameData._playerData.MagicCostRate));

        ////      print (GameData._playerData.MagicId);
        //if (GameData._playerData.MagicId / 10000 == 303 || GameData._playerData.MagicId / 10000 == 305)
        //{
        //    int heal = (int)(GameData._playerData.property[24] * GameData._playerData.MagicPower / 10);
        //    _gameData.ChangeProperty(0, heal);
        //    ui.AddLog("你使用" + LoadTxt.MatDic[GameData._playerData.MagicId / 10000].name + ",回复了" + heal + "点生命。", 0);
        //}
        //else
        //{
        //    int dam = (int)(GameData._playerData.property[24] * GameData._playerData.MagicPower);
        //    enemy.hp -= dam;
        //    enemy.hp = Mathf.Max(0, enemy.hp);
        //    ui.AddLog("你使用" + LoadTxt.MatDic[GameData._playerData.MagicId / 10000].name + ",对" + enemy.name + "造成" + dam + "点伤害。", 0);
        //}

        //SetEnemyHpSlider();
        //SetMyHpSlider();
        //CheckBattleEnd();

        //Achievement
        //_achieveActions.Fight("Magic");
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


}
