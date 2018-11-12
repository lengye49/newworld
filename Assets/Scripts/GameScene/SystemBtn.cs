using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBtn : MonoBehaviour {

    public GameObject knapscakPanel;
    public GameObject backpackPanel;
    public GameObject characterPanel;
    public GameObject settingsPanel;
    public GameObject worldMapPanel;
    public GameObject skillPanel;

    //0消失 1中间 2左边 3右边
    private bool isShowKnapscak = false;
    private bool isShowBackpack = false;
    private bool isShowCharacter = false;
    private bool isShowSettings = false;
    private bool isShowWorldMap = false;
    private bool isShowSkill = false;

   
    //所有关闭按钮均指向全部关闭
    public void OnCloseBtn()
    {
        CloseAll();
    }

    void CloseAll()
    {
        if (isShowKnapscak)
            CloseKnapscak();
        if (isShowBackpack )
            CloseBackpack();
        if (isShowCharacter )
            CloseCharacter();
        if (isShowSettings )
            CloseSettings();
        if (isShowWorldMap)
            CloseWorldMap();
        if (isShowSkill )
            CloseSkill();
    }


    public void OnKnapscakBtn()
    {
        if (isShowKnapscak)
        {
            CloseKnapscak();
            if (isShowCharacter)
                PanelController.Instance.MoveToCenter(characterPanel);
            if (isShowBackpack)
                CloseBackpack();
        }
        else
        {
            if (isShowSkill)
            {
                CloseSkill();
                PanelController.Instance.MoveInRight(knapscakPanel);
            }else if(isShowCharacter){
                PanelController.Instance.MoveToLeft(characterPanel);
                PanelController.Instance.MoveInRight(knapscakPanel);
            }else{
                CloseAll();
                PanelController.Instance.MoveIn(knapscakPanel);
            }
            isShowKnapscak = true;
        }
    }      

    void CloseKnapscak(){
        isShowKnapscak = false;
        PanelController.Instance.MoveOut(knapscakPanel);
    }

    public void OnBackpackBtn(){
        if (isShowBackpack)
        {
            CloseBackpack();
            PanelController.Instance.MoveToCenter(knapscakPanel);
        }
        else
        {
            if (isShowCharacter)
            {
                CloseCharacter();
                PanelController.Instance.MoveInLeft(backpackPanel);
            }
            else
            {
                PanelController.Instance.MoveToRight(knapscakPanel);
                PanelController.Instance.MoveInLeft(backpackPanel);
            }
            isShowBackpack = true;
        }
    }
    void CloseBackpack(){
        isShowBackpack = false;
        PanelController.Instance.MoveOut(backpackPanel);
    }


    public void OnCharacterBtn(){
        Debug.Log("OnCharacter");
        if(isShowCharacter){
            CloseCharacter();
            if (isShowSkill)
                PanelController.Instance.MoveToCenter(skillPanel);
            if(isShowKnapscak)
                PanelController.Instance.MoveToCenter(knapscakPanel);
        }else{
            Debug.Log("OpenCharacter");
            if(isShowBackpack){
                CloseBackpack();
                PanelController.Instance.MoveToLeft(characterPanel);
            }else if(isShowKnapscak){
                PanelController.Instance.MoveToRight(knapscakPanel);
                PanelController.Instance.MoveInLeft(characterPanel);
            }else if(isShowSkill){
                PanelController.Instance.MoveToRight(skillPanel);
                PanelController.Instance.MoveInLeft(characterPanel);
            }else{
                CloseAll();
                PanelController.Instance.MoveIn(characterPanel);
            }
            isShowCharacter = true;
        }
    }
    void CloseCharacter(){
        Debug.Log("CloseCharacter"); 
        isShowCharacter = false;
        PanelController.Instance.MoveOut(characterPanel);
    }

    public void OnSkillBtn(){
        if(isShowSkill){
            CloseSkill();
            if (isShowCharacter)
                PanelController.Instance.MoveToCenter(characterPanel);
        }else{
            if(isShowCharacter){
                if(isShowBackpack){
                    CloseBackpack();
                }else{
                    PanelController.Instance.MoveToLeft(characterPanel);
                }
                PanelController.Instance.MoveInRight(skillPanel);
            }else{
                CloseAll();
                PanelController.Instance.MoveIn(skillPanel);
            }
            isShowSkill = true;
        }
    }

    void CloseSkill(){
        isShowSkill = false;
        PanelController.Instance.MoveOut(skillPanel);
    }


    public void OnSettingsBtn(){
        if (isShowSettings)
        {
            CloseSettings();
        }
        else
        {
            CloseAll();
            PanelController.Instance.MoveIn(settingsPanel);
            isShowSettings = true;
        }
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
            PanelController.Instance.MoveIn(worldMapPanel);
            isShowWorldMap = true;
        }
    }
    void CloseWorldMap()
    {
        isShowWorldMap = false;
        PanelController.Instance.MoveOut(worldMapPanel);
    }


}
