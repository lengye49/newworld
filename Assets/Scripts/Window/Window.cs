using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour
{
    protected void OpenWindow(){
        gameObject.SetActive(true);
    }

    public void CloseWindow(){
        gameObject.SetActive(false);
    }
    
}
