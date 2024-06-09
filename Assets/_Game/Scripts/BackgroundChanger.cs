using _Game.Scripts.Managers;
using UnityEngine;
namespace _Game.Scripts {
    public class BackgroundChanger : MonoBehaviour {
        [SerializeField] private Sprite[] sprites;
        private SpriteRenderer _spriteRenderer;
        private GameManager _gameManager;

        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _gameManager = GameManager.Instance;
            _gameManager.onGameRestart.AddListener(() => {
                _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            });
        }
    }
}