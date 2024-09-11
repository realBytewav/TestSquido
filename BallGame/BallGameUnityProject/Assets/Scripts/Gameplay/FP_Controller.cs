namespace Squido {
    using UnityEngine;
    using SF = UnityEngine.SerializeField;

    // Responsible for controlling Camera direction
    [RequireComponent(typeof(Camera))]
    public class FirstPersonCameraController : MonoBehaviour {
        private static readonly float EPSILON = 1e-6f;
        private static readonly float TAU = 6.283185307179586f;
        private static readonly float PI = 3.141592653589793f;
        private static readonly float HALF_PI = 1.5707963267948966f;

        [SF] private float MouseSensitivity = 0.1f;
        [SF] private IGameController gameController;// assigned by game
        [SF] private float theta = PI;
        [SF] private float phi = PI;
        private float height = 1.75f;

        public Vector2 GetFlattenForward () {
            Vector2 no_up = new(transform.forward.x, transform.forward.z);
            return no_up.normalized;
        }

        public Vector2 GetFlattenRight() {
            Vector2 no_up = new(transform.right.x, transform.right.z);
            return no_up.normalized;
        }

        public void SetHeight(float height) {
            this.height = height;
        }

        // Polar coordinate system (a point on a sphere)
        // | Latitude | Longitude
        // | theta θ  | phi φ
        private static Vector3 SphericalToVector(float theta, float phy) {
            //  wrap around 0 to max_value
            void wrapRadian(ref float value, float max_value) {
                while (theta < 0f)
                    value += max_value;

                value %= max_value;
            }
            //wrapRadian(ref theta, TAU);
            //wrapRadian(ref phy, TAU);

            return new Vector3(
                Mathf.Sin(theta),
                Mathf.Sin(theta) * Mathf.Sin(phy),
                Mathf.Cos(theta)
            );
        }

        public static Vector3 SphericalToCartesian(float polar, float elevation, float radius = 1){
            float a = radius * Mathf.Cos(elevation);
            return new Vector3(
                a * Mathf.Cos(polar),
                radius * Mathf.Sin(elevation),
                a * Mathf.Sin(polar)
                );
        }

        public void GameUpdate(in IGameController gameController) {
            if (gameController == null)
                return;

            // Assign polar coordinates
            {
                void wrapRadian(ref float value, float max_value) {
                    //while (theta < 0f)
                    //    value += max_value;

                    value %= max_value;
                }
                this.theta += gameController.CameraAxisHorizontal() * this.MouseSensitivity;
                this.phi += gameController.CameraAxisVertical() * this.MouseSensitivity;
                this.phi = Mathf.Clamp(this.phi,-HALF_PI+EPSILON,HALF_PI-EPSILON);
                wrapRadian(ref this.phi, TAU);
            }

            this.transform.localPosition = new (0f, this.height, 0f);
            this.transform.forward = SphericalToCartesian(-this.theta, this.phi);
        }

        public void OnGUI(){
        }
    }
}
