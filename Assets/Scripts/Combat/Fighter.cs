using UnityEngine;
using RPG.Movement;
using RPG.Stats;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        Health target;
        Animator animator;
        float timeSinceLastAttack = Mathf.Infinity;

        Weapon currentWeapon = null;

        private void Start()
        {
            animator = GetComponent<Animator>();
            EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null || target.IsDead())
                return;

            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform, Vector3.up);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //this will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        //Animation Event
        void Hit()
        {
            if (target != null)
            {
                if(currentWeapon.HasProjectile())
                {
                    currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
                }
                else
                {
                    target.TakeDamage(currentWeapon.GetWeaponDamage());
                }
            }
        }
        //Animation Event

        void FootR()
        {

        }

        void FootL()
        {

        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("StopAttack");
            animator.SetTrigger("Attack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget != null && !combatTarget.GetComponent<Health>().IsDead())
                return true;
            else
                return false;
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= currentWeapon.GetWeaponRange();
        }

        public void Attack(GameObject combatTarget)
        {
            target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);
        }

        public void Cancel()
        {
            target = null;
            StopAttack();
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("StopAttack");
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (weapon == null)
                return;
            currentWeapon = weapon;
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
        }
    }
}
