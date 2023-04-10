using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : Projectile_New
{
    public float _destroyDelay = 5.0f;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(DestroyParticle(_destroyDelay));
    }

    protected override void FixedUpdate()
    {
        if (_target)
        {
            Vector3 destination = _target.transform.position;
            destination.y += 1.5f;
            transform.LookAt(destination);
        }

        base.FixedUpdate();
    }
}
