using System;

public interface IPopupRuntime
{
    public void Show(Action beforeShow = null);

    public void BeforeShow();

    public void Hide(Action beforeHide = null);

    public void BeforeHide();
}