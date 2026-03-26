using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference move;
        [SerializeField] private float speed;
        private Vector2 _movement;
        private bool _isMoving;

        private void Awake()
        {
            if (move == null)  return; // why unity does not update to c# 14??
            move.action.performed += Move;
            move.action.canceled += Stop;
        }

        private void Move(InputAction.CallbackContext ctx)
        {
            _movement = Time.deltaTime * speed * ctx.ReadValue<Vector2>();
            _isMoving = true;
        }

        private void Stop(InputAction.CallbackContext _) => _isMoving = false;

        private void Update()
        {
            if (_isMoving) transform.Translate(_movement.x, 0, _movement.y);
            Debug.Log(_movement);
        }

        private void OnDestroy()
        {
            move.action.performed -= Move;
            move.action.canceled -= Stop;
        }
    }
}
