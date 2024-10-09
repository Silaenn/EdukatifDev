using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float jump = 14f;
    private bool moveUp = false;
    public int item1 = 0, item2 = 0, item3 = 0, item4 = 0, item5 = 0, max = 10;

    public Text item1UI, item2UI, item3UI, item4UI, item5UI;
    private bool isGrounded = true;
    private Rigidbody2D rb;

     public AudioClip pickupSound;  // AudioClip untuk suara yang akan dimainkan
    private AudioSource audioSource;
    private Animator anim;
    private bool shouldChangeScene = false;
    public BackgroundScroll backgroundScroll;
     public string SceneEndingType { get; private set; }
     public string currentJumpAnimation = "Jump";
     public AudioClip jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
         if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  // Tambahkan AudioSource jika belum ada
        }
    }

    void Update()
    {
        if(moveUp && isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.SetTrigger(currentJumpAnimation);
            PlayJumpSound();
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
        PlaySound();
         other.gameObject.SetActive(false); 
    }
    else if(other.CompareTag("Item2")){
        AddItem2();
        PlaySound();
         other.gameObject.SetActive(false); 
    }
    else if(other.CompareTag("Item3")){
        AddItem3();
        PlaySound();
         other.gameObject.SetActive(false); 
    }
    else if(other.CompareTag("Item4")){
        AddItem4();
        PlaySound();
         other.gameObject.SetActive(false); 
    }
    else if(other.CompareTag("Item5")){
        AddItem5();
        PlaySound();
         other.gameObject.SetActive(false); 
    }

    
}
     void AddItem1(){
        if(item1 < max){
        item1++;
        item1UI.text = item1.ToString() + " / " + max;
        CheckIfBuff();
        } 
    }
     void AddItem2(){
        if(item2 < max){
        item2++;
        item2UI.text = item2.ToString() + " / " + max;
        CheckIfBuff();
        } 
    }
     void AddItem3(){
        if(item3 < max){
        item3++;
        item3UI.text = item3.ToString() + " / " + max;
        CheckIfBuff();
        } 
    }
     void AddItem4(){
        if(item4 < max){
        item4++;
        item4UI.text = item4.ToString() + " / " + max;
        CheckIfBuff();
        } 
    }
     void AddItem5(){
        if(item5 < max){
        item5++;
        item5UI.text = item5.ToString() + " / " + max;
        CheckIfBuff();
        } 
    }

      private void PlaySound()
    {
        audioSource.PlayOneShot(pickupSound);  // Mainkan suara sekali
    }

    private void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);  // Mainkan suara lompat sekali
    }

    void CheckIfBuff(){
        if((item2 >= 8 && item2 <= max) || (item5 >= 8 && item5 <= max)){
            shouldChangeScene = true;
            SceneEndingType = "SadEnding";
        }
        else if((item1 == max) || (item3 == max)  || (item4 == max)){
            shouldChangeScene = true;
            SceneEndingType = "NetralEnding";
        }

        else if((item1 >= 5 && item1 <= max) || (item3 >= 5 && item3 <= max) || (item4 >= 5 && item4 <= max)){
            shouldChangeScene = true;
            SceneEndingType = "CutScene";
        }
    }

    public bool ShouldChangeScene(){
        return shouldChangeScene;
    }
}
