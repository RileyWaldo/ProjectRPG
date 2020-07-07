using UnityEngine;
using RPG.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experience = 0;

        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
        }

        public void GainXP(float xp)
        {
            experience += xp;
            var xpToNextLevel = baseStats.GetStat(Stat.XpToLevelUp);
            if(experience >= xpToNextLevel)
            {
                baseStats.LevelUp();
            }
        }

        public float GetXP()
        {
            return experience;
        }

        public object CaptureState()
        {
            return experience;
        }

        public void RestoreState(object state)
        {
            experience = (float)state;
        }
    }
}
