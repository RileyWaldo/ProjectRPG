using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int combatLevel = 1;
        [SerializeField] int respect = 0;
        [SerializeField] CharacterClass characterClass = default;
        [SerializeField] Progression progression = default;
        [SerializeField] GameObject levelUpVFX = default;

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + (GetPercentageModifier(stat)/100));
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, combatLevel);
        }

        private float GetAdditiveModifier(Stat stat)
        {
            float total = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        public void LevelUp()
        {
            combatLevel += 1;
            GetComponent<Health>().SetHealth(GetStat(Stat.Health));
            LevelUpParticleEffect();
        }

        private void LevelUpParticleEffect()
        {
            Instantiate(levelUpVFX, transform);
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
