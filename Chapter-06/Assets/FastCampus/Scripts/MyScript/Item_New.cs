using FastCampus.InventorySystem.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item_New
{
    public int _id = -1;
    public string _name;

    public ItemBuff_New[] _buffs;

    public Item_New()
    {
        _id = -1;
        _name = "";
    }

    public Item_New(ItemObject_New itemObejct)
    {
        _name = itemObejct.name;
        _id = itemObejct._data._id;

        _buffs = new ItemBuff_New[itemObejct._data._buffs.Length];
        for (int i = 0; i < _buffs.Length; ++i)
        {
            _buffs[i] = new ItemBuff_New(itemObejct._data._buffs[i].Min, itemObejct._data._buffs[i].Max)
            {
                _stat = itemObejct._data._buffs[i]._stat
            };
        }
    }
}
