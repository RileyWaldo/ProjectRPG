using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Questing
{
    public class QuestTracker : MonoBehaviour//, ISaveable
    {
        List<QuestStatus> statuses = new List<QuestStatus>();

        public event Action onUpdateQuest;

        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest))
                return;

            QuestStatus newQuest = new QuestStatus(quest);
            statuses.Add(newQuest);
            onUpdateQuest?.Invoke();
        }

        public bool HasQuest(Quest quest)
        {
            foreach (QuestStatus status in statuses)
            {
                if (status.GetQuest() == quest)
                    return true;
            }
            return false;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

        //public object CaptureState()
        //{
        //    throw new NotImplementedException();
        //}

        //public void RestoreState(object state)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
