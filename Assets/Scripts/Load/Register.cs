using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public Text userName;
    public Text pwdFirst;
    public Text pwdSecond;

    public void Confirm(){
        string u = userName.text;
        string p1 = pwdFirst.text;
        string p2 = pwdSecond.text;

        if (u == "" || p1 == "" || p2 == "")
            Debug.Log("账号密码不能为空！");
        if (p1 != p2)
            Debug.Log("两次输入的密码不同！");

    }

    public void ReturnToSignIn(){
        GetComponentInParent<StartGame>().GoSignIn("Register");
    }


}
