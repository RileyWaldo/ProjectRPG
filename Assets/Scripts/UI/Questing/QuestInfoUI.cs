using UnityEngine;

namespace RPG.UI.Questing
{
    public class QuestInfoUI : MonoBehaviour
    {
        public void ShowInfo(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}
