using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    public GameObject signInPanel;
    public GameObject registerPanel;
    public GameObject selectProgressPanel;
    public GameObject createCharacterPanel;


	void Start () {
        MusicController.Instance.Play();
        GoSignIn();
	}
	
    public void GoSignIn(string lastPanel=""){
        if (lastPanel == "Register")
            PanelController.Instance.MoveOut(registerPanel);
        PanelController.Instance.MoveIn(signInPanel);
    }

    public void GoRegister(){
        PanelController.Instance.MoveOut(signInPanel);
        PanelController.Instance.MoveIn(registerPanel);
    }

    public void GoSelectProgress(string lastPanel){
        if(lastPanel=="SignIn")
            PanelController.Instance.MoveOut(signInPanel);
        if(lastPanel == "Register")
            PanelController.Instance.MoveOut(registerPanel);

        PanelController.Instance.MoveIn(registerPanel);
    }

    public void GoCreateCharacterPanel(){
        PanelController.Instance.MoveOut(registerPanel);
        PanelController.Instance.MoveIn(createCharacterPanel);
    }

    public void LoadGame(){
        GetComponent<LoadGameScene>().Load();
    }


   
}
