namespace Squido {
    using UnityEngine;
    using UnityEngine.Events;
    using SF = UnityEngine.SerializeField;

    public class GameScore {
        public UnityEvent OnGrantPoints;
        private static readonly uint MAX_POINTS = 99; // to avoid int overflow
        private uint points = 0;
        private string pointsString = "Points: 0";

        public string PointsString => pointsString;

        public void GrantPoints(uint num_points) {
            if (this.points == MAX_POINTS){
                return;
            }

            uint new_points = this.points + num_points;
            if (new_points < MAX_POINTS){
                this.points = new_points;
            }
            else
                this.points = MAX_POINTS;

            // Todo: Use a string builder
            this.pointsString = "Points:" + this.points.ToString();
            if (OnGrantPoints != null)
                OnGrantPoints.Invoke();
        }
    }
}
