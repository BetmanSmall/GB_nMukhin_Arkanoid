using UnityEngine;
namespace _Game.Scripts {
    public class Paddle : MonoBehaviour {
        [SerializeField] private GameObject Ball;
        [SerializeField] private float _speed = 10f;
        private Rigidbody2D _ballRigidbody;
        private Vector3 _initialPosition;
        private float _minX;
        private float _maxX;
        private float _maxBounceAngle = 75;

        public void ToInitialPosition() {
            transform.position = _initialPosition;
        }

        private void Start() {
            _ballRigidbody = Ball.GetComponent<Rigidbody2D>();
            _initialPosition = transform.position;
            CalculateMovementBoundaries();
        }

        private void Update() {
            Move();
        }

        private void CalculateMovementBoundaries() {
            float cameraWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
            float halfPaddleWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
            _minX = -cameraWidth / 2f + halfPaddleWidth;
            _maxX = cameraWidth / 2f - halfPaddleWidth;
        }

        private void Move() {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float newPosition = transform.position.x + horizontalInput * _speed * Time.deltaTime;
            float clampedPosition = Mathf.Clamp(newPosition, _minX, _maxX);
            transform.position = new Vector3(clampedPosition, transform.position.y, transform.position.z);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            float width = other.otherCollider.bounds.size.x / 2;
            float offset = CalculateOffset(other);
            float newAngle = CalculateNewAngle(offset, width);
            ApplyBallVelocity(newAngle);
        }

        private void ApplyBallVelocity(float newAngle) {
            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            _ballRigidbody.velocity = rotation * Vector2.up * _ballRigidbody.velocity.magnitude;
        }

        private float CalculateOffset(Collision2D other) {
            Vector3 paddlePosition = transform.position;
            Vector2 contactPoint = other.GetContact(0).point;
            float offset = paddlePosition.x - contactPoint.x;
            return offset;
        }

        private float CalculateNewAngle(float offset, float width) {
            float currentAngle = Vector2.SignedAngle(Vector2.up, _ballRigidbody.velocity);
            float bounceAngle = (offset / width) * _maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -_maxBounceAngle, _maxBounceAngle);
            return newAngle;
        }
    }
}