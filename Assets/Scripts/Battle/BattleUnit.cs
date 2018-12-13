
public class BattleUnit
{
    public int Id;
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
    public int Shield;

    public Skill[] SkillList;
    public string Drop;
    public string HitBody;
    public string HitMove;
    public string HitHands;
    public string HitVital;
    public int CanCapture;

    //玩家一出生就获得百毒不侵buff

    public BattleUnitInfo info;

    public void InitBattle(){
        info.Init(Avatar,Name);
    }

    public void LoseHp(int value){
        if (value <= Hp)
            Hp -= value;
        else
            Hp = 0;

        info.LoseHp(value, Hp / MaxHp);
    }

    public void AddHp(int value){
        if (value + Hp > MaxHp)
            Hp = MaxHp;
        else
            Hp += value;

        info.AddHp(value, Hp / MaxHp);
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

        info.LoseHp(value, Mp / MaxMp);
    }

    public void AddMp(int value)
    {
        if (value + Mp > MaxMp)
            Mp = MaxMp;
        else
            MaxMp += value;

        info.AddMp(value, Mp / MaxMp);
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

        info.UpdateMp(Mp / MaxMp);
    }

    public void LoseShield(int value){
        if (value <= Shield)
            Shield -= value;
        else
            Shield = 0;
        info.LoseShield(value, Shield / MaxHp);
    }

    public void AddShield(int value){
        Shield += value;
        info.AddShield(value, Shield / MaxHp);
    }


    public BattleUnit() {
        Id = 0;
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
        Shield = 0;

        SkillList = null;
        Drop = null;
        HitBody = "身体";
        HitMove = "双腿";
        HitHands = "双臂";
        HitVital = "头部";
        CanCapture = 0;
    }


    public BattleUnit(Npc npc, int day, int title)
    {
        NpcModel thisModel = LoadTxt.Instance.ReadNpcModel(npc.Model);
        NpcTitle thisTitle = LoadTxt.Instance.ReadNpcTitle(title);

        Id = npc.Id;
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
        Shield = 0;

        SkillList = new Skill[npc.Skills.Length];
        for (int i = 0; i < SkillList.Length; i++)
        {
            SkillList[i] = LoadTxt.Instance.ReadSkill(npc.Skills[i]);
        }

        Drop = "100|1";
        HitBody = "身体";
        HitMove = "肢体";
        HitHands = "肢体";
        HitVital = "要害";
        CanCapture = 0;
    }
}
