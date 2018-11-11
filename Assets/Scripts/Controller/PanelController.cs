using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PanelController : Singleton<PanelController> {

    private float leftX = -310f;
    private float rightX = 310f;

    public void MoveIn(GameObject g) {
        g.SetActive(true);
        g.transform.localPosition = Vector2.zero;
    }

    public void MoveInLeft(GameObject g){
        g.SetActive(true);
        g.transform.localPosition = new Vector2(leftX, 0);
    }

    public void MoveInRight(GameObject g){
        g.SetActive(true);
        g.transform.localPosition = new Vector2(rightX, 0);
    }

    public void MoveOut(GameObject g) {
        g.SetActive(false);
    }

    public void MoveToCenter(GameObject g){
        g.transform.localPosition = Vector2.zero;
    }

    public void MoveToLeft(GameObject g)
    {
        g.transform.localPosition = new Vector2(leftX, 0);
    }

    public void MoveToRight(GameObject g)
    {
        g.transform.localPosition = new Vector2(rightX, 0);
    }

}
