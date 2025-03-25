using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

namespace Player {
	public class codaScript : MonoBehaviour
	{
		[SerializeField] public float jumpVelocity = 7f;
		[SerializeField] private float fallMultiplier = 2f; 
		[SerializeField] private SpriteRenderer sprite;
		[SerializeField] private Canvas hud;
		[SerializeField] private GameObject death_text;

		private bool jump = false;
		private Rigidbody2D RB;
		private bool isGrounded = false;
		public bool dead = false;

		public Checkpoint current_checkpoint = null;

		private void Awake()
		{
			RB = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			if (dead) {
				if (Input.GetKeyDown(KeyCode.Space)) {
					RestartToCheckpoint(current_checkpoint);
				}
				return;
			}

			if (jump) {
				RB.velocity += new Vector2(0, jumpVelocity);
				jump = false;
			}

			// Jump Input Handling
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (isGrounded) {
					jump = true;
				}
			}

			if (Input.GetKeyUp(KeyCode.Space)) {
				if (!isGrounded) {
					jump = false;
				}
			}

			// Fall multiplier for better jump feel
			if (RB.velocity.y < 0) {
				RB.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
			}
		}

		public void RestartToCheckpoint(Checkpoint checkpoint) {
			if (checkpoint != null) {
				transform.SetPositionAndRotation(checkpoint.gameObject.transform.position, transform.rotation);
			}
			else {
				transform.SetPositionAndRotation(new Vector2(-13.0f, -1.0f), transform.rotation);
			}

			dead = false;
			sprite.enabled = true;
			death_text.SetActive(false);
			print("Player Respawned!");
		}

		public void SetCheckpoint(Checkpoint checkpoint) {
			if (current_checkpoint != null)
				current_checkpoint.is_current_checkpoint = false;

			current_checkpoint = checkpoint;
			checkpoint.is_current_checkpoint = true;

			print("Player new checkpoint!");
		}

		public void KillPlayer() {
			dead = true;
			print("Player died!");

			death_text.SetActive(true);
			sprite.enabled = false;
		}

		private void OnCollisionEnter2D(Collision2D collision) //happens when player hitbox lands on building's hitbox
		{
			if (collision.gameObject.CompareTag("Buildings")) {
				isGrounded = true;
			}
		}
	}
}
