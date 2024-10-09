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

    // Fungsi ini dipanggil saat tombol ditekan
    public void OnButtonClick()
    {
        if (playerAnimator == null){
            playerAnimator = player.GetComponent<Animator>();
        }

        
        // Loop untuk mengecek jarak dengan setiap NPC
        foreach (GameObject currentNpc in npc)
        {
            float distance = Vector3.Distance(player.transform.position, currentNpc.transform.position);

            if (distance < interactionDistance)
            {
              if (backgroundScroll != null)
             {
            backgroundScroll.StopScrolling(); 
             }
                TriggerPlayerStopAnimation();
                StartCoroutine(TalkToNPC(currentNpc));
                return;
            }
            else
            {
                Debug.Log("No interaction possible with " + currentNpc.name);
                if (backgroundScroll != null)
             {
            backgroundScroll.StartScrolling(); 
             }
            }
        }
    }

    // Fungsi interaksi dengan NPC
    IEnumerator TalkToNPC(GameObject npc)
    {
        
        Text npcText = npc.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();

        if(npcText != null){
            npcText.text = "Thank You";
        } else{
            Debug.Log("No problem");
        }

        yield return new WaitForSeconds(2f);

        ResetPlayerAnimation();

          if (backgroundScroll != null)
        {
            backgroundScroll.StartScrolling(); // Panggil fungsi untuk memulai penggulangan lagi
        }
    }

     void TriggerPlayerStopAnimation()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Stop");  
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
            playerAnimator.ResetTrigger("Stop");  
            playerAnimator.SetTrigger("Walk");  
        }
    }
}
