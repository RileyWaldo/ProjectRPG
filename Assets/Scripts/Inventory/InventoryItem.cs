using UnityEngine;

namespace RPG.Inventory
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "RPG/Inventory/Create New Item")]
    public class InventoryItem : ScriptableObject
    {
        [SerializeField] Sprite icon = default;
        [SerializeField] string itemName = "";
        [TextArea(4, 5)]
        [SerializeField] string itemDescription = "";
        [SerializeField] int value = 0;
        [SerializeField] bool isStackable = false;
        [SerializeField] int maxStack = 1;
        
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
    }
}
