/*
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
    public int Power;
    public int PowerInc;
    public int BuffType;
    public int BuffParam;
    public int BuffParamInc;
}

public enum SkillType
{
    Mental,
    Attack,
    SoulAttack,
    AddShield,
    AddBuff,
}