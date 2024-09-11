namespace Squido {
    using UnityEngine;
    using SF = UnityEngine.SerializeField;

    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour {
        [SF] private Rigidbody rigidbody;

        public void GameUpdate() {}
        public void GameFixedUpdate() {}

        private void Start() {
            this.rigidbody = GetComponent<Rigidbody>();
            this.rigidbody.angularVelocity = new (-50,0,0);
        }

    }
}
