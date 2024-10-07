using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneGame : MonoBehaviour
{
    public void SwitchScene(){
         SceneManager.LoadScene("MainGame");
    }

    public void ExitGame(){
         Application.Quit();
    }
}
