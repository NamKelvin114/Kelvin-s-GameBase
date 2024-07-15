using System;
using UnityEngine;

public interface IPopupRuntime
{
    public RectTransform GetRectTransform { get; }

    public Canvas CanvasPopup { get; }

    public void Show(Action beforeShow = null);

    public void Hide(Action beforeHide = null);
}