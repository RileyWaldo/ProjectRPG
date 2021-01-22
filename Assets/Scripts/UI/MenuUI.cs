using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.UI
{
    public class MenuUI : MonoBehaviour
    {
        [Header("Tab Buttons")]
        [SerializeField] TextMeshProUGUI tabText = null;
        [SerializeField] Button equipmentTab = null;
        [SerializeField] Button questTab = null;
        [SerializeField] Button systemTab = null;

        [Header("Windows")]
        [SerializeField] GameObject equipmentWindow = null;
        [SerializeField] GameObject questWindow = null;
        [SerializeField] GameObject inventoryWindow = null;

        private void Start()
        {
            equipmentTab.onClick.AddListener(OnEquipmentTab);
            questTab.onClick.AddListener(OnQuestTab);
            systemTab.onClick.AddListener(OnSystemTab);
        }

        private void OnEquipmentTab()
        {
            tabText.text = "Equipment";
            HideWindows();
            equipmentWindow.SetActive(true);
            inventoryWindow.SetActive(true);
        }

        private void OnQuestTab()
        {
            tabText.text = "Quests";
            HideWindows();
            questWindow.SetActive(true);
            inventoryWindow.SetActive(true);
        }

        private void OnSystemTab()
        {
            tabText.text = "System";
            HideWindows();
        }

        private void HideWindows()
        {
            equipmentWindow.SetActive(false);
            questWindow.SetActive(false);
            inventoryWindow.SetActive(false);
        }
    }
}
