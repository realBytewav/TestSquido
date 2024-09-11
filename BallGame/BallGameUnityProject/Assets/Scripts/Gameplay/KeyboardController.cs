namespace Squido {
    using UnityEngine;

    // Using legacy Inputs for simplicity atm
    public class KeyboardController: IGameController {
        private static readonly int MOUSE_LEFT_CLICK = 0;

        bool IGameController.NavigateBack() { return Input.GetKeyDown(KeyCode.Escape); }
        bool IGameController.Forward() { return Input.GetKey(KeyCode.W); }
        bool IGameController.Left() { return Input.GetKey(KeyCode.A); }
        bool IGameController.Back() { return Input.GetKey(KeyCode.S); }
        bool IGameController.Right() { return Input.GetKey(KeyCode.D); }
        bool IGameController.Jump() { return Input.GetKeyDown(KeyCode.Space); }
        bool IGameController.HoldingThrow() { return Input.GetMouseButton(MOUSE_LEFT_CLICK);}
        bool IGameController.Throw() { return Input.GetMouseButtonUp(MOUSE_LEFT_CLICK); }
        bool IGameController.Pick() { return Input.GetKeyDown(KeyCode.E); }

        float IGameController.CameraAxisHorizontal(){
             return Input.GetAxisRaw("Mouse X");
        }

        float IGameController.CameraAxisVertical(){
             return Input.GetAxisRaw("Mouse Y");
        }
    }
}
