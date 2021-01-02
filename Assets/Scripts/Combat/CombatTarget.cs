using UnityEngine;
using RPG.Stats;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController callingController)
        {
            if (!enabled)
                return false;

            if(callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    callingController.GetComponent<Fighter>().Attack(gameObject);
                }
                return true;
            }
            return false;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }
    }
}
