using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeTime = 2f;

        CanvasGroup canvas;

        private void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut()
        {
            while(canvas.alpha < 1f)
            {
                canvas.alpha += Time.deltaTime / fadeTime;
                yield return null;
            }
        }

        public IEnumerator FadeIn()
        {
            while (canvas.alpha > 0)
            {
                canvas.alpha -= Time.deltaTime / fadeTime;
                yield return null;
            }
        }

        public void FadeOutNow()
        {
            canvas.alpha = 1f;
        }
    }
}
