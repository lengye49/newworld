using UnityEngine;
using System.Collections;

public class Chest
{
    public int id;
    public string reward;
    public int sprite;
    public int mapUnitType{
        get { return id + 100; }
    }
}
