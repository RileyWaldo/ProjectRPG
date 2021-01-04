using UnityEngine;
using RPG.Questing;

namespace RPG.UI.Questing
{
    public class QuestObjectiveContainerUI : MonoBehaviour
    {
        [SerializeField] GameObject objectivePrefab = null;

        public void SetUp(QuestStatus status)
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            foreach(var objective in status.GetQuest().GetObjectives())
            {
                GameObject newObjective = Instantiate(objectivePrefab, transform);
                newObjective.GetComponent<QuestObjectiveUI>().SetUp(objective.description, status.IsObjectiveComplete(objective.reference));
            }
        }
    }
}
