namespace Squido {
    using UnityEngine;
    using UnityEngine.Events;
    using SF = UnityEngine.SerializeField;

    public class GameScore {
        public UnityEvent OnGrantPoints;
        private static readonly uint MAX_POINTS;
        private uint points = 0;

        public void GrantPoints(uint num_points) {
            if (this.points == MAX_POINTS)
                return;

            uint new_points = this.points + num_points;
            if (new_points < MAX_POINTS)
                this.points += points;
            else
                this.points = MAX_POINTS;

            if (OnGrantPoints != null)
                OnGrantPoints.Invoke();
        }
    }
}
