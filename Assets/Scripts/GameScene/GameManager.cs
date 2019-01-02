using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance{
        get{
            if(_instance==null)
            _instance= GameObject.Find("Canvas").GetComponent<GameManager>();
            return _instance;
        }
    }

    public BattleManager battle;
    public void StartBattle(){
        battle.gameObject.SetActive(true);
        battle.TestBattle();
    }
}
