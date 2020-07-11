using UnityEngine;
using RPG.Control;

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
                PickUp(GetComponent<Fighter>());
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if(Input.GetMouseButtonDown(0))
            {
                PickUp(callingController.GetComponent<Fighter>());
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.PickUp;
        }
    }
}
