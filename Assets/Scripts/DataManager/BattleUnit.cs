
public class BattleUnit
{
    public string name;
    public int id;
    public int level;
    public float maxHp;
    public float hp;
    public int spirit;
    public float atk;
    public float def;
    public float hit;
    public float dodge;
    public float speed;
    public float range;
    public Skill[] skillList;
    public string drop;
    public int vitalSensibility;
    public string hit_Body;
    public string hit_Move;
    public string hit_Vital;
    public int canCapture;
    public float castSpeedBonus;
    public int mapOpen;
    public int renown;
    public string avatar;

    public BattleUnit(Unit u,int level,int title){
        this.id = u.id;
        this.name = u.name;
        //怪物实力随等级增强
        this.level = u.level;
        //      Debug.Log (this.level);

        //Unit.UnitModel md = new m.model);

        //this.hp = md.hp + md.hp_inc * (this.level - 1);
        //thisMaxHp = this.hp;
        //this.spirit = m.spirit;
        //this.atk = md.atk + md.atk_inc * (this.level - 1);
        ////      Debug.Log("ThisthisInitAtk = " + this.atk);
        //this.def = md.def + md.def_inc * (this.level - 1);
        //this.hit = md.hit;
        //this.dodge = md.dodge;
        //this.speed = m.speed;
        //this.range = m.range;
        ////        print(m.range);
        //this.castSpeedBonus = 0;
        //this.skillList = m.skillList;
        //this.drop = m.drop;
        //this.vitalSensibility = m.vitalSensibility;
        //this.hit_Body = m.bodyPart[0];
        //this.hit_Vital = m.bodyPart[1];
        //this.hit_Move = m.bodyPart[2];
        //this.canCapture = m.canCapture;
        //this.mapOpen = m.mapOpen;
        //this.renown = m.renown;


        //MonsterTitle mt = LoadTxt.GetMonsterTitle(titleIndex);
        //enemy.name += "[" + mt.title + "]";
        //enemy.hp *= 1.0f + mt.hpBonus;
        //enemyMaxHp = enemy.hp;
        //enemy.atk *= 1.0f + mt.atkBonus;
        //enemy.def *= 1.0f + mt.defBonus;
        //enemy.dodge *= 1.0f + mt.dodgeBonus;
        //enemy.speed *= (1.0f + mt.speedBonus);
        //enemy.castSpeedBonus = mt.attSpeedBonus;
    }
}
