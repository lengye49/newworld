using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    public GameObject signInPanel;
    public GameObject registerPanel;
    public GameObject selectProgressPanel;
    public GameObject createCharacterPanel;
    public GameObject loadGamePanel;

	void Start () {
        MusicController.Instance.Play();
        HideAll();
        GoSignIn();
	}

    void HideAll(){
        signInPanel.SetActive(false);
        registerPanel.SetActive(false);
        selectProgressPanel.SetActive(false);
        createCharacterPanel.SetActive(false);
        loadGamePanel.SetActive(false);
    }
	
    public void GoSignIn(string lastPanel=""){
        if (lastPanel == "Register")
            PanelController.Instance.MoveOut(registerPanel);
        PanelController.Instance.MoveIn(signInPanel);
        signInPanel.GetComponent<SignIn>().Refresh();
    }

    public void GoRegister(){
        PanelController.Instance.MoveOut(signInPanel);
        PanelController.Instance.MoveIn(registerPanel);
        registerPanel.GetComponent<Register>().Refresh();
    }

    public void GoSelectProgress()
    {
        PanelController.Instance.MoveOut(signInPanel);
        PanelController.Instance.MoveIn(selectProgressPanel);
    }

    public void GoCreateCharacterPanel(string lastPanel)
    {
        if (lastPanel == "Register")
            PanelController.Instance.MoveOut(registerPanel);
        if (lastPanel == "SelectProgress")
            PanelController.Instance.MoveOut(selectProgressPanel);
        PanelController.Instance.MoveIn(createCharacterPanel);
    }

    public void LoadGame(string lastPanel){
        if(lastPanel=="SelectProgress")
            PanelController.Instance.MoveOut(selectProgressPanel);
        if(lastPanel=="CreateCharacter")
            PanelController.Instance.MoveOut(createCharacterPanel);

        PanelController.Instance.MoveIn(loadGamePanel);
        loadGamePanel.GetComponent<LoadGameScene>().Load();
    }


   
}
