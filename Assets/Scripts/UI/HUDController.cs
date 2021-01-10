using UnityEngine;
using RPG.UI.Questing;

namespace RPG.UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("Windows")]
        [SerializeField] GameObject inventoryWindow = null;
        [SerializeField] GameObject questWindow = null;
        [SerializeField] GameObject questInfoWindow = null;

        [Header("Key Bindings")]
        [SerializeField] KeyCode inventoryHotKey = KeyCode.I;
        [SerializeField] KeyCode questHotKey = KeyCode.Q;

        private void Start()
        {
            inventoryWindow.SetActive(false);
            questInfoWindow.GetComponent<QuestInfoUI>().ShowVisible(false);
            questWindow.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(inventoryHotKey))
            {
                ToggleInventory();
            }

            if (Input.GetKeyDown(questHotKey))
            {
                ToggleQuest();
            }
        }

        public void ToggleInventory()
        {
            inventoryWindow.SetActive(!inventoryWindow.activeSelf);
        }

        public void ToggleQuest()
        {
            questWindow.SetActive(!questWindow.activeSelf);
            questInfoWindow.GetComponent<QuestInfoUI>().ShowVisible(false);
        }

    }
}
