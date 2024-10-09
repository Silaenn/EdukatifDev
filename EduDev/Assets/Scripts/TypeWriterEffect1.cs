using System.Collections;
using UnityEngine;
using TMPro;  // Import untuk TextMeshPro, jika menggunakan UI Text, ganti TMP_Text dengan Text.

public class TypeWriterEffect1 : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;  // Kecepatan pengetikan
    [SerializeField] private TMP_Text textMeshPro;       // Referensi ke TextMeshPro

    private string fullText;   // Teks penuh yang ingin ditampilkan
    private string currentText = "";  // Teks saat ini yang sedang ditampilkan

    private void Start()
    {
        // Misalnya teks penuh yang ingin ditampilkan
        fullText = "Sayangnya, Sadewa gagal mempelajari ilmu-ilmu Pancasila,\n yang menyebabkan hidupnya dipenuhi dengan keburukan. \n Ia sering mengabaikan nilai-nilai moral, \n Sehingga terjerumus dalam perilaku negatif yang merugikan dirinya dan orang lain.";
        
        // Mulai coroutine untuk menampilkan teks
        StartCoroutine(ShowText());
    }

    // Coroutine untuk menampilkan teks seperti diketik
    private IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);  // Mengambil sebagian dari teks penuh
            textMeshPro.text = currentText;  // Menampilkan teks yang sedang diketik
            yield return new WaitForSeconds(typingSpeed);  // Jeda sejenak sebelum menampilkan karakter berikutnya
        }
    }
}
