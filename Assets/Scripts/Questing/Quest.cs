using System.Collections.Generic;
using UnityEngine;

namespace RPG.Questing
{
    [CreateAssetMenu(fileName = "Quest", menuName = "RPG/Create New Quest", order = 3)]
    public class Quest : ScriptableObject
    {
        [SerializeField] string[] objectives = default;

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

        public bool HasObjective(string objectiveToCheck)
        {
            foreach(string objective in objectives)
            {
                if (objective == objectiveToCheck)
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
