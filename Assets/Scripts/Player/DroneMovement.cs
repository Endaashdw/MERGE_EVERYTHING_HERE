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
        private SpriteRenderer sprite;
        private Shooting shootingScript;
        private Health healthScript;
        public bool dead = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            shootingScript = GetComponent<Shooting>();
            healthScript = GetComponent<Health>();

        }

        private void Update()
        {
            if (!dead)
            {
                _movementInput.x = Input.GetAxisRaw("Horizontal");
                _movementInput.y = Input.GetAxisRaw("Vertical");
            } else 
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RevivePlayer();
                }
            }
        }

        private void FixedUpdate()
        {
            var targetSpeed = movementSpeed * _movementInput;
            var accelerationRate = (targetSpeed.sqrMagnitude > Mathf.Epsilon) ? acceleration : deceleration;
            var speedDifference = targetSpeed - _rigidbody.linearVelocity;
            
            _rigidbody.AddForce(_rigidbody.mass * accelerationRate * speedDifference);
        }

        // Called when the player dies
        public void KillPlayer() {
            dead = true;
            Debug.Log("Player died!");
            sprite.enabled = false;

            // Disable the shooting script so the player can't shoot while dead
            if (shootingScript != null) {
                shootingScript.enabled = false;
            }
        }

        // Call this method to revive the player
        public void RevivePlayer() {
            dead = false;
            Debug.Log("Player revived!");
            sprite.enabled = true;
            shootingScript.OnRevive();
            healthScript.FullHeal();

            // Re-enable the shooting script so the player can shoot again
            if (shootingScript != null) {
                shootingScript.enabled = true;
            }
        }
    }
}