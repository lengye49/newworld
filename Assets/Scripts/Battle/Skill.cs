﻿/*
0   心法
1   攻击
2   防御
*/
public class Skill{
    public int Id;
    public string Name;
    public string Desc;
    public int Type;
    public float Sing;//吟唱
    public float CD;
    public int CostType;//0内力 1力量 2生命 3精神 4生命上限 5寿命
    public int CostValue;
    public int Power;
    public int PowerFixed;
    public int BuffType;
    public int BuffParam;
    public int BuffParamInc;
    public int Range;
    public int Sprite;

    public Skill(int id,string name,string desc,int type,float sing,float cd,int costType,int costValue,int power,int powerInc,int buffType,int buffParam,int buffParamInc,int range,int sprite){
        Id = id;Name = name;Desc = desc;Type = type;Sing = sing;CD = cd;CostType = costType;CostValue = costValue;Power = power;PowerFixed = powerInc;BuffType = buffType;BuffParam = buffParam;BuffParamInc = buffParamInc;
        Range = range;
        Sprite = sprite;

    }
}

public enum SkillType
{
    Mental,//心法
    PhysicalAttack,//肉体攻击
    MagicalAttack,//法力攻击
    SoulAttack,//灵魂攻击
    AddShield,
    AddBuff,
}