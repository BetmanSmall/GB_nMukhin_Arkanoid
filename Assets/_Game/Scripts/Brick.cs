using UnityEngine;
namespace _Game.Scripts {
    public class Brick : MonoBehaviour {
        [SerializeField] private int _scoreForBrick;
        [SerializeField] private Sprite[] _states;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Sprite _initialSprite;
        private SoundManager _soundManager;
        private GameManager _gameManager;
        private ScoreManager _scoreManager;
        private int _health;

        private void Start() {
            _soundManager = SoundManager.Instance;
            _gameManager = GameManager.Instance;
            _scoreManager = ScoreManager.Instance;
            _health = _states.Length;
            _initialSprite = _spriteRenderer.sprite;
        }

        private void Hit() {
            _soundManager.PlaySound(_soundManager.hitSound);
            _health--;
            if (_health < 0) {
                gameObject.SetActive(false);
                _scoreManager.AddScore(_scoreForBrick);
                _gameManager.CheckGameState();
                return;
            }
            UpdateSprite();
        }

        public void ResetSprite() {
            _spriteRenderer.sprite = _initialSprite;
        }

        private void UpdateSprite() {
            _spriteRenderer.sprite = _states[_health];
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.name != "Ball") return;
            Hit();
        }
    }
}