namespace Squido {
    using UnityEngine;
    using SF = UnityEngine.SerializeField;

    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour {
        [SF] private Rigidbody rigidbody;
        [SF] private Vector3 start_initial_angular_velocity;
        private DampedVector3 pick_animation_damper = new DampedVector3();
        private bool isHeld = false;

        public void Drop(GameCharacter character, Vector3 velocity = default) {
            this.isHeld = false;
            Vector3 current_world_pos = this.transform.position;
            this.transform.parent = null;
            this.transform.position = current_world_pos;
            this.rigidbody.isKinematic = false;

            // Random spin
            this.rigidbody.angularVelocity = Random.onUnitSphere * Random.Range(-20, 20);

            this.rigidbody.velocity = velocity;
        }

        public bool IsGoingDown() {
            return this.rigidbody.velocity.y < 0;
        }

        private void Start() {
            this.rigidbody = GetComponent<Rigidbody>();
            this.rigidbody.angularVelocity = start_initial_angular_velocity;

            this.pick_animation_damper.Damping = 0.2f;
        }

        private void Update() {
            this.pick_animation_damper.ApplyDamping();

            if (isHeld)
                this.transform.localPosition = this.pick_animation_damper.Value;
        }

        public void GetPicked(GameCharacter character) {
            this.rigidbody.isKinematic = true;
            this.transform.parent = character.PickedBallAnchor;

            this.pick_animation_damper.ForceToValue(this.transform.localPosition);
            this.pick_animation_damper.Target = Vector3.zero;

            this.isHeld = true;
        }

    }
}
