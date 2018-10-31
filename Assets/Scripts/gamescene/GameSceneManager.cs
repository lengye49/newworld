using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    public GameObject normalBackpackPanel;
    public GameObject hiddenBackpackPanel;
    public GameObject characterPanel;
    public GameObject settingsPanel;
    public GameObject worldMapPanel;

    private bool isShowBpNormal = false;
    private bool isShowBpHidden = false;
    private bool isShowCharacter = false;
    private bool isShowSettings = false;
    private bool isShowWorldMap = false;

   
    //所有关闭按钮均指向全部关闭
    public void OnCloseBtn()
    {
        CloseAll();
    }

    void CloseAll()
    {
        if (isShowBpNormal)
            CloseNormalBackpack();
        if (isShowBpHidden)
            CloseHiddenBackpack();
        if (isShowCharacter)
            CloseCharacter();
        if (isShowSettings)
            CloseSettings();
        if (isShowWorldMap)
            CloseWorldMap();
    }

    //乾坤袋和角色互斥，打开背包始终有两个页面。
    public void OnBackpackBtn(){
        if(isShowBpNormal)
        {
            CloseAll();
        }else{
            CloseAll();
            OpenNormalBackpack();
            OpenCharacter();
        }
    }      

    void OpenNormalBackpack(){
        isShowBpNormal = true;
        PanelController.Instance.MoveInRight(normalBackpackPanel);
    }
    void CloseNormalBackpack(){
        isShowBpNormal = false;
        PanelController.Instance.MoveOut(normalBackpackPanel);
    }
    void OpenHiddenBackpack(){
        CloseCharacter();
        isShowBpHidden = true;
        PanelController.Instance.MoveInLeft(hiddenBackpackPanel);
    }
    void CloseHiddenBackpack(){
        isShowBpHidden = false;
        PanelController.Instance.MoveOut(hiddenBackpackPanel);
    }
    void OpenCharacter(){
        isShowCharacter = true;
        PanelController.Instance.MoveInLeft(characterPanel);
    }
    void CloseCharacter(){
        isShowCharacter = false;
        PanelController.Instance.MoveOut(characterPanel);
    }


    public void OnSettingsBtn(){
        if (isShowSettings)
        {
            CloseSettings();
        }
        else
        {
            CloseAll();
            OpenSettings();
        }
    }
    void OpenSettings()
    {
        isShowSettings = true;
        PanelController.Instance.MoveIn(settingsPanel);
    }
    void CloseSettings(){
        isShowSettings = false;
        PanelController.Instance.MoveOut(settingsPanel);
    }

    public void OnWorldMapBtn()
    {
        if (isShowWorldMap)
        {
            CloseWorldMap();
        }
        else
        {
            CloseAll();
            OpenWorldMap();
        }
    }
    void OpenWorldMap()
    {
        isShowWorldMap = true;
        PanelController.Instance.MoveIn(worldMapPanel);
    }
    void CloseWorldMap()
    {
        isShowWorldMap = false;
        PanelController.Instance.MoveOut(worldMapPanel);
    }


}
