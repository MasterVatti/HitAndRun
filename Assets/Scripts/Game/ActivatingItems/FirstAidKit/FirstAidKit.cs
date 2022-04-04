using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] _firstAidKitEffects;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GlobalConstants.CAHARACTER_TAG)
        {
            EventStreams.Game.Publish(new FirstAidKitActivatedEvent(gameObject));
        }
    }
    
    public void DestroyEffects()
    {
        foreach (var firstAidKitEffect in _firstAidKitEffects)
        {
            Destroy(firstAidKitEffect);
        }
    }
}