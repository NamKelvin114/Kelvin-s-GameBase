using System;
using Kelvin;
using NaughtyAttributes;
using PrimeTween;
using UnityEngine;

public abstract class Popup : CacheGameComponent<Canvas>, IPopupRuntime
{
    [SerializeField] protected bool isUseAnimation;

    [ShowIf(nameof(isUseAnimation))] [SerializeField]
    private TypeAnimation typeAnimation;

    [ShowIf(nameof(OptionMove))] [SerializeField]
    private TypeAnimMove typeAnimMove;

    private readonly int _offset = 200;

    [NonSerialized] private Action _actionBeforeHide;

    [NonSerialized] private Action _actionBeforeShow;

    private Vector3 PopupPos => GetRectTransform.localPosition;
    public bool IsShowing { get; set; }

    protected override void Reset()
    {
        if (component == null)
        {
            gameObject.AddComponent<Canvas>();
            component = gameObject.GetComponent<Canvas>();
        }

        base.Reset();
        component.overrideSorting = true;
        ResetPopupName();
    }

    public Canvas CanvasPopup => component;
    public RectTransform GetRectTransform => component.GetComponent<RectTransform>();

    public void Show(Action beforeShow = null)
    {
        _actionBeforeHide = null;
        _actionBeforeShow = null;
        if (!IsShowing)
        {
            CanvasPopup.overrideSorting = true;
            _actionBeforeShow += beforeShow;
            BeforeShow();
            Active(true);
            if (isUseAnimation)
                switch (typeAnimation)
                {
                    case TypeAnimation.Move:
                        IsShowing = true;
                        switch (typeAnimMove)
                        {
                            case TypeAnimMove.Down:
                                MoveDownAnimation();
                                break;
                            case TypeAnimMove.Up:
                                MoveUpAnimation();
                                break;
                            case TypeAnimMove.Right:
                                MoveRightAnimation();
                                break;
                            case TypeAnimMove.Left:
                                MoveLeftAnimation();
                                break;
                        }

                        break;
                    case TypeAnimation.Scale:
                        IsShowing = true;
                        DoScaleOpen();
                        break;
                }

            ShowContent();
        }
    }
    public void Hide()
    {
        HideHandle();
    }

    public void Hide(Action beforeHide)
    {
       HideHandle(beforeHide);
    }
    void HideHandle(Action beforeHide = null)
    {
         if (!IsShowing)
        {
            _actionBeforeHide += beforeHide;
            if (isUseAnimation)
                switch (typeAnimation)
                {
                    case TypeAnimation.Move:
                        IsShowing = true;
                        switch (typeAnimMove)
                        {
                            case TypeAnimMove.Down:
                                Tween.LocalPosition(GetRectTransform, new Vector3(PopupPos.x, PopupPos.y -
                                        (GetRectTransform.rect.height + _offset), PopupPos.z), 1)
                                    .OnComplete(() =>
                                    {
                                        IsShowing = false;
                                        Close();
                                    }, false);
                                break;
                            case TypeAnimMove.Up:
                                Tween.LocalPosition(GetRectTransform, new Vector3(PopupPos.x,
                                        PopupPos.y + (GetRectTransform.rect.height + _offset), PopupPos.z), 1)
                                    .OnComplete(() =>
                                    {
                                        IsShowing = false;
                                        Close();
                                    }, false);
                                ;
                                break;
                            case TypeAnimMove.Right:
                                Tween.LocalPosition(GetRectTransform, new Vector3(
                                        PopupPos.x + (GetRectTransform.rect.width + _offset)
                                        , PopupPos.y, PopupPos.z), 1)
                                    .OnComplete(() =>
                                    {
                                        IsShowing = false;
                                        Close();
                                    }, false);
                                ;
                                break;
                            case TypeAnimMove.Left:
                                Tween.LocalPosition(GetRectTransform, new Vector3(
                                        PopupPos.x - (GetRectTransform.rect.width + _offset),
                                        PopupPos.y, PopupPos.z), 1)
                                    .OnComplete(() =>
                                    {
                                        IsShowing = false;
                                        Close();
                                    }, false);
                                ;
                                break;
                        }

                        break;
                    case TypeAnimation.Scale:
                        Close();
                        break;
                }
            else
                Close();
        }
    }
   

    private bool OptionMove()
    {
        return isUseAnimation && typeAnimation == TypeAnimation.Move;
    }

    protected virtual void BeforeShow()
    {
        _actionBeforeShow?.Invoke();
        _actionBeforeShow = null;
    }

    protected virtual void BeforeHide()
    {
        _actionBeforeHide?.Invoke();
        _actionBeforeHide = null;
    }

    private void Active(bool isActive)
    {
        gameObject.SetActive(isActive);
        GetRectTransform.localPosition = Vector3.zero;
    }

    protected virtual void ShowContent()
    {
    }

    private void Close()
    {
        BeforeHide();
        gameObject.SetActive(false);
    }

    private void DoScaleOpen()
    {
        var scaleDes = GetRectTransform.localScale;
        var scaleStart = scaleDes * 0.1f;
        GetRectTransform.localScale = scaleStart;
        Tween.Scale(GetRectTransform, scaleDes, 1).OnComplete(() => { IsShowing = false; }, false);
    }

    private void MoveDownAnimation()
    {
        GetRectTransform.localPosition = new Vector3(PopupPos.x, -(GetRectTransform.rect.height + _offset), PopupPos.z);
        MovePopup(new Vector3(PopupPos.x, PopupPos.y + (GetRectTransform.rect.height + _offset), PopupPos.z), 1);
    }

    private void MoveUpAnimation()
    {
        GetRectTransform.localPosition = new Vector3(PopupPos.x, GetRectTransform.rect.height + _offset, PopupPos.z);
        MovePopup(new Vector3(PopupPos.x, PopupPos.y - (GetRectTransform.rect.height + _offset), PopupPos.z), 1);
    }

    private void MoveLeftAnimation()
    {
        GetRectTransform.localPosition =
            new Vector3(PopupPos.x - (GetRectTransform.rect.width + _offset), PopupPos.y, PopupPos.z);
        MovePopup(new Vector3(PopupPos.x + (GetRectTransform.rect.width + _offset), PopupPos.y, PopupPos.z), 1);
    }

    private void MoveRightAnimation()
    {
        GetRectTransform.localPosition =
            new Vector3(PopupPos.x + (GetRectTransform.rect.width + _offset), PopupPos.y, PopupPos.z);
        MovePopup(new Vector3(PopupPos.x - (GetRectTransform.rect.width + _offset),
            PopupPos.y, PopupPos.z), 1);
    }

    private void MovePopup(Vector3 destination, float duration)
    {
        Tween.LocalPosition(GetRectTransform, destination, duration)
            .OnComplete(() => { IsShowing = false; });
        ;
    }

    [ContextMenu("Reset PopupName")]
    private void ResetPopupName()
    {
        gameObject.name = GetType().Name;
    }
}


public enum TypeAnimation
{
    Move,
    Scale
}

public enum TypeAnimMove
{
    Up,
    Down,
    Left,
    Right
}