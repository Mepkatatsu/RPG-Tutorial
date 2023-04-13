using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Stats", menuName ="Stats System/New Character Stats_New")]
public class StatsObject_New : ScriptableObject
{
    public Attribute_New[] _attributes;

    public int _level;
    public int _exp;

    public int Health
    {
        get; set;
    }

    public int Mana
    {
        get; set;
    }

    public float HealthPercentage
    {
        get
        {
            int health = Health;
            int maxHealth = Health;

            foreach (Attribute_New attribute in _attributes)
            {
                if (attribute._type == AttributeType.Health)
                {
                    maxHealth = attribute._value.ModifiedValue;
                }
            }

            return (maxHealth > 0 ? ((float)health / (float)maxHealth) : 0f);
        }
    }

    public float ManaPercentage
    {
        get
        {
            int mana = Mana;
            int maxMana = Mana;

            foreach (Attribute_New attribute in _attributes)
            {
                if (attribute._type == AttributeType.Mana)
                {
                    maxMana = attribute._value.ModifiedValue;
                }
            }

            return (maxMana > 0 ? ((float)mana / (float)maxMana) : 0f);
        }
    }

    public Action<StatsObject_New> OnChangedStats;

    [NonSerialized]
    private bool _isInitialize = false;
    public void OnEnable()
    {
        InitializeAttribute();
    }

    public void InitializeAttribute()
    {
        if (_isInitialize)
        {
            return;
        }

        _isInitialize = true;

        foreach (Attribute_New attribute in _attributes)
        {
            attribute._value = new ModifiableInt_New(OnModifiedValue);
        }

        _level = 1;
        _exp = 0;

        SetBaseValue(AttributeType.Agility, 100);
        SetBaseValue(AttributeType.Intellect, 150);
        SetBaseValue(AttributeType.Stamina, 100);
        SetBaseValue(AttributeType.Strength, 100);
        SetBaseValue(AttributeType.Health, 100);
        SetBaseValue(AttributeType.Mana, 100);

        Health = GetModifiedValue(AttributeType.Health);
        Mana = GetModifiedValue(AttributeType.Mana);
    }

    private void OnModifiedValue(ModifiableInt_New value)
    {
        OnChangedStats?.Invoke(this);
    }

    public int GetBaseValue(AttributeType type)
    {
        foreach (Attribute_New attribute in _attributes)
        {
            if (attribute._type == type)
            {
                return attribute._value.BaseValue;
            }
        }

        return -1;
    }

    public void SetBaseValue(AttributeType type, int value)
    {
        foreach (Attribute_New attribute in _attributes)
        {
            if (attribute._type == type)
            {
                attribute._value.BaseValue = value;
            }
        }
    }

    public int GetModifiedValue(AttributeType type)
    {
        foreach (Attribute_New attribute in _attributes)
        {
            if (attribute._type == type)
            {
                return attribute._value.ModifiedValue;
            }
        }

        return -1;
    }

    public int AddHealth(int value)
    {
        Health += value;

        OnChangedStats?.Invoke(this);

        return Health;
    }

    public int AddMana(int value)
    {
        Mana += value;

        OnChangedStats?.Invoke(this);

        return Mana;
    }
}
