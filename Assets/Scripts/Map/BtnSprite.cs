using UnityEngine;
using System.Collections;

public class BtnSprite : MonoBehaviour
{
    private SpriteRenderer render;
    private Vector3 pointerInPos;
    private Vector3 pointerOutPos;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        render.color = Color.red;
        pointerInPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        render.color = Color.white;
        pointerOutPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(pointerInPos + " ; " + pointerOutPos);
        if (Mathf.Abs(pointerInPos.x - pointerOutPos.x) < 0.2f && Mathf.Abs(pointerInPos.y - pointerOutPos.y) < 0.2f)
        {
            MapManager.Instance.MoveToPoint(this.gameObject.name);
        }
    }

}
