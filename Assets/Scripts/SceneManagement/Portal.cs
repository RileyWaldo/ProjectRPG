using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Core;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = 0;
        [SerializeField] Transform spawnPoint = default;
        [SerializeField] DestinationTag destinationTag = default;
        [SerializeField] float fadeWaitTime = 0.5f;

        enum DestinationTag
        {
            A, B, C, D, E, F
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            SaveWrapper saveWrapper = FindObjectOfType<SaveWrapper>();

            DisableControl();

            yield return fader.FadeOut();

            saveWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            DisableControl();

            saveWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            saveWrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn();
            EnableControl();
            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            foreach(Portal portal in portals)
            {
                if (portal == this)
                    continue;
                if (portal.destinationTag == destinationTag)
                    return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            if (otherPortal == null)
                return;
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private void DisableControl()
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnableControl()
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
