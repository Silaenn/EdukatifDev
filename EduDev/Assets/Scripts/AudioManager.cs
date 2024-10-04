using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip[] clips;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[0];
        audioSource.loop = true;
        if(!audioSource.isPlaying){
            audioSource.Play();
        }
    }

   public void Buttonclick(){
    audioSource.PlayOneShot(clips[1]);
   }
}
