using UnityEngine;
using RPG.Questing;
using TMPro;

namespace RPG.UI.Questing
{
    public class QuestItemUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title = null;
        [SerializeField] TextMeshProUGUI progress = null;
        [SerializeField] GameObject questInfoUIPrefab = null;

        public void SetUp(QuestStatus status)
        {
            title.text = status.GetQuest().GetTitle();
            progress.text = status.GetCompleteObjectives().ToString() + "/" + status.GetQuest().GetObjectivesCount().ToString();
        }
    }
}
