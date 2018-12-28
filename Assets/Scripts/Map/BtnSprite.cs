using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BtnSprite : MonoBehaviour
{
    private SpriteRenderer render;
    private Vector3 pointerInPos;
    private Vector3 pointerOutPos;
    private bool isOnUi;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        isOnUi = IsPointerOverGameObject(Input.mousePosition);
        if (isOnUi)
            return;
        render.color = Color.red;
        pointerInPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        if (isOnUi)
            return;
        render.color = Color.white;
        pointerOutPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(pointerInPos + " ; " + pointerOutPos);
        if (Mathf.Abs(pointerInPos.x - pointerOutPos.x) < 0.2f && Mathf.Abs(pointerInPos.y - pointerOutPos.y) < 0.2f)
        {
            MapManager.Instance.MoveToPoint(this.gameObject.name);
        }
    }

    /// <summary>
    /// 判断鼠标点击位置是否是UI
    /// </summary>
    /// <param name="screenPosition">Screen position.</param>
    public bool IsPointerOverGameObject(Vector2 screenPosition){
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
