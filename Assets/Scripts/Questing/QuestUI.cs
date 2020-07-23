using UnityEngine;
using UnityEngine.UI;

namespace RPG.Questing
{
    public class QuestUI : MonoBehaviour
    {
        [SerializeField] Canvas canvas = default;
        [SerializeField] Text titleText = default;
        [SerializeField] Text descriptionText = default;

        QuestTracker questTracker;

        private void Awake()
        {
            questTracker = FindObjectOfType<QuestTracker>();
        }

        private void OnEnable()
        {
            questTracker.onQuestUpdated += OnQuestUpdated;
        }

        private void OnDisable()
        {
            questTracker.onQuestUpdated -= OnQuestUpdated;
        }

        private void OnQuestUpdated()
        {
            //TODO: update quest UI here.
        }
    }
}
