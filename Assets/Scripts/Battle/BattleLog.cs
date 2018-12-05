using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleLog : MonoBehaviour
{
    private Text[] battleLogs;
    private int maxIndex;

    private void Start()
    {
        battleLogs = GetComponentsInChildren<Text>();
        maxIndex = battleLogs.Length - 1;
    }

    public void Init()
    {
        for (int i = 0; i < battleLogs.Length; i++)
        {
            battleLogs[i].text = "";
        }
    }

    /// <summary>
    /// Adds new battle log.
    /// </summary>
    /// <param name="s">S.</param>
    /// <param name="isGood">1Green 2Red 0Black.</param>
    public void AddLog(string s, int isGood)
    {
        for (int i = 0; i < maxIndex; i++)
        {
            battleLogs[i].text = battleLogs[i + 1].text;
            battleLogs[i].color = new Color(battleLogs[i + 1].color.r, battleLogs[i + 1].color.g, battleLogs[i + 1].color.b, battleLogs[i + 1].color.a - 0.1f);
        }

        battleLogs[maxIndex].text = "→" + s;
        if (isGood == 1)
            battleLogs[maxIndex].color = new Color(0f, 1f, 0f, 1f);
        else if (isGood == 2)
            battleLogs[maxIndex].color = new Color(1f, 0f, 0f, 1f);
        else
            battleLogs[maxIndex].color = new Color(1f, 1f, 1f, 1f);
    }
}
