using UnityEngine;
using UnityEngine.SceneManagement;

public class Portalscript : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.nextMapName = sceneToLoad;
            GameManager.Instance.nextSpawnPosisition = new Vector3(0, 0, 0);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
