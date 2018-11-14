using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Grid : IComparable
{
    public int x;
    public int y;
    public GridType type;

    public int g;//距离起点
    public int h;//距离终点
    public int f;//总值

    public bool isOpen;
    public bool isPicked;
    public Grid parent;

    public Grid(int x, int y)
    {
        this.x = x;
        this.y = y;
        isOpen = false;
        isPicked = false;
    }

    public bool IsWalkable()
    {
        if (!this.isOpen)
            return false;
        if (this.type == GridType.Road || this.type == GridType.Enter)
            return true;
        return false;
    }

    //升序，用于Sort方法
    public int CompareTo(object obj)
    {
        Grid grid = (Grid)obj;
        if (this.f < grid.f)
            return -1;
        if (this.f > grid.f)
            return 1;
        return 0;
    }

    public void Print()
    {
        Debug.Log(" = [" + x + "," + y + "]");
    }
}

public enum GridType
{
    Covered,
    Road,
    Boss,
    Monster,
    Event,
    Enter,
    Block,
}
