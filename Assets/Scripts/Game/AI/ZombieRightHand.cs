using System;
using UnityEngine;

public class ZombieRightHand : MonoBehaviour
{
    private bool _isCollisionCharacter;

   private void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.tag == GlobalConstants.CAHARACTER_TAG)
       {
           _isCollisionCharacter = true;
       }
   }
   
   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.tag == GlobalConstants.CAHARACTER_TAG)
      {
          _isCollisionCharacter = false;
      }
   }

   public bool GetCollisionState()
    {
        return _isCollisionCharacter;
    }
}