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
            float t = timeScale > 0f ? Damping / timeScale : Damping;
            Value = Vector2.SmoothDamp(Value, Target, ref Velocity, t, float.MaxValue, dt);
        }

        public void ForceToTargetValue() {
            Velocity = Vector2.zero;
            Value = Target;
        }
    }
}
