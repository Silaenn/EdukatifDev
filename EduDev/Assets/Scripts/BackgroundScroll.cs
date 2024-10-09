using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] private float backgroundSpeed = 5f;
    private Vector3[] startPositions;
    private float[] backgroundWidths;
    private bool isScrolling = true;  // Flag untuk menghentikan scrolling saat background selesai

    private PlayerController playerController;  

    [SerializeField] private Sprite[] newPlayerSkin;
    
    [SerializeField] private string[] newPlayerAnimationTrigger;
    private bool isFirstAnimation = true; // Untuk menentukan animasi mana yang aktif

    private void Start()
    {
        int backgroundCount = backgrounds.Length;
        startPositions = new Vector3[backgroundCount];
        backgroundWidths = new float[backgroundCount];

        for (int i = 0; i < backgroundCount; i++)
        {
            startPositions[i] = backgrounds[i].transform.position;
            
            SpriteRenderer spriteRenderer = backgrounds[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                backgroundWidths[i] = spriteRenderer.bounds.size.x;
            }
            else
            {
                backgroundWidths[i] = 1f;
            }
        }

        playerController = FindObjectOfType<PlayerController>();  // Mendapatkan referensi ke ItemManager
    }

    private void Update()
    {
        // Hanya scroll jika flag isScrolling = true
        if (isScrolling)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].transform.Translate(Vector3.left * backgroundSpeed * Time.deltaTime);

                if (backgrounds[i].transform.position.x <= -backgroundWidths[i])
                {
                    // Hentikan scroll ketika background terakhir melewati batas
                    if (i == backgrounds.Length - 2)
                    {
                        StopScrolling();
                    }
                }
            }
        }

        // Mengecek apakah semua item telah dikumpulkan
        if (playerController != null && !isScrolling)
        {
            CheckEnding();
        }
    }

     private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Entered trigger with: " + other.gameObject.name);

    if (other.CompareTag("Player"))
    {
        Debug.Log("It's the player, changing animation...");
        ChangePlayerSkinAndAnimation(other.gameObject);
    }
}

   private void ChangePlayerSkinAndAnimation(GameObject player)
{
    Animator playerAnimator = player.GetComponent<Animator>();
    Debug.Log("Masuk");

    // Ganti skin jika perlu
    if (player != null && newPlayerSkin.Length > 0)
    {
        SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        if (playerSpriteRenderer != null)
        {
            playerSpriteRenderer.sprite = newPlayerSkin[0];  // Skin tetap bisa diganti jika ada
        }
    }

    // Ganti animasi dengan trigger
    if (playerAnimator != null)
    {
        if (isFirstAnimation)
        {
            // Panggil trigger untuk animasi pertama
            playerAnimator.SetTrigger("Sma");
        }
        else
        {
            // Panggil trigger untuk animasi kedua
            playerAnimator.SetTrigger("Kerja");
        }
        isFirstAnimation = !isFirstAnimation; // Tukar antara animasi 1 dan 2
        Debug.Log("Keluar");
    }
}

    // Fungsi untuk menghentikan scrolling
    public void StopScrolling()
    {
        isScrolling = false;  // Menghentikan pergerakan background
        Debug.Log("Scrolling Stopped");
    }

    public void StartScrolling(){
        isScrolling = true;
    }


    private void CheckEnding()
    {
        if (playerController.ShouldChangeScene())  // Jika kondisi item terpenuhi
        {
            if(playerController.SceneEndingType == "SadEnding"){
                SceneManager.LoadScene("SadEnding");
            } else if(playerController.SceneEndingType == "CutScene"){
                SceneManager.LoadScene("CutScene");
            }           
        }
    }
}
