using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PanelController : MonoBehaviour {

    private static PanelController instance;
    private PanelController(){}

    public static PanelController Instance{
        get{
            if(instance == null){
                instance = new PanelController();
            }
            return instance;
        }
    }

    public static void MoveIn(GameObject g) {
        g.SetActive(false);
    }

    public static void MoveOut(GameObject g) {
        g.SetActive(true);
        g.transform.localPosition = Vector2.zero;
    }
}
