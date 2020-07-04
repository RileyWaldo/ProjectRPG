using UnityEngine;
using UnityEngine.AI;
using RPG.Stats;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;

        bool isDead = false;

        private void Start()
        {
            health = GetComponent<BaseStats>().GetHealth();
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            if (health == 0)
                return;
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            health = 0;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
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
