
using System.Collections.Generic;
public class ItemHandler 
{
    public static void UseItemInBattle(Item item, BattleUnit unit,BattleManager manager){
        if(item.Type == Item.ItemType.Consumable || item.Type == Item.ItemType.Tailsman)
        {
            if(item.Effect!=""){
                AddBattleEffect(item.Effect, unit);
            }
            if(item.Skill!=0){
                Skill skill = LoadTxt.Instance.ReadSkill(item.Skill);
                if (unit.UnitType == BattleUnitType.Player)
                    manager.PlayerCastSkill(skill);
                else
                    manager.EnemyCastSkill(skill);
            }
        }
    }

    static void AddBattleEffect(string effects,BattleUnit unit){
        Dictionary<int, int> eff = Algorithms.SplitStrToDic(effects);
        foreach(int key in eff.Keys){
            ChangeBattleProp(key, eff[key], unit);
        }
    }

    static void ChangeBattleProp(int type,int value,BattleUnit unit){
        switch(type){
            case 0:
                unit.AddHp(value);
                break;
            case 1:
                break;
            default:
                break;
        }
    }
}
