using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

namespace Player {
    public class codaScript : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Canvas hud;
        private GameObject death_text;

        public Checkpoint current_checkpoint = null;
        bool isGrounded = false;
        public bool dead = false;

        private void Awake() {
            if (death_text == null)
            {
                GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
                foreach (GameObject obj in allObjects)
                {
                    if (obj.name == "Death Text")
                    {
                        death_text = obj;
                        break;
                    }
                }
                if (death_text == null)
                {
                    Debug.LogError("Death Text object not found in the scene!");
                }
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
            if (death_text != null)
            {
                death_text.SetActive(false);
            }
            Debug.Log("Player Respawned!");
        }

        public void SetCheckpoint(Checkpoint checkpoint) {
            if (current_checkpoint != null)
                current_checkpoint.is_current_checkpoint = false;

            current_checkpoint = checkpoint;
            checkpoint.is_current_checkpoint = true;

            Debug.Log("Player new checkpoint!");
        }

        void Update()
        {
            if (dead) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    RestartToCheckpoint(current_checkpoint);
                }
                return;
            }
        }

        public void KillPlayer() {
            dead = true;
            Debug.Log("Player died!");
            if (death_text != null)
            {
                death_text.SetActive(true);
            }
            sprite.enabled = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Buildings"))
            {
                if (!isGrounded)
                {
                    isGrounded = true;
                }
            }
        }
    }
}
