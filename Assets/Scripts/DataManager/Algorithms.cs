using UnityEngine;
using System.Collections.Generic;
public static class Algorithms
{
    #region "通用"
    /// <summary>
    /// Gets the index by range.
    /// </summary>
    /// <returns>The index by range.</returns>
    /// <param name="min">整数，包含.</param>
    /// <param name="max">整数，不包含.</param>
    public static int GetIndexByRange(int min, int max)
    {
        return Random.Range(min, max);
    }

    /// <summary>
    /// Gets the index by range.
    /// </summary>
    /// <returns>The index by range.</returns>
    /// <param name="min">浮点数，包含.</param>
    /// <param name="max">浮点数，包含.</param>
    public static float GetIndexByRange(float min, float max)
    {
        return Random.Range(min, max);
    }

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

    /// <summary>
    /// item|num|pro
    /// </summary>
    /// <returns>The reward.</returns>
    /// <param name="items">Items.</param>
    /// <param name="pros">Pros.</param>
    public static Dictionary<int, int> GetReward(Dictionary<int, int> items, float[] pros)
    {

        Dictionary<int, int> r = new Dictionary<int, int>();
        if (items.Count != pros.Length)
        {
            return r;
        }

        int i = 0;
        foreach (int key in items.Keys)
        {
            if (pros[i] >= 1)
            {
                if (r.ContainsKey(key))
                    r[key] += items[key];
                else
                    r.Add(key, items[key]);
            }
            else
            {
                float rand = Random.Range(0f, 1f);
                int num = 0;
                for (int j = 0; j < items[key]; j++)
                {
                    if (rand < pros[i] * (j + 1f))
                    {
                        num = items[key] - j;
                        break;
                    }
                }
                if (num >= 1)
                {
                    if (r.ContainsKey(key))
                        r[key] += num;
                    else
                        r.Add(key, num);
                }
            }
            i++;
        }

        return r;
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
            dam *= (s.power / 100f);
        }
        return (int)dam;
    }


    #endregion
}
