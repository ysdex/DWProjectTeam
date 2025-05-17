using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonscript : MonoBehaviour
{
    public void LoadSceneByName(string scenename)
    {
        GameManager.Instance.nextMapName = scenename;
        GameManager.Instance.nextSpawnPosisition = new Vector3(5, 0, 0);
        SceneManager.LoadScene(scenename);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
