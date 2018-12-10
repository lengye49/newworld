using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //接口实现的方法，鼠标覆盖时触发
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        if (this.transform.childCount > 0)
        {
            //显示提示框
        }
    }

    //接口实现的方法，鼠标离开时触发
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        if (this.transform.childCount > 0)
        {
            //隐藏提示框
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)//为虚函数，方便子类EquipmentSlot重写
    {
    }
}
