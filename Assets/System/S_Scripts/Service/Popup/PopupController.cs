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

    public UniTask ShowPopup(string popupName, Action actionBeforeShow = null)
    {
        bool isPopupExisted = popupsInstance.Find(p => p.name == popupName);
        if (!isPopupExisted)
        {
            Debug.LogWarning(popupName);
            var loadPopup = Addressables.LoadAssetAsync<GameObject>(popupName);
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
            var getPopup = popupsInstance.Where(p => p.name == popupName).FirstOrDefault();
            getPopup.Show();
        }

        return UniTask.CompletedTask;
    }

    public UniTask HidePopup(string popupName, Action actionBeforeHide = null)
    {
        bool isPopupExisted = popupsInstance.Find(p => p.name == popupName);
        if (!isPopupExisted)
        {
            Debug.LogError("Popup was not found");
        }
        else
        {
            var getPopup = popupsInstance.Where(p => p.name == popupName).FirstOrDefault();
            getPopup.Hide();
        }

        return UniTask.CompletedTask;
    }

    public Popup Find(Popup popup)
    {
        var getPopup = popupsInstance.Where(p => p = popup).FirstOrDefault();
        if (getPopup != null) return getPopup;

        Debug.LogError("Popup was not found");
        return null;
    }
}