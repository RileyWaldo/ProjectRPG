using UnityEngine;
using RPG.Inventories;

namespace RPG.UI.Inventories
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] GameObject inventorySlotPrefab = default;

        Inventory inventory;

        private void Awake()
        {
            inventory = Inventory.GetPlayerInventory();
            inventory.onUpdateInventory += ReDraw;
        }

        private void Start()
        {
            ReDraw();
        }

        void ReDraw()
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for(int i=0; i<inventory.GetSize(); i++)
            {
                GameObject newSlot = Instantiate(inventorySlotPrefab, transform);
                newSlot.GetComponent<InventorySlotUI>().SetUp(inventory, i);
            }
        }
    }
}
