using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEquipment_New : MonoBehaviour
{
    public InventoryObject_New _equipment;

    private EquipmentCombiner_New _combiner;

    private ItemInstances_New[] _itemInstances = new ItemInstances_New[8];

    public ItemObject_New[] _defaultItemObjects = new ItemObject_New[8];

    private void Awake()
    {
        _combiner = new EquipmentCombiner_New(gameObject);

        for (int i = 0; i < _equipment.Slots.Length; ++i)
        {
            _equipment.Slots[i]._OnPreUpdate += OnRemoveItem;
            _equipment.Slots[i]._OnPostUpdate += OnEquipItem;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (InventorySlot_New slot in _equipment.Slots)
        {
            OnEquipItem(slot);
        }
    }

    private void OnEquipItem(InventorySlot_New slot)
    {
        ItemObject_New itemObject = slot.ItemObject;
        if (itemObject == null)
        {
            EquitDefaultItemBy(slot._allowedItems[0]);
            return;
        }

        int index = (int)slot._allowedItems[0];

        switch (slot._allowedItems[0])
        {
            case ItemType_New.Helmet:
            case ItemType_New.Chest:
            case ItemType_New.Pants:
            case ItemType_New.Boots:
            case ItemType_New.Gloves:
                _itemInstances[index] = EquipSkinnedItem(itemObject);
                break;

            case ItemType_New.Pauldrons:
            case ItemType_New.LeftWeapon:
            case ItemType_New.RightWeapon:
                _itemInstances[index] = EquipMeshItem(itemObject);
                break;
        }
    }

    private ItemInstances_New EquipSkinnedItem(ItemObject_New itemObject)
    {
        if (itemObject == null)
        {
            return null;
        }

        Transform itemTransform = _combiner.Addlimb(itemObject._modelPrefab, itemObject._boneNames);
        if (itemTransform != null)
        {
            ItemInstances_New instance = new ItemInstances_New();
            instance._itemTransforms.Add(itemTransform);
            return instance;
        }

        return null;
    }

    private ItemInstances_New EquipMeshItem(ItemObject_New itemObject)
    {
        if (itemObject == null)
        {
            return null;
        }

        Transform[] itemTransforms = _combiner.AddMesh(itemObject._modelPrefab);
        if (itemTransforms.Length > 0)
        {
            ItemInstances_New instance = new ItemInstances_New();
            instance._itemTransforms.AddRange(itemTransforms.ToList());
            return instance;
        }

        return null;
    }

    private void EquitDefaultItemBy(ItemType_New type)
    {
        int index = (int)type;

        ItemObject_New itemObject = _defaultItemObjects[index];
        switch(type)
        {
            case ItemType_New.Helmet:
            case ItemType_New.Chest:
            case ItemType_New.Pants:
            case ItemType_New.Boots:
            case ItemType_New.Gloves:
                _itemInstances[index] = EquipSkinnedItem(itemObject);
                break;

            case ItemType_New.Pauldrons:
            case ItemType_New.LeftWeapon:
            case ItemType_New.RightWeapon:
                _itemInstances[index] = EquipMeshItem(itemObject);
                break;
        }
    }

    private void OnDestroy()
    {
        foreach (ItemInstances_New item in _itemInstances)
        {
            if(item != null)
            {
                item.Destroy();
            }
        }
    }

    private void OnRemoveItem(InventorySlot_New slot)
    {
        ItemObject_New itemObject = slot.ItemObject;
        if (itemObject == null)
        {
            RemoveItemBy(slot._allowedItems[0]);
            return;
        }

        if (slot.ItemObject._modelPrefab != null)
        {
            RemoveItemBy(slot._allowedItems[0]);
        }
    }

    private void RemoveItemBy(ItemType_New type)
    {
        int index = (int)type;
        if (_itemInstances[index] != null)
        {
            _itemInstances[index].Destroy();
            _itemInstances[index] = null;
        }
    }
}
