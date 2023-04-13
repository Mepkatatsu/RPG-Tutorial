using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FastCampus.Core;

[Serializable]
public class ModifiableInt_New
{
    [NonSerialized] private int _baseValue;
    [SerializeField] private int _modifiedValue;

    public int BaseValue
    {
        get => _baseValue;
        set
        {
            _baseValue = value;
            UpdateModifiedValue();
        }
    }

    public int ModifiedValue
    {
        get => _modifiedValue;
        set => _modifiedValue = value;
    }

    private event Action<ModifiableInt_New> OnModifiedValue;

    private List<IModifier> _modifiers = new List<IModifier>();

    public ModifiableInt_New(Action<ModifiableInt_New> method = null)
    {
        ModifiedValue = _baseValue;
        RegisterModEvent(method);
    }

    public void RegisterModEvent(Action<ModifiableInt_New> method)
    {
        if (method != null)
        {
            OnModifiedValue += method;
        }
    }

    public void UnregisterModEvent(Action<ModifiableInt_New> method)
    {
        if (method != null)
        {
            OnModifiedValue -= method;
        }
    }

    private void UpdateModifiedValue()
    {
        int valueToAdd = 0;
        foreach (IModifier modifier in _modifiers)
        {
            modifier.AddValue(ref valueToAdd);
        }

        ModifiedValue = _baseValue + valueToAdd;

        OnModifiedValue?.Invoke(this);
    }

    public void AddModifier(IModifier modifier)
    {
        _modifiers.Add(modifier);

        UpdateModifiedValue();
    }

    public void RemoveModifier(IModifier modifier)
    {
        _modifiers.Remove(modifier);
        UpdateModifiedValue();
    }
}
