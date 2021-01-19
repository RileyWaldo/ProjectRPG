using UnityEngine;
using TMPro;

namespace RPG.UI.Questing
{
    public class QuestRewardUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI rewardText = null;
        [SerializeField] TextMeshProUGUI amountText = null;

        public void SetUp(string rewardName, string rewardAmount)
        {
            rewardText.text = rewardName;
            amountText.text = rewardAmount;
        }
    }
}
