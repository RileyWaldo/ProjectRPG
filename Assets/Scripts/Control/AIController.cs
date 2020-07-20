using UnityEngine;
using RPG.Core;
using RPG.Combat;
using RPG.Stats;
using RPG.Movement;
using GameDevTV.Utils;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 4f;
        [SerializeField] PatrolPath patrolPath = default;
        [SerializeField] float wayPointTollerance = 1f;
        [SerializeField] float dwellTime = 1f;
        [Range(0, 1f)][SerializeField] float patrolSpeedFraction = 0.5f;
        [SerializeField] float aggroTime = 4f;
        [SerializeField] float aggroDistance = 4f;

        Health health;
        Fighter fighter;
        Mover mover;
        GameObject player;

        LazyValue<Vector3> guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        float timeSinceAggrevated = Mathf.Infinity;
        int currentWayPointIndex = 0;

        private void Awake()
        {
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");
            guardPosition = new LazyValue<Vector3>(InitGuardPos);
        }

        private Vector3 InitGuardPos()
        {
            return transform.position;
        }

        private void Update()
        {
            if (health.IsDead())
                return;

            if (IsAggrevated() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        public void Aggrevate()
        {
            timeSinceAggrevated = 0f;
        }

        private void UpdateTimers()
        {
            float deltaTime = Time.deltaTime;
            timeSinceLastSawPlayer += deltaTime;
            timeSinceArrivedAtWaypoint += deltaTime;
            timeSinceAggrevated += deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition.value;

            if(patrolPath != null)
            {
                if(AtWayPoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }
            if (timeSinceArrivedAtWaypoint >= dwellTime)
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }

        private bool AtWayPoint()
        {
            float distance = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return (distance <= wayPointTollerance);
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWayPoint(currentWayPointIndex);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);

            AggrevateNearbyEnemies();
        }

        private void AggrevateNearbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, aggroDistance, Vector3.up, 0f);

            foreach(RaycastHit hit in hits)
            {
                AIController ai = hit.transform.GetComponent<AIController>();
                if (ai == null) continue;

                ai.Aggrevate();
            }
        }

        private bool IsAggrevated()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance ||timeSinceAggrevated < aggroTime;
        }

        //called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
