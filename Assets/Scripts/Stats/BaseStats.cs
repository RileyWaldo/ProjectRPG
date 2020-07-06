using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range (1, 99)]
        [SerializeField] int combatLevel = 1;
        [SerializeField] int respect = 0;
        [SerializeField] CharacterClass characterClass = default;
        [SerializeField] Progression progression = default;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, combatLevel);
        }

        public void LevelUp()
        {
            combatLevel += 1;
            GetComponent<Health>().SetHealth(GetStat(Stat.Health));
        }

        public int GetCombatLevel()
        {
            return combatLevel;
        }

        public int GetRespectLevel()
        {
            return respect;
        }
    }
}
