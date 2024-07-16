using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PopupController : Singleton<PopupController>
{
    [SerializeField] private Transform popupContainer;

    [ReadOnly] [SerializeField] private List<Popup> popupsInstance;

    public async void ShowPopup(Type popupType, Action actionBeforeShow = null)
    {
        bool isPopupExisted = popupsInstance.Find(p => p.GetType() == popupType) == null ? false : true;
        if (!isPopupExisted)
        {
            Debug.LogWarning(popupType);
            GameObject loadPopup = await Addressables.LoadAssetAsync<GameObject>(popupType.ToString());
            Debug.Log(loadPopup.name);
            var getPopup = loadPopup.GetComponent<Popup>();
            var insPopup = Instantiate(getPopup, popupContainer);
            insPopup.Show(actionBeforeShow);
            popupsInstance.Add(insPopup);
        }
        else
        {
            var getPopup = popupsInstance.Where(p => p.GetType() == popupType).FirstOrDefault();
            getPopup.Show();
        }
        
    }

    public void HidePopup(Type popupName, Action actionBeforeHide = null)
    {
        bool isPopupExisted = popupsInstance.Find(p => p.GetType() == popupName)== null ? false : true;;
        if (!isPopupExisted)
        {
            Debug.LogError("Popup was not found");
        }
        else
        {
            var getPopup = popupsInstance.Where(p => p.GetType() == popupName).FirstOrDefault();
            getPopup.Hide();
        }
    }

    public Popup Find(Type popupName)
    {
        var getPopup = popupsInstance.Where(p => p.GetType() == popupName).FirstOrDefault();
        if (getPopup != null) return getPopup;

        Debug.LogError("Popup was not found");
        return null;
    }
}