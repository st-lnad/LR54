using UnityEngine;

namespace Source
{
    public class DesktopInput : IInput
    {
        private const string MouseHorizontalAxis = "Mouse X";
        private const string MouseVerticalAxis = "Mouse Y";
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const KeyCode GrapplingHookUpKey = KeyCode.E;
        private const KeyCode GrapplingHookDownKey = KeyCode.Q;
        private const KeyCode ShootKey = KeyCode.Mouse0;
        private const KeyCode ReturnHookKey = KeyCode.Mouse1;
        private const KeyCode JumpKey = KeyCode.W;

        public float GrapplingHookMove()
        {
            return (Input.GetKey(GrapplingHookUpKey) ? 1f : 0f) - (Input.GetKey(GrapplingHookDownKey) ? 1f : 0f);
        }

        public float HorizontalMove()
        {
            return Input.GetAxis(HorizontalAxis);
        }

        public float VerticalMove()
        {
            return Input.GetAxis(VerticalAxis);
        }

        public float MouseHorizontalMove()
        {
            return Input.GetAxis(MouseHorizontalAxis);
        }

        public float MouseVerticalMove()
        {
            return Input.GetAxis(MouseVerticalAxis);
        }

        public float[] CursorPos()
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new float[2] { position.x, position.y };
        }

        public bool Shoot()
        {
            return Input.GetKeyDown(ShootKey);
        }

        public bool ReturnHook()
        {
            return Input.GetKeyDown(ReturnHookKey);
        }

        public bool Jump()
        {
            return Input.GetKeyDown(JumpKey);
        }
    }
}