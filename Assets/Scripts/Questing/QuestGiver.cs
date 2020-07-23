using UnityEngine;

namespace RPG.Questing
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] Quest quest = default;

        public Quest GiveQuest()
        {
            return quest;
        }
    }
}
