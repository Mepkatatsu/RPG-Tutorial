using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType_New: int
{
    Helmet = 0,
    Chest = 1,
    Pants = 2,
    Boots = 3,
    Pauldrons = 4,
    Gloves = 5,
    LeftWeapon = 6,
    RightWeapon = 7,
    Food,
    Default,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/New Item_New")]
public class ItemObject_New : ScriptableObject
{
    public ItemType_New _type;
    public bool _stackable;

    public Sprite _icon;
    public GameObject _modelPrefab;

    public Item_New _data = new Item_New();

    public List<string> _boneNames = new List<string>();

    [TextArea(15, 20)]
    public string _description;

    private void OnValidate()
    {
        _boneNames.Clear();

        if (_modelPrefab == null || _modelPrefab.GetComponentInChildren<SkinnedMeshRenderer>() == null)
        {
            return;
        }

        SkinnedMeshRenderer renderer = _modelPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
        Transform[] bones = renderer.bones;

        foreach (Transform t in bones)
        {
            _boneNames.Add(t.name);
        }
    }

    public Item_New CreateItem()
    {
        Item_New newItem = new Item_New();
        return newItem;
    }
}
