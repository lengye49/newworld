using UnityEngine.UI;
public class MapEventWindow : Window
{
    private Text NameTxt;
    private Text DescTxt;
    private Button[] ChoiceBtns;

    private void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        NameTxt = texts[0];
        DescTxt = texts[1];
        ChoiceBtns = GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponentsInChildren<Button>();
    }

    public void ShowWindow(MapEvent _event){
        OpenWindow();

        //NameTxt.text = npc.Name;
        DescTxt.text = _event.Desc;
        //DialogueTxt.text = npc.Dialogues.ToString();
        //SetUpChoices(npc.Dialogues);
    }

    void SetUpChoices(int[] choices){
        for (int i = 0; i < ChoiceBtns.Length;i++){
            if(i<choices.Length){
                ChoiceBtns[i].gameObject.name = choices[i].ToString();
                ChoiceBtns[i].GetComponentInChildren<Text>().text = choices[i].ToString();
            }
            else{
                ChoiceBtns[i].gameObject.SetActive(false);
            }
        }
    }
}
