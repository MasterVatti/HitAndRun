using System.Collections.Generic;
using UnityEngine;

public class ItemsRotation : MonoBehaviour
{
    [SerializeField]
    private GameObject _item;
    [SerializeField]
    private float speedRotation;

    private bool _isRotate = true;

    void Start()
    {
        
    }

    public void StopRotation()
    {
        _isRotate = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_isRotate)
        {
            return;
        }
        _item.transform.Rotate(Vector3.up * speedRotation); 
    }
}