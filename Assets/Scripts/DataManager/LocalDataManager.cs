using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LocalDataManager : MonoBehaviour
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
        //      File.Delete (Application.persistentDataPath + "/info.dat");
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


    }
}

//不影响数值的内容用PlayerPrefs存储
//影响数值的内容用PlayerInfo存储
[System.Serializable]
public class PlayerInfo
{

    //玩家信息
    public int AccountId;
    public string Name;
    public int Country;

    //角色信息






}
