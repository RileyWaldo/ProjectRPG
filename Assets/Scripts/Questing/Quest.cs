using System.Collections.Generic;
using UnityEngine;

namespace RPG.Questing
{
    [CreateAssetMenu(fileName = "Quest", menuName = "RPG/Create New Quest", order = 3)]
    public class Quest : ScriptableObject
    {
        [SerializeField] bool isMainQuest = true;
        [SerializeField] Objective[] objectives = default;
        [SerializeField] Reward[] rewards = default;

        static Dictionary<string, Quest> questLookUpCache;

        public static Quest GetByName(string questName)
        {
            if(questLookUpCache == null)
            {
                questLookUpCache = new Dictionary<string, Quest>();
                Quest[] quests = Resources.LoadAll<Quest>("");
                foreach(Quest quest in quests)
                {
                    if(questLookUpCache.ContainsKey(quest.GetTitle()))
                    {
                        Debug.LogError("Looks like there is a duplicate quest: " + quest.GetTitle());
                        continue;
                    }
                    questLookUpCache[quest.GetTitle()] = quest;
                }
            }

            if (questName == null || !questLookUpCache.ContainsKey(questName))
                return null;

            return questLookUpCache[questName];
        }

        [System.Serializable]
        public class Objective
        {
            public string reference = "";
            public string description = "";
        }

        public string GetTitle()
        {
            return name;
        }

        public bool IsMainQuest()
        {
            return isMainQuest;
        }

        public int GetObjectivesCount()
        {
            return objectives.Length;
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return objectives;
        }

        public IEnumerable<Reward> GetRewards()
        {
            return rewards;
        }

        public bool HasObjective(string objectiveRef)
        {
            foreach(Objective objective in objectives)
            {
                if (objective.reference == objectiveRef)
                    return true;
            }
            return false;
        }
    }
}
