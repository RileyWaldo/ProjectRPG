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
        [SerializeField] float raycastRadius = 0.5f;

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
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
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
                if (!GetComponent<Mover>().CanMoveTo(target))
                    return false;

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
            target = Vector3.zero;

            if (!hasHit)
                return false;

            NavMeshHit navMeshHit;
            if (!NavMesh.SamplePosition(hitInfo.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas))
                return false;

            target = navMeshHit.position;

            return true;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
