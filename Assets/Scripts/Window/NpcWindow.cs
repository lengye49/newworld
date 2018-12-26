using UnityEngine.UI;
public class NpcWindow : Window
{
    private Text NameTxt;
    private Text DescTxt;
    private Text DialogueTxt;
    private Button[] ChoiceBtns;
    private Text LeaveTxt;
    public void ShowWindow(Npc npc)
    {
        NameTxt.text = npc.Name;
        DescTxt.text = npc.Desc;
        DialogueTxt.text = npc.Dialogues.ToString();
        SetUpChoices(npc.Dialogues);
        LeaveTxt.text = "告辞";
    }

    void SetUpChoices(int[] choices)
    {
        for (int i = 0; i < ChoiceBtns.Length; i++)
        {
            if (i < choices.Length)
            {
                ChoiceBtns[i].gameObject.name = choices[i].ToString();
                ChoiceBtns[i].GetComponentInChildren<Text>().text = choices[i].ToString();
            }
            else
            {
                ChoiceBtns[i].gameObject.SetActive(false);
            }
        }
    }
}
