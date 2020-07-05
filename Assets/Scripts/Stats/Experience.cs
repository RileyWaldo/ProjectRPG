using UnityEngine;
using RPG.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experience = 0;

        public void GainXP(float xp)
        {
            experience += xp;
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
