using System;
using UnityEngine;
using UnityEngine.UI;
using RPG.Combat;
using RPG.Stats;

namespace RPG.UI
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        [SerializeField] Text enemyHealthText = null;

        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            Health target = fighter.GetTarget();
            if(target == null)
            {
                enemyHealthText.text = "";
            }
            else
            {
                enemyHealthText.text = String.Format("Enemy: {0:0}%", target.GetFraction());
            }
        }
    }
}
