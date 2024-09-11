namespace Squido {
    using UnityEngine;
    using SF = UnityEngine.SerializeField;

    public class GameCharacter : MonoBehaviour {
        [SF] private FirstPersonCameraController fp_camera_controller;
        [SF] private float player_height = 1.75f; // in meter
        [SF] private float horizontal_speed = 20f;
        [SF] private DampedVector2 horizontal_speed_damper = new DampedVector2(); // world space
        [SF] private float velocity_vertical = 0f; // world space
        [SF] private float acceleration_factor = 20f;
        [SF] private float deceleration_factor = 20f;

        [SF] private bool is_on_ground = true;
        [SF] private Ball currently_held_ball = null;

        public void GameUpdate(IGameController gameController) {

            void HorizontalMovements() {
                float forward_back = 0f;
                float sides = 0f;

                if (gameController.Forward())
                    forward_back += horizontal_speed ;
                if (gameController.Back())
                    forward_back -= horizontal_speed ;
                if (gameController.Left())
                    sides -= horizontal_speed ;
                if (gameController.Right())
                    sides += horizontal_speed ;

                Vector2 sum = this.fp_camera_controller.GetFlattenForward() * forward_back;
                sum += this.fp_camera_controller.GetFlattenRight() * sides;
                this.horizontal_speed_damper.Target = sum;
            }

            void VerticalMovements() {
                float forward_back = 0f;
                float sides = 0f;

                if (gameController.Forward())
                    forward_back += this.acceleration_factor;
            }

            void AssignVelocity() {
                transform.position += new Vector3(this.horizontal_speed_damper.Value.x, this.velocity_vertical, this.horizontal_speed_damper.Value.y) * Time.deltaTime;
            }

            void Camera() {
                if (this.fp_camera_controller == null){
                    Debug.LogError("GameCharacter.fp_camera_controller is null");
                    return;
                }
                this.fp_camera_controller.SetHeight(this.player_height);
                this.fp_camera_controller.GameUpdate(gameController);
            }

            void ApplyDampers() {
                this.horizontal_speed_damper.ApplyDamping();
            }

            HorizontalMovements();
            VerticalMovements();
            AssignVelocity();
            Camera();
            ApplyDampers();
        }
    }
}
