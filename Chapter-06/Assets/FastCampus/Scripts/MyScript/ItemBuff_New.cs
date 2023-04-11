using FastCampus.InventorySystem.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterAttribute_New
{
    Agility,
    Intellect,
    Stamina,
    Strength
}

[Serializable]
public class ItemBuff_New
{
    public CharacterAttribute_New _stat;
    public int _value;

    [SerializeField] private int _min;
    [SerializeField] private int _max;

    public int Min => _min;
    public int Max => _max;

    public ItemBuff_New(int min, int max)
    {
        _min = min;
        _max = max;

        GenerateValue();
    }

    public void GenerateValue()
    {
        _value = UnityEngine.Random.Range(_min, _max);
    }

    public void AddValue(ref int v)
    {
        v += _value;
    }
}
