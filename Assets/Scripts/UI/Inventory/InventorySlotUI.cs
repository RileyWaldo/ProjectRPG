using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Inventories;

namespace RPG.UI.Inventories
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] Image icon = null;
        [SerializeField] TextMeshProUGUI textAmount = null;

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
            {
                icon.sprite = item.Item.Icon;
                if(item.Amount > 1)
                {
                    textAmount.enabled = true;
                    textAmount.text = item.Amount.ToString();
                }
            }
        }
    }
}
