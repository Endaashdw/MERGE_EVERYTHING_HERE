using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class codaScript : MonoBehaviour
{
    [SerializeField] public float jumpVelocity = 7f;
    [SerializeField] private float fallMultiplier = 2f; 
    bool isGrounded = false;
    private bool jump = false;
    private Rigidbody2D RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (jump) {
            RB.linearVelocity += new Vector2(0, jumpVelocity);
            jump = false;
        }

        //inputs
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isGrounded)
            {
                jump = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            if (!isGrounded)
            {
                jump = false;
            }
        }
        
        //maybe implement variable jump height? VJH = more control; no VJH = emphasis precision


        //fall multiplier when falling
        if (RB.linearVelocity.y < 0)
        {
            RB.linearVelocity += Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //happens when player hitbox lands on building's hitbox
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