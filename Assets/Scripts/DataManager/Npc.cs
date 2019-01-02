
public class Npc
{
    public int Id;
    public string Name;
    public string Desc;
    public int Level;
    public int LevelInc;
    public int Gender;
    public int Dialogues;
    public int Model;
    public int Image;
    public int[] Skills;
    public int[] Friends;
    public int Mate;
    public int[] Enemies;
    public string[] NickNames;

    public int mapUnitType{ get { return Id + 1000; }}

    public Npc(int id, string name, string desc, int level, int levelInc, int gender, int dialogues, int model, int image, int[] skills, int[] friends, int mate, int[] enemies, string[] nickNames)
    {
        Id = id; Name = name; Desc = desc; Level = level; LevelInc = levelInc; Gender = gender; Dialogues = dialogues; Model = model; Image = image; Skills = skills; Friends = friends; Mate = mate; Enemies = enemies; NickNames = nickNames;
    }

}

public class NpcModel{
    public int Id;
    public int Hp;
    public int HpInc;
    public int Mp;
    public int MpInc;
    public int Atk;
    public int AtkInc;

    public NpcModel(int id, int hp, int hpInc, int mp, int mpInc, int atk, int atkInc)
    {
        Id = id; Hp = hp; HpInc = hpInc; Mp = mp; MpInc = mpInc; Atk = atk; AtkInc = atkInc;
    }
}

public class NpcTitle{
    public int Id;
    public string Name;
    public int HpBonus;
    public int MpBonus;
    public int AtkBonus;
    public int DefBonus;
    public int SpeedBonus;

    public NpcTitle(int id, string name, int hpBonus, int mpBonus, int atkBonus, int defBonus, int speedBonus)
    {
        Id = id; Name = name; HpBonus = hpBonus; MpBonus = mpBonus; AtkBonus = atkBonus; DefBonus = defBonus; SpeedBonus = speedBonus;
    }
}

public class Dialogue{
    public int Id;
    public string[] Questions;
    public string[] Answers;
    public string[] Actions;//0Nothing,1Battle

    public Dialogue(int id,string[] questions,string[] answers,string[] actions){
        Id = id;
        Questions = questions;
        Answers = answers;
        Actions = actions;
    }
}