namespace Squido {
    // Represent a game controller. Could be keyboard, gamepad, or ai
    public interface IGameController {
        public bool NavigateBack();
        public bool Forward();
        public bool Back();
        public bool Left();
        public bool Right();
        public bool Jump();
        public bool Throw();
        public float CameraAxisHorizontal();
        public float CameraAxisVertical();
    }
}
