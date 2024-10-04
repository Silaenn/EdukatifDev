using UnityEngine;
using UnityEngine.UI;  // Atau gunakan TMPro jika pakai TextMeshPro

public class NPCDialog : MonoBehaviour
{
    public GameObject player;      // Drag Player ke sini di Inspector
    public GameObject dialogUI;    // Drag panel UI yang digunakan untuk dialog
    public Text dialogText;        // atau TextMeshProUGUI jika pakai TextMeshPro
    public string[] dialogLines;   // Array untuk menyimpan dialog
    public float displayTime = 3f; // Waktu tampilan setiap dialog sebelum pindah ke baris berikutnya
    public float detectionRadius = 5f; // Jarak dari NPC untuk menampilkan dialog

    private int currentLine = 0;
    private bool isDialogActive = false;

    void Update()
    {
        // Hitung jarak antara Player dan NPC
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Jika jarak Player dan NPC lebih kecil dari detectionRadius dan dialog belum aktif
        if (distance <= detectionRadius && !isDialogActive)
        {
            ShowNextLine();  // Mulai dialog
        }
        // Jika jarak Player lebih besar dari detectionRadius dan dialog aktif
        else if (distance > detectionRadius && isDialogActive)
        {
            HideDialog();  // Sembunyikan dialog saat player keluar dari jarak
        }
    }

    private void ShowNextLine()
    {
        if (currentLine < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLine]; // Tampilkan dialog saat ini
            dialogUI.SetActive(true);  // Tampilkan UI dialog
            currentLine++;
            isDialogActive = true;  // Set dialog aktif

            // Tampilkan baris berikutnya setelah beberapa waktu
            Invoke("ShowNextLine", displayTime); // Panggil lagi setelah 'displayTime' detik
        }
        else
        {
            Invoke("HideDialog", displayTime);  // Sembunyikan dialog setelah semua baris selesai
        }
    }

    private void HideDialog()
    {
        dialogUI.SetActive(false);  // Sembunyikan UI dialog
        isDialogActive = false;     // Reset status dialog
        currentLine = 0;            // Reset baris dialog untuk pemutaran ulang
    }
}
