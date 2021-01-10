﻿using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Core;

namespace RPG.Questing
{
    public class QuestTracker : MonoBehaviour, ISaveable, IPredicateEvaluator
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

        public void CompleteObjective(Quest quest, string objective)
        {
            QuestStatus status = GetQuestStatus(quest);
            if (status.IsObjectiveComplete(objective))
                return;

            status.CompleteObjective(objective);

            if(status.IsComplete())
            {
                GiveReward(quest);
            }

            onUpdateQuest?.Invoke();
        }

        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

        public object CaptureState()
        {
            List<object> state = new List<object>();
            foreach(QuestStatus status in statuses)
            {
                state.Add(status.CaptureState());
            }
            return state;
        }

        public void RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList == null)
                return;

            statuses.Clear();
            foreach(object objectState in stateList)
            {
                statuses.Add(new QuestStatus(objectState));
            }
        }

        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in statuses)
            {
                if (status.GetQuest() == quest)
                    return status;
            }
            return null;
        }

        private void GiveReward(Quest quest)
        {
            Debug.Log("Quest Complete: heres your real fake rewards!");
            //TODO: give rewards!!
        }

        public bool? Evaluate(PredicateType predicate, string[] parameters)
        {
            switch(predicate)
            {
                case PredicateType.HasQuest:
                    return HasQuest(Quest.GetByName(parameters[0]));

                case PredicateType.CompletedQuest:
                    return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();

                case PredicateType.CompletedObjective:
                    return GetQuestStatus(Quest.GetByName(parameters[0])).IsObjectiveComplete(parameters[1]);
            }

            return null;
        }
    }
}
