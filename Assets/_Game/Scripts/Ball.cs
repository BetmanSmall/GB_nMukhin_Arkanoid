using _Game.Scripts.Managers;
using UnityEngine;
namespace _Game.Scripts {
    public class Ball : MonoBehaviour {
        [SerializeField] private Sprite[] sprites;
        private SpriteRenderer _spriteRenderer;
        private GameManager _gameManager;
        private SoundManager _soundManager;
        private Rigidbody2D _rigidbody;
        private float _speed = 10f;
        private bool _isButtonPressed;
        private Vector3 _initialPosition;
        private Paddle _paddle;

        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _gameManager = GameManager.Instance;
            _soundManager = SoundManager.Instance;
            _rigidbody = GetComponent<Rigidbody2D>();
            _paddle = GameObject.FindGameObjectWithTag("Paddle").GetComponent<Paddle>();
            _initialPosition = transform.position;
        }

        private void Update() {
            if (!Input.GetKeyDown(KeyCode.Mouse0) || _isButtonPressed) return;
            transform.SetParent(null);
            StartBall();
            _isButtonPressed = true;
        }

        private void StartBall() {
            _rigidbody.AddForce(Vector2.up.normalized * _speed, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Paddle")) {
                _soundManager.PlayHitSound();
                _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            }
            if (other.gameObject.CompareTag("BottomWall")) {
                ResetBall();
                _gameManager.CheckGameState();
            }
        }

        public void ResetBall() {
            _rigidbody.velocity = Vector2.zero;
            _initialPosition.x = _paddle.transform.position.x;
            transform.SetParent(_paddle.transform);
            transform.position = _initialPosition;
            _gameManager.lives--;
            _isButtonPressed = false;
        }
    }
}