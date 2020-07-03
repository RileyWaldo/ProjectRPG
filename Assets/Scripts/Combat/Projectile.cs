using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 4f;
        [SerializeField] bool isHoming = false;

        Health target = null;
        float damage = 0;
        float destroyTimer = 10f;

        private void Start()
        {
            SeekTarget();
        }

        private void Update()
        {
            if (isHoming && !target.IsDead())
            {
                SeekTarget();
            }
            UpdatePosition();
            DestroyTimer();
        }

        private void UpdatePosition()
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }

        private void DestroyTimer()
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
                Destroy(gameObject);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private void SeekTarget()
        {
            CapsuleCollider capsule = target.GetComponent<CapsuleCollider>();
            if (capsule == null)
                transform.LookAt(target.transform.position);
            else
            {
                Vector3 offSet = Vector3.up * capsule.height / 2;
                transform.LookAt(target.transform.position + offSet);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponent<Health>();
            if(health == target)
            {
                health.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

    }
}
