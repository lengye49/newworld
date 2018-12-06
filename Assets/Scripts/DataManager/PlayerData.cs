using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerData : MonoBehaviour
{
    public static PlayerInfo _player;
    void Awake()
    {
        _player = new PlayerInfo();
        LoadData();
    }

    public static void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/info.dat");
        bf.Serialize(file, _player);
        file.Close();
    }

    void LoadData()
    {
        //测试，删除数据
        File.Delete(Application.persistentDataPath + "/info.dat");

        if (File.Exists(Application.persistentDataPath + "/info.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/info.dat", FileMode.Open);
            _player = (PlayerInfo)bf.Deserialize(file);
            file.Close();
            Debug.Log("加载成功！");
        }
        else
        {
            InitPlayerInfo();
        }
    }

    void InitPlayerInfo()
    {
        _player.AccountId = 0;
        _player.Name = "无名氏";
        _player.Country = 0;
        _player.Profession = 0;

        _player.Level = 1;
        _player.MaxAge = 100;
        _player.BirthYear = 0;
        _player.Intelligent = 1;
        _player.NimbusAffinity = 1;
        _player.Renown = 0;


        _player.MpRecover = 0;
        _player.StrengthRecover = 0;

        _player.MaxHp = 100;
        _player.Hp = 100;
        _player.MaxMp = 0;
        _player.Mp = 0;
        _player.MaxStrength = 100;
        _player.Strength = 100;
        _player.MaxSpirit = 100;
        _player.Spirit = 100;
        _player.Defense = 0;
        _player.Speed = 5;
        _player.Shield = 0;
        _player.Skills = new Dictionary<int, int>();

        _player.Backpack = new Dictionary<int, int>();
        _player.Knapscak = new Dictionary<int, int>();
    }
}

//不影响数值的内容用PlayerPrefs存储
//影响数值的内容用PlayerInfo存储
//还有1个地方应该存放经常要用的技能、物品等数据，避免每次都读取数据查询
[System.Serializable]
public class PlayerInfo
{
    //玩家信息
    public int AccountId;
    public string Name;
    public int Country;
    public int Profession;

    //角色信息-基础属性
    public int Level;
    public int MaxAge;
    public int BirthYear;
    public int Intelligent;//悟性
    public int NimbusAffinity;//灵气亲和度=灵根
    public int Renown;//声望

    //角色信息-回复属性
    public int MpRecover;
    public int StrengthRecover;

    //角色信息-战斗属性-战斗外
    public int MaxHp;
    public int Hp;
    public int MaxMp;
    public int Mp;
    public int MaxStrength;
    public int Strength;
    public int MaxSpirit;
    public int Spirit;
    public int Defense;//炼体的防御
    public int Speed;
    public int Shield;//基础护盾，用于防御偷袭
    public Dictionary<int,int> Skills;//技能、熟练度

    //角色信息-物品信息
    public Dictionary<int, int> Backpack;//背包
    public Dictionary<int, int> Knapscak;//乾坤袋
}


