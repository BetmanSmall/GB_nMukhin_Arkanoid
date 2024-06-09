using TMPro;
using UnityEngine;
namespace _Game.Scripts.Managers {
    public class ScoreManager : MonoBehaviour {
        public static ScoreManager Instance { get; private set; }
        public int Score { get; private set; }
        [SerializeField] private TMP_Text scoreText;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            } else {
                Instance = this;
            }
        }

        public void AddScore(int amount) {
            Score += amount;
            UpdateScoreText();
        }

        public void ResetScore() {
            Score = 0;
            UpdateScoreText();
        }

        private void UpdateScoreText() {
            scoreText.text = Score.ToString();
        }
    }
}