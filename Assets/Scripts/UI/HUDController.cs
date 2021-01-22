using UnityEngine;
using RPG.UI.Questing;

namespace RPG.UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("Windows")]
        [SerializeField] GameObject menuyWindow = null;

        [Header("Key Bindings")]
        [SerializeField] KeyCode menuHotKey = KeyCode.I;

        private void Start()
        {
            menuyWindow.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(menuHotKey))
            {
                ToggleInventory();
            }
        }

        public void ToggleInventory()
        {
            menuyWindow.SetActive(!menuyWindow.activeSelf);
        }
    }
}
