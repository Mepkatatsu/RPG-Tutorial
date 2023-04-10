using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText_New : MonoBehaviour
{
    public float _delayTimeToDestroy = 1.0f;

    public float Damage
    {
        get; set;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _delayTimeToDestroy);
    }
}
