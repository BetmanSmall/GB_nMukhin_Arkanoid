using Unity.VisualScripting;
using UnityEngine;
namespace _Game.Scripts {
    public class SoundManager : MonoBehaviour {
        public static SoundManager Instance { get; private set; }
        public AudioClip hitSound;
        public AudioClip[] hitsSounds;
        public AudioClip winSound;
        public AudioClip loseSound;
        private AudioSource _audioSource;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this.gameObject);
            } else {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Start() {
            _audioSource = this.gameObject.GetOrAddComponent<AudioSource>();
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