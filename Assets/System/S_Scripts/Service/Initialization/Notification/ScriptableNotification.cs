using System;
using System.Collections.Generic;
using System.IO;
using Kelvin;
using Kelvin.Notification;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Notification", fileName = "notification_data.asset")]
[Serializable]
public class ScriptableNotification : ScriptableObject
{
    [ReadOnly] [SerializeField] private string identifier;
    public int minute;
    [SerializeField] private bool repeat;
    [SerializeField] internal bool bigPicture;

    [ShowIf(nameof(bigPicture))] [SerializeField]
    internal string namePicture;

    [SerializeField] internal bool overrideIcon;

    [SerializeField] [ShowIf(nameof(overrideIcon))] [Label("  Small Icon")]
    internal string smallIcon = "icon_0";

    [SerializeField] [ShowIf(nameof(overrideIcon))] [Label("  Large Icon")]
    internal string largeIcon = "icon_1";


    [SerializeField] private List<NotificationData> datas = new();

    private void Reset()
    {
        identifier = Guid.NewGuid().ToString();
    }


    public void Send()
    {
        if (!Application.isMobilePlatform) return;
        var data = datas.PickRandom();
        var pathPicture = Path.Combine(Application.persistentDataPath, namePicture);
        var title = "";
        var message = "";
        if (data.enableLocalization)
        {
            //title = data.titleLocale.Value;
            // message = data.messageLocale.Value;
        }
        else
        {
            title = data.title;
            message = data.message;
        }

        NotificationConsole.Send(identifier,
            title,
            message,
            smallIcon: smallIcon,
            largeIcon: largeIcon,
            bigPicture: bigPicture,
            namePicture: pathPicture);
    }

    public void Schedule()
    {
        if (!Application.isMobilePlatform) return;
        var data = datas.PickRandom();

        var pathPicture = Path.Combine(Application.persistentDataPath, namePicture);
        var title = "";
        var message = "";
        if (data.enableLocalization)
        {
            //title = data.titleLocale.Value;
            // message = data.messageLocale.Value;
        }
        else
        {
            title = data.title;
            message = data.message;
        }

        NotificationConsole.Schedule(identifier,
            title,
            message,
            TimeSpan.FromMinutes(minute),
            smallIcon: smallIcon,
            largeIcon: largeIcon,
            bigPicture: bigPicture,
            namePicture: pathPicture,
            repeat: repeat);
    }
}

[Serializable]
public class NotificationData
{
    public bool enableLocalization;

    [ShowIf(nameof(enableLocalization), false)]
    public string title;

    [ShowIf(nameof(enableLocalization), false)]
    public string message;

    [ShowIf(nameof(enableLocalization), true)]
    public string titleLocale;

    [ShowIf(nameof(enableLocalization), true)]
    public string messageLocale;


    public NotificationData(string title, string message)
    {
        this.title = title;
        this.message = message;
    }
}