using UnityEngine;

namespace RPG.Questing
{
    public abstract class QuestGoal : ScriptableObject
    {
        [SerializeField] int amountToAchieve = 1;

        bool isComplete = false;
        int amount = 0;

        public abstract void CheckProgress();

        public int GetProgressAmount()
        {
            return amount;
        }

        public int GetAchieveAmount()
        {
            return amountToAchieve;
        }

        public bool IsComplete()
        {
            return isComplete;
        }
    }
}
