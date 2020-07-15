using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using RPG.Core;
using RPG.Saving;
using GameDevTV.Utils;

namespace RPG.Stats
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] TakeDamageEvent takeDamage = default;
        [SerializeField] UnityEvent onDie = default;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        { 
        }

        LazyValue<float> health;
        float maxHealth;
        bool isDead = false;

        private void Awake()
        {
            health = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            health.ForceInit();
            maxHealth = health.value;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            health.value -= damage;
            if (health.value <= 0)
            {
                health.value = 0;
                if(!isDead)
                {
                    onDie.Invoke();
                    GainXP(instigator);
                    Die();
                }
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public float GetFraction()
        {
            return health.value / maxHealth;
        }

        private void Die()
        {
            isDead = true;
            DisableComponents();
        }

        private void DisableComponents()
        {
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }

        private void GainXP(GameObject instigator)
        {
            var experience = instigator.GetComponent<Experience>();
            if (experience == null) { return; }

            var xpToGain = GetComponent<BaseStats>().GetStat(Stat.XpReward);

            experience.GainXP(xpToGain);
        }

        public void SetHealth(float value)
        {
            health.value = value;
            maxHealth = value;
        }

        //saving functions
        public object CaptureState()
        {
            return health.value;
        }

        public void RestoreState(object state)
        {
            health.value = (float)state;
            if(health.value <= 0)
            {
                Die();
            }
        }
    }
}
