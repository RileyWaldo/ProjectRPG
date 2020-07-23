using UnityEngine;
using RPG.Combat;

namespace RPG.Questing
{
    [CreateAssetMenu(fileName = "KillGoal", menuName = "RPG/Questing/Goals/Create Kill Goal")]
    public class GoalKill : QuestGoal
    {
        [SerializeField] CombatTarget enemyToKill = default;

        public override void CheckProgress()
        {
            
        }
    }
}
