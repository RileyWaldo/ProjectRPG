using System;
using UnityEngine;
using UnityEngine.UI;
using RPG.Stats;

namespace RPG.UI
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] Text healthText = null;

        Health health;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            healthText.text = String.Format("Health: {0:0}%", health.GetPercentage().ToString());
        }
    }
}
