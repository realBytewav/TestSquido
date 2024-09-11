namespace Squido {
    using UnityEngine;
    using System.Collections.Generic;
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
        [SF] private float pick_ball_distance_treshold = 10f;
        [SF] private Ball currently_held_ball = null;
        [SF] private AnimationCurve throw_velocity_curve;
        [SF] private Transform pickedBallAnchor;
        [SF] private float throw_charge_anim_distance = 1f;
        [SF] private float shoot_force_multiplier = 10f;

        private Vector3 initial_anchor_position; // in Local
        private List<Ball> all_balls = new List<Ball>();
        private float ThrowPressTime;

        public Transform PickedBallAnchor => pickedBallAnchor;

        public void Start() {
            this.initial_anchor_position = pickedBallAnchor.transform.localPosition;

            void FindAllBalls() {
                all_balls.AddRange(Object.FindObjectsOfType<Ball>());
            }FindAllBalls();
        }

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

            void PickOrDropBall() {
                if (!((IGameController)gameController).Pick())
                    return;

                { //Drop
                    if (this.currently_held_ball != null) {
                        Vector3 passedVelocity = new Vector3();
                        Vector2 horizontal = this.horizontal_speed_damper.Value;
                        this.currently_held_ball.Drop(this, new Vector3(horizontal.x, 0.1f, horizontal.y));
                        this.currently_held_ball = null;
                        return;
                    }
                }

                { //Pick the closest ball
                    // Sort all_balls by distance from the current position
                    all_balls.Sort((ball1, ball2) => 
                    {
                        float distance1 = Vector3.Distance(transform.position, ball1.transform.position);
                        float distance2 = Vector3.Distance(transform.position, ball2.transform.position);
                        return distance1.CompareTo(distance2);
                    });

                    foreach(Ball iBall in this.all_balls) {
                        float distance = Vector3.Distance(transform.position, iBall.transform.position);
                        if (distance < this.pick_ball_distance_treshold) {
                            iBall.GetPicked(this);
                            this.currently_held_ball = iBall;
                            this.ThrowPressTime = 0f;
                            return;
                        }
                    }
                }
            }

            void ThrowBall() {
                if (this.currently_held_ball == null)
                    return;

                // Hold throw button
                if ( ((IGameController)gameController).HoldingThrow() )
                    this.ThrowPressTime += Time.deltaTime;

                // Let Go of throw button to actually lauch the ball
                if ( ((IGameController)gameController).Throw() ){

                    float shoot_force = this.throw_velocity_curve.Evaluate(this.ThrowPressTime);

                    Vector3 passedVelocity = this.fp_camera_controller.transform.forward * shoot_force * this.shoot_force_multiplier;
                    this.currently_held_ball.Drop(this, passedVelocity);
                    this.currently_held_ball = null;
                    this.ThrowPressTime = 0f;
                }
                return;
            }

            void MoveBallAnchorToShowThrowForce() {
                if (this.currently_held_ball == null)
                    return;

                Vector3 max_force_position = this.initial_anchor_position - Vector3.up * this.throw_charge_anim_distance;
                float throw_force_percent = this.throw_velocity_curve.Evaluate(this.ThrowPressTime);

                this.PickedBallAnchor.transform.localPosition = Vector3.Lerp(
                        this.initial_anchor_position,
                        max_force_position,
                        throw_force_percent
                        );
            }

            HorizontalMovements();
            VerticalMovements();
            AssignVelocity();
            Camera();
            ApplyDampers();
            PickOrDropBall();
            MoveBallAnchorToShowThrowForce();
            ThrowBall();
        }

    }
}
