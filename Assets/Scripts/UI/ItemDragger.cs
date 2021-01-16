using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RPG.UI
{
    public class ItemDragger : MonoBehaviour
    {
        [SerializeField] Image image = null;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            if (!EventSystem.current.IsPointerOverGameObject())
                return;

            GameObject UI = EventSystem.current.currentSelectedGameObject;
            //if (UI.GetComponent<>())
        }
    }
}
