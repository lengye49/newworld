using UnityEngine;
using System.Collections;

public class MapNameGenerator 
{
    //生成的是三级及以下地名，一级、二级手动配置
    public static string[] GenerateMapName(int count){
        string[] prefix = LoadTxt.Instance.ReadSingleTxtFile("village_prefix");
        string[] postfix = LoadTxt.Instance.ReadSingleTxtFile("village_postfix");
        string[] wilds = LoadTxt.Instance.ReadSingleTxtFile("wilds");
        string[] names = new string[count];
        int r1, r2, r3;
        for (int i = 0; i < count;i++){
            string str="";
            bool done = false;

            r1 = Random.Range(0, 10000);

            //10%的概率是地名
            if(r1<1000){
                while (!done)
                {
                    r2 = Random.Range(0, prefix.Length);
                    str = prefix[r2];
                    r3 = Random.Range(0, postfix.Length);
                    str += postfix[r3];
                    if (!prefix[r2].Contains(postfix[r3]))
                        done = true;
                }

            }
            //90%概率是荒野
            else{
                r2 = Random.Range(0, wilds.Length);
                str = wilds[r2];
            }
            names[i] = str;
        }
        return names;
    }




    public static string GetMapName(int idx){
        return "";
    }
}
