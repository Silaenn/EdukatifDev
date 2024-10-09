using UnityEngine;
using UnityEngine.Playables; // Untuk mengakses PlayableDirector
using UnityEngine.SceneManagement; // Untuk mengelola scene

public class TimelineSceneChanger : MonoBehaviour
{
    public PlayableDirector playableDirector; // Drag and drop PlayableDirector Anda di sini
    public string sceneToLoad; // Nama scene yang akan dimuat setelah timeline selesai

    private void Start()
    {
        if (playableDirector != null)
        {
            // Mendaftarkan callback saat timeline selesai
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        // Memindahkan ke scene yang ditentukan setelah timeline selesai
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnDestroy()
    {
        // Mencabut callback saat script dihapus
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }
}
