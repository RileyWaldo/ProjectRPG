using UnityEngine;

namespace RPG.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomSFX : MonoBehaviour
    {
        [SerializeField] AudioClip[] audioClips = default;

        AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public AudioClip GetRandomClip()
        {
            var index = Random.Range(0, audioClips.Length);
            return audioClips[index];
        }

        public void PlayRandomClip()
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }
}
