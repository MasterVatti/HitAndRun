using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.5f;

    private void OnCollisionEnter(Collision other)
    {
        EventStreams.Game.Publish(new BulletHitEvent(this, other.gameObject));
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void SetBulletRotation(Quaternion characterTransformRotation)
    {
        transform.rotation = characterTransformRotation;
    }
}