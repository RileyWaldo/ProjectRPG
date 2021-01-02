using UnityEngine;

namespace RPG.Combat
{
    public class AggroGroup : MonoBehaviour
    {
        [SerializeField] bool activateOnStart = false;
        [SerializeField] Fighter[] fighters;

        private void Start()
        {
            Activate(activateOnStart);
        }

        public void Activate(bool shouldActivate)
        {
            foreach(Fighter fighter in fighters)
            {
                fighter.enabled = shouldActivate;
                CombatTarget target = fighter.GetComponent<CombatTarget>();
                if(target != null)
                {
                    target.enabled = shouldActivate;
                }
            }
        }
    }
}
