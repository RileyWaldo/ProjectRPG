using UnityEngine;
using RPG.Questing;

namespace RPG.UI.Questing
{
    public class QuestObjectiveContainerUI : MonoBehaviour
    {
        [SerializeField] GameObject objectivePrefab = null;

        public void SetUp(QuestStatus status, QuestInfoUI questInfo)
        {
            questInfo.ShowInfo(true);

            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            foreach(string objective in status.GetQuest().GetObjectives())
            {
                GameObject newObjective = Instantiate(objectivePrefab, transform);
                newObjective.GetComponent<QuestObjectiveUI>().SetUp(objective, status.IsObjectiveComplete(objective));
            }
        }
    }
}
