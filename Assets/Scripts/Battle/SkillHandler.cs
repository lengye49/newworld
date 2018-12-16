
using System.Collections;

public class SkillHandler 
{

    public static int CastSkill(Skill s, BattleUnit attacker, BattleUnit defender)
    {
        int power = 0;
        switch(s.Type){
            case 0:return 0; //心法
            case 1:
                power = s.CostValue * attacker.Strength / 10000 * s.Power + s.PowerFixed;
                return ExcuteAttackDamage(power, defender);
            case 2:
                power = s.CostValue * attacker.Mp / 10000 * s.Power + s.PowerFixed;
                return ExcuteAttackDamage(power, defender);
            case 3:
                power = s.CostValue * s.Power + s.PowerFixed;
                return ExcuteSoulAttackDamage(power, defender);
            case 4:
                power = s.CostValue * s.Power + s.PowerFixed;
                return AddBuff(s.BuffType,power, attacker);
            case 5:
                power = s.CostValue * attacker.Mp / 10000 * s.Power + s.PowerFixed;
                return SuckMp(power, attacker, defender);
        }

        return 1;
    }

    public static int ExcuteAttackDamage(int power,BattleUnit target)
    {
        int shield = target.HasShield(0);
        if (shield > 0)
        {
            if (power > shield)
            {
                target.RemoveBuff(0);
                power -= shield;
            }else{
                target.CostBuff(0,power);
                power = 0;
            }
        }

        if (power > 0)
            target.LoseHp(power);
            
        return power;
    }

    public static int ExcuteSoulAttackDamage(int power,BattleUnit target)
    {
        target.LoseHp(power);
        return power;
    }

    /// <summary>
    /// Shield会分种类
    /// </summary>
    /// <returns>The shield.</returns>
    /// <param name="power">Power.</param>
    /// <param name="target">Target.</param>
    public static int AddBuff(int buffType, int power,BattleUnit target){
        target.AddBuff(buffType,power);
        return power;
    }

    public static int SuckMp(int power,BattleUnit attacker,BattleUnit defender){
        power = power > defender.Mp ? defender.Mp : power;
        defender.LoseMp(power);
        attacker.AddMp(power);
        return power;
    }


    public static bool CanSkillCast(Skill s, BattleUnit attacker){
        switch(s.CostType){
            case 0:return attacker.Strength >= s.CostValue;
            case 1:return attacker.Mp >= s.CostValue;
            case 2:return attacker.Hp >= s.CostValue;
            case 3:return attacker.Spirit >= s.CostValue;
            case 4:return attacker.MaxAge >= s.CostValue;
            default: return false;
        }
    }

    public static void SkillCastCost(Skill s,BattleUnit attacker){
        switch (s.CostType)
        {
            case 0:attacker.Strength -= s.CostValue;break;
            case 1: attacker.Mp -= s.CostValue;break;
            case 2:attacker.Hp -= s.CostValue;break;
            case 3:attacker.Spirit -= s.CostValue;break;
            case 4:attacker.MaxAge -= s.CostValue;break;
            default:break;
        }
    }
}
