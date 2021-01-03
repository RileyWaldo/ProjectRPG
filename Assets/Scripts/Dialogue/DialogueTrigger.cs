using UnityEngine;
using UnityEngine.Events;

namespace RPG.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] string action = "";
        [SerializeField] UnityEvent onTrigger = null;

        public void Trigger(string actionToTrigger)
        {
            if(actionToTrigger == action)
            {
                onTrigger.Invoke();
            }
        }
    }
}
