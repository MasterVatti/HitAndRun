using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GlobalConstants.CAHARACTER_TAG)
        {
            EventStreams.Game.Publish(new FirstAidKitActivatedEvent());
        }
    }
}