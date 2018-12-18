using System.Collections.Generic;

public class BattleUnit
{
    public int Id;
    public BattleUnitType UnitType;

    public string Name;
    public string Avatar;
    public int Level;

    public int MaxHp;
    public int Hp;

    public int MaxMp;
    public int Mp;
    public int MpRecover;

    public int MaxStrength;
    public int Strength;
    public int StrengthRecover;

    public int MaxSpirit;
    public int Spirit;

    public int MaxAge;

    public int Speed;
    public int Defense;
    public int CastSpeedBonus;
    public int CastRangeBonus;

    //Buff
    public Dictionary<int, int> buffs;

    public Skill[] SkillList;
    public Item[] ItemList;

    public string Drop;
    public string HitBody;
    public string HitMove;
    public string HitHands;
    public string HitVital;
    public int CanCapture;

    //战斗进度条，进度条满就可以释放技能
    private float CD;
    public bool IsSing;
    private float SingTime;
    public Skill SkillSinging;
    //进度条冻结时间，受到特殊状态时会增加冻结时间。先清除冻结时间才能走进度条
    public float FrozenTime;
    //显示
    public BattleUnitInfo info;



    //****************************************** 战斗中的计算方法 *****************
    public void InitBattle(){
        info.Init(Avatar,Name);
    }

    public void LoseHp(int value){
        if (value <= Hp)
            Hp -= value;
        else
            Hp = 0;

        info.LoseHp(value, (float)Hp / MaxHp);
    }

    public void AddHp(int value){
        if (value + Hp > MaxHp)
            Hp = MaxHp;
        else
            Hp += value;

        info.AddHp(value, (float)Hp / MaxHp);
    }

    /// <summary>
    /// 被吸取、吞噬Mp
    /// </summary>
    /// <param name="value">Value.</param>
    public void LoseMp(int value){
        if (value <= Mp)
            Mp -= value;
        else
            Mp = 0;

        info.LoseHp(value, (float)Mp / MaxMp);
    }

    public void AddMp(int value)
    {
        if (value + Mp > MaxMp)
            Mp = MaxMp;
        else
            MaxMp += value;

        info.AddMp(value, (float)Mp / MaxMp);
    }

    /// <summary>
    /// 释放技能使用MP
    /// </summary>
    /// <param name="value">Value.</param>
    public void UseMp(int value){
        if (value <= Mp)
            Mp -= value;
        else
            Mp = 0;

        info.UpdateMp((float)Mp / MaxMp);
    }

    public int HasShield(int shieldType){
        if (buffs.ContainsKey(shieldType))
            return buffs[shieldType];
        return 0;
    }

    public void AddBuff(int type,int power=0){
        if(buffs.ContainsKey(type)){
            buffs[type] += power;
        }else{
            buffs.Add(type, power);
        }
        info.UpdateBuffs(buffs);
    }

    public void CostBuff(int type,int power){
        if (!buffs.ContainsKey(type))
            return;
        if (buffs[type] <= power)
            RemoveBuff(type);
        else
            buffs[type] -= power;
    }

    public void RemoveBuff(int type){
        if (buffs.ContainsKey(type))
            buffs.Remove(type);
        info.UpdateBuffs(buffs);
    }

    //***************************************** 选择使用的物品*****************
    public Item SelectItem(){
        return null;
    }

    //***************************************** 选择自动释放的技能*****************
    public Skill SelectAutoSkill(float distance,ref bool isTooFar)
    {
        int priority = -1;
        int index = -1;
        for (int i = 0; i < SkillList.Length; i++)
        {
            if (SkillHandler.CanSkillCast(SkillList[i], this) )
            {
                if(SkillList[i].Range * (1 + CastRangeBonus / 100) < distance)
                {
                    isTooFar = true;
                    continue;
                }

                if (SkillList[i].Id > priority)
                {
                    priority = SkillList[i].Id;
                    index = i;
                }
            }
        }

        if (index == -1)
            return null;
        return SkillList[index];
    }

    //***************************************** 下次行动时机*****************
    public float NextMoveTime(){
        if (IsSing)
            return SingTime + FrozenTime;
        else
            return CD + FrozenTime;
    }

    public void SingSkill(Skill s){
        SkillSinging = s;
        SingTime = s.Sing;
        IsSing = true;
        AddCD(s.CD);
    }

    public void CompleteSing(){
        SingTime = 0;
        IsSing = false;
    }


    //技能被打断，终止吟唱，终止CD，并有1秒僵硬
    public void BreakSkill(){
        SingTime = 0;
        IsSing = false;
        CD = 1;
    }

    public void Frozen(float t){
        FrozenTime += t;
    }

    public void TimePass(float t){
        if (IsSing)
        {
            SingTime -= t;
            t = 0;
        }
        else {
            if(FrozenTime>0){
                if (FrozenTime >= t)
                { 
                    FrozenTime -= t;
                    t = 0;
                }else{
                    FrozenTime = 0;
                    t -= FrozenTime;
                }
            }
        }
        CD -= t;
    }

    public void AddCD(float t){
        CD = t;
    }

    //***************************************** 玩家战斗属性 *********************
    public BattleUnit(BattleUnitInfo _info) {
        Id = 0;
        UnitType = BattleUnitType.Player;
        Name = PlayerData._player.Name;
        Avatar = "PlayerAvatar/" + PlayerData._player.Profession;
        Level = PlayerData._player.Level;
        MaxHp = PlayerData._player.MaxHp;
        Hp = PlayerData._player.Hp;
        MaxMp = PlayerData._player.MaxMp;
        Mp = PlayerData._player.Mp;
        MpRecover = PlayerData._player.MpRecover;
        MaxStrength = PlayerData._player.MaxStrength;
        Strength = PlayerData._player.Strength;
        StrengthRecover = PlayerData._player.StrengthRecover;
        MaxSpirit = PlayerData._player.MaxSpirit;
        Spirit = PlayerData._player.Spirit;
        MaxAge = PlayerData._player.MaxAge;
        Speed = PlayerData._player.Speed;
        Defense = PlayerData._player.Defence;
        CastSpeedBonus = PlayerData._player.CastSpeedBonus;
        CastRangeBonus = PlayerData._player.CastRangeBonus;
        buffs = new Dictionary<int, int>();

        SkillList = null;
        ItemList = null;
        Drop = null;
        HitBody = "身体";
        HitMove = "双腿";
        HitHands = "双臂";
        HitVital = "头部";
        CanCapture = 0;

        CD = 0;
        SingTime = 0;
        FrozenTime = 0;

        info = _info;
        InitBattle();
    }

    //***************************************** 敌人战斗属性 *********************
    public BattleUnit(Npc npc, int day, int title)
    {
        NpcModel thisModel = LoadTxt.Instance.ReadNpcModel(npc.Model);
        NpcTitle thisTitle = LoadTxt.Instance.ReadNpcTitle(title);

        Id = npc.Id;
        UnitType = BattleUnitType.Enemy;
        Name = npc.Name;
        Avatar = "NpcAvatar/" + npc.Image;
        Level = npc.Level + (int)(day / npc.LevelInc);
        MaxHp = (thisModel.Hp + thisModel.HpInc * Level) * (thisTitle.HpBonus / 10000 + 1);

        Hp = MaxHp;
        MaxMp = 100;
        Mp = 100;
        MpRecover = 0;
        MaxStrength = 100;
        Strength = 100;
        StrengthRecover = 0;
        MaxSpirit = 100;
        Spirit = 100;
        MaxAge = 1000;//根据level==>MaxAge Todo
        Speed = 100 * (thisTitle.SpeedBonus / 10000 + 1);
        Defense = 0;
        CastSpeedBonus = 0;
        CastRangeBonus = 0;
        buffs = new Dictionary<int, int>();

        SkillList = new Skill[npc.Skills.Length];
        for (int i = 0; i < SkillList.Length; i++)
        {
            SkillList[i] = LoadTxt.Instance.ReadSkill(npc.Skills[i]);
        }
        ItemList = null;

        Drop = "100|1";
        HitBody = "身体";
        HitMove = "肢体";
        HitHands = "肢体";
        HitVital = "要害";
        CanCapture = 0;

        CD = 0;
        SingTime = 0;
        FrozenTime = 0;
    }
}

public enum BattleUnitType{
    Player,
    Enemy,
}
