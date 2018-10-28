using UnityEngine;
using System.Collections;

public class SelectProgress : MonoBehaviour
{
    public void Continue(){
        //Read Memmory
        //Enter game scene.
        GetComponentInParent<StartGame>().LoadGame("SelectProgress");
    }

    public void NewGame()
    {
        //Clear Memmory
        //Start a new game.
        //Enter create character panel.
        GetComponentInParent<StartGame>().GoCreateCharacterPanel("SelectProgress");
    }
}
