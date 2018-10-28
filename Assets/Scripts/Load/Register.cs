using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField userName;
    public InputField pwdFirst;
    public InputField pwdSecond;

    public void Refresh()
    {
        userName.text = "";
        pwdFirst.text = "";
        pwdSecond.text = "";
        Debug.Log("Refresh");
    }

    public void Confirm(){
        string u = userName.text;
        string p1 = pwdFirst.text;
        string p2 = pwdSecond.text;

        if (u == "" || p1 == "" || p2 == "")
        {
            Debug.Log("账号密码不能为空！");
            return;
        }
        if (p1 != p2)
        {
            Debug.Log("两次输入的密码不同！");
            return;
        }

        bool success = DataManager.Instance.Register(u, p1);

        if(success){
            GetComponentInParent<StartGame>().GoCreateCharacterPanel("Register");
        }
    }

    public void Cancel(){
        GetComponentInParent<StartGame>().GoSignIn("Register");
    }


}
