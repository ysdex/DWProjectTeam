using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void RetryGame()
    {
        if (Character_Move.Instance != null)
        {
            Destroy(Character_Move.Instance.gameObject);
            Character_Move.Instance = null;
        }

        GameObject existingCamera = GameObject.FindWithTag("MainCamera");
        if (existingCamera != null)
        {
            Destroy(existingCamera);
        }

        SceneManager.LoadScene("Start");
    }

    public void LoadScene(string sceneName)
    {
        if(Character_Move.Instance != null)
        {
            Destroy(Character_Move.Instance.gameObject);
        }
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }
}
