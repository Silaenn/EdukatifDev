using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float jump = 10f;
    private bool moveUp = false;
    private int item1 = 0, item2 = 0, item3 = 0, max = 10;

    public Text item1UI, item2UI, item3UI;
    private bool isGrounded = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(moveUp && isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jump);
            moveUp = false;
            isGrounded = false;
        }
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

   private void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Item1")){
        AddItem1();
         other.gameObject.SetActive(false); 
    }
    else if(other.CompareTag("Item2")){
        AddItem2();
         other.gameObject.SetActive(false); 
    }
    else if(other.CompareTag("Item3")){
        AddItem3();
         other.gameObject.SetActive(false); 
    }
}
     void AddItem1(){
        if(item1 < 10){
        item1++;
        item1UI.text = item1.ToString() + " / " + max;
        } 
    }
     void AddItem2(){
        if(item2 < 10){
        item2++;
        item2UI.text = item2.ToString() + " / " + max;
        } 
    }
     void AddItem3(){
        if(item3 < 10){
        item3++;
        item3UI.text = item3.ToString() + " / " + max;
        } 
    }
}
