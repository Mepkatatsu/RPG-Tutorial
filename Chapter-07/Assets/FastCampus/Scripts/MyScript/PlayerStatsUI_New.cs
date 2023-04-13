using FastCampus.InventorySystem.Inventory;
using FastCampus.InventorySystem.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI_New : MonoBehaviour
{
    public InventoryObject _equipment;
    public StatsObject_New _playerStats;

    public Text[] _attributeText;

    private void OnEnable()
    {
        _playerStats.OnChangedStats += OnChangedStats;

        if (_equipment != null && _playerStats != null)
        {
            foreach (InventorySlot slot in _equipment.Slots)
            {
                slot.OnPreUpdate += OnRemoveItem;
                slot.OnPostUpdate += OnEquipItem;
            }
        }

        UpdateAttributeTexts();
    }

    private void OnDisable()
    {
        _playerStats.OnChangedStats -= OnChangedStats;

        if (_equipment != null && _playerStats != null)
        {
            foreach (InventorySlot slot in _equipment.Slots)
            {
                slot.OnPreUpdate -= OnRemoveItem;
                slot.OnPostUpdate -= OnEquipItem;
            }
        }
    }

    private void UpdateAttributeTexts()
    {
        _attributeText[0].text = _playerStats.GetModifiedValue(AttributeType.Agility).ToString("n0");
        _attributeText[1].text = _playerStats.GetModifiedValue(AttributeType.Intellect).ToString("n0");
        _attributeText[2].text = _playerStats.GetModifiedValue(AttributeType.Stamina).ToString("n0");
        _attributeText[3].text = _playerStats.GetModifiedValue(AttributeType.Strength).ToString("n0");
    }

    private void OnRemoveItem(InventorySlot slot)
    {
        if (slot.ItemObject == null)
        {
            return;
        }

        foreach (ItemBuff buff in slot.item.buffs)
        {
            foreach (Attribute_New attribute in _playerStats._attributes)
            {
                if (attribute._type == buff.stat)
                {
                    attribute._value.RemoveModifier(buff);
                }
            }
        }
    }

    private void OnEquipItem(InventorySlot slot)
    {
        if (slot.ItemObject == null)
        {
            return;
        }

        foreach (ItemBuff buff in slot.item.buffs)
        {
            foreach (Attribute_New attribute in _playerStats._attributes)
            {
                if (attribute._type == buff.stat)
                {
                    attribute._value.AddModifier(buff);
                }
            }
        }
    }

    private void OnChangedStats(StatsObject_New statsObject)
    {
        UpdateAttributeTexts();
    }
}
