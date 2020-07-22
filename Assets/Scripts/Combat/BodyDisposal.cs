using System.Collections;
using UnityEngine;
using RPG.Stats;

namespace RPG.Combat
{
    public class BodyDisposal : MonoBehaviour
    {
        [SerializeField] float timeToFadeOut = 10f;
        [SerializeField] SkinnedMeshRenderer mesh = default;

        Health health;
        Material material;
        float fadeOutTime = 0f;
        bool fadeOut = false;

        private void Awake()
        {
            health = GetComponent<Health>();
            material = mesh.material;
        }

        private void Update()
        {
            if(health.IsDead())
            {
                fadeOutTime += Time.deltaTime;
                if(fadeOutTime >= timeToFadeOut && !fadeOut)
                {
                    fadeOut = true;
                    StartCoroutine(StartFadeOut());
                }
            }
        }

        private IEnumerator StartFadeOut()
        {
            while(material.color.a > 0.1f)
            {
                material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - 0.01f);
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
