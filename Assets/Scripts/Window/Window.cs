using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour
{

    public void CloseWindow(){
        gameObject.SetActive(false);
    }
}
