
public class BattleUnit
{
    public int Id;
    public string Name;
    public int Level;
    public float MaxHp;
    public float Hp;
    public int Spirit;
    public float Atk;
    public float Def;
    public float Hit;
    public float Dodge;
    public float Speed;
    public float Range;
    public Skill[] SkillList;
    public string Drop;
    public int VitalSensibility;
    public string HitBody;
    public string HitMove;
    public string HitVital;
    public int CanCapture;
    public float CastSpeedBonus;
    public int MapOpen;
    public int Renown;
    public string Avatar;

    public BattleUnit(Npc npc,int day,int title){
        NpcModel thisModel = LoadTxt.Instance.ReadNpcModel(npc.Model);
        NpcTitle thisTitle = LoadTxt.Instance.ReadNpcTitle(title);

        Id = npc.Id;
        Name = npc.Name;
        Level = npc.Level + (int)(day / npc.LevelInc);
        MaxHp = (thisModel.Hp + thisModel.HpInc * Level) * (thisTitle.HpBonus / 10000f + 1f);
        Hp = MaxHp;
        Spirit = 100;
        Atk = (thisModel.Atk + thisModel.AtkInc * Level) * (thisTitle.AtkBonus / 10000f + 1f);
        Def = 100f;
        Hit = 100f;
        Dodge = 100f;
        Speed = 100f * (thisTitle.SpeedBonus / 10000f + 1f);
        Range = 10f;

        SkillList = new Skill[npc.Skills.Length];
        for (int i = 0; i < SkillList.Length;i++){
            SkillList[i] = LoadTxt.Instance.ReadSkill(npc.Skills[i]);
        }

        Drop = "100|1";
        VitalSensibility = 100;
        HitBody = "身体";
        HitMove = "肢体";
        HitVital = "要害";
        CanCapture = 0;
        CastSpeedBonus = 0;
        MapOpen = 0;
        Renown = 0;
        Avatar = npc.Image.ToString();
    }
}
