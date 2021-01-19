using UnityEngine;

namespace RPG.Inventories
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] InventoryItem item = null;
        [SerializeField] int amount = 0;

        public InventorySlot(InventoryItem item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        public InventorySlot(object restoreState)
        {
            InventorySlotRecord recover = restoreState as InventorySlotRecord;
            item = InventoryItem.GetFromID(recover.itemID);
            amount = recover.amount;
        }

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

        public object CaptureState()
        {
            InventorySlotRecord record = new InventorySlotRecord();
            if (item != null)
                record.itemID = item.ItemID;
            record.amount = amount;
            return record;
        }

        [System.Serializable]
        class InventorySlotRecord
        {
            public string itemID = "";
            public int amount = 0;
        }
    }
}
