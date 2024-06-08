using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace _Game.Scripts {
    public class BricksManager : MonoBehaviour {
        public static BricksManager Instance { get; private set; }
        private Brick[] _bricks;
        [SerializeField] private GameObject[] _brickRows;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
            } else {
                Instance = this;
            }
        }

        private void Start() {
            AssignBricks();
        }

        public void ResetBricks() {
            foreach (var brick in _bricks) {
                brick.gameObject.SetActive(true);
                brick.ResetSprite();
            }
        }

        public bool IsCleared() {
            return _bricks.All(brick => !brick.gameObject.activeSelf);
        }

        private void AssignBricks() {
            List<Brick> allBricks = new List<Brick>();
            foreach (GameObject brickRow in _brickRows) {
                IEnumerable<Brick> bricksInRow = brickRow.transform
                    .Cast<Transform>()
                    .Select(brickTransform => brickTransform.GetComponent<Brick>());
                allBricks.AddRange(bricksInRow);
            }
            _bricks = allBricks.ToArray();
        }
    }
}