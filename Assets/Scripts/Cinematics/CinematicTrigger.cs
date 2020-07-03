using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" && !isTriggered)
            {
                isTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}
