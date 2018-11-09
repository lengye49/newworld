using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class BattleActions : MonoBehaviour
{

    public EventLogManager eventLog;

    private BattleUI ui;
    private float myNextTurn;
    private float enemyNextTurn;
    private float distance;
    private List<BattleUnit> enemys;

    //private AchieveActions _achieveActions;

    void Start()
    {
        ui = GetComponent<BattleUI>();

        //_floating = GameObject.Find("FloatingSystem").GetComponent<FloatingActions>();

    }

    public void StartBattle(List<Unit> _enemys,List<int> _levels,List<int> _titles,bool isAttacked)
    {
        ui.InitBattle();

        for (int i = 0; i < _enemys.Count;i++){
            BattleUnit u = new BattleUnit(_enemys[i], _levels[i],_titles[i]);
            enemys.Add(u);
        }

        if (isAttacked)
        {
            if (_enemys.Count > 1)
                ui.AddLog("注意，你被" + _enemys.Count + "名敌人围攻了!", 2);
            else
                ui.AddLog("注意，你被" + _enemys[0].name + "偷袭了！", 2);
        }
        else
        {
            if (_enemys.Count > 1)
                ui.AddLog("你发现了" + _enemys.Count + "名敌人!", 0);
            else
                ui.AddLog("你发现了" + _enemys[0].name + "!", 0);
        }

        StartAFight(isAttacked);
    }

    void StartAFight(bool isAttacked){
        distance = 10f;
        ui.InitFight(enemys[0],distance);

        myNextTurn = 0;
        enemyNextTurn = 0;

        if(isAttacked)
            EnemyAction();
    }




   

    /// <summary>
    /// Add log.需要处理新加一行，显示被遮挡的问题
    /// </summary>
    /// <param name="s">S.</param>
    /// <param name="isGood">Is good:0normal 1good 2bad.</param>


    void AutoFight()
    {
        while (GameData._playerData.hpNow > 0 && enemy.hp > 0)
        {
            if (myNextTurn < enemyNextTurn)
            {
                //如果近战攻击高，则向前移动后再攻击；如果远程攻击高，直接用远程攻击
                if (GameData._playerData.property[13] > GameData._playerData.property[14])
                {
                    if (distance > GameData._playerData.property[19])
                    {
                        Move(true, GameData._playerData.property[23], false);
                    }
                    else
                    {
                        MeleeFight();
                    }
                }
                else
                {
                    if (distance > GameData._playerData.property[20])
                    {
                        Move(true, GameData._playerData.property[23], false);
                    }
                    else
                    {
                        RangedFight();
                    }
                }
            }
            else
            {
                if (distance > enemy.range)
                {
                    Move(true, enemy.speed, true);
                }
                else
                {
                    EnemyCastSkill();
                }
            }
        }
    }

    void Move(bool forward, float speed, bool isEnemyMove)
    {

        int f = forward ? -1 : 1;
        float d = Mathf.Max(0, distance + speed * f);
        float dis = distance - d;
        distance = d;
        string s = forward ? "前进" : "后退";
        if (isEnemyMove)
        {
            enemyNextTurn += 1;
            ui.AddLog(enemy.name + s + "了" + dis + "米。", 0);
        }
        else
        {
            myNextTurn += 1;
            ui.AddLog("你" + s + "了" + dis + "米。", 0);
        }

        enemyDistance.text = distance + "米";
        SetPoint();
        SetActions();
    }

    void Fight(Skill s)
    {
        AddEffect(s, true, 0);
    }

    void Fight(float hit, float dodge, float vitalSensibility, float spirit, float atk, float def, Skill s, bool isMyAtk)
    {
        int hitRate = Algorithms.IsDodgeOrCrit(hit, dodge, vitalSensibility, spirit);
        string hitPart = "";
        if (hitRate == 0)
        {
            //          Debug.Log ("Missed!");
            ui.AddLog((isMyAtk ? "你" : enemy.name) + "发起攻击，但是" + (isMyAtk ? enemy.name : "你") + "灵巧地躲开了!", 0);
            return;
        }
        else if (hitRate == 1)
        {
            hitPart = isMyAtk ? GetHitPart(enemy.hit_Body) : "身体";
        }
        else
        {
            hitPart = isMyAtk ? GetHitPart(enemy.hit_Vital) : "头部";
        }

        int dam = Algorithms.CalculateDamage(atk, def, s, hitRate, isMyAtk);
        //      Debug.Log ("Atk = " + atk + ", Dam = " + dam);

        if (isMyAtk)
        {
            enemy.hp -= dam;
            SetEnemyHpSlider();
            //          if(s>0)
            //              AddEffect (LoadTxt.skillDic [skillId], isMyAtk, dam);
            ui.AddLog("你击中了" + enemy.name + "的" + hitPart + "，造成" + dam + "点伤害。", 0);
        }
        else
        {
            _gameData.ChangeProperty(0, -dam);
            SetMyHpSlider();
            AddEffect(s, isMyAtk, dam);
            ui.AddLog(enemy.name + "击中了你的" + hitPart + "，造成" + dam + "点伤害。", 0);
        }

        CheckBattleEnd();
    }

    string GetHitPart(string s)
    {
        string[] ss = s.Split('|');
        return ss[Algorithms.GetIndexByRange(0, ss.Length)];
    }

    void AddEffect(Skill s, bool isMyAtk, int dam)
    {
        if (s.effectId == 0)
            return;
        if (s.effectId > 0)
        {
            int r = Algorithms.GetIndexByRange(0, 100);
            if (r < s.effectProp)
            {
                switch (s.effectId)
                {
                    case 100:
                        if (isMyAtk)
                        {
                            enemyNextTurn += 2;
                            ui.AddLog(enemy.name + "受到[减速]效果，2秒内无法行动。", 0);
                        }
                        else
                        {
                            myNextTurn += 2;
                            ui.AddLog("你受到[减速]效果，2秒内无法行动。", 0);
                        }
                        break;
                    case 101:
                        if (isMyAtk)
                        {
                            enemyNextTurn += 3;
                            ui.AddLog(enemy.name + "受到[减速]效果，3秒内无法行动。", 0);
                        }
                        else
                        {
                            myNextTurn += 3;
                            ui.AddLog("你受到[混乱]效果，3秒内无法行动。", 0);
                        }
                        break;
                    case 102:
                        if (isMyAtk)
                        {
                            enemyNextTurn += 5;
                            ui.AddLog(enemy.name + "受到[魅惑]效果，5秒内无法行动。", 0);
                        }
                        else
                        {
                            myNextTurn += 5;
                            ui.AddLog("你受到[魅惑]效果，5秒内无法行动。", 0);
                        }
                        break;
                    case 103:
                        if (isMyAtk)
                        {
                            enemyNextTurn += 4;
                            ui.AddLog(enemy.name + "受到[眩晕]效果，7秒内无法行动。", 0);
                        }
                        else
                        {
                            myNextTurn += 4;
                            ui.AddLog("你受到[眩晕]效果，7秒内无法行动", 0);
                        }
                        break;
                    case 104:
                        if (isMyAtk)
                        {
                            enemyNextTurn += 4;
                            ui.AddLog(enemy.name + "受到[冰冻]效果，7秒内无法行动。", 0);
                        }
                        else
                        {
                            myNextTurn += 4;
                            _gameData.ChangeProperty(10, -5);
                            ui.AddLog("你受到[冰冻]效果，7秒内无法行动，温度-5℃。", 0);
                        }
                        break;
                    case 105:
                        if (isMyAtk)
                        {
                            enemyNextTurn += 5;
                            ui.AddLog(enemy.name + "被石化了5秒。", 0);
                        }
                        else
                        {
                            myNextTurn += 5;
                            ui.AddLog("你被石化了5秒。", 0);
                        }
                        break;
                    case 106:
                        if (isMyAtk)
                        {
                            _gameData.ChangeProperty(2, 5);
                            enemy.spirit -= 5;
                            ui.AddLog("你吸取了" + enemy.name + "5点精神。", 0);
                        }
                        else
                        {
                            _gameData.ChangeProperty(2, -5);
                            enemy.spirit += 5;
                            ui.AddLog(enemy.name + "吸取了你5点精神。", 0);
                        }
                        break;
                    case 107:
                        if (isMyAtk)
                        {
                            enemyNextTurn += 3;
                            ui.AddLog(enemy.name + "受到[束缚]效果，3秒内无法行动。", 0);
                        }
                        else
                        {
                            myNextTurn += 3;
                            ui.AddLog("你受到[束缚]效果，3秒内无法行动。", 0);
                        }
                        break;
                    case 109:
                        int hpPlus = (int)(dam * 0.3f);
                        if (isMyAtk)
                        {
                            _gameData.ChangeProperty(0, hpPlus);
                            ui.AddLog("并吸取了" + hpPlus + "点生命。", 0);
                        }
                        else
                        {
                            enemy.hp += hpPlus;
                            ui.AddLog("并吸取了" + hpPlus + "点生命。", 0);
                        }
                        break;
                    case 110:
                        int hpPlus1 = (int)(dam * 0.5f);
                        if (isMyAtk)
                        {
                            _gameData.ChangeProperty(0, hpPlus1);
                            ui.AddLog("并吸取了" + hpPlus1 + "点生命。", 0);
                        }
                        else
                        {
                            enemy.hp += hpPlus1;
                            ui.AddLog("并吸取了" + hpPlus1 + "点生命。", 0);
                        }
                        break;
                    case 111:
                        if (!isMyAtk)
                        {
                            _gameData.ChangeProperty(10, 5);
                            ui.AddLog("温度+5℃。", 0);
                        }
                        break;
                    default:
                        Debug.Log("Incorrect effectId: " + s.effectId);
                        break;
                }
            }
            SetMyHpSlider();
            SetEnemyHpSlider();
        }
    }

    void CheckBattleEnd()
    {
        if (enemy.hp > 0)
        {
            CheckEnemyAction();
            SetActions();
            return;
        }

        string s = "你击败了" + enemy.name + "。";
        ui.AddLog(s, 1);

        if (enemy.mapOpen > 0)
        {
            if (GameData._playerData.MapOpenState[enemy.mapOpen] == 0)
            {
                GameData._playerData.MapOpenState[enemy.mapOpen] = 1;
                _gameData.StoreData("MapOpenState", _gameData.GetStrFromMapOpenState(GameData._playerData.MapOpenState));
                s = "他告诉你去往" + LoadTxt.MapDic[enemy.mapOpen].name + "的方向。";
                ui.AddLog(s, 1);
                eventLog.AddLog("你发现了去" + LoadTxt.MapDic[enemy.mapOpen].name + "的路。", true);
            }
            else
            {
                s = "他告诉你去往" + LoadTxt.MapDic[enemy.mapOpen].name + "的方向。";
                ui.AddLog(s, 1);
                s = "但是你已经知道了。";
                ui.AddLog(s, 1);
            }
        }

        Dictionary<int, int> drop = Algorithms.GetReward(enemy.drop);

        if (drop.Count > 0)
        {
            s = "获得了";
            foreach (int key in drop.Keys)
            {
                int itemId = GenerateItemId(key);
                _gameData.AddItem(itemId, drop[key]);
                s += LoadTxt.MatDic[key].name + " ×" + drop[key] + ",";

                //Achievement
                switch (LoadTxt.MatDic[key].type)
                {
                    case 3:
                        this.gameObject.GetComponentInParent<AchieveActions>().CollectMeleeWeapon(key);
                        break;
                    case 4:
                        this.gameObject.GetComponentInParent<AchieveActions>().CollectRangedWeapon(key);
                        break;
                    case 5:
                        this.gameObject.GetComponentInParent<AchieveActions>().CollectMagicWeapon(key);
                        break;
                    default:
                        break;
                }
            }
            s = s.Substring(0, s.Length - 1);
            if (enemy.renown > 0)
            {
                s += "和" + enemy.renown + "点声望。";
            }
            else
            {
                s += "。";
            }
            ui.AddLog(s, 1);
        }
        else
        {
            if (enemy.renown > 0)
            {
                s = "获得了" + enemy.renown + "点声望。";
                ui.AddLog(s, 1);
            }
        }

        if (enemy.renown > 0)
            _gameData.AddRenown(enemy.renown);


        if (enemy.monsterId == 3008 && GameData._playerData.orderCamp == 0)
        {
            ui.AddLog("你获得了秩序阵营的认可，秩序阵营决定退出战争。", 1);
        }
        else if (enemy.monsterId == 3108 && GameData._playerData.truthCamp == 0)
        {
            ui.AddLog("你获得了真理阵营的认可，真理阵营决定不再挑起战争。", 1);
        }
        else if (enemy.monsterId == 3208 && GameData._playerData.lifeCamp == 0)
        {
            ui.AddLog("你获得了生命阵营的认可，生命阵营决定回归森林。", 1);
        }
        else if (enemy.monsterId == 3308 && GameData._playerData.chaosCamp == 0)
        {
            ui.AddLog("你获得了混乱阵营的认可，混乱阵营决定退回深渊。", 1);
        }
        else if (enemy.monsterId == 3408 && GameData._playerData.deathCamp == 0)
        {
            ui.AddLog("你获得了死亡阵营的认可，死亡阵营决定保持沉寂。", 1);
        }

        //添加成就
        _achieveActions.DefeatEnemy(enemy.monsterId);

        StartCoroutine(WaitAndCheck());
    }

    IEnumerator WaitAndCheck()
    {
        meleeAttack.interactable = false;
        rangedAttack.interactable = false;
        magicAttack.interactable = false;
        jumpForward.interactable = false;
        jumpBackward.interactable = false;
        capture.interactable = false;
        yield return new WaitForSeconds(1f);
        CheckNextEnemy();

    }

    void CheckNextEnemy()
    {
        if (thisEnemyIndex < enemys.Length)
        {
            SetEnemy();
        }
        else
        {
            autoButton.gameObject.SetActive(false);
            returnButton.gameObject.SetActive(true);
        }
    }

    int GenerateItemId(int orgId)
    {
        int i = 0;
        int j = 0;
        if (LoadTxt.MatDic[orgId].type == 3)
        {
            i = Algorithms.GetIndexByRange(0, 10);
            j = _gameData.meleeIdUsedData;
            PlayerPrefs.SetInt("MeleeIdUsed", j++);
        }
        else if (LoadTxt.MatDic[orgId].type == 4)
        {
            i = Algorithms.GetIndexByRange(0, 10);
            j = _gameData.rangedIdUsedData;
            PlayerPrefs.SetInt("RangedIdUsed", j++);
        }

        return orgId * 10000 + i * 1000 + j;
    }


    void SetEnemyHpSlider()
    {
        enemyHpSlider.value = enemy.hp / enemyMaxHp;
        enemyHpFill.color = GetColor(enemy.hp / enemyMaxHp);
    }

    void SetMyHpSlider()
    {
        myHpSlider.value = GameData._playerData.property[0] / GameData._playerData.property[1];
        myHpFill.color = GetColor(GameData._playerData.property[0] / GameData._playerData.property[1]);
    }

    Color GetColor(float value)
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

    public void MeleeFight()
    {
        //      int skillId = LoadTxt.MatDic [(int)(GameData._playerData.MeleeId/10000)].skillId;
        myNextTurn += GameData._playerData.property[21];
        SetPoint();
        Fight(GameData._playerData.property[16], enemy.dodge, enemy.vitalSensibility, GameData._playerData.property[2], GameData._playerData.property[13], enemy.def, new Skill(), true);
        //Achievement
        _achieveActions.Fight("Melee");
    }
    public void RangedFight()
    {
        //      int skillId = LoadTxt.MatDic [(int)(GameData._playerData.RangedId/10000)].skillId;
        myNextTurn += GameData._playerData.property[22];

        float att = GameData._playerData.property[14];

        int ammoId = GameData._playerData.AmmoId / 10000;
        if (LoadTxt.MatDic[ammoId].property.ContainsKey(26))
            att *= (LoadTxt.MatDic[ammoId].property[26] / 100f + 1f);

        if (LoadTxt.MatDic[ammoId].property.ContainsKey(14))
            att += LoadTxt.MatDic[ammoId].property[14];

        SetPoint();
        Fight(GameData._playerData.property[17], enemy.dodge, enemy.vitalSensibility, GameData._playerData.property[2], att, enemy.def, new Skill(), true);

        GameData._playerData.AmmoNum--;
        _gameData.StoreData("AmmoNum", GameData._playerData.AmmoNum);

        if (GameData._playerData.AmmoNum == 0)
        {
            GameData._playerData.AmmoId = 0;
            _gameData.StoreData("AmmoId", 0);
        }

        //Achievement
        _achieveActions.Fight("Ranged");
    }
    public void MagicFight()
    {
        myNextTurn += 1;
        SetPoint();
        _gameData.ChangeProperty(2, -(int)(LoadTxt.MatDic[GameData._playerData.MagicId / 10000].castSpirit * GameData._playerData.MagicCostRate));

        //      print (GameData._playerData.MagicId);
        if (GameData._playerData.MagicId / 10000 == 303 || GameData._playerData.MagicId / 10000 == 305)
        {
            int heal = (int)(GameData._playerData.property[24] * GameData._playerData.MagicPower / 10);
            _gameData.ChangeProperty(0, heal);
            ui.AddLog("你使用" + LoadTxt.MatDic[GameData._playerData.MagicId / 10000].name + ",回复了" + heal + "点生命。", 0);
        }
        else
        {
            int dam = (int)(GameData._playerData.property[24] * GameData._playerData.MagicPower);
            enemy.hp -= dam;
            enemy.hp = Mathf.Max(0, enemy.hp);
            ui.AddLog("你使用" + LoadTxt.MatDic[GameData._playerData.MagicId / 10000].name + ",对" + enemy.name + "造成" + dam + "点伤害。", 0);
        }

        SetEnemyHpSlider();
        SetMyHpSlider();
        CheckBattleEnd();

        //Achievement
        _achieveActions.Fight("Magic");
    }
    public void JumpForward()
    {
        Move(true, GameData._playerData.property[23], false);
        CheckEnemyAction();
    }
    public void JumpBackward()
    {
        Move(false, GameData._playerData.property[23], false);
        CheckEnemyAction();
    }
    public void Capture()
    {
        if (enemy.canCapture == 0 || (enemy.hp / enemyMaxHp > 0.5f) || captureFailTime >= 3)
            return;
        if (_gameData.GetUsedPetSpace() + enemy.canCapture > GameData._playerData.PetsOpen * 10)
        {
            _floating.CallInFloating("宠物笼空间不足!", 1);
            return;
        }

        myNextTurn++;
        SetPoint();
        float rate = 0.2f - enemy.level / 100 * 0.5f;

        rate *= GameData._playerData.CaptureRate;
        int i = Algorithms.GetIndexByRange(0, 10000);
        if (i < (int)(rate * 10000))
        {
            Pet p = new Pet();
            p.monsterId = enemy.monsterId;
            p.state = 0;
            p.alertness = enemy.level;
            p.speed = (int)enemy.speed;
            p.name = enemy.name;
            _gameData.AddPet(p);
            ui.AddLog("你捕获了新宠物: " + enemy.name, 0);

            //Achievement
            this.gameObject.GetComponentInParent<AchieveActions>().CapturePet();
            //战斗结束。。
            StartCoroutine(WaitAndCheck());
        }
        else
        {

            ui.AddLog("你试图抓捕" + enemy.name + "，但是失败了。", 0);
            CheckEnemyAction();
        }
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


}
