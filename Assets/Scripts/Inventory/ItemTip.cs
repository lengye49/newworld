using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 提示框类
/// </summary>
public class ItemTip : MonoBehaviour
{

    private static ItemTip _instance;
    public static ItemTip Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("ItemTip").GetComponent<ItemTip>();
            }
            return _instance;
        }
    }

    private Text toolTipText;//提示框的父Text，主要用来控制提示框的大小
    private Text contentText;//提示框的子Text，主要用来显示提示
    private Vector3 offset = new Vector3(-5, -5, 0);

    void Awake()
    {
        toolTipText = this.GetComponent<Text>();
        contentText = this.transform.Find("Content").GetComponent<Text>();
    }

    //提示框的显示方法
    public void Show(string text)
    {
        gameObject.SetActive(true);
        this.toolTipText.text = text;
        this.contentText.text = text;
    }
    //提示框的隐藏方法
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //设置提示框自身的位置
    public void SetLocalPosition(Vector3 postion)
    {
        SetPivot(postion.x, postion.y);
        this.transform.localPosition = postion + offset;
    }

    void SetPivot(float x,float y){
        float xP = x < 0 ? 0f : 1f;
        float yP = y < 0 ? 0f : 1f;
        GetComponent<RectTransform>().pivot = new Vector2(xP, yP);
    }
}