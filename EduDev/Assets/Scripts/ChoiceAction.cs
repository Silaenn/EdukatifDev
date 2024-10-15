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

    // Tambahkan class untuk menyimpan status NPC
    [System.Serializable]
    public class NPCStatus
    {
        public GameObject npcObject;
        public bool hasTalked = false;  // Menandai apakah NPC sudah diajak bicara
    }

    public NPCStatus[] npcStatuses; // Array untuk status NPC


    public void OnButtonClick()
    {
        if (isInteracting) return;

        if (playerAnimator == null)
        {
            playerAnimator = player.GetComponent<Animator>();
        }

        for (int i = 0; i < npcStatuses.Length; i++)
        {
            NPCStatus currentNpcStatus = npcStatuses[i];
            GameObject currentNpc = currentNpcStatus.npcObject;
            float distance = Vector3.Distance(player.transform.position, currentNpc.transform.position);

            // Cek jika sudah pernah bicara dengan NPC ini
            if (currentNpcStatus.hasTalked)
            {
                Debug.Log("NPC " + i + " sudah pernah diajak bicara.");
                continue; // Lewati NPC ini jika sudah pernah bicara
            }

            if (distance < interactionDistance)
            {
                if (backgroundScroll != null)
                {
                    backgroundScroll.StopScrolling(); 
                }

                TriggerPlayerStopAnimation();
                isIndialogue = true;
                isInteracting = true;

                StartCoroutine(StartDialogueWithNPC(i)); // Mulai dialog dengan NPC
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

    IEnumerator StartDialogueWithNPC(int npcIndex)
    {
        NPCStatus currentNpcStatus = npcStatuses[npcIndex];
        GameObject currentNpc = currentNpcStatus.npcObject;

        if (dialogManager != null)
        {
            dialogManager.StartDialogue(npcIndex); 
        }

        while (dialogManager.isInDialogue)
        {
            yield return null;
        }

        isInteracting = false;

        Canvas npcCanvas = currentNpc.GetComponentInChildren<Canvas>();

        if (npcCanvas != null)
        {
            Text npcText = npcCanvas.GetComponentInChildren<Text>();
            if (npcText != null)
            {
                npcText.text = "Thank You"; 
            }
            else
            {
                Debug.LogWarning("Text component not found in NPC!");
            }
        }

        // Tandai bahwa NPC ini sudah diajak bicara
        currentNpcStatus.hasTalked = true;
        Debug.Log("NPC " + npcIndex + " telah diajak bicara.");

        ResetPlayerAnimation();
        isIndialogue = false;

        if (backgroundScroll != null)
        {
            backgroundScroll.StartScrolling();
        }
    }

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

    void ResetPlayerAnimation()
    {
        if (playerAnimator != null)
        {
            playerAnimator.ResetTrigger(currentStopAnimation);  
            playerAnimator.SetTrigger(backgroundScroll.walk);  
        }
    }
}
