using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core
{
    public class OnEnterSceneTrigger : MonoBehaviour
    {
        [SerializeField] UnityEvent trigger = default;

        public void Trigger()
        {
            trigger.Invoke();
        }
    }
}
