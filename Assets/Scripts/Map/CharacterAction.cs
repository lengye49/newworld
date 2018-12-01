using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CharacterAction : MonoBehaviour
{
    private static CharacterAction _instance;
    public static CharacterAction Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<CharacterAction>();
            }
            return _instance;
        }
    }

    private bool IsMoving;
    private GameObject _mask;
    private CharacterAnimation _animation;
    private float _moveInterval = 0.5f;
    private float z = -10f;
    private List<Vector2> _path;

    private void Awake()
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

    public void SetPosition(Vector2 pos){
        Debug.Log("Player Position = " + pos);
        transform.DOLocalMove(new Vector3(pos.x, pos.y, z), 0.01f, true);
        CameraAction.Instance.CameraSetPos(pos);
    }

    public void MoveToPos(List<Vector2> path){
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

        Vector3 nextStep = new Vector3(_path[1].x, _path[1].y, z);
        _path.RemoveAt(1);
        transform.DOLocalMove(nextStep, _moveInterval, false);
        CameraAction.Instance.CameraMovingAlong(nextStep, _moveInterval);
        _animation.Run();

        StartCoroutine(ContinueMoving());
    }

    IEnumerator ContinueMoving(){
        yield return new WaitForSeconds(_moveInterval);
        Moving();
    }

}
