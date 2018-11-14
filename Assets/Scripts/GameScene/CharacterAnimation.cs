﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimation : MonoBehaviour {

    private Image ImageSource;
    private int mCurFrame = 0;
    private float mDelta = 0;

    private int startFrame;
    private int endFrame;

    public float FPS = 5;
    public List<Sprite> SpriteFrames;

    private bool IsPlaying = true;
    private bool isChanged = false;

    private float t;

    void Awake()
    {
        ImageSource = GetComponent<Image>();
    }

    void Start()
    {
        Rest();
    }

    public void Rest()
    {
        isChanged = true;
        startFrame = 0;
        endFrame = 3;
        FPS = 4;
    }

    public void Run()
    {
        isChanged = true;
        startFrame = 4;
        endFrame = 11;
        FPS = 8;
    }

    private void SetSprite(int idx)
    {
        ImageSource.sprite = SpriteFrames[idx];
        //该部分为设置成原始图片大小，如果只需要显示Image设定好的图片大小，注释掉该行即可。
        ImageSource.SetNativeSize();
    }

    void Update()
    {

        if (!IsPlaying || startFrame == endFrame)
        {
            return;
        }

        mDelta += Time.deltaTime;

        if (mDelta > 1 / FPS)
        {
            mDelta = 0;

            mCurFrame++;

            if (mCurFrame > endFrame)
            {
                mCurFrame = startFrame;
            }

            if (isChanged)
            {
                mCurFrame = startFrame;
                isChanged = false;
            }

            SetSprite(mCurFrame);
        }
    }
}
