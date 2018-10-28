using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PanelController : Singleton<PanelController> {

    public void MoveIn(GameObject g) {
        g.SetActive(true);
        g.transform.localPosition = Vector2.zero;
    }

    public void MoveOut(GameObject g) {
        g.SetActive(false);
    }
}
