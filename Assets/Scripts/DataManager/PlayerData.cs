//这个文件里的数据是要上传的


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
        Debug.Log("Loading PlayerData...");
        LoadData();
    }


    #region 存储/加载数据
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

    #endregion

    #region 初始化角色数据
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

        _player.BasicHp = 100;
        _player.Hp = 100;
        _player.BasicMp = 0;
        _player.Mp = 0;
        _player.BasicStrength = 100;
        _player.Strength = 100;
        _player.BasicSpirit = 100;
        _player.Spirit = 100;
        _player.BasicDefense = 0;
        _player.BasicSpeed = 5;

        _player.Skills = new Dictionary<int, int>();

        _player.Weapon = 0;
        _player.Armor = 0;
        _player.Shoes = 0;
        _player.Necklace = 0;
        _player.Waist = 0;
        _player.Bracelet = 0;

        _player.UpdateMaxValue();

        _player.BeiBao = new Dictionary<int, int>();
        _player.QianKunDai = new Dictionary<int, int>();
    }
    #endregion
}

//不影响数值的内容用PlayerPrefs存储
//影响数值的内容用PlayerInfo存储
//还有1个地方应该存放经常要用的技能、物品等数据，避免每次都读取数据查询
[System.Serializable]
public class PlayerInfo
{
    #region 玩家基础数据
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


    //角色信息-保存值
    public int BasicHp;
    public int Hp;
    public int BasicMp;
    public int Mp;
    public int BasicStrength;
    public int Strength;
    public int BasicSpirit;
    public int Spirit;
    public int BasicDefense;//炼体的防御
    public int BasicSpeed;

    //角色属性-计算值
    public int MaxHp { get; private set; }
    public int MaxMp { get; private set; }
    public int MaxStrength { get; private set; }
    public int MaxSpirit { get; private set; }
    public int Defence { get; private set; }
    public int Speed { get; private set; }
    public int Shield { get; private set; }
    public int MpRecover { get; private set; }
    public int StrengthRecover { get; private set; }
    public int CastSpeedBonus { get; private set; }//吟唱加速
    public int CastRangeBonus { get; private set; }//攻击范围增加

    //技能
    public Dictionary<int, int> Skills;//技能、熟练度



    //装备信息
    public int Weapon;
    public int Armor;
    public int Shoes;
    public int Necklace;
    public int Waist;
    public int Bracelet;

    //角色信息-物品信息
    public Dictionary<int, int> BeiBao;//背包
    public Dictionary<int, int> QianKunDai;//乾坤袋

    #endregion

    #region 数据方法
    public string PropertyInfo
    {
        get
        {
            return string.Format("生命：{0}\n法力：{1}\n力量：{2}\n精神：{3}\n护甲：{4}\n速度：{5}\n护盾：{6}\n法力回复：{7}\n力量回复：{8}", MaxHp, MaxMp, MaxStrength, MaxSpirit, Defence, Speed, Shield, MpRecover, StrengthRecover);
        }
    }

    public bool PutOnEquip(){
        return true;
    }

    public bool TakeOffEquip(){
        return true;
    }

    public bool UseItem(){
        return true;
    }

    //Todo 更新装备信息
    public void UpdateMaxValue()
    {
        MaxHp = BasicHp;
        MaxMp = BasicMp;
        MaxStrength = BasicStrength;
        MaxSpirit = BasicSpirit;
        Defence = BasicDefense;
        Speed = BasicSpeed;
        Shield = 0;
        MpRecover = 0;
        StrengthRecover = 0;
        CastSpeedBonus = 0;
        CastRangeBonus = 0;
    }

    public int ItemCountInBeiBao(int itemId)
    {
        if (BeiBao.ContainsKey(itemId))
            return BeiBao[itemId];
        return 0;
    }
    public int ItemCountInQianKunDai(int itemId)
    {
        if (QianKunDai.ContainsKey(itemId))
            return QianKunDai[itemId];
        return 0;
    }

    #endregion
}


