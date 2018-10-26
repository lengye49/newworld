
using System.Collections;

public class DataManager : Singleton<DataManager>
{
    public bool UserCheck(string u, string p){
        if (u != "" && p != "")
            return true;
        return false;
    }
}
