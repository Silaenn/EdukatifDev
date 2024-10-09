using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Dapatkan komponen AudioSource
        audioSource = GetComponent<AudioSource>();

        // Periksa apakah AudioSource ada
        if (audioSource != null)
        {
            // Jika audio belum dimainkan, mulai memainkannya
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogError("AudioSource tidak ditemukan pada GameObject ini.");
        }
    }
}