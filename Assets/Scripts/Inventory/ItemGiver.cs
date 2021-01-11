using UnityEngine;

namespace RPG.Inventorys
{
    public class ItemGiver : MonoBehaviour
    {
        [SerializeField] InventoryItem item = null;
        [SerializeField] int amount = 1;
        [SerializeField] bool removeFromInventory = false;

        public void GiveOrTakeItem()
        {
            Inventory inventory = Inventory.GetPlayerInventory();
            if(removeFromInventory)
            {
                inventory.RemoveItem(item, amount);
            }
            else
            {
                if(!inventory.AddItem(item, amount))
                {
                    //TODO: Drop items that can't fit.
                }
            }
        }
    }
}
