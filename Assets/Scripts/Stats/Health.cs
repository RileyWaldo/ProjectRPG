﻿using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using RPG.Core;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] TakeDamageEvent takeDamage = default;
        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        { 
        }

        float health = -1f;
        float maxHealth;
        bool isDead = false;

        private void Start()
        {
            if(health < 0)
            {
                SetHealth(GetComponent<BaseStats>().GetStat(Stat.Health));
            }
            maxHealth = health;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                if(!isDead)
                {
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
            return health / maxHealth;
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
            if(health <= 0)
            {
                Die();
            }
        }
    }
}
