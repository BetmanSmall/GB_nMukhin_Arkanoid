using UnityEngine;
namespace _Game.Scripts {
    public class Health : MonoBehaviour {
        private GameManager _gameManager;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _states;

        private void Awake() {
            _gameManager = GameManager.Instance;
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void Update() {
            if (_gameManager.Lives > 0) {
                _spriteRenderer.sprite = _states[_gameManager.Lives - 1];
            }
        }
    }
}