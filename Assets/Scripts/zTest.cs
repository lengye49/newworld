using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class zTest : MonoBehaviour {

    void Start()
    {
        //TestInventory();
    }

    void TestInventory()
    {
        Debug.Log("Inventory Test************Start");
        Knapscak.Instance.StoreItem(1);
        Debug.Log("Inventory Test************END");
    }

    void TestStringBuilder(){
        Debug.Log("StringBuilder Test************Start");
        StringBuilder sb = new StringBuilder();
        sb.Append("Hello,hello,");
        sb.Append("You little thing.");
        Debug.Log(sb);
        Debug.Log("StringBuilder Test************END");
    }


    void Update () {
		
	}
}
