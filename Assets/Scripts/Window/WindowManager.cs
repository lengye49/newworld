using UnityEngine;
using System.Collections;

public class WindowManager : MonoBehaviour
{
    public NpcWindow _NpcWindow;
    public MapTreasureWindow _MapTreasureWindow;

    public void ShowWindow(Npc npc){
        _NpcWindow.ShowWindow(npc);
    }

    public void ShowWindow(MapTreasure treasure){
        _MapTreasureWindow.ShowWindow(treasure);
    }

    public void ShowWindow(MapPortal portal)
    {

    }

    public void ShowWindow(MapPickableItem pickableItem)
    {

    }

    public void ShowWindow(MapEvent _event){

    }
}
