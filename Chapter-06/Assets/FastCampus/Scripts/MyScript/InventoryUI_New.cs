using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class MouseData_New
{
    public static InventoryUI_New interfaceMouseIsOver;
    public static GameObject slotHoveredOver;
    public static GameObject tempItemBeingDragged;
}

[RequireComponent(typeof(EventTrigger))]
public abstract class InventoryUI_New : MonoBehaviour
{
    public InventoryObject_New _inventoryObject;
    private InventoryObject_New _previousInventoryObject;

    public Dictionary<GameObject, InventorySlot_New> slotUis = new Dictionary<GameObject, InventorySlot_New>();

    private void Awake()
    {
        CreateSlotUIs();

        for (int i = 0; i < _inventoryObject.Slots.Length; ++i)
        {
            _inventoryObject.Slots[i]._parent = _inventoryObject;
            _inventoryObject.Slots[i]._OnPostUpdate += OnPostUpdate;
        }

        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    protected virtual void Start()
    {
        for (int i = 0; i < _inventoryObject.Slots.Length; ++i)
        {
            _inventoryObject.Slots[i].UpdateSlot(_inventoryObject.Slots[i]._item, _inventoryObject.Slots[i]._amount);
        }
    }

    public abstract void CreateSlotUIs();

    protected void AddEvent(GameObject go, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = go.GetComponent<EventTrigger>();
        if (!trigger)
        {
            Debug.LogWarning("No EventTrigget component found!");
            return;
        }

        EventTrigger.Entry eventTrigger = new EventTrigger.Entry { eventID = type };
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnPostUpdate(InventorySlot_New slot)
    {
        slot._slotUI.transform.GetChild(0).GetComponent<Image>().sprite = slot._item._id < 0 ? null : slot.ItemObject._icon;
        slot._slotUI.transform.GetChild(0).GetComponent<Image>().color = slot._item._id < 0 ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
        slot._slotUI.GetComponentInChildren<TextMeshProUGUI>().text = slot._item._id < 0 ? string.Empty : (slot._amount == 1 ? string.Empty : slot._amount.ToString("n0"));
    }

    public void OnEnterInterface(GameObject go)
    {
        MouseData_New.interfaceMouseIsOver = go.GetComponent<InventoryUI_New>();
    }

    public void OnExitInterface(GameObject go)
    {
        MouseData_New.interfaceMouseIsOver = null;
    }

    public void OnEnterSlot(GameObject go)
    {
        MouseData_New.slotHoveredOver = go;
    }

    public void OnExitSlot(GameObject go)
    {
        MouseData_New.slotHoveredOver = null;
    }

    public void OnStartDrag(GameObject go)
    {
        MouseData_New.tempItemBeingDragged = CreateDragImage(go);
    }

    private GameObject CreateDragImage(GameObject go)
    {
        if (slotUis[go]._item._id < 0)
        {
            return null;
        }

        GameObject dragImageGo = new GameObject();

        RectTransform rectTransform = dragImageGo.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(50, 50);
        dragImageGo.transform.SetParent(transform.parent);

        Image image = dragImageGo.AddComponent<Image>();
        image.sprite = slotUis[go].ItemObject._icon;
        image.raycastTarget = false;

        dragImageGo.name = "Drag Image";

        return dragImageGo;
    }

    public void OnDrag(GameObject go)
    {
        if (MouseData_New.tempItemBeingDragged == null)
        {
            return;
        }

        MouseData_New.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnEndDrag(GameObject go)
    {
        Destroy(MouseData_New.tempItemBeingDragged);

        if (MouseData_New.interfaceMouseIsOver == null)
        {
            slotUis[go].RemoveItem();
        }
        else if (MouseData_New.slotHoveredOver)
        {
            InventorySlot_New mouseHoverSlotData = MouseData_New.interfaceMouseIsOver.slotUis[MouseData_New.slotHoveredOver];
            _inventoryObject.SwapItems(slotUis[go], mouseHoverSlotData);
        }
    }
}
