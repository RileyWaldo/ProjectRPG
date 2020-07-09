using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = default;

        public void Spawn(float damage)
        {
            var damageText = Instantiate(damageTextPrefab, transform);
            damageText.SetValue(damage);
        }
    }
}
