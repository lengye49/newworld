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
    void Start()
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
    }

    void UpdateEndTime(){
        endTime.text = "开始战斗";
    }

    string GetEndTimeStr(){
        return "";
    }

    void SetPoint()
    {

        timeBarEnd.text = maxTime.ToString();
        float myPointX = 800f * myNextTurn / (float)maxTime - 400f;
        float enemyPointX = 800f * enemyNextTurn / (float)maxTime - 400f;
        myPoint.transform.DOLocalMoveX(myPointX, 0.5f);
        enemyPoint.transform.DOLocalMoveX(enemyPointX, 0.5f);
    }
}
