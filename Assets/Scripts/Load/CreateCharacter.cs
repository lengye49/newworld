using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter : MonoBehaviour {


    public void ConfirmStart(){
        GetComponentInParent<StartGame>().LoadGame("CreateCharacter");
    }
}
