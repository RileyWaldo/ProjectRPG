using UnityEngine;
using TMPro;
using RPG.Questing;

namespace RPG.UI.Questing
{
    public class QuestInfoUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText = default;
        [SerializeField] QuestObjectiveContainerUI questObjectiveContainer = default;
        [SerializeField] QuestRewardContainerUI questRewardContainer = default;

        QuestStatus questStatus = null;

        public void UpdateUI()
        {
            if(questStatus != null)
            {
                ReDrawUI();
            }
        }

        public void ShowInfo(QuestStatus status)
        {
            questStatus = status;
            ShowVisible(true);
            ReDrawUI();
        }

        public void ShowVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }

        private void ReDrawUI()
        {
            titleText.text = questStatus.GetQuest().GetTitle();
            questObjectiveContainer.SetUp(questStatus);
            questRewardContainer.SetUp(questStatus);
        }
    }
}
