﻿using System.Collections.Generic;
using UnityEngine;

namespace RPG.Questing
{
    [CreateAssetMenu(fileName = "Quest", menuName = "RPG/Create New Quest", order = 3)]
    public class Quest : ScriptableObject
    {
        [SerializeField] string[] objectives;

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectivesCount()
        {
            return objectives.Length;
        }

        public IEnumerable<string> GetObjectives()
        {
            return objectives;
        }


        //[SerializeField] bool mainStoryQuest = true;
        //[SerializeField] string title = "";
        //[TextArea(6, 6)]
        //[SerializeField] string description = "";
        //[SerializeField] QuestGoal[] goals = default;

        //public string GetTitle()
        //{
        //    return title;
        //}

        //public string GetDescription()
        //{
        //    return description;
        //}

        //public QuestGoal[] GetQuestGoals()
        //{
        //    return goals;
        //}

        //public bool IsComplete()
        //{
        //    bool isComplete = true;
        //    foreach(QuestGoal goal in goals)
        //    {
        //        if(!goal.IsComplete())
        //        {
        //            isComplete = false;
        //        }
        //    }
        //    return isComplete;
        //}

        //public bool IsMainStory()
        //{
        //    return mainStoryQuest;
        //}
    }
}
