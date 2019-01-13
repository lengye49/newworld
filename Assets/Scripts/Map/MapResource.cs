using UnityEngine;
using System.Collections.Generic;

public struct MapResource{
    public ResourceType Type;
    public Dictionary<int, int> Rewards;
}

public enum ResourceType{
    杨树=100,
    杉树,
    矿石=200,
    土堆,
    铁矿石,
    精铁矿,
    灵石矿,
}
