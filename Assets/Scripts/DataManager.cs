using UnityEngine;
using System.Collections;

public class DataManager : Singleton<DataManager>
{
    public bool UserCheck(string u, string p){
        Debug.Log("New login: user = " + u + ", pwd = " + p);
        if (u != "" && p != "")
            return true;
        return false;
    }

    public bool Register(string u,string p){
        Debug.Log("New register: user = " + u + ", pwd = " + p);
        return true;
    }

    //必须是6的倍数，最大42
    public int GetKnapscakCount(){
        return 42;
    }

    //必须是7的倍数，最大49
    public int GetBackpackCount(){
        return 49;
    }
}
