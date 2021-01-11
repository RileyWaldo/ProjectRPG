using UnityEngine;
using UnityEngine.UI;
using RPG.Inventorys;

namespace RPG.UI.Inventorys
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] Image icon = null;

        Inventory inventory;
        int index;

        public InventorySlot GetItem()
        {
            return inventory.GetItemByIndex(index);
        }

        public void SetUp(Inventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;
            var item = inventory.GetItemByIndex(index);
            if (item == null || item.Item == null)
                icon.gameObject.SetActive(false);
            else
                icon.sprite = item.Item.Icon;

        }
    }
}
