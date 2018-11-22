using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CharacterAction : MonoBehaviour
{

    private bool IsMoving;
    private GameObject _mask;
    private CharacterAnimation _animation;
    private float _moveInterval = 0.5f;
    private List<Vector3> _path;

    private void Start()
    {
        _animation = GetComponent<CharacterAnimation>();
        _mask = GameObject.FindGameObjectWithTag("PlayerMovingMask");
        IsMoving = false;
    }

    private void Update()
    {
        if (!IsMoving && _mask.activeSelf)
        {
            _mask.SetActive(false);
        }
        else if (IsMoving && !_mask.activeSelf)
        {
            _mask.SetActive(true);
        }
    }

    public void SetPosition(Vector3 pos){
        transform.DOLocalMove(pos, 0.01f, true);
    }

    public void MoveToPos(List<Vector3> path){
        _path = path;
        IsMoving = true;

        Moving();
    }

    void Moving(){
        if(_path.Count==1)
        {
            _animation.Rest();
            IsMoving = false;
            return;
        }

        Vector3 nextStep = _path[1];
        _path.RemoveAt(1);
        transform.DOLocalMove(nextStep, _moveInterval, false);
        _animation.Run();

        StartCoroutine(ContinueMoving());
    }

    IEnumerator ContinueMoving(){
        yield return new WaitForSeconds(_moveInterval);
        Moving();
    }

}
