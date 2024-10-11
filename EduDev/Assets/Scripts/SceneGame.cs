using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGame : MonoBehaviour
{
    public AudioSource audioSource;  // Pastikan AudioSource terhubung
    public AudioClip buttonClickClip; // Klip audio untuk tombol

    public void SwitchScene()
    {
        StartCoroutine(PlaySoundThenSwitchScene("MainGame"));
    }

    public void ExitGame()
    {
        StartCoroutine(PlaySoundThenExitGame());
    }

    private IEnumerator PlaySoundThenSwitchScene(string sceneName)
    {
        audioSource.PlayOneShot(buttonClickClip);

        yield return new WaitForSeconds(buttonClickClip.length);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator PlaySoundThenExitGame()
    {
    audioSource.PlayOneShot(buttonClickClip);
    yield return new WaitForSeconds(buttonClickClip.length);

      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

      public void SoundClick()
    {
        audioSource.PlayOneShot(buttonClickClip);
    }
}
