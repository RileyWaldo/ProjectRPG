using UnityEngine;
using UnityEngine.UI;
using RPG.Questing;

namespace RPG.UI.Questing
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] QuestItemUI questPrefab = null;
        [SerializeField] QuestInfoUI questInfo = null;
        [SerializeField] QuestObjectiveContainerUI questObjectiveContainerUI = null;

        QuestTracker questTracker;

        private void Start()
        {
            questTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestTracker>();
            questTracker.onUpdateQuest += UpdateUI;
            UpdateUI();
        }

        private void UpdateUI()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            foreach (QuestStatus status in questTracker.GetStatuses())
            {
                QuestItemUI questItem = Instantiate(questPrefab, transform);
                questItem.SetUp(status);
                questItem.GetComponent<Button>().onClick.AddListener(() => questObjectiveContainerUI.SetUp(status, questInfo));
            }
        }
    }
}
