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

            foreach(Reward reward in status.GetQuest().GetRewards())
            {
                if (reward.item == null)
                    continue;

                GameObject newReward = Instantiate(rewardPrefab, transform);
                newReward.GetComponent<QuestRewardUI>().SetUp(reward.item.Name, reward.amount.ToString());
            }

            foreach (Reward reward in status.GetQuest().GetRewards())
            {
                if (string.IsNullOrWhiteSpace(reward.faction))
                    continue;

                GameObject newReward = Instantiate(rewardPrefab, transform);
                newReward.GetComponent<QuestRewardUI>().SetUp(reward.faction, reward.respect.ToString() + " Respect");
            }
        }
    }
}
