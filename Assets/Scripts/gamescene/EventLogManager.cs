using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EventLogManager : MonoBehaviour {
    private Text[] logs;
    private int maxCount = 9;

    void Start()
    {
        logs = this.gameObject.GetComponentsInChildren<Text>();
        ClearLogs();
        AddLog("我真的真的不是你的二大爷。");
    }

    void ClearLogs()
    {
        for (int i = 0; i < logs.Length; i++)
        {
            logs[i].text = string.Empty;
        }
    }

    public void AddLog(string s, int colorType = 0)
    {
        int logCount = (int)s.Length / maxCount + 1;
        Color c;
        if (colorType == 0)
            c = Color.white;
        else
            c = colorType == 1 ? Color.green : Color.red;

        string[] ss = new string[logCount];
        for (int i = 0; i < logCount; i++)
        {
            if (i == logCount - 1)
                ss[i] = s.Substring(i * maxCount, s.Length - i * maxCount);
            else
                ss[i] = s.Substring(i * maxCount, maxCount);
        }

        for (int i = logCount - 1; i >= 0; i--)
        {
            AddNewLog(ss[i], c);
        }
    }

    void AddNewLog(string s, Color c)
    {
        for (int i = logs.Length - 1; i > 0; i--)
        {
            logs[i].text = logs[i - 1].text;
            logs[i].color = logs[i - 1].color;
            logs[i].color = new Color(logs[i - 1].color.r, logs[i - 1].color.g, logs[i - 1].color.b, GetAlpha(i) / 255f);
        }
        logs[0].text = ">" + s;
        logs[0].color = c;
    }

    float GetAlpha(int index)
    {
        return 255f - index * (255f - 55f) / logs.Length;
    }

}
