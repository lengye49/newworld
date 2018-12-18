using UnityEngine;
using UnityEngine.UI;

public class WarningTip : Singleton<WarningTip>
{
    /// <summary>
    /// Shows the tip.
    /// </summary>
    /// <param name="colorType">0Black,1Green,2Red,3Blue,4Grey</param>
    /// <param name="content">Content.</param>
    /// <param name="parent">Position.</param>
    public void ShowTip(int colorType, string content, Transform parent)
    {
        GameObject f = Instantiate(Resources.Load("Warning/ShortWarning")) as GameObject;

        f.SetActive(true);
        f.transform.SetParent(parent);
        f.transform.localPosition = Vector3.zero;
        Text t = f.GetComponentInChildren<Text>();
        t.text = content;
        t.color = GetWarningColor(colorType);

        TweenController.Instance.PopIn(f.transform);
    }

    Color GetWarningColor(int colorType)
    {
        Color c = Color.black;

        if (colorType == 1)
            c = Color.green;
        else if (colorType == 2)
            c = Color.red;
        else if (colorType == 3)
            c = Color.blue;
        else if (colorType == 4)
            c = Color.grey;


        return c;
    }

}
