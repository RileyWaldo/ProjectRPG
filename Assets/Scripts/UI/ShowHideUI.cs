using UnityEngine;
using UnityEngine.Events;

namespace RPG.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] KeyCode toggleKey = KeyCode.Escape;
        [SerializeField] GameObject uiContainer = null;
        [SerializeField] UnityEvent onToggle = null;

        private void Start()
        {
            uiContainer.SetActive(false);
        }

        private void Update()
        {
            if(Input.GetKeyDown(toggleKey))
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            uiContainer.SetActive(!uiContainer.activeSelf);
            onToggle.Invoke();
        }

        public void SetToggle(bool toggle)
        {
            uiContainer.SetActive(toggle);
        }
    }
}
