using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleTime : MonoBehaviour
{
    private Transform enemyPoint;
    private Transform playerPoint;
    private Text startTime;
    private Text endTime;

    private float startX = -275f;
    private float totalX = 550f;
    private float y = 58f;
    private float maxTime = 0f;

    // Use this for initialization
    void Awake()
    {
        enemyPoint = GetComponentsInChildren<Image>()[1].transform;
        playerPoint = GetComponentsInChildren<Image>()[2].transform;
        startTime = GetComponentsInChildren<Text>()[0];
        endTime = GetComponentsInChildren<Text>()[1];
    }

    public void Init()
    {
        enemyPoint.localPosition = new Vector2(startX, y);
        playerPoint.localPosition = new Vector2(startX, y);
        startTime.text = "开始战斗";
        UpdateEndTime();
    }

    public void UpdateBattleTime(float playerNextTurn,float enemyNextTurn){
        if (maxTime < 1f)
            maxTime = 20f;
        else if (Mathf.Max(playerNextTurn, enemyNextTurn) > maxTime * 0.9f)
            maxTime = maxTime * 3f;

        endTime.text = GetEndTimeStr(maxTime);

        float playerX = 800f * playerNextTurn / (float)maxTime - 400f;
        playerPoint.localPosition = new Vector2(playerX, 0.5f);
        float enemyX = 800f * enemyNextTurn / (float)maxTime - 400f;
        enemyPoint.localPosition = new Vector2(enemyX, 0.5f);
    }

    void UpdateEndTime(){
        endTime.text = "开始战斗";
    }

    string GetEndTimeStr(float t){
        return t + "s";
    }

}
