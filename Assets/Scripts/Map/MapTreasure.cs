using System.Collections.Generic;

public class MapTreasure : MapUnit
{
    public int Key;//0no,1~99KeyList
    public Dictionary<int, int> Rewards;
    public MapEvent mapEvent;
    public string Name;

    public MapTreasure(){
        Id = 1;
        Name = "木制的箱子";
        Desc = "你发现了一个木制的箱子，看起来有些年头了。";

        Image = 1;
        Key = 0;
        mapEvent = null;
        Rewards = new Dictionary<int, int>();
        Rewards.Add(1, 1);
        Rewards.Add(2, 1);
    }
}
