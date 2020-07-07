using UnityEngine;
using UnityEngine.UI;
using RPG.Stats;

namespace RPG.UI
{
    public class LevelDisplay : MonoBehaviour
    {
        [SerializeField] Text levelText = default;

        BaseStats stats;

        private void Awake()
        {
            stats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            levelText.text = "Level: " + stats.GetCombatLevel().ToString();
        }
    }
}
