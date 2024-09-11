namespace Squido {
    using UnityEngine;

    [System.Serializable]
    public struct DampedVector2 {
        public Vector2 Value;
        public Vector2 Target;
        public Vector2 Velocity;
        public float Damping;

        public void ApplyDamping() {
            ApplyDamping(Time.deltaTime, Time.timeScale);
        }

        public void ApplyDamping(float dt, float timeScale) {
            float t = timeScale > 0f ? this.Damping / timeScale : this.Damping;
            this.Value = Vector2.SmoothDamp(this.Value, this.Target, ref Velocity, t, float.MaxValue, dt);
        }

        public void ForceToTargetValue() {
            this.Velocity = Vector2.zero;
            this.Value = Target;
        }
    }

    [System.Serializable]
    public struct DampedVector3 {
        public Vector3 Value;
        public Vector3 Target;
        public Vector3 Velocity;
        public float Damping;

        public void ApplyDamping() {
            ApplyDamping(Time.deltaTime, Time.timeScale);
        }

        public void ApplyDamping(float dt, float timeScale) {
            float t = timeScale > 0f ? this.Damping / timeScale : this.Damping;
            this.Value = Vector3.SmoothDamp(this.Value, this.Target, ref Velocity, t, float.MaxValue, dt);
        }

        public void ForceToTargetValue() {
            this.Velocity = Vector3.zero;
            this.Value = Target;
        }

        public void ForceToValue(Vector3 value) {
            this.Velocity = Vector3.zero;
            this.Value = value;
        }
    }
}
