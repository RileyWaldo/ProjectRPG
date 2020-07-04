using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Stats
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;

        float maxHealth;
        bool isDead = false;

        private void Start()
        {
            SetHealth(GetComponent<BaseStats>().GetHealth());
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                if(!isDead)
                    Die();
            }
        }

        public float GetPercentage()
        {
            return (health / maxHealth) * 100f;
        }

        private void Die()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }

        private void SetHealth(float value)
        {
            health = value;
            maxHealth = value;
        }

        //saving functions
        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            if(health == 0)
            {
                Die();
            }
        }
    }
}
