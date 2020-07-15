using UnityEngine;
using RPG.Control;
using RPG.Movement;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon = null;

        private void PickUp(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                PickUp(other.GetComponent<Fighter>());
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(callingController.GetComponent<Mover>().CanMoveTo(transform.position))
                {
                    callingController.GetComponent<Mover>().StartMoveAction(transform.position, 1f);
                }
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.PickUp;
        }
    }
}
