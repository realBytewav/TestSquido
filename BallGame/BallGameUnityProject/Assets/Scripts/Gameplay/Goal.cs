namespace Squido {
    using UnityEngine;
    using SF = UnityEngine.SerializeField;

    public class Goal : MonoBehaviour {
        public void GameUpdate(GameScore score) {
            bool DetectGoal() { return false;}

            if (DetectGoal())
                score.GrantPoints(1);
        }
    }
}

