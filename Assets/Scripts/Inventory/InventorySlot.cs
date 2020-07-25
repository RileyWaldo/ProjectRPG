using UnityEngine;

namespace RPG.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] InventoryItem item = default;
        [SerializeField] int amount = 0;

        public void SetItem(InventorySlot item)
        {
            this.item = item.Item;
            amount = item.Amount;
        }

        public InventoryItem Item
        {
            get { return item; }
            set { item = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }
}
