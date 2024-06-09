using Unity.VisualScripting;
using UnityEngine;
namespace _Game.Scripts.Managers {
    public class SoundManager : MonoBehaviour {
        public static SoundManager Instance { get; private set; }
        public AudioClip hitSound;
        public AudioClip[] hitsSounds;
        public AudioClip winSound;
        public AudioClip loseSound;
        private AudioSource _audioSource;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            } else {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start() {
            _audioSource = gameObject.GetOrAddComponent<AudioSource>();
        }

        public void PlaySound(AudioClip sound) {
            _audioSource.PlayOneShot(sound);
        }

        public void PlayHitSound() {
            int index = Random.Range(0, hitsSounds.Length);
            _audioSource.PlayOneShot(hitsSounds[index]);
        }
    }
}