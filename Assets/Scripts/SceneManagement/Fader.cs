using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeOutTime = 0.5f;
        [SerializeField] float fadeInTime = 0.5f;

        CanvasGroup canvas;
        Coroutine currentActiveFade = null;

        private void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
        }

        public Coroutine FadeOut()
        {
            return Fade(1, fadeOutTime);
        }

        public Coroutine FadeIn()
        {
            return Fade(0, fadeInTime);
        }

        public Coroutine Fade(float target, float time)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFade;
        }

        public void FadeOutImmediate()
        {
            canvas.alpha = 1f;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvas.alpha, target))
            {
                canvas.alpha = Mathf.MoveTowards(canvas.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}
