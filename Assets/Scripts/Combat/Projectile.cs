using UnityEngine;
using UnityEngine.Events;
using RPG.Stats;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 4f;
        [SerializeField] bool isHoming = false;
        [SerializeField] UnityEvent onHit = default;

        Health target = null;
        GameObject instigator = null;
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

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.instigator = instigator;
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
                health.TakeDamage(instigator, damage);
                onHit.Invoke();
                Destroy(gameObject);
            }
        }

    }
}
