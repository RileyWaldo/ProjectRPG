using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Questing
{
    public class QuestTracker : MonoBehaviour, ISaveable
    {
        List<Quest> activeQuests = new List<Quest>();
        List<Quest> completedQuests = new List<Quest>();

        public event Action onQuestUpdated;

        private void Update()
        {
            foreach(Quest quest in activeQuests)
            {
                UpdateGoals(quest);
                if(quest.IsComplete())
                {
                    CompleteQuest(quest);
                }
            }
        }

        private static void UpdateGoals(Quest quest)
        {
            foreach (QuestGoal questGoal in quest.GetQuestGoals())
            {
                questGoal.CheckProgress();
            }
        }

        private void CompleteQuest(Quest quest)
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            onQuestUpdated.Invoke();
        }

        public void StartQuest(Quest quest)
        {
            activeQuests.Add(quest);
            onQuestUpdated.Invoke();
        }

        public object CaptureState()
        {
            throw new NotImplementedException();
        }

        public void RestoreState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
