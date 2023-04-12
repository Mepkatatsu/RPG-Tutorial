using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInventoryUI_New : InventoryUI_New
{
    [SerializeField] protected GameObject _slotPrefab;

    [SerializeField] protected Vector2 _start;
    [SerializeField] protected Vector2 _size;
    [SerializeField] protected Vector2 _space;

    [Min(1), SerializeField] protected int _numberOfColumn = 4;

    public override void CreateSlotUIs()
    {
        _slotUis = new Dictionary<GameObject, InventorySlot_New>();

        for (int i = 0; i < _inventoryObject.Slots.Length; ++i)
        {
            GameObject go = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity, transform);
            go.GetComponent<RectTransform>().anchoredPosition = CalculatePosition(i);

            AddEvent(go, EventTriggerType.PointerEnter, delegate { OnEnterSlot(go);});
            AddEvent(go, EventTriggerType.PointerExit, delegate { OnExitSlot(go);});
            AddEvent(go, EventTriggerType.BeginDrag, delegate { OnStartDrag(go);});
            AddEvent(go, EventTriggerType.EndDrag, delegate { OnEndDrag(go);});
            AddEvent(go, EventTriggerType.Drag, delegate { OnDrag(go);});
            AddEvent(go, EventTriggerType.PointerClick, (data) => { OnClick(go, (PointerEventData)data); });

            _inventoryObject.Slots[i]._slotUI = go;
            _slotUis.Add(go, _inventoryObject.Slots[i]);

            go.name += ": " + i;
        }
    }

    public Vector3 CalculatePosition(int i)
    {
        float x = _start.x + ((_space.x + _size.x) * (i % _numberOfColumn));
        float y = _start.y + (-(_space.y + _size.y) * (i / _numberOfColumn));

        return new Vector3(x, y, 0f);
    }

    protected override void OnRightClick(InventorySlot_New slot)
    {

    }
}
