using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignIn : MonoBehaviour {
    public InputField userName;
    public InputField userPassword;

    public void Refresh(){
        userName.text = "";
        userPassword.text = "";
        Debug.Log("Refresh");
    }

    public void Login(){
        string u = userName.text;
        string p = userPassword.text;
        if (DataManager.Instance.UserCheck(u, p))
            GetComponentInParent<StartGame>().GoSelectProgress();
        else
            Debug.Log("用户名或密码错误！");
    }
	
    public void Register(){
        GetComponentInParent<StartGame>().GoRegister();
    }

    void Clear(){
        userName.text = "";
        userPassword.text = "";
    }

}
