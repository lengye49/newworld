using UnityEngine;
using UnityEngine.UI;

public class WarningController : Singleton<WarningController>
{
    public void ShowShortWarning(int colorType, string content, Vector3 pos, bool shortTime = true)
    {
        GameObject f = Instantiate(Resources.Load("shortwarning")) as GameObject;

        Transform p = GameObject.Find("Canvas").transform;
        f.SetActive(true);
        f.transform.SetParent(p);
        f.transform.localPosition = pos;
        Text t = f.GetComponentInChildren<Text>();
        t.text = content;
        t.color = GetWarningColor(colorType);

        TweenController.Instance.PopIn(f.transform);
    }

    Color GetWarningColor(int colorType)
    {
        Color c = Color.black;
        if (colorType == 0)
            c = Color.black;
        else if (colorType == 1)
            c = Color.green;
        else if (colorType == 2)
            c = Color.red;

        return c;
    }

}
