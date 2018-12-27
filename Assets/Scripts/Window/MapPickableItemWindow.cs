using UnityEngine.UI;
public class MapPickableItemWindow : Window
{
    private Text NameTxt;
    private Text DescTxt;

    private Button[] ChoiceBtns;
    private Text LeaveTxt;
    public void ShowWindow(MapPickableItem item){
        //NameTxt.text = npc.Name;
        DescTxt.text = item._Item.Description;
        //DialogueTxt.text = npc.Dialogues.ToString();
        //SetUpChoices(npc.Dialogues);
        LeaveTxt.text = "离开";
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
