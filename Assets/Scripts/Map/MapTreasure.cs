using System.Collections.Generic;

public class MapTreasure : MapUnit
{
    public int key;//0no,1~99KeyList
    public Dictionary<int, int> rewards;
    public MapEvent mapEvent;

    public MapTreasure(){
        Id = 1;
        Image = 1;
        key = 0;
        mapEvent = null;
        rewards = new Dictionary<int, int>();
    }
}
