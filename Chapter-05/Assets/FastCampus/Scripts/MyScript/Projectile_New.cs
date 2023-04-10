using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Projectile_New : MonoBehaviour
{
    #region Variables

    public float _speed;

    public GameObject _muzzlePrefab;
    public GameObject _hitPrefab;

    public AudioClip _shotSFX;
    public AudioClip _hitSFX;

    private bool _isCollided;
    private Rigidbody _rigidbody;

    [HideInInspector]
    public AttackBehaviour_New _attackBehaviour;

    [HideInInspector]
    public GameObject _owner;

    [HideInInspector]
    public GameObject _target;

    #endregion Variables
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (_target != null)
        {
            Vector3 destination = _target.transform.position;
            destination.y += 1.5f;
            transform.LookAt(destination);
        }

        if (_owner)
        {
            Collider projectileCollider = GetComponent<Collider>();
            Collider[] ownerColliders = _owner.GetComponentsInChildren<Collider>();

            foreach (Collider collider in ownerColliders)
            {
                Physics.IgnoreCollision(projectileCollider, collider);
            }
        }

        _rigidbody = GetComponent<Rigidbody>();

        if (_muzzlePrefab)
        {
            GameObject muzzleVFX = Instantiate(_muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            ParticleSystem particleSystem = muzzleVFX.GetComponent<ParticleSystem>();
            if (particleSystem)
            {
                Destroy(muzzleVFX, particleSystem.main.duration);
            }
            else
            {
                ParticleSystem childParticleSystem = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                if (childParticleSystem)
                {
                    Destroy(muzzleVFX, childParticleSystem.main.duration);
                }
            }

            if (_shotSFX != null && GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().PlayOneShot(_shotSFX);
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (_speed != 0 && _rigidbody != null)
        {
            _rigidbody.position += (transform.forward) * (_speed * Time.deltaTime);
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (_isCollided)
        {
            return;
        }

        _isCollided = true;

        Collider projectileCollider = GetComponent<Collider>();
        projectileCollider.enabled = false;

        if (_hitSFX != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(_hitSFX);
        }

        _speed = 0;
        _rigidbody.isKinematic = true;

        ContactPoint contactPoint = collision.contacts[0];
        Quaternion contatctRotation = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
        Vector3 contactPosition = contactPoint.point;

        if (_hitPrefab)
        {
            GameObject hitVFX = Instantiate(_hitPrefab, contactPosition, contatctRotation);
            ParticleSystem particleSystem = hitVFX.GetComponent<ParticleSystem>();
            if (particleSystem)
            {
                Destroy(hitVFX, particleSystem.main.duration);
            }
            else
            {
                ParticleSystem childParticleSystem = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                if (childParticleSystem)
                {
                    Destroy(hitVFX, childParticleSystem.main.duration);
                }
            }

            if (_shotSFX != null && GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().PlayOneShot(_shotSFX);
            }
        }

        IDamageable_New damageable = collision.gameObject.GetComponent<IDamageable_New>();
        if (damageable != null)
        {
            damageable.TakeDamage(_attackBehaviour?._damage ?? 0, null);
        }

        StartCoroutine(DestroyParticle(5.0f));
    }

    public IEnumerator DestroyParticle(float waitTime)
    {
        if (transform.childCount > 0 && waitTime != 0)
        {
            List<Transform> childs = new List<Transform>();

            foreach (Transform t in transform.GetChild(0).transform)
            {
                childs.Add(t);
            }

            while (transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);

                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                for (int i = 0; i < childs.Count; ++i)
                {
                    childs[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
