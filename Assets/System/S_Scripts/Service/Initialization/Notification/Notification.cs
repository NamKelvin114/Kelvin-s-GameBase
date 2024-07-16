using System;
using UnityEngine;
using UnityEngine.Android;

public class Notification : Initialize
{
    [SerializeField] private ScriptableNotification notification;

    public override void Init(Action<bool> loadComplete)
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
#endif

        if (notification != null) notification.Schedule();
        base.Init(loadComplete);
    }
}