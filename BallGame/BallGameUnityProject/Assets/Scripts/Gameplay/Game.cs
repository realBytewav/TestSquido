namespace Squido {
    using UnityEngine;
    using SF = UnityEngine.SerializeField;

    public class Game : MonoBehaviour {
        [SF] private GameCharacter gameCharacter;
        [SF] private Goal goal;
        private KeyboardController keyboardController = new();// use keyboard controller for now
        private GameScore gameScore = new();

        private void Start() {
            { // Assertions
                if (this.gameCharacter == null) {
                    Debug.LogError("Game.gameCharacter is null");
                    return;
                }
                if (this.goal == null) {
                    Debug.LogError("Game.goal is null");
                    return;
                }
            }

            Cursor.lockState = CursorLockMode.Locked;
            this.goal.GameStart(this.gameScore);
        }

        private void Update() {
            if (this.gameCharacter != null)
                this.gameCharacter.GameUpdate(this.keyboardController);

            // Unlock cursor
            if (((IGameController)this.keyboardController).NavigateBack())
                Cursor.lockState = CursorLockMode.None;
        }

        private void OnGUI() {
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleLeft;

            GUI.Label(new Rect(10, 10, 300, 40), "Look around: mouse", style);
            GUI.Label(new Rect(10, 30, 300, 40), "Move: W, A, S, D", style);
            GUI.Label(new Rect(10, 50, 300, 40), "Pick/Drop ball: E", style);
            GUI.Label(new Rect(10, 70, 300, 40), "Throw: HOLD mouse left and release", style);

            GUI.Label(new Rect(10, 100, 300, 40), this.gameScore.PointsString , style);

            GUIStyle styleSmall = new GUIStyle();
            styleSmall.fontSize = 10;
            styleSmall.normal.textColor = Color.white;
            styleSmall.alignment = TextAnchor.MiddleLeft;
            GUI.Label(new Rect(10, 130, 300, 40), "I wish I had time to do more. Enjoy :)", styleSmall);
        }
    }
}
