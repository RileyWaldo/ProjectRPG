using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Saving;

namespace RPG.Inventorys
{
    public class Inventory : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        [SerializeField] List<InventorySlot> inventorySlots = new List<InventorySlot>();

        public event Action onUpdateInventory;

        public static Inventory GetPlayerInventory()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            return player.GetComponent<Inventory>();
        }

        private void Awake()
        {
            var item = InventoryItem.GetFromID("7ac553d3-2b47-4453-a13e-2af83738c19f");
            AddItem(item, 1);
        }

        public int GetSlotCount()
        {
            return inventorySlots.Count;
        }

        public InventorySlot GetItemByIndex(int index)
        {
            if (inventorySlots[index] == null)
                return null;
            return inventorySlots[index];
        }

        public bool AddItem(InventoryItem itemToAdd, int amount)
        {
            if (!itemToAdd.IsStackable)
                amount = 1;
            var item = new InventorySlot(itemToAdd, amount);
            bool canAdd = false;
            foreach(InventorySlot slot in inventorySlots)
            {
                if(slot.Item == null)
                {
                    canAdd = true;
                    slot.SetItem(item);
                    break;
                }
                if(slot.Item == item.Item && slot.Item.IsStackable && slot.Amount + item.Amount <= slot.Item.MaxStack)
                {
                    canAdd = true;
                    slot.Amount += item.Amount;
                    break;
                }
            }

            onUpdateInventory?.Invoke();
            return canAdd;
        }

        public bool RemoveItem(InventoryItem itemToRemove, int amount)
        {
            if (!itemToRemove.IsStackable)
                amount = 1;

            List<InventorySlot> slotToRemove = new List<InventorySlot>();

            bool canRemove = false;

            foreach(InventorySlot slot in inventorySlots)
            {
                if(slot.Item == itemToRemove)
                {
                    if(slot.Amount < amount)
                    {
                        amount -= slot.Amount;
                        slotToRemove.Add(slot);
                        continue;
                    }
                    if(slot.Amount >= amount)
                    {
                        canRemove = true;
                        slot.Amount -= amount;
                        if (slot.Amount == 0)
                            slot.Item = null;
                        break;
                    }
                }
            }

            if(canRemove)
            {
                foreach (InventorySlot slot in slotToRemove)
                {
                    slot.Item = null;
                    slot.Amount = 0;
                }
            }

            onUpdateInventory?.Invoke();
            return canRemove;
        }

        public bool HasItem(InventoryItem item)
        {
            foreach(InventorySlot slot in inventorySlots)
            {
                if (slot.Item == item)
                    return true;
            }
            return false;
        }

        public object CaptureState()
        {
            List<object> state = new List<object>();
            foreach(InventorySlot slot in inventorySlots)
            {
                state.Add(slot.CaptureState());
            }
            return state;
        }

        public void RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList == null)
                return;

            inventorySlots.Clear();
            foreach(object slot in stateList)
            {
                inventorySlots.Add(new InventorySlot(slot));
            }
            onUpdateInventory?.Invoke();
        }

        public bool? Evaluate(PredicateType predicate, string[] parameters)
        {
            switch(predicate)
            {
                case PredicateType.HasItem:
                    return HasItem(InventoryItem.GetFromID(parameters[0]));
            }
            return null;
        }
    }
}
