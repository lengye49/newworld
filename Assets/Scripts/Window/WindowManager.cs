using UnityEngine;
using System.Collections;

public class WindowManager : MonoBehaviour
{
    private static WindowManager _instance;
    public static WindowManager Instance{
        get{
            if(_instance==null){
                _instance = GameObject.FindWithTag("WindowManager").GetComponent<WindowManager>();
            }
            return _instance;
        }
    }


    public NpcWindow _NpcWindow;
    public MapTreasureWindow _MapTreasureWindow;
    public MapPortalWindow _MapPortalWindow;
    public MapPickableItemWindow _MapPickableItemWindow;
    public MapEventWindow _MapEventWindow;

    public void ShowWindow(Npc npc){
        _NpcWindow.ShowWindow(npc);
    }

    public void ShowWindow(MapTreasure treasure){
        _MapTreasureWindow.ShowWindow(treasure);
    }

    public void ShowWindow(MapPortal portal)
    {
        _MapPortalWindow.ShowWindow(portal);
    }

    public void ShowWindow(MapPickableItem pickableItem)
    {
        _MapPickableItemWindow.ShowWindow(pickableItem);
    }

    public void ShowWindow(MapEvent _event){
        _MapEventWindow.ShowWindow(_event);
    }
}
