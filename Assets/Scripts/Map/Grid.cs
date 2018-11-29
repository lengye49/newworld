using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Grid : IComparable
{
    public int x;
    public int y;
    public int type;

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
        isOpen = true;
        isPicked = false;
        type = 0;
    }

    public bool IsWalkable()
    {
        //测试
        return true;

        //正式代码：
        //if (!this.isOpen)
        //    return false;
        //if (this.type == 0 || this.type == 1)
        //    return true;
        //return false;
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

