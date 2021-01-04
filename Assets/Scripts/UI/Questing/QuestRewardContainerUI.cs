using UnityEngine;
using RPG.Questing;

namespace RPG.UI.Questing
{
    public class QuestRewardContainerUI : MonoBehaviour
    {
        [SerializeField] GameObject rewardPrefab = default;

        public void SetUp(QuestStatus status)
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
