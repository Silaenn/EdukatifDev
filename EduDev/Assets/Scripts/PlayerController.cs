using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5f;
    private float jump = 8f;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;
    bool isGrounded = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (moveLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.eulerAngles = Vector2.up * 180;
        }
        else if (moveRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.eulerAngles = Vector2.zero;
        } 
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if(moveUp && isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jump);
            moveUp = false;
            isGrounded = false;
        }
    }

    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void StopMovingLeft()
    {
        moveLeft = false;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void StopMovingRight()
    {
        moveRight = false;
    }

      public void MoveUp()
    {
        if(isGrounded){
          moveUp = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }

}
