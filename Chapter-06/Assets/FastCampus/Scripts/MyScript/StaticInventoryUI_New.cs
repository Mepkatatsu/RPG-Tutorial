using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInventoryUI_New : InventoryUI_New
{
    public GameObject[] staticSlots = null;

    public override void CreateSlotUIs()
    {
        _slotUis = new Dictionary<GameObject, InventorySlot_New>();
        for (int i = 0; i < _inventoryObject.Slots.Length; ++i)
        {
            GameObject go = staticSlots[i];

            AddEvent(go, EventTriggerType.PointerEnter, delegate { OnEnterSlot(go); });
            AddEvent(go, EventTriggerType.PointerExit, delegate { OnExitSlot(go); });
            AddEvent(go, EventTriggerType.BeginDrag, delegate { OnStartDrag(go); });
            AddEvent(go, EventTriggerType.EndDrag, delegate { OnEndDrag(go); });
            AddEvent(go, EventTriggerType.Drag, delegate { OnDrag(go); });

            _inventoryObject.Slots[i]._slotUI = go;
            _slotUis.Add(go, _inventoryObject.Slots[i]);

            go.name += ": " + i;
        }
    }
}
