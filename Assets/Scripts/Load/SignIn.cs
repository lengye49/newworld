using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignIn : MonoBehaviour {
    public Text userName;
    public Text userPassword;

    public void Login(){
        string u = userName.text;
        string p = userPassword.text;
        if (DataManager.Instance.UserCheck(u, p))
            GetComponentInParent<StartGame>().GoSelectProgress("SignIn");
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
