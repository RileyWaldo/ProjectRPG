using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.UI.Questing
{
    public class QuestObjectiveUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title = null;
        [SerializeField] Image checkMark = null;

        public void SetUp(string newTitle, bool isComplete)
        {
            title.text = newTitle;
            checkMark.enabled = isComplete;
        }
    }
}
