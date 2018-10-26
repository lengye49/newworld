using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    public Slider progressSlider;
    public Image fillImage;
    public Text loadingTxt;
    public Text loadingPercent;

    public void Load()
    {
        StartCoroutine(StartLoading());
    }

    IEnumerator StartLoading()
    {
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync(1);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }

        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }

    public void SetLoadingPercentage(int value)
    {
        float v = value / 100f;
        progressSlider.value = v;
        fillImage.color = new Color(0, Mathf.Max(0, (value + 0.5f) / 1f), 0f, 1f);

        if ((int)(v * 10) % 4 == 0)
        {
            loadingTxt.text = "加载中";
        }
        else if ((int)(v * 10) % 4 == 1)
        {
            loadingTxt.text = "加载中.";
        }
        else if ((int)(v * 10) % 4 == 2)
        {
            loadingTxt.text = "加载中..";
        }
        else
        {
            loadingTxt.text = "加载中...";
        }
        loadingPercent.text = value + "%";
    }
}
