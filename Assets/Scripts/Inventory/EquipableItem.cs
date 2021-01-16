using UnityEngine;

namespace RPG.Inventorys
{
    [CreateAssetMenu(menuName = ("RPG/Inventory/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        [Tooltip("Where are we allowed to put this item.")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.RightHand;

        public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }
    }
}
