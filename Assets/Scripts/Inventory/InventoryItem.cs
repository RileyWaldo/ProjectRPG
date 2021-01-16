using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventorys
{
    public abstract class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] string itemID = "";
        [SerializeField] Sprite icon = default;
        [SerializeField] string itemName = "";
        [TextArea(4, 5)]
        [SerializeField] string itemDescription = "";
        [SerializeField] int value = 0;
        [SerializeField] bool isStackable = false;
        [SerializeField] int maxStack = 1;

        static Dictionary<string, InventoryItem> itemLookUpCache;

        public static InventoryItem GetFromID(string itemID)
        {
            if(itemLookUpCache == null)
            {
                itemLookUpCache = new Dictionary<string, InventoryItem>();
                InventoryItem[] itemList = Resources.LoadAll<InventoryItem>("");
                foreach(InventoryItem item in itemList)
                {
                    if(itemLookUpCache.ContainsKey(item.ItemID))
                    {
                        InventoryItem itemInLookUp = itemLookUpCache[item.ItemID];
                        Debug.LogError($"Looks like there's a duplicate itemID. \n" + itemInLookUp.ItemID + "\n" + itemInLookUp.Name + "\n" + item.Name);
                        continue;
                    }

                    itemLookUpCache[item.ItemID] = item;
                }
            }

            if (itemID == null || !itemLookUpCache.ContainsKey(itemID))
                return null;

            return itemLookUpCache[itemID];
        }

        public string ItemID
        {
            get { return itemID; }
        }
        
        public Sprite Icon
        {
            get { return icon; }
        }

        public string Name
        {
            get { return itemName; }
        }

        public string Description
        {
            get { return itemDescription; }
        }

        public int Value
        {
            get { return value; }
        }

        public bool IsStackable
        {
            get { return isStackable; }
        }

        public int MaxStack
        {
            get { return maxStack; }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //not used
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if(string.IsNullOrWhiteSpace(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }
    }
}
