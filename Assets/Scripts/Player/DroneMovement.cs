using System;
using UnityEngine;

namespace Player
{
    public class DroneMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 movementSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;

        private Vector2 _movementInput;
        
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            var targetSpeed = movementSpeed * _movementInput;
            var accelerationRate = (targetSpeed.sqrMagnitude > Mathf.Epsilon) ? acceleration : deceleration;
            var speedDifference = targetSpeed - _rigidbody.linearVelocity;
            
            _rigidbody.AddForce(_rigidbody.mass * accelerationRate * speedDifference);
        }
    }
}