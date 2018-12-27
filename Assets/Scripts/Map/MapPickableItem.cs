

public class MapPickableItem : MapUnit
{
    public Item _Item;
    public MapPickableItem(){
        Id = 1;
        Image = 1;
        _Item = LoadTxt.Instance.ReadItem(Id);
    }
}
