using UnityEngine;

namespace RPG.Questing
{
    public class QuestCompletion : MonoBehaviour
    {
        [SerializeField] Quest quest = null;
        [SerializeField] string objective = "";

        public void CompleteObjective()
        {
            QuestTracker questTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestTracker>();
            questTracker.CompleteObjective(quest, objective);
        }
    }
}
