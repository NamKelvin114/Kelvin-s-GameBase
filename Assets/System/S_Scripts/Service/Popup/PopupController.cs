using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PopupController : Singleton<PopupController>
{
    [SerializeField] private Transform popupContainer;

    [ReadOnly] [SerializeField] private List<Popup> popupsInstance;

    public UniTask ShowPopup(Popup popup, Action actionBeforeShow = null)
    {
        if (!popupsInstance.Contains(popup))
        {
            var loadPopup = Addressables.LoadAssetAsync<GameObject>(popup.ToString());
            UniTask.WaitUntil(() => loadPopup.IsDone);
            var getPopupObject = loadPopup.Result;
            var getPopup = getPopupObject.GetComponent<Popup>();
            var insPopup = Instantiate(getPopup, popupContainer);
            insPopup.Show(actionBeforeShow);
            popupsInstance.Add(insPopup);
            return UniTask.CompletedTask;
        }
        else
        {
            var getPopup = popupsInstance.Where(p => p = popup).FirstOrDefault();
            getPopup.Show();
        }

        return UniTask.CompletedTask;
    }

    public UniTask HidePopup(Popup popup, Action actionBeforeHide = null)
    {
        if (!popupsInstance.Contains(popup))
        {
            Debug.LogError("Popup was not found");
        }
        else
        {
            var getPopup = popupsInstance.Where(p => p = popup).FirstOrDefault();
            getPopup.Hide();
        }

        return UniTask.CompletedTask;
    }

    public Popup Find(Popup popup)
    {
        var getPopup = popupsInstance.Where(p => p = popup).FirstOrDefault();
        if (getPopup != null)
        {
            return getPopup;
        }

        Debug.LogError("Popup was not found");
        return null;
    }
}