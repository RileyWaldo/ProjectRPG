using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG/Stats/Make New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = default;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            foreach(ProgressionCharacterClass progressionClass in characterClasses)
            {
                if(progressionClass.characterClass == characterClass)
                {
                    foreach(ProgressionStat progressionStat in progressionClass.stats)
                    {
                        if(progressionStat.stat == stat)
                        {
                            var length = progressionStat.levels.Length;
                            var index = level - 1;
                            if (index < length)
                                return progressionStat.levels[index];
                            else
                            {
                                Debug.Log("Trying to access stat that doesn't exist. " + progressionClass.characterClass +" : " + progressionStat.stat);
                                return 0;
                            }
                        }
                    }
                }
            }
            Debug.Log("Trying to access stat that doesn't exist.");
            return 0;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass = default;
            public ProgressionStat[] stats = default;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat = default;
            public float[] levels = default;
        }
    }
}