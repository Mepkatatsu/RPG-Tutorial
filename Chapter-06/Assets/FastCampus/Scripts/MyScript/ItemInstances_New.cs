using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstances_New
{
    public List<Transform> _itemTransforms = new List<Transform>();

    public void Destroy()
    {
        foreach (Transform item in _itemTransforms)
        {
            GameObject.Destroy(item.gameObject);
        }
    }
}
