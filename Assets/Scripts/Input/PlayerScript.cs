using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.CustomInput
{
    public class PlayerScript : MonoBehaviour
    {
        private Rigidbody2D rb;
        private PlayerInput playerInput;
        private float direction;
        [SerializeField] private float speed = 5f;
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
            {
                if (rb.velocity.y == 0)
                {
                    rb.velocity = new Vector2(0, 15f);
                }

            }
        }

        public void Move(InputAction.CallbackContext callback)
        {
            direction = callback.ReadValue<float>();
        }


    }
}