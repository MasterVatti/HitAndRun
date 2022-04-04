using SimpleEventBus.Events;
using UnityEngine;

public class FirstAidKitActivatedEvent: EventBase
{
    public GameObject FirstAidKit;

    public FirstAidKitActivatedEvent(GameObject firstAidKit)
    {
        FirstAidKit = firstAidKit;
    }
}