namespace Source
{
    public interface IInput
    {
        public float MouseHorizontalMove();
        public float MouseVerticalMove();
        public float HorizontalMove();
        public float VerticalMove();
        public float GrapplingHookMove();
        public float[] CursorPos();
        bool Shoot();
        bool ReturnHook();
        bool Jump();
    }
}