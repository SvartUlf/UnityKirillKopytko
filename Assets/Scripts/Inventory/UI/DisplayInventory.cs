using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Game.Inventory
{
    public class DisplayInventory : MonoBehaviour
    {
        [SerializeField] private GameObject InventoryPrefab;
        [SerializeField] private InventoryObject Inventory;
        [SerializeField] private int X_Start, Y_Start;
        [SerializeField] private int X_End, Y_End;
        [SerializeField] private int X_Space_Between_Items, Y_Space_Between_Items;
        [SerializeField] private int Number_Of_Collumns;
        [SerializeField] private Vector2 _hoverItemSize = new Vector2(50, 50);

        [Header("References")]
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _rootPanel;
        [SerializeField] private GameObject _container;

        [Header("Settings")]
        [SerializeField] private KeyCode _toggleInventory = KeyCode.I;

        private Dictionary<GameObject, InventorySlot> _itemsDisplay = new Dictionary<GameObject, InventorySlot>();
        private MouseItem _mouseItem = new MouseItem();
        private GameObject _tempMouseObject;
        private bool _isInventoryOpen = false;

        public void Start()
        {
            CreateSlots();
            _rootPanel.SetActive(false);
        }

        private void Update()
        {
            HandleInventoryToggle();
        }
        public void LateUpdate()
        {
            UpdateSlots();
        }

        private void HandleInventoryToggle()
        {
            if (!Input.GetKeyDown(_toggleInventory))
            {
                return;
            }

            _isInventoryOpen = !_isInventoryOpen;
            _rootPanel.SetActive(_isInventoryOpen);
        }
        private void UpdateSlots()
        {
            foreach (KeyValuePair<GameObject, InventorySlot> slot in _itemsDisplay)
            {
                if (slot.Value.Id >= 0)
                {
                    slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = Inventory.Database.GetItem[slot.Value.Item.Id].UiDisplay;
                    slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = slot.Value.Amount == 1 ? "" : slot.Value.Amount.ToString();
                }
                else
                {
                    slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = null;
                    slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
                    slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }
        private void CreateSlots()
        {
            _itemsDisplay = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < Inventory.Container.Items.Length; i++)
            {
                var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, _container.transform);
                //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

                _itemsDisplay.Add(obj, Inventory.Container.Items[i]);
            }
        }

        private void OnDrag(GameObject obj)
        {
            if (_mouseItem.Obj)
            {
                var mousePos = Input.mousePosition;
                var worldPos = _camera.ScreenToWorldPoint(mousePos);
                var localPos = _container.transform.InverseTransformPoint(worldPos);
                localPos.z = 0;

                _mouseItem.Obj.GetComponent<RectTransform>().localPosition = localPos;
            }
        }
        private void OnDragStart(GameObject obj)
        {
            _tempMouseObject = new GameObject();
            _tempMouseObject.transform.SetParent(_container.transform, false);
            var rt = _tempMouseObject.AddComponent<RectTransform>(); 
            rt.anchorMin = rt.anchorMax = new Vector2(0f, 1f);
            rt.sizeDelta = _hoverItemSize;
            rt.localScale = Vector3.one;
            var mousePos = Input.mousePosition;
            var worldPos = _camera.ScreenToWorldPoint(mousePos);
            var localPos = _container.transform.InverseTransformPoint(worldPos);
            localPos.z = 0;
            rt.localPosition = localPos;
            if (_itemsDisplay[obj].Id >= 0)
            {
                var image = _tempMouseObject.AddComponent<Image>();
                image.sprite = Inventory.Database.GetItem[_itemsDisplay[obj].Id].UiDisplay;
                image.raycastTarget = false;
                _mouseItem.Obj = _tempMouseObject;
                _mouseItem.Item = _itemsDisplay[obj];
            }
        }

        private void OnDragEnd(GameObject obj)
        {
            if (_mouseItem.HoverObj)
            {
                Inventory.MoveItem(_itemsDisplay[obj], _itemsDisplay[_mouseItem.HoverObj]);
            }
            else
            {
                Inventory.RemoveItem(_itemsDisplay[obj].Item);
            }
            if (_tempMouseObject)
            {
                Destroy(_tempMouseObject);
                _tempMouseObject = null;
                _mouseItem.HoverItem = null;
                _mouseItem.Obj = null;
            }
        }

        private void OnExit(GameObject obj)
        {
            _mouseItem.HoverObj = null;
            _mouseItem.HoverItem = null;
        }

        private void OnEnter(GameObject obj)
        {
            _mouseItem.HoverObj = obj;
            if (_itemsDisplay.ContainsKey(obj))
            {
                _mouseItem.HoverItem = _itemsDisplay[obj];
            }
        }

        private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }
        public Vector3 GetPosition(int index)
        {
            return new Vector3(X_Start + (X_Space_Between_Items * (index % Number_Of_Collumns)), Y_Start + (-Y_Space_Between_Items * (index / Number_Of_Collumns)), 0f);
        }
    }

    public class MouseItem
    {
        public GameObject Obj;
        public GameObject HoverObj;
        public InventorySlot Item;
        public InventorySlot HoverItem;

    }
}