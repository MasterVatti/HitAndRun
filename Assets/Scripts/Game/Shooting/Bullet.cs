using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletSpeed;

    private void OnCollisionEnter(Collision other)
    {
        EventStreams.Game.Publish(new BulletHitEvent(this, other.gameObject));
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime);
    }

    public void Initialize(Quaternion characterTransformRotation, float bulletSpeed)
    {
        transform.rotation = characterTransformRotation;
        _bulletSpeed = bulletSpeed;
    }
}