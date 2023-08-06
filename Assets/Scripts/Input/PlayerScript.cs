using UnityEngine;
using UnityEngine.InputSystem;

namespace CustomInput
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private float direction;
        private PlayerInput playerInput;
        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }

        public void Jump(InputAction.CallbackContext callback)
        {
            if (callback.performed)
                if (rb.velocity.y == 0)
                    rb.velocity = new Vector2(0, 15f);
        }

        public void Move(InputAction.CallbackContext callback)
        {
            direction = callback.ReadValue<float>();
        }
    }
}