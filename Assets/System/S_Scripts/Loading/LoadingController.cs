using System;
using NaughtyAttributes;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    [SerializeField] private Slider circleSlider;
    [SerializeField] private Slider horizonSlider;
    [SerializeField, Range(0.5f, 2f)] private float timeLoading;

    void UseTypeLoading(bool isHori)
    {
        circleSlider.gameObject.SetActive(!isHori);
        horizonSlider.gameObject.SetActive(isHori);
    }

    private void Awake()
    {
        if (circleSlider.gameObject.activeSelf)
        {
            Tween.UISliderValue(circleSlider, 1f, timeLoading, Ease.OutCirc);
        }
        else
        {
            Tween.UISliderValue(horizonSlider, 1f, timeLoading, Ease.OutCirc);
        }
    }
#if UNITY_EDITOR
    [Button()]
    public void UseCircleLoading()
    {
        UseTypeLoading(false);
    }

    [Button()]
    public void UseHorizonLoading()
    {
        UseTypeLoading(true);
    }

#endif
}