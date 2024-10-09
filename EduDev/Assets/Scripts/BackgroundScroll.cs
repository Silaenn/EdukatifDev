using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] private float backgroundSpeed = 3f;
    private Vector3[] startPositions;
    private float[] backgroundWidths;
    private bool isScrolling = true;  // Flag untuk menghentikan scrolling saat background selesai

    private PlayerController playerController;  
    
    [SerializeField] private string[] newPlayerAnimationTrigger;

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

                 if (i == 4 && backgrounds[i].transform.position.x <= 0)
                {
                    ChangePlayerAnimation("SmaWalk");  
                     if (playerController != null)
                 {
                    playerController.currentJumpAnimation = "SmaJump";  // Ganti animasi jump menjadi SmaJump
                 }
                }

                // Cek apakah background ke-10 sudah mencapai posisi tertentu (misal: X <= 0)
                if (i == 9 && backgrounds[i].transform.position.x <= 0)
                {
                    ChangePlayerAnimation("Kerja");  
                    playerController.currentJumpAnimation = "KerjaJump";
                }
            }
        }

        // Mengecek apakah semua item telah dikumpulkan
        if (playerController != null && !isScrolling)
        {
            ChoiceAction choiceAction = FindObjectOfType<ChoiceAction>();
            if(choiceAction != null && !choiceAction.isIndialogue){
                CheckEnding();
            }
        }
    }

    private void ChangePlayerAnimation(string animationTrigger)
    {
        if (playerController != null)
        {
            Animator playerAnimator = playerController.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger(animationTrigger);  // Aktifkan animasi berdasarkan trigger yang diberikan
            }
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
            } else if(playerController.SceneEndingType == "NetralEnding")       {
                SceneManager.LoadScene("NetralEnding");
            }
        }
    }
}
