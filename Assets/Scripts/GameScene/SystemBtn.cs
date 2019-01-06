using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBtn : MonoBehaviour {

    public GameObject beibaoPanel;
    public GameObject qiankundaiPanel;
    public GameObject characterPanel;
    public GameObject settingsPanel;
    public GameObject worldMapPanel;
    public GameObject skillPanel;

    //0消失 1中间 2左边 3右边
    private bool isShowBeiBao = false;
    private bool isShowQianKunDai = false;
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
        if (isShowBeiBao)
            CloseBeiBao();
        if (isShowQianKunDai )
            CloseQianKunDai();
        if (isShowCharacter )
            CloseCharacter();
        if (isShowSettings )
            CloseSettings();
        if (isShowWorldMap)
            CloseWorldMap();
        if (isShowSkill )
            CloseSkill();
    }


    public void OnBeiBaoBtn()
    {
        if (isShowBeiBao)
        {
            CloseBeiBao();
            if (isShowCharacter)
                PanelController.Instance.MoveToCenter(characterPanel);
            if (isShowQianKunDai)
                CloseQianKunDai();
        }
        else
        {
            if (isShowSkill)
            {
                CloseSkill();
                PanelController.Instance.MoveIn(beibaoPanel);
            }else if(isShowCharacter){
                PanelController.Instance.MoveToLeft(characterPanel);
                PanelController.Instance.MoveInRight(beibaoPanel);
            }else{
                CloseAll();
                PanelController.Instance.MoveIn(beibaoPanel);
            }
            isShowBeiBao = true;
        }
    }      

    void CloseBeiBao(){
        isShowBeiBao = false;
        PanelController.Instance.MoveOut(beibaoPanel);
    }

    public void OnQianKunDaiBtn(){
        if (isShowQianKunDai)
        {
            CloseQianKunDai();
            PanelController.Instance.MoveToCenter(beibaoPanel);
        }
        else
        {
            if (isShowCharacter)
            {
                CloseCharacter();
                PanelController.Instance.MoveInLeft(qiankundaiPanel);
            }
            else
            {
                PanelController.Instance.MoveToRight(beibaoPanel);
                PanelController.Instance.MoveInLeft(qiankundaiPanel);
            }
            isShowQianKunDai = true;
        }
    }
    void CloseQianKunDai(){
        isShowQianKunDai = false;
        PanelController.Instance.MoveOut(qiankundaiPanel);
    }


    public void OnCharacterBtn(){
        Debug.Log("OnCharacter");
        if(isShowCharacter){
            CloseCharacter();
            if (isShowSkill)
                PanelController.Instance.MoveToCenter(skillPanel);
            if(isShowBeiBao)
                PanelController.Instance.MoveToCenter(beibaoPanel);
        }else{
            Debug.Log("OpenCharacter");
            if(isShowQianKunDai){
                CloseQianKunDai();
                PanelController.Instance.MoveToLeft(characterPanel);
            }else if(isShowBeiBao){
                PanelController.Instance.MoveToRight(beibaoPanel);
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
                if(isShowBeiBao){
                    CloseBeiBao();
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
