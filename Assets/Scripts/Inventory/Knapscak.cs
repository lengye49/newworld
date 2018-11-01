using UnityEngine;
using System.Collections;

public class Knapscak : Inventroy
{
    private static Knapscak _instance;
    public static Knapscak Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("KnapscakPanel").GetComponent<Knapscak>();
            }
            return _instance;
        }
    }
}
