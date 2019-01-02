using UnityEngine.UI;
public class NpcWindow : Window
{
    private Text NameTxt;
    private Text DescTxt;
    private Text DialogueTxt;
    private Button[] ChoiceBtns;
    private Dialogue dialogue;

    private void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        NameTxt = texts[0];
        DescTxt = texts[1];
        DialogueTxt = texts[2];
        ChoiceBtns = GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponentsInChildren<Button>();
    }


    public void ShowWindow(Npc npc)
    {
        OpenWindow();
        NameTxt.text = npc.Name;
        DescTxt.text = npc.Desc;
        dialogue = LoadTxt.Instance.ReadDialogue(npc.Dialogues);

        DialogueTxt.text = dialogue.Questions[0];
        SetUpChoices();
    }

    void SetUpChoices()
    {
        for (int i = 0; i < ChoiceBtns.Length; i++)
        {
            if (i < dialogue.Questions.Length-1)
            {
                ChoiceBtns[i].gameObject.SetActive(true);
                ChoiceBtns[i].GetComponentInChildren<Text>().text = dialogue.Questions[i+1];
            }
            else
            {
                ChoiceBtns[i].gameObject.SetActive(false);
            }
        }
    }

    public void Answer(int index){
        DialogueTxt.text = dialogue.Answers[index];
        if(dialogue.Actions[index]!="0"){
            StartAction(dialogue.Actions[index]);
        }
    }

    void StartAction(string ac){
        GameManager.Instance.StartBattle();
    }
}
