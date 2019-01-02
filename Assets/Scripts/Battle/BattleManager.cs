using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleManager : MonoBehaviour
{

    private BattleUI battleUI;
    private float distance;
    private List<BattleUnit> enemys;

    private BattleUnit enemy;
    private BattleUnit player;

    void Start()
    {
        battleUI = GetComponent<BattleUI>();
        //TestBattle(10000, 10, 1);
    }

    public void TestBattle(){
        Npc npc = LoadTxt.Instance.ReadNpc(10000);
        List<Npc> es = new List<Npc>();
        es.Add(npc);
        List<int> ls = new List<int>();
        ls.Add(10);
        List<int> ts = new List<int>();
        ts.Add(1);

        StartBattle(es, ls, ts, false);
    }

    public void StartBattle(List<Npc> _enemys,List<int> _days,List<int> _titles,bool isAttacked)
    {
        battleUI.InitBattle();
        enemys = new List<BattleUnit>();
        player = new BattleUnit(battleUI.PlayerBattleInfo);

        for (int i = 0; i < _enemys.Count;i++){
            BattleUnit u = new BattleUnit(_enemys[i], _days[i],_titles[i]);
            //Debug.Log("Enemy Hp = " + u.Hp);
            enemys.Add(u);
        }

        FightOnce(isAttacked);
    }

    void FightOnce(bool isAttacked){

        //设定敌人，重置敌人ui信息
        enemy = enemys[0];
        enemy.info = battleUI.EnemyBattleInfo;
        enemy.InitBattle();
        enemys.RemoveAt(0);

        //设置战场
        distance = 10f;
        battleUI.InitFight(distance);

        if(isAttacked)
            EnemyAction();
    }

    /// <summary>
    /// Move the specified unit and direction.
    /// </summary>
    /// <returns>The move.</returns>
    /// <param name="unit">Unit.</param>
    /// <param name="direction">-1forward,1backward.</param>
    public float Move(BattleUnit unit, int direction){
        float d1 = Mathf.Max(0, distance + unit.Speed * direction);
        float realMoveDistance = distance - d1;
        distance = d1;

        float realMoveTime = realMoveDistance / unit.Speed;
        unit.AddCD(realMoveTime);

        return realMoveDistance;
    }

    public void PlayerUseItem(Item item){
        ItemHandler.UseItemInBattle(item, player, this);
    }

    public void EnemyUseItem(Item item){
        ItemHandler.UseItemInBattle(item, enemy, this);
    }

    public void PlayerCastSkill(Skill s)
    {
        Debug.Log("Player cast skill --> " + s.Name);
        SkillHandler.SkillCastCost(s, player);
        Debug.Log("Start singing.." + s.Name + ", waiting for " + s.Sing + "s");
        player.SingSkill(s);
        CheckBattleEnd();
    }

    public void EnemyCastSkill(Skill s){
        Debug.Log("Enemy cast skill --> " + s.Name + ", waiting for " + s.Sing + "s");
        SkillHandler.SkillCastCost(s, enemy);
        enemy.SingSkill(s);
        CheckBattleEnd();
    }

    void ReleaseSkill(BattleUnit attacker,BattleUnit defender){
        attacker.CompleteSing();
        SkillHandler.CastSkill(attacker.SkillSinging, attacker, defender);
        //Debug.Log("Enemy Hp = " + enemy.Hp);
    }

    void CheckBattleEnd()
    {
        if (enemy.Hp > 0)
        {
            CheckNextAction();
            return;
        }

        //CheckDrop todo
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
            FightOnce(false);
        }
        else
        {
            //Todo 战斗结束
        }
    }

    void CheckNextAction()
    {
        Debug.Log("Checking Auto Movement...");
        bool isPlayerAction = false;
        while(!isPlayerAction){
            if(enemy.NextMoveTime() < player.NextMoveTime()){
                player.TimePass(enemy.NextMoveTime());
                if (enemy.IsSing)
                {
                    Debug.Log("Enemy Release Skill --> " + enemy.SkillSinging.Name);
                    ReleaseSkill(enemy, player);
                }
                else
                {
                    Debug.Log("Selecting Enemy Action");
                    EnemyAction();
                }
            }else{
                enemy.TimePass(player.NextMoveTime());
                if (player.IsSing)
                {
                    Debug.Log("Player Release Skill --> " + player.SkillSinging.Name);
                    ReleaseSkill(player, enemy);
                }
                else
                {
                    Debug.Log("Player Action!");
                    isPlayerAction = true;
                }
            }
        }
    }



    void EnemyAction()
    {
        //Todo 添加遮罩1s，用于展示敌方动作
        //Todo 生命过低时概率逃跑

        //使用物品
        Item item = enemy.SelectItem();
        if (item != null)
        {
            Debug.Log("Enemy Using Item " + item.Name);
            EnemyUseItem(item);
        }

        //根据优先级释放技能，距离不足则移动。
        bool isTooFar = false;
        Skill s = enemy.SelectAutoSkill(distance,ref isTooFar);
        if (s != null)
        {
            EnemyCastSkill(s);
        }else{
            if (isTooFar)
                Move(enemy, -1);
            else
                Move(enemy, 1);
        }

        //Todo 界面展示敌方的动作名称
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
