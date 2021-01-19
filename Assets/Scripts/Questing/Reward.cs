using UnityEngine;
using RPG.Inventories;
using RPG.Stats;

namespace RPG.Questing
{
    [System.Serializable]
    public class Reward
    {
        public InventoryItem item = null;
        public int amount = 0;
        public string faction = "";
        public int respect = 0;

        public void GiveReward()
        {
            if (item != null)
            {
                Inventory inventory = Inventory.GetPlayerInventory();
                int drop = inventory.ForceAddItem(item, amount);
                if(drop > 0)
                {
                    Debug.Log($"Dropped {drop} {item.Name}");
                }
            }

            if(!string.IsNullOrWhiteSpace(faction))
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Respect>().GiveRespect(faction, respect);
            }
        }
    }
}
