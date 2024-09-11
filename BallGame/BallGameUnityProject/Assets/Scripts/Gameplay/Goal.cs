namespace Squido {
    using UnityEngine;
    using SF = UnityEngine.SerializeField;

    public class Goal : MonoBehaviour {
        private static readonly string BALL_TAG = "Ball"; // Tag of the ball
        private GameScore score;
        public void GameStart(GameScore score) {
            this.score = score;
        }

        void OnTriggerEnter(Collider other) {
            Debug.Log("OnTriggerEnter");
            if (!other.CompareTag(BALL_TAG)){
                Debug.Log("Not ball");
                return;
            }

            Ball ball = other.GetComponent<Ball>();
            if (ball == null){
                Debug.Log("No ball Component");
                return;
            }

            if (!ball.IsGoingDown()) {
                Debug.Log("Not going down");
                return;
            }

            Debug.Log("GrantPoints");
            score.GrantPoints(1);
        }
    }
}
