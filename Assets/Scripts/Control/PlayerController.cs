using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using RPG.Movement;
using RPG.Stats;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float maxNavMeshPathLength = 40f;

        Health health;
        CursorControl cursor;

        private void Awake()
        {
            health = GetComponent<Health>();
            cursor = FindObjectOfType<CursorControl>();
        }

        void Update()
        {
            if (InteractWithUI())
                return;

            if (health.IsDead())
            {
                cursor.SetCursor(CursorType.None);
                return;
            }

            if (InteractWithComponent())
                return;

            if (InteractWithMovement()) 
                return;

            cursor.SetCursor(CursorType.None);
        }

        private bool InteractWithUI()
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                cursor.SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach(RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycastable in raycastables)
                {
                    if(raycastable.HandleRaycast(this))
                    {
                        cursor.SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        private RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];

            for(int i=0; i<hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits;
        }

        private bool InteractWithMovement()
        {
            Vector3 target = Vector3.zero;
            bool hasHit = RaycastNavMesh(out target);

            if (hasHit)
            {
                cursor.SetCursor(CursorType.Movement);
                if (Input.GetMouseButton(0))
                    GetComponent<Mover>().StartMoveAction(target, 1f);
            }
            return hasHit;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            RaycastHit hitInfo;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hitInfo);
            target = hitInfo.point;

            if (!hasHit)
                return false;

            NavMeshHit navMeshHit;
            if (!NavMesh.SamplePosition(target, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas))
                return false;

            target = navMeshHit.position;

            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path))
                return false;
            if (path.status != NavMeshPathStatus.PathComplete)
                return false;
            if (GetPathLength(path) > maxNavMeshPathLength)
                return false;

            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0f;

            if (path.corners.Length < 2)
                return total;

            for(int i=0; i< path.corners.Length-1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
