using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Stats;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        CursorControl cursor;

        private void Awake()
        {
            health = GetComponent<Health>();
            cursor = FindObjectOfType<CursorControl>();
        }

        void Update()
        {
            if (health.IsDead())
                return;
            if (InteractWithCombat())
                return;
            if (InteractWithMovement()) 
                return;
            cursor.SetCursor(CursorType.None);
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null || !GetComponent<Fighter>().CanAttack(target.gameObject))
                    continue;
                if (Input.GetMouseButton(0))
                    GetComponent<Fighter>().Attack(target.gameObject);
                cursor.SetCursor(CursorType.Combat);
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hitInfo;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hitInfo);
            if (hasHit)
            {
                cursor.SetCursor(CursorType.Movement);
                if (Input.GetMouseButton(0))
                    GetComponent<Mover>().StartMoveAction(hitInfo.point, 1f);
            }
            return hasHit;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
