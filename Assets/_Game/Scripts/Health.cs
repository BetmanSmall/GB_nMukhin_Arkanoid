using _Game.Scripts.Managers;
using UnityEngine;
namespace _Game.Scripts {
    public class Health : MonoBehaviour {
        private GameManager _gameManager;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] states;

        private void Awake() {
            _gameManager = GameManager.Instance;
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void Update() {
            if (_gameManager.lives > 0) {
                _spriteRenderer.sprite = states[_gameManager.lives - 1];
            }
        }
    }
}