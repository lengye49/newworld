using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleEnter : MonoBehaviour {

    private Transform left;
    private Transform right;
    public void Show()
    {
        transform.localPosition = Vector3.zero;
        left = GetComponentsInChildren<Image>()[0].transform;
        right = GetComponentsInChildren<Image>()[1].transform;
        left.localPosition = new Vector3(-500f, 0, 0);
        right.localPosition = new Vector3(500f, 0, 0);

        Move(left, -150f);
        Move(right, 150f);

        StartCoroutine(DestroyThis());
    }

    void Move(Transform t,float x)
    {
        Tweener tweener = t.DOLocalMoveX(x, 0.8f);
        tweener.SetEase(Ease.InOutBack);
        tweener.OnComplete(CompleteMoving);
    }

    void CompleteMoving(){
        //Debug.Log("Tween over");
    }

    IEnumerator DestroyThis(){
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
