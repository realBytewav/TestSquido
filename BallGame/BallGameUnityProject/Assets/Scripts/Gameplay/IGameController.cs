namespace Squido {
    // Represent a game controller. Could be keyboard, gamepad, or ai
    public interface IGameController {
        public bool NavigateBack();
        public bool Forward();
        public bool Back();
        public bool Left();
        public bool Right();
        public bool Jump();
        public bool HoldingThrow();
        public bool Throw();
        public bool Pick();
        public float CameraAxisHorizontal();
        public float CameraAxisVertical();
    }
}
