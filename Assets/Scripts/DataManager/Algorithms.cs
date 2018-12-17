using UnityEngine;
using System.Collections.Generic;
public static class Algorithms
{
    #region "权重随机"

    /// <summary>
    /// Gets the result by weight.
    /// </summary>
    /// <returns>The result by weight.</returns>
    /// <param name="weights">权重数组.</param>
    public static int GetResultByWeight(int[] weights)
    {

        int[] prop = new int[weights.Length];
        for (int i = 0; i < weights.Length; i++)
        {
            if (i == 0)
            {
                prop[i] = weights[i];
            }
            else
            {
                prop[i] = weights[i] + prop[i - 1];
            }
        }

        int r = (int)(Random.Range(0, prop[prop.Length - 1] + 1));

        for (int i = 0; i < prop.Length; i++)
        {
            if (r <= prop[i])
            {
                return i;
            }
        }

        return 0;
    }


    /// <summary>
    /// Gets the result by dic.
    /// </summary>
    /// <returns>The result by dic.</returns>
    /// <param name="d">id,权重</param>
    public static int GetResultByDic(Dictionary<int, int> d)
    {
        int[] r = new int[d.Count];
        int[] w = new int[d.Count];
        int index = 0;
        foreach (int key in d.Keys)
        {
            r[index] = key;
            w[index] = d[key];
            index++;
        }
        int i = GetResultByWeight(w);
        return r[i];
    }

    #endregion

    #region "拆分|组合字符串"

    /// <summary>
    /// 将字符串分割为整数数组，以“|”分隔
    /// </summary>
    /// <returns>The string to ints.</returns>
    /// <param name="str">String.</param>
    public static int[] SplitStrToInts(string str)
    {
        if (str.Contains("|"))
        {
            string[] ss = str.Split('|');
            int[] ints = new int[ss.Length];
            for (int i = 0; i < ss.Length; i++)
                ints[i] = int.Parse(ss[i]);
            return ints;
        }
        else
        {
            return new int[] { int.Parse(str) };
        }
    }


    public static string[] SplitStrToStrs(string str){
        if(str.Contains("|")){
            return str.Split('|');
        }else{
            return new string[] { str };
        }
    }

    public static Dictionary<int,int> SplitStrToDic(string str){
        Dictionary<int, int> dic = new Dictionary<int, int>();
        string[] ss;
        if (str.Contains(";")){
            ss = str.Split(';');
        }else{
            ss = new string[1] { str };
        }

        for (int i = 0; i < ss.Length;i++){
            string[] sss = ss[i].Split('|');
            dic.Add(int.Parse(sss[0]), int.Parse(sss[1]));
        }

        return dic;
    }

    #endregion

    #region "战斗"

    /// <summary>
    /// 根据攻击方命中、防御方闪避、防御方要害大小、攻击方精神判断命中、闪避、暴击.
    /// </summary>
    /// <returns><c>true</c> dodge:0 crit:2 hit:1 <c>false</c>.</returns>
    /// <param name="hit">攻击方命中.</param>
    /// <param name="dodge">防御方闪避.</param>
    /// <param name="vitalSensibility">防御方要害大小.</param>
    /// <param name="spirit">攻击方精神.</param>
    public static int IsDodgeOrCrit(float hit, float dodge, float vitalSensibility, float spirit)
    {
        float hitRate = 1 - dodge * dodge / (dodge + 3 * hit) / 100 * SpiritParam(spirit);
        float dodgeRate = 1 - hitRate;
        float critRate = hit * hit / (hit + dodge * 8) / 100 * (1 - vitalSensibility / 100f) * SpiritParam(spirit);
        //      Debug.Log ("Dodge:" + dodgeRate + " Crit:" + critRate);
        float r = Random.Range(0f, 1.0f);
        if (r < dodgeRate)
            return 0;
        else if (r < dodgeRate + critRate)
            return 2;
        else
            return 1;
    }


    /// <summary>
    /// Spirit parameter to hit&dodge.
    /// </summary>
    /// <returns>The parameter.</returns>
    /// <param name="spirit">Spirit.</param>
    public static float SpiritParam(float spirit)
    {
        return spirit / (spirit + (100 - spirit) / 10);
    }

    public static int CalculateDamage(float atk, float def, Skill s, int hitRate, bool isMyAtk)
    {
        float dam = atk * (1 - def / (ConfigData.DefParam + def));
        if (!isMyAtk)
        {
            dam *= (s.Power / 100f);
        }
        return (int)dam;
    }


    #endregion


}
