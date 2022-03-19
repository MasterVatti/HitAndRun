using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRotation : MonoBehaviour
{
public GameObject bomb;
public float speedRotation = 3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bomb.transform.Rotate(Vector3.up * speedRotation); 
    }
}
