using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference move;
        [SerializeField] private InputActionReference toggleDamping;
        [SerializeField] private float engineForce;
        [SerializeField] private float damping;

        [CanBeNull] private Rigidbody _rigidbody;
        private Vector2 _movement;
        private bool _isMoving;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            if (_rigidbody != null) _rigidbody.linearDamping = damping;

            if (move != null) 
            {
                move.action.performed += Move;
                move.action.canceled += Stop;
            }

            if (toggleDamping != null)
            {
                toggleDamping.action.performed += ToggleDamping;
            }
        }

        private void ToggleDamping(InputAction.CallbackContext _)
        {
            if (_rigidbody == null) return;

            if (_rigidbody.linearDamping > float.Epsilon)
            {
                _rigidbody.linearDamping = 0;
            }
            else
            {
                _rigidbody.linearDamping = damping;
            }
        }

        private void Move(InputAction.CallbackContext ctx)
        {
            _movement = Time.deltaTime * engineForce * ctx.ReadValue<Vector2>();
            _isMoving = true;
        }

        private void Stop(InputAction.CallbackContext _) => _isMoving = false;

        private void Update()
        {
            if (_isMoving) _rigidbody?.AddForce(_movement.x, 0, _movement.y, ForceMode.Acceleration);
        }

        private void OnDestroy()
        {
            move.action.performed -= Move;
            move.action.canceled -= Stop;
        }
    }
}