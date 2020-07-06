using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG/Stats/Make New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = default;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookUpTable;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookUp();

            float[] levels = lookUpTable[characterClass][stat];

            if(levels.Length < level)
            {
                Debug.LogWarning("Trying to access that doesn't exsist. "+characterClass+" : "+stat);
                return 0;
            }

            return levels[level - 1];
        }

        private void BuildLookUp()
        {
            if (lookUpTable != null) { return; }

            lookUpTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach(ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookUpTable = new Dictionary<Stat, float[]>();

                foreach(ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookUpTable[progressionStat.stat] = progressionStat.levels;
                }

                lookUpTable[progressionClass.characterClass] = statLookUpTable;
            }
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