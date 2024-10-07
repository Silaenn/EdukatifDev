using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject settingsPanel; // Panel yang akan dijadikan pop-up settings
    private bool isSettingsOpen = false; // Untuk melacak apakah settings sedang terbuka atau tidak
    private bool isAnimating = false; // Untuk melacak apakah animasi sedang berjalan

    // Fungsi untuk memanggil ketika tombol settings ditekan
    public void ToggleSettings()
    {
        // Cegah fungsi dipanggil jika animasi sedang berjalan
        if (isAnimating)
            return;

        isSettingsOpen = !isSettingsOpen;
        isAnimating = true; // Tandai animasi sedang dimulai

        if (isSettingsOpen)
        {
            settingsPanel.SetActive(true); // Aktifkan panel
            settingsPanel.transform.localScale = Vector3.zero; // Atur skala awal
            LeanTween.scale(settingsPanel, Vector3.one, 0.5f)
                .setEaseOutBack()
                .setIgnoreTimeScale(true) // Abaikan timeScale agar tetap berjalan
                .setOnComplete(() =>
            {
                isAnimating = false; // Animasi selesai
                Time.timeScale = 0;  // Pause game setelah animasi selesai
            });
        }
        else
        {
            LeanTween.scale(settingsPanel, Vector3.zero, 0.5f)
                .setEaseInBack()
                .setIgnoreTimeScale(true) // Abaikan timeScale agar tetap berjalan
                .setOnComplete(() => 
            {
                settingsPanel.SetActive(false); // Matikan panel setelah animasi selesai
                isAnimating = false; // Animasi selesai
                Time.timeScale = 1;  // Resume game setelah panel hilang
            });
        }
    }

    public void ClickRestart(){
        isSettingsOpen = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ClickContinue(){
        isSettingsOpen = false;
        Time.timeScale = 1;
         settingsPanel.SetActive(false); // Panel settings dimatikan
    }
}



