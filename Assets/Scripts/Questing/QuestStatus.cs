using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Questing
{
    public class QuestStatus
    {
        Quest quest;
        List<string> completedObjectives = new List<string>();

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompleteObjectives()
        {
            return completedObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective)
        {
            foreach(string objectiveToCheck in completedObjectives)
            {
                if (objective == objectiveToCheck)
                    return true;
            }
            return false;
        }
    }
}
