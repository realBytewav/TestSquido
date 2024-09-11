namespace Squido {
    using UnityEngine;
    using SF = UnityEngine.SerializeField;

    // Should be the only MonoBehaviour
    // Use GameBehaviour for the rest and update them from this class
    public class Game : MonoBehaviour {
        [SF] private GameCharacter gameCharacter;
        [SF] private Ball ball;
        [SF] private Goal goal;
        private KeyboardController keyboardController = new();// use keyboard controller for now
        private GameScore gameScore = new();

        private void Start() {
            // Assertions
            {
                if (this.gameCharacter == null) {
                    Debug.LogError("Game.gameCharacter is null");
                    return;
                }
                if (this.ball == null) {
                    Debug.LogError("Game.ball is null");
                    return;
                }
                if (this.goal == null) {
                    Debug.LogError("Game.goal is null");
                    return;
                }
            }

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update() {
            if (this.gameCharacter != null)
                this.gameCharacter.GameUpdate(this.keyboardController);
            if (this.goal != null)
                this.goal.GameUpdate(this.gameScore);
            if (this.ball != null)
                this.ball.GameUpdate();

            // Unlock cursor
            if (((IGameController)this.keyboardController).NavigateBack())
                Cursor.lockState = CursorLockMode.None;
        }

        private void FixedUpdate() {
            if (this.ball != null)
                this.ball.GameFixedUpdate();
        }
    }
}
