using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraAction : MonoBehaviour
{
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private static CameraAction _instance;
    public static CameraAction Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Camera.main.GetComponent<CameraAction>();
            }
            return _instance;
        }
    }

    public void SetBorders(int rows,int columns){
        if(columns <=38)
        {
            xMin = 0f;
            xMax = 0f;
        }else{
            xMin = -0.4622f * columns + 8.88f;
            xMax = 0.4622f * columns - 8.88f;
        }

        if(yMin<=18){
            yMin = 0f;
            yMax = 0f;
        }else{
            yMin = -0.54f * rows + 5f;
            yMax = 0.54f * rows - 5f;
        }
    }

    public void CameraMovingAlong(Vector3 pos,float t){
        Vector2 targetPos = new Vector2(pos.x, pos.y);
        Vector2 p = AdjustPos(targetPos);
        transform.DOLocalMove(targetPos, t, false);
    }

    public void CameraSetPos(Vector2 pos){
        Vector2 p = AdjustPos(pos);
        transform.DOLocalMove(p, 0.01f, false);
    }

    Vector2 AdjustPos(Vector2 org){
        float x = org.x;
        float y = org.y;
        x = x < xMin ? xMin : x;
        x = x > xMax ? xMax : x;
        y = y < yMin ? yMin : y;
        y = y > yMax ? yMax : y;
        return new Vector2(x, y);
    }

}
