using TMPro;
using UnityEngine;
using UnityEngine.Events;
namespace _Game.Scripts.Managers {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TextMeshProUGUI endGameText;
        [SerializeField] private Ball ball;
        [SerializeField] private Paddle paddle;
        private SoundManager _soundManager;
        private BricksManager _bricksManager;
        private ScoreManager _scoreManager;
        public int lives = 8;
        public UnityEvent onGameRestart = new UnityEvent();

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
            } else {
                Instance = this;
            }
        }

        private void Start() {
            _bricksManager = BricksManager.Instance;
            _soundManager = SoundManager.Instance;
            _scoreManager = ScoreManager.Instance;
        }

        public void CheckGameState() {
            if (lives <= 0) {
                GameOver();
            } else if (_bricksManager.IsCleared()) {
                Win();
            }
        }

        private void GameOver() {
            Time.timeScale = 0;
            _soundManager.PlaySound(_soundManager.loseSound);
            gameOverPanel.SetActive(true);
            endGameText.text = "Game Over";
        }

        private void Win() {
            Time.timeScale = 0;
            _soundManager.PlaySound(_soundManager.winSound);
            gameOverPanel.SetActive(true);
            endGameText.text = "You Win";
        }

        public void RestartGame() {
            ball.ResetBall();
            lives = 8;
            _scoreManager.ResetScore();
            gameOverPanel.SetActive(false);
            paddle.ToInitialPosition();
            _bricksManager.ResetBricks();
            Time.timeScale = 1;
            onGameRestart.Invoke();
        }

        public void QuitGame() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}