using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceAction : MonoBehaviour
{
    public GameObject[] npc;  // Array untuk menyimpan banyak NPC
    public GameObject player; 
    public float interactionDistance = 6.0f; 
    private Animator playerAnimator;
    public BackgroundScroll backgroundScroll;
    public bool isIndialogue = false;
    public string currentStopAnimation = "Stop"; 
    private bool isInteracting = false;

    public DialogManager dialogManager; // Menghubungkan dengan DialogManager
    

    public void OnButtonClick()
    {
        if (isInteracting) return;

        if (playerAnimator == null)
        {
            playerAnimator = player.GetComponent<Animator>();
        }
        
        // Loop untuk mengecek jarak dengan setiap NPC
        for (int i = 0; i < npc.Length; i++)
        {
            GameObject currentNpc = npc[i];
            float distance = Vector3.Distance(player.transform.position, currentNpc.transform.position);


            if (distance < interactionDistance)
            {
                if (backgroundScroll != null)
                {
                    backgroundScroll.StopScrolling(); 
                }

                TriggerPlayerStopAnimation();
                isIndialogue = true;
                isInteracting = true;
                // Mulai dialog dengan NPC berdasarkan indeks
                StartCoroutine(StartDialogueWithNPC(i)); // Mengirim indeks NPC ke dialogManager
                return;
            }
            else
            {
                if (backgroundScroll != null)
                {
                    backgroundScroll.StartScrolling(); 
                }
            }
        }
    }

    // Fungsi interaksi dengan NPC
    IEnumerator StartDialogueWithNPC(int npcIndex)
    {
        GameObject currentNpc = npc[npcIndex];
        // Mengaktifkan dialog menggunakan DialogManager dengan NPC tertentu
        if (dialogManager != null)
        {
            dialogManager.StartDialogue(npcIndex); // Panggil fungsi dari DialogManager dan kirimkan indeks NPC
        }

        // Tunggu sampai dialog selesai
        while (dialogManager.isInDialogue)
        {
            yield return null; // Menunggu hingga dialog selesai
        }

        isInteracting = false;

        Canvas npcCanvas = currentNpc.GetComponentInChildren<Canvas>();

        if(npcCanvas != null){
           Text npcText = npcCanvas.GetComponentInChildren<Text>();
            if (npcText != null) {
            npcText.text = "Thank You"; // Mengubah teks NPC
         }
        else
        {
        Debug.LogWarning("Text component not found in NPC!");
        }
        } else{
            Debug.Log("No problem");
        }

        // Setelah dialog selesai
        ResetPlayerAnimation();
        isIndialogue = false;

        if (backgroundScroll != null)
        {
            backgroundScroll.StartScrolling(); // Panggil fungsi untuk memulai penggulungan lagi
        }
    }

    // Fungsi untuk memicu animasi berhenti pada karakter pemain
    void TriggerPlayerStopAnimation()
    {
        if (playerAnimator != null)
        {
            playerAnimator.ResetTrigger(backgroundScroll.walk);
            playerAnimator.SetTrigger(currentStopAnimation);  
        }
        else
        {
            Debug.LogWarning("Player Animator not found!");
        }
    }

    // Fungsi untuk mengatur ulang animasi pemain setelah dialog selesai
    void ResetPlayerAnimation()
    {
        if (playerAnimator != null)
        {
            playerAnimator.ResetTrigger(currentStopAnimation);  
            playerAnimator.SetTrigger(backgroundScroll.walk);  
        }
    }
}
