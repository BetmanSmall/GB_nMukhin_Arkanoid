using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace _Game.Scripts.Managers {
    public class BricksManager : MonoBehaviour {
        public static BricksManager Instance { get; private set; }
        private List<Brick> _allBricks;
        [SerializeField] private GameObject[] brickRows;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
            } else {
                Instance = this;
            }
        }

        private void Start() {
            _allBricks = new List<Brick>();
            foreach (GameObject brickRow in brickRows) {
                IEnumerable<Brick> bricksInRow = brickRow.transform
                    .Cast<Transform>()
                    .Select(brickTransform => brickTransform.GetComponent<Brick>());
                _allBricks.AddRange(bricksInRow);
            }
        }

        public void ResetBricks() {
            foreach (var brick in _allBricks) {
                brick.gameObject.SetActive(true);
                brick.ResetSprite();
            }
        }

        public bool IsCleared() {
            return _allBricks.All(brick => !brick.gameObject.activeSelf);
        }
    }
}