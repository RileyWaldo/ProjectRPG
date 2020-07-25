using UnityEngine;

namespace RPG.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] InventorySlot[] inventorySlots = default;

        public bool AddItem(InventorySlot item)
        {
            bool canAdd = false;
            foreach(InventorySlot slot in inventorySlots)
            {
                if(slot == null)
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

            return canAdd;
        }
    }
}
