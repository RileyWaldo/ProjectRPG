using System.Collections.Generic;
using UnityEngine;

namespace RPG.Questing
{
    [CreateAssetMenu(fileName = "Quest", menuName = "RPG/Create New Quest", order = 3)]
    public class Quest : ScriptableObject
    {
        [SerializeField] Objective[] objectives = default;

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

        public int GetObjectivesCount()
        {
            return objectives.Length;
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return objectives;
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

        public static Quest GetByName(string questName)
        {
            foreach(Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName)
                    return quest;
            }
            return null;
        }
    }
}
