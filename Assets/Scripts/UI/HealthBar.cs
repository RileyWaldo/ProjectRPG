using UnityEngine;
using RPG.Stats;

namespace RPG.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = default;
        [SerializeField] RectTransform forground = default;
        [SerializeField] Canvas canvas = default;

        private void Update()
        {
            float xScale = healthComponent.GetFraction();

            if(Mathf.Approximately(xScale, 0) || Mathf.Approximately(xScale, 1f))
            {
                canvas.enabled = false;
                return;
            }

            canvas.enabled = true;
            forground.localScale = new Vector3(xScale, 1f, 1f);
        }
    }
}
